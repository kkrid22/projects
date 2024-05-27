from socket import *
import threading

max_user_number = 5
loginList = {}


def szal_munka(connectionSocket, addr):
    user_name = ''
    try:
        while True:
            sentence = connectionSocket.recv(1024)
            sentence = sentence.decode()
            print(sentence)
            words = sentence.split()

            if words[0] == "LOGIN" and (words[1] not in loginList):
                user_name = words[1]
                loginList[user_name] = connectionSocket
                response = 'SUCCES'
                response = response.encode()
                connectionSocket.send(response)

            elif words[0] == 'LOGIN' and words[1] in loginList:
                response = 'USERNAME ALREADY IN USE'
                response = response.encode()
                connectionSocket.send(response)

            elif (not user_name == "") and user_name in loginList:
                if words[0] == "EXIT":
                    response = 'LOGGED OUT'
                    response = response.encode()
                    connectionSocket.send(response)
                    del loginList[user_name]
                    break

                elif words[0] == "SHOW":  # show active users
                    active_users = 'SHOW ' + ' '.join(loginList.keys())
                    active_users = active_users.encode()
                    connectionSocket.send(active_users)

                elif words[0] == "ALL":
                    message_all = 'ALL ' + ' '.join(words[1:])
                    message_all = message_all.encode()
                    for key, value in loginList.items():
                        value.send(message_all)

                # formailag ugy fog kinezni hogy TO VALAKI MESSAGE VALAMI
                elif words[0] == "TO":
                    to_user = words[1]
                    if to_user not in loginList:
                        err = 'THE SPECIFIED USER DOESNT EXISTS'
                        err = err.encode()
                        connectionSocket.send(err)

                    else:
                        message = 'FROM ' + user_name + ' ' + ' '.join(words[3:])
                        print(message)
                        message = message.encode()
                        for key, value in loginList.items():
                            if key == to_user:
                                value.send(message)

                elif words[0] == "GOOD":  # response words[1] got the message from words[2]
                    response = 'GOOD ' + words[1] + ' ' + words[2]
                    to = loginList[words[2]]
                    response = response.encode()
                    to.send(response)
                else:
                    sorry = 'SORRY EVERYTHING WENT WRONG, YOU ARE LOGGED OUT'
                    sorry = sorry.encode()
                    connectionSocket.send(sorry)
                    del loginList[user_name]
                    break
            else:
                sorry = 'SORRY EVERYTHING WENT WRONG, YOU ARE LOGGED OUT'
                sorry = sorry.encode()
                connectionSocket.send(sorry)
                del loginList[user_name]
                break

    except Exception as e:
        print("{} - error occured    {}:".format(threading.get_ident(), e))
    finally:
        connectionSocket.close()


serverPort = 12000
serverSocket = socket(AF_INET, SOCK_STREAM)
serverSocket.bind(('', serverPort))
serverSocket.listen(max_user_number)
print('Server is ready to receive...')

while True:
    connectionSocket, addr = serverSocket.accept()

    thread = threading.Thread(target=szal_munka, args=(connectionSocket, addr))
    thread.start()
