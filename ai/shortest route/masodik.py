# b)102, a)188
import math
import matplotlib.pyplot as plt
from queue import PriorityQueue
import numpy as np
import seaborn as sn
from mpl_toolkits.mplot3d import Axes3D

# hanyszor hanyas matrixra lesz szukseg ahhoz hogy ellenorizzuk hogy egy adott csomopontnal mar voltunk
n = 100
melyik = 2

###################################################################################################################
# tavolsag szamito fuggveny
def tavolsag3D(x1, y1, z1, x2, y2, z2):
    res = ((x2 - x1) ** 2 + (y2 - y1) ** 2 + (z2 - z1) ** 2)
    return math.sqrt(res)


#######################################################################################################################
# feldolgozasa a fajlnak

parents = [[-1] * n for _ in range(n)]  # hasznalhatom ezt is, ha az ertek az adott (x,y) ponton 0 akkor meg nem volt
# latogatva, vagyis ha meg nincsen szuleje
matrix = [[-1.0] * n for _ in range(n)]  # tartalmazni fogja az x,y pozicion a magassagot
distance_g = [[math.inf] * n for _ in range(n)]
distance_f = [[math.inf] * n for _ in range(n)]
matrix2 = [[-1.0] * n for _ in range(n)]  # tartalmazni fogja az x,y pozicion a magassagot
volt = [[0] * n for _ in range(n)]

file = open('surface_100x100.txt', 'r')
Lines = file.readlines()

barrier = []

for line in Lines:

    numbers = line.split()

    x = int(numbers[0])
    y = int(numbers[1])
    z = float(numbers[2])
    b = int(numbers[3])

    matrix2[x][y] = z
    if b == 0:
        matrix[x][y] = z
    if b == 1:
        barrier.append((x, y, z))

file.close()

#############################################################################################################

file = open('surface_100x100.end_points.txt', 'r')
Lines = file.readlines()
i = 0

endX = 0
endY = 0
startX = 0
startY = 0

for line in Lines:
    numbers = line.split()
    if i == 0:
        startX = int(numbers[0])
        startY = int(numbers[1])
    if i == 1:
        endX = int(numbers[0])
        endY = int(numbers[1])
    i += 1

szomszedI = [-1, -1, -1, 0, 0, 1, 1, 1]
szomszedJ = [-1, 0, 1, -1, 1, -1, 0, 1]
if melyik == 1:
    pq = PriorityQueue()
    pq.put((matrix[startX][startY], (startX, startY)))

    distance_g[startX][startY] = 0
    distance_f[startX][startY] = tavolsag3D(startX, startY, matrix[startX][startY], endX, endY, matrix[endX][endY])
    parents[startX][startY] = (startX, startY)

    while not pq.empty():
        res = pq.get()
        (w, (x, y)) = res

        if volt[x][y] == 0:

            if x == endX and y == endY:
                break

            volt[x][y] = 1

            for i in range(8):
                ujX = x + szomszedI[i]
                ujY = y + szomszedJ[i]

                if 0 <= ujX < n and 0 <= ujY < n and matrix[ujX][ujY] != -1:
                    z = matrix[x][y]
                    ujZ = matrix[ujX][ujY]
                    end_z = matrix[endX][endY]

                    uj_g = distance_g[x][y]
                    uj_g += tavolsag3D(x, y, z, ujX, ujY, ujZ)
                    uj_f = uj_g + tavolsag3D(ujX, ujY, ujZ, endX, endY, end_z)

                    if uj_g < distance_g[ujX][ujY] or distance_g[ujX][ujY] == math.inf:
                        parents[ujX][ujY] = (x, y)
                        distance_g[ujX][ujY] = uj_g
                        distance_f[ujX][ujY] = uj_f
                        pq.put((uj_f, (ujX, ujY)))

    kezdX = endX
    kezdY = endY

    tav = [(endX, endY, matrix[endX][endY])]

    while not (kezdX == startX and kezdY == startY):
        kezdX, kezdY = parents[kezdX][kezdY]
        tav.append((kezdX, kezdY, matrix[kezdX][kezdY]))

    x = endX
    y = endY
    z = matrix[endX][endY]

    osszeg = 0
    with open('output.txt','w') as f:

        for item in tav:
            ujX, ujY, ujZ = item
            f.write("({} {})\n".format(ujX, ujY))
            osszeg += tavolsag3D(x, y, z, ujX, ujY, ujZ)
            x = ujX
            y = ujY
            z = ujZ
        f.write("Az utvonal teljes osszege = {}\n".format(osszeg))
else:
    #####################################################################################################################################
    #b alpont kezdete
    pq = PriorityQueue()
    pq.put((1, (startX, startY)))

    distance_g[startX][startY] = 0
    distance_f[startX][startY] = tavolsag3D(startX, startY, 1, endX, endY, 1)
    parents[startX][startY] = (startX, startY)

    while not pq.empty():
        res = pq.get()
        (w, (x, y)) = res

        if volt[x][y] == 0:

            if x == endX and y == endY:
                break

            volt[x][y] = 1

            for i in range(8):
                ujX = x + szomszedI[i]
                ujY = y + szomszedJ[i]

                if 0 <= ujX < n and 0 <= ujY < n and matrix[ujX][ujY] != -1:
                    z = 1
                    ujZ = 1
                    end_z = 1

                    uj_g = distance_g[x][y]
                    uj_g += tavolsag3D(x, y, z, ujX, ujY, ujZ)
                    uj_f = uj_g + tavolsag3D(ujX, ujY, ujZ, endX, endY, end_z)

                    if uj_g < distance_g[ujX][ujY] or distance_g[ujX][ujY] == math.inf:
                        parents[ujX][ujY] = (x, y)
                        distance_g[ujX][ujY] = uj_g
                        distance_f[ujX][ujY] = uj_f
                        pq.put((uj_f, (ujX, ujY)))

#######################################################################################################################################
#kirajzolas

plt.figure(figsize=(8, 6))
plt.imshow(matrix2, cmap='viridis', origin='lower', aspect='auto')
plt.colorbar(label='Value')
plt.xlabel('X')
plt.ylabel('Y')
plt.title('Heatmap')

xs = [point[0] for point in tav]
ys = [point[1] for point in tav]

xb = [p[0] for p in barrier]
yb = [p[1] for p in barrier]

plt.scatter(xs, ys, color='red', linewidth=2, label='path')
plt.scatter(xb, yb, color='purple', linewidth=2, label='barrier')

plt.legend()
plt.show()
############################################################################################
