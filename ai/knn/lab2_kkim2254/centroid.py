# itt van minden ami a centroidhoz tartozik
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


def cosinust(x, y):
    szorzatuk = 0
    normX = 0
    normY = 0
    for i in range(64):
        szorzatuk += (x[i] * y[i])
        normX += x[i] ** 2
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


def kirajzol_szamok(matrix):
    fig, axes = plt.subplots(2, 5, figsize=(20, 8))

    for i, ax in enumerate(axes.flat):
        data = matrix[i]
        data = data[:-1]
        data = np.array(data)

        data_grid = data.reshape(8, 8)

        im = ax.imshow(data_grid, cmap='viridis', interpolation='nearest', origin='upper', aspect='equal')

        # Add color bar
        fig.colorbar(im, ax=ax)

        # Set labels and title
        ax.set_xlabel('X')
        ax.set_ylabel('Y')
        ax.set_title(f'Heatmap {i}')

    plt.tight_layout()

    plt.show()


def plot_digit(digit):
    digit = digit[:-1]
    digit_image = np.array(digit).reshape(8, 8)

    plt.imshow(digit_image, cmap='gray')
    plt.axis('off')
    plt.show()


def plot_heatmap2(matrix):
    plt.figure(figsize=(8, 6))
    plt.imshow(matrix, cmap='viridis', interpolation='nearest')
    plt.colorbar(label='Height')
    plt.xlabel('X')
    plt.ylabel('Y')
    plt.title('Heatmap')
    plt.show()


def plot_heatmap(matrix, n):
    num_rows = len(matrix)
    num_cols = len(matrix[0])  # Assuming all rows have the same number of columns

    num_subplots = (num_rows + n - 1) // n  # Number of subplots

    fig, axs = plt.subplots(1, num_subplots, figsize=(16, 6))  # Adjust figsize as needed

    for i in range(0, num_rows, n):
        subplot_index = i // n
        data = matrix[i:i + n]  # Get the group of rows from i to i+n
        # Pad the data if it's shorter than n rows
        if len(data) < n:
            data.extend([np.zeros_like(data[0])] * (n - len(data)))
        axs[subplot_index].imshow(data, cmap='viridis', aspect='auto')
        axs[subplot_index].set_title(f'Rows {i + 1} to {min(i + n, num_rows)}')
        axs[subplot_index].set_xlabel('number 0-9')
        axs[subplot_index].set_ylabel('tes numbers')
        axs[subplot_index].set_xticks([])
        axs[subplot_index].set_yticks([])

    plt.tight_layout()
    plt.show()


def heatmap_euklid():
    matrix = [[0] * 65 for _ in range(10)]
    heat = [[0] * 10 for _ in range(ossz_tes)]

    with open('optdigits.tra', 'r') as bemenet:
        for line in bemenet:
            test_bemenet_sor = [int(x) for x in line.strip().split(',')]

            for i in range(64):
                matrix[test_bemenet_sor[-1]][i] += test_bemenet_sor[i]

    for i in range(10):
        for j in range(64):
            matrix[i][j] = int(matrix[i][j] / many_tra[i])

    hanyadik = 0
    with open('optdigits.tes', 'r') as test:
        for line in test:
            megmondando = [int(x) for x in line.strip().split(',')]
            #pq = PriorityQueue()

            for i in range(10):
                tav = euklidesz(matrix, megmondando, i)
                heat[hanyadik][i] = tav

            hanyadik += 1

    plot_heatmap(heat, 250)


def tesz_a_teszhez():
    # Read the lines from the file into a list
    with open('in.txt', 'r') as f:
        lines = f.readlines()

    # Sort the lines based on the last number in each line
    lines.sort(key=lambda line: int(line.strip().split(',')[-1]))

    # Write the sorted lines back to the same file
    with open('in.txt', 'w') as f:
        f.writelines(lines)

    heat = [[0] * ossz_tes for _ in range(ossz_tes)]
    x = 0
    with open('in.txt', 'r') as mit:
        for i in mit:
            sor_i = [int(x) for x in i.strip().split(',')]
            y = 0
            with open('in.txt', 'r') as mihez:
                for j in mihez:
                    sor_j = [int(x) for x in j.strip().split(',')]
                    tav = cosinust(sor_i, sor_j)
                    heat[x][y] = tav
                    y += 1
            x += 1

    plot_heatmap2(heat)


def program(str, melyik, oke):
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
    if oke:
        kirajzol_szamok(matrix)
        # for i in range(10):#ha netalan az kellene hogy csak egy vektort lehessen atkuldeni akkor itt van
        #   plot_digit(matrix[i])


def conf_draw(conf):
    plt.imshow(conf, cmap='viridis', interpolation='nearest')
    plt.colorbar(label='Value')
    plt.xlabel('X')
    plt.ylabel('Y')
    plt.title('Heatmap')
    plt.show()


def confusion(melyik):
    matrix = [[0] * 65 for _ in range(10)]
    conf = [[0] * 10 for _ in range(10)]  # ebbe lesz az hogy melyiket melyikkel teveszti

    with open('optdigits.tra', 'r') as bemenet:
        for line in bemenet:
            test_bemenet_sor = [int(x) for x in line.strip().split(',')]

            for i in range(64):
                matrix[test_bemenet_sor[-1]][i] += test_bemenet_sor[i]

    for i in range(10):
        for j in range(64):
            matrix[i][j] = int(matrix[i][j] / many_tra[i])

    with open('optdigits.tes', 'r') as test:
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

            conf[megmondando[-1]][x] += 1

    conf_draw(conf)


program('optdigits.tes', 0, False)
program('optdigits.tes', 1, False)
program('optdigits.tra', 0, False)
program('optdigits.tra', 1, False)

# az abrazolasok innen kezdodnek
program('optdigits.tes', 0, True)  # 4.1 es feladat, a true hatasara fogja engedelyezni azt az alprogramot ami kirajzolja a szamokat
heatmap_euklid()  # euklideszi tavolsag centroidtol heatmap
tesz_a_teszhez()  # 4.3 feladat
confusion(0)  # confusion matrix, euklidian
confusion(1)  # confusion matrix, cosine distance
