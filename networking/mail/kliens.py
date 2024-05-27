from socket import *
import threading
import time
import tkinter as tk

user_name = ''
sikeres_bejelentkezes = 0
port_number = 12000
exited = 0
aktiv_users = []


def popup_message(message):
    root = tk.Tk()
    root.title("Popup Message")

    label = tk.Label(root, text=message)
    label.pack(padx=20, pady=20)

    root.mainloop()


def receive_incoming_messages():
    exit_received = 0
    while exit_received == 0:
        received_message = clientSocket.recv(1024).decode()
        received_message_parced = received_message.split()

        if received_message == 'LOGGED OUT':
            exit_received = 1
        elif received_message == 'SORRY EVERYTHING WENT WRONG, YOU ARE LOGGED OUT':
            exit_received = 1
            global exited
            exited = 1
            clientSocket.close()
        elif received_message == 'THE SPECIFIED USER DOESNT EXISTS':
            popup_message('the user that you wanted to send the message to either logged out or didnt exist\n')

        elif received_message_parced[0] == 'SHOW':
            output = 'Active users:\n'
            received_message_parced = received_message_parced[1:]
            for user in received_message_parced:
                output = output + user + '\n'
            popup_message(output)

        elif received_message_parced[0] == 'ALL':
            received_message_parced = received_message_parced[1:]
            received_message_parced = ' '.join(received_message_parced)
            popup_message('Message to all users!\n {}\n'.format(received_message_parced))

        elif received_message_parced[0] == 'GOOD':
            megkapta = received_message_parced[1]
            popup_message('User {} has gotten your message\n'.format(megkapta))

        elif received_message_parced[0] == 'FROM':
            sender = received_message_parced[1]
            received_message_parced = received_message_parced[2:]
            received_message_parced = ' '.join(received_message_parced)
            popup_message('User {} has sent you a message \n {}'.format(sender, received_message_parced))
            sent_back = 'GOOD ' + user_name + ' ' + sender
            sent_back = sent_back.encode()
            clientSocket.send(sent_back)


clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((gethostname(), 12000))

while sikeres_bejelentkezes == 0:
    user_name = input('Please Enter Your username: ')
    if user_name != "":
        login = 'LOGIN ' + user_name
        login = login.encode()
        clientSocket.send(login)

        response = clientSocket.recv(1024)
        response = response.decode()
        response.split()

        if response == "SUCCES":
            sikeres_bejelentkezes = 1

print('Sikeres bejelentkezes')

new_thread = threading.Thread(target=receive_incoming_messages)

new_thread.start()

# this is going to be the sending side, the function is gonna be the receiveing side
while exited == 0:
    szeretne_kijelentkezni = input('Szeretne e kijelentkezni?(0 ha nem 1 ha igen)\n ')
    if szeretne_kijelentkezni != '1':
        mindenki = input('Mindenkinek szeretnel e uzenetet kuldeni?(0 ha nem 1 ha igen) ')

        if mindenki != '0':
            mindenki_uzenet = input('Mi az uzenet?\n ')
            mindenki_uzenet = 'ALL ' + mindenki_uzenet
            mindenki_uzenet = mindenki_uzenet.encode()
            clientSocket.send(mindenki_uzenet)

        else:
            aktiv_felhasznalok = input('Szeretne e latni az aktiv felhasznalokat?(0 ha nem 1 ha igen)\n ')
            if aktiv_felhasznalok == '1':
                aktivak = 'SHOW ' + user_name
                aktivak = aktivak.encode()
                clientSocket.send(aktivak)
                time.sleep(1)

            kinek_kuldi = input('Kinek szeretned kuldeni az uzenetet?\n ')
            mi_az_uzenet = input('Mi az uzenet tartalma?\n ')

            vegso_uzenet = 'TO ' + kinek_kuldi + ' ' + ' MESSAGE ' + mi_az_uzenet
            vegso_uzenet = vegso_uzenet.encode()
            clientSocket.send(vegso_uzenet)

    else:
        exit_message = 'EXIT ' + user_name
        exit_message = exit_message.encode()
        clientSocket.send(exit_message)
        time.sleep(2)
        exited = 1
