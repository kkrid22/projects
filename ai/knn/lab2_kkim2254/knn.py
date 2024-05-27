import numpy as np
import matplotlib.pyplot as plt

max_list_size = 5  # a lista maximalis feresi helye
k = 0  # listaban jelenleg hany szam par van
elemek_szama = 1797.0  # 3823.0

many_tra = [376, 389, 380, 389, 387, 376, 377, 387, 380, 382]
many_tes = [178, 182, 177, 183, 181, 182, 181, 179, 174, 180]

ossz_tra = 3823
ossz_tes = 1797


def conf_draw(conf):
    plt.imshow(conf, cmap='viridis', interpolation='nearest')
    plt.colorbar(label='Value')
    plt.xlabel('X')
    plt.ylabel('Y')
    plt.title('Heatmap')
    plt.show()


def beszur(x, a):
    x.append(a)
    x = sorted(x, key=lambda x: x[0])
    x = x[:-1]
    return x


def cosinusTav(x, y):
    szorzatuk = 0
    normX = 0
    normY = 0
    for i in range(len(x) - 1):
        szorzatuk += (x[i] * y[i])
        normX += x[i] ** 2
        normY += y[i] ** 2

    normY = np.sqrt(normY)
    normX = np.sqrt(normX)

    vegeredmeny = 1 - (szorzatuk / (normX * normY))
    return vegeredmeny


def euklidesz(x, y):
    osszeg = 0
    for i in range(len(x) - 1):
        osszeg += (x[i] - y[i]) ** 2
    return np.sqrt(osszeg)


def melyik_lett(lista):
    megjelenes = [0 for _ in range(10)]
    for i in lista:
        (a, b) = i
        megjelenes[b] += 1

    legnagyobb = 0
    ertek = 0
    for j in range(len(megjelenes)):
        if megjelenes[j] > legnagyobb:
            legnagyobb = megjelenes[j]
            ertek = j
    return ertek


def program(str, mennyi, oke, melyik):
    pontossag = 0

    conf = [[0] * 10 for _ in range(10)]  # ebbe lesz az hogy melyiket melyikkel teveszti
    talalat_mindre = [0.0] * 10

    with open(str, 'r') as test:
        for line in test:
            test_bemenet = [int(x) for x in line.strip().split(',')]

            lista = []  # toupleteket fog tartalmazni
            k = 0  # jelenleg mennyi van benne

            with open('optdigits.tra', 'r') as tra:
                for line2 in tra:
                    valos_bemenet = [int(y) for y in line2.strip().split(',')]

                    if melyik == 0:
                        tav = euklidesz(test_bemenet, valos_bemenet)
                    else:
                        tav = cosinusTav(test_bemenet, valos_bemenet)

                    if k < max_list_size:
                        lista.append((tav, valos_bemenet[-1]))
                    elif k >= max_list_size:
                        lista = beszur(lista, (tav, valos_bemenet[-1]))
                    k += 1

            talalt = melyik_lett(lista)

            conf[test_bemenet[-1]][talalt] += 1

            if talalt == test_bemenet[-1]:
                pontossag += 1
                talalat_mindre[talalt] += 1

    print("a program pontossaga = {}".format(float(pontossag) / mennyi))
    if oke:
        conf_draw(conf)

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

            output.write("a program ossz pontossaga {} eseten= {}\n\n".format(str, float(pontossag) / ossz_tes))

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

            output.write("a program ossz pontossaga {} eseten= {}\n\n".format(str, float(pontossag) / ossz_tra))


program('optdigits.tes', 1797.0, True, 0)
program('optdigits.tes', 1797.0, True, 1)
program('optdigits.tra', 3823.0, True, 0)
program('optdigits.tra', 3823.0, True, 1)
