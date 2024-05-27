import matplotlib.pyplot as plt
from socket import *
import threading
import sys

import numpy as np

threadNumber = 0
width = 0
height = 0
maxIterations = 0
results = []
connected = 0
numberOfCoordinates = 0


def generate_equally_distributed_numbers(a, b, n):
    # Calculate the interval size
    interval = (b - a) / (n - 1)
    # Generate the numbers
    numbers = [a + i * interval for i in range(n)]
    return numbers


def szal_munka(connectionSocket, addr, points, iterations):
    partialResult = []
    numberOfCoordinate = len(points)
    message = "{} {}".format(iterations, numberOfCoordinate)
    connectionSocket.send(message.encode())

    for elements in points:
        connectionSocket.send(str(elements).encode())
        (x, y) = elements
        bitReceived = int(connectionSocket.recv(1024).decode())
        partialResult.append((x, y, bitReceived))

    results.extend(partialResult)

    connectionSocket.close()


if len(sys.argv) == 5:
    threadNumber = int(sys.argv[1])
    maxIterations = int(sys.argv[2])
    width = int(sys.argv[3])
    height = int(sys.argv[4])

    if (width * height) % threadNumber != 0:
        print('i couldnt equally distribute the points')
        exit(0)

    x = generate_equally_distributed_numbers(-2.00, 0.47, width)
    y = generate_equally_distributed_numbers(-1.12, 1.12, height)
    point = []
    for i in range(height):
        for j in range(width):
            point.append((x[j], y[i]))  # es akkor ezzel meglesznek a pontok lekepezve
            # meg annyit kell elintezni hogy a pontokbol mindenki annyit kapjon amennyit szabad

    osszpontok = width * height
    regiReszek = 0
    ujReszek = 0

    serverPort = 12000
    serverSocket = socket(AF_INET, SOCK_STREAM)
    serverSocket.bind(('', serverPort))
    serverSocket.listen(threadNumber)
    print('Server is ready to send...')

    numberOfCoordinates = osszpontok // threadNumber
    threads = []

    for i in range(threadNumber):
        connectionSocket, addr = serverSocket.accept()
        regiReszek = ujReszek
        ujReszek = ujReszek + numberOfCoordinates
        elkuldendo = point[regiReszek:ujReszek]
        thread = threading.Thread(target=szal_munka,
                                  args=(connectionSocket, addr, elkuldendo, maxIterations))
        thread.start()
        threads.append(thread)
        connected += 1

    for thread in threads:
        thread.join()

    results_array = np.array(results)

    # Extract x, y coordinates and bit values
    x_values = results_array[:, 0]
    y_values = results_array[:, 1]
    bit_values = results_array[:, 2]

    # Plot points based on bit value
    plt.scatter(x_values[bit_values == 0], y_values[bit_values == 0], color='black', label='Bit 0')
    plt.scatter(x_values[bit_values == 1], y_values[bit_values == 1], color='white', label='Bit 1')

    # Set plot title and axis labels
    plt.title('Plot of Points')
    plt.xlabel('X')
    plt.ylabel('Y')

    # Set aspect of the plot to be equal
    plt.axis('equal')

    # Show plot
    plt.legend()
    plt.show()
