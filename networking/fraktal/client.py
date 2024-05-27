import time
from socket import *

clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((gethostname(), 12000))

info = clientSocket.recv(1024).decode()

info = info.split()

numberOfElements = int(info[1])
iteration = int(info[0])
print(numberOfElements)
print(iteration)


for i in range(numberOfElements):
    # print('i am alive')
    coordinates = clientSocket.recv(1024).decode()
    coordinates = eval(coordinates)  # Convert string to tuple using eval
    coordinates = tuple(map(float, coordinates))

    (xo, yo) = coordinates

    x = 0.0
    y = 0.0
    localIteration = 0

    while x ** 2 + y ** 2 <= 2 ** 2 and localIteration < iteration:
        xtemp = x ** 2 - y ** 2 + xo
        y = 2 * x * y + yo
        x = xtemp
        localIteration += 1

    if x ** 2 + y ** 2 <= 2 ** 2 and localIteration >= iteration:
        result = 1
        clientSocket.send(str(result).encode())
    else:
        result = 0
        clientSocket.send(str(result).encode())

time.sleep(1)
clientSocket.close()
