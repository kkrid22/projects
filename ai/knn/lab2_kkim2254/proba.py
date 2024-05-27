import numpy as np
from queue import PriorityQueue
import matplotlib.pyplot as plt

many_tra = [376, 389, 380, 389, 387, 376, 377, 387, 380, 382]
many_tes = [178, 182, 177, 183, 181, 182, 181, 179, 174, 180]

ossz_tra = 3823
ossz_tes = 1797


def cosinusTav(matrix, y, j):
    szorzatuk = 0
    normX = 0
    normY = 0
    for i in range(64):
        szorzatuk += (matrix[j][i] * y[i])
        normX += matrix[j][i] ** 2
        normY += y[i] ** 2

    normY = np.sqrt(normY)
    normX = np.sqrt(normX)

    vegeredmeny = 1 - (szorzatuk / (normX * normY))
    return vegeredmeny


def euklidesz(matrix, source, j):
    osszeg = 0
    for i in range(64):
        osszeg += (matrix[j][i] - source[i]) ** 2
    return np.sqrt(osszeg)


def plot_digit(digit):
    digit = digit[:-1]
    digit_image = np.array(digit).reshape(8, 8)

    plt.imshow(digit_image, cmap='gray')
    plt.axis('off')
    plt.show()


def program(str, melyik):
    matrix = [[0] * 65 for _ in range(10)]

    talalat_mindre = [0.0] * 10

    with open('optdigits.tra', 'r') as bemenet:
        for line in bemenet:
            test_bemenet_sor = [int(x) for x in line.strip().split(',')]

            for i in range(64):
                matrix[test_bemenet_sor[-1]][i] += test_bemenet_sor[i]

    for i in range(10):
        for j in range(64):
            matrix[i][j] = int(matrix[i][j] / many_tra[i])

    talalat = 0
    with open(str, 'r') as test:
        for line in test:
            megmondando = [int(x) for x in line.strip().split(',')]
            pq = PriorityQueue()

            for i in range(10):
                if melyik == 0:
                    tav = euklidesz(matrix, megmondando, i)
                else:
                    tav = cosinusTav(matrix, megmondando, i)

                pq.put((tav, i))

            (_, x) = pq.get()
            if x == megmondando[-1]:
                talalat += 1
                talalat_mindre[x] += 1

    if str == 'optdigits.tes':
        for i in range(10):
            talalat_mindre[i] = talalat_mindre[i] / many_tes[i]

        with open("output.txt", 'a') as output:
            if melyik == 0:
                output.write("A talalati rata minden szamra tes eseten euklideszivel:\n")
            else:
                output.write("A talalati rata minden szamra tes eseten coszinusszal:\n")

            for i in range(10):
                output.write("{} : {}\n".format(i, talalat_mindre[i]))

            output.write("a program ossz pontossaga {} eseten= {}\n\n".format(str, float(talalat) / ossz_tes))

    elif str == 'optdigits.tra':
        for i in range(10):
            talalat_mindre[i] = talalat_mindre[i] / many_tra[i]
        with open("output.txt", 'a') as output:
            if melyik == 0:
                output.write("A talalati rata minden szamra tra eseten euklideszivel:\n")
            else:
                output.write("A talalati rata minden szamra tra eseten coszinusszal:\n")

            for i in range(10):
                output.write("{} : {}\n".format(i, talalat_mindre[i]))

            output.write("a program ossz pontossaga {} eseten= {}\n\n".format(str, float(talalat) / ossz_tra))

    for i in range(10):
        plot_digit(matrix[i])


program('optdigits.tes', 0)
# program('optdigits.tes', 1)
# program('optdigits.tra', 0)
# program('optdigits.tra', 1)
