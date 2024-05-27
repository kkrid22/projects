--create database lab3_tabla
GO
USE lab3_tabla
GO

CREATE TABLE FelhasznaloCsoport(
	FelhaszCsopID INT PRIMARY KEY,
	Csoport INT --milyen szerepe van, ha a ez 0 akkor admin ha 1 akkor felhasznalo ha 2 akkor guest
);


CREATE TABLE Felhasznalok(
	FelhaszID INT PRIMARY KEY IDENTITY(1,1),
	Nev VARCHAR(50),
	Jelszo VARCHAR(255),
	Salt VARCHAR(255),
	CsoportID INT,
	CONSTRAINT FK_Felhasznalok_FelhasznaloCsoportok FOREIGN KEY(CsoportID) REFERENCES FelhasznaloCsoport(FelhaszCsopID)
);

CREATE TABLE Szerzok(
	SZID INT,
	Szuletett DATE,
	SzerzoNev VARCHAR(50),
	CONSTRAINT PK_Szerzok PRIMARY KEY (SZID),
)

CREATE TABLE Konyvek(
	KID INT,
	SZID INT,
	Cim VARCHAR(50),
	CONSTRAINT PK_Konyvek PRIMARY KEY (KID),
	CONSTRAINT FK_Konyvek_Szerzok FOREIGN KEY(SZID) REFERENCES Szerzok(SZID),
)

CREATE TABLE Konyvesboltok(
	BID INT,
	Fizetes INT,
	BoltNev VARCHAR(50),
	Cim VARCHAR(50),
	CONSTRAINT PK_Konyvesboltok PRIMARY KEY (BID),
)

CREATE TABLE Tartalmazza(
	BID INT,
	KID INT,
	Ar INT,
	Peldany INT,
	CONSTRAINT PK_Tartalmazza PRIMARY KEY(KID,BID),
	CONSTRAINT FK_Konyvek_Tartalmazza FOREIGN KEY(KID) REFERENCES Konyvek(KID),
	CONSTRAINT FK_Konyvesboltok_Tartalmazza FOREIGN KEY(BID) REFERENCES Konyvesboltok(BID)
)

CREATE TABLE Vasarlok(
	VID INT IDENTITY,
	Nev VARCHAR(50),
	Telefon VARCHAR(13),
	CONSTRAINT PK_Vasarlok PRIMARY KEY(VID)
)

CREATE TABLE Vasarlasok(
	VAID INT IDENTITY,
	BID INT,
	VID INT,
	Datum DATE,
	Ar FLOAT,
	CONSTRAINT PK_Vasarlasok PRIMARY KEY(VAID),
	CONSTRAINT FK_Konyvesboltok_Vasarlasok FOREIGN KEY(BID) REFERENCES Konyvesboltok(BID),
	CONSTRAINT FK_Vasarlok_Vasarlasok FOREIGN KEY(VID) REFERENCES Vasarlok(VID)
)

CREATE TABLE Mufajok(
	MID INT,
	MufajNev VARCHAR(50),
	CONSTRAINT PK_Mufajok PRIMARY KEY(MID),
)

CREATE TABLE Mufaja(
	MID INT,
	KID INT,
	CONSTRAINT PK_Mufaja PRIMARY KEY(MID,KID),
	CONSTRAINT FK_Mufajok_Mufaja FOREIGN KEY(MID) REFERENCES Mufajok(MID),
	CONSTRAINT FK_Konyvek_Mudaja FOREIGN KEY(KID) REFERENCES Konyvek(KID)
	
)


CREATE TABLE VasarlasReszletek(
	VAID INT,
	KID INT,
	Peldany INT,
	CONSTRAINT PK_Vasarlas PRIMARY KEY(VAID,KID),
	CONSTRAINT FK_Vasarlasok_Vasarlas FOREIGN KEY(VAID) REFERENCES Vasarlasok(VAID),
	CONSTRAINT FK_Konyvek_Vasarlas FOREIGN KEY(KID) REFERENCES Konyvek(KID) 
)


INSERT INTO Szerzok (SZID, Szuletett, SzerzoNev)
VALUES
    (1, '1970-01-01', 'its a me mario'),
    (2, '1980-02-15', 'Laji'),
    (3, '1995-05-20', 'Jon Stewarts');

INSERT INTO Konyvek (KID, SZID, Cim)
VALUES
    (101,1, 'how to talk to your cat about gun safety'),
    (102,1, 'ducks and how to make them pay'),
    (103,2, 'a practical guide to racism'),
	(104,2, 'the begginers guide to human sacrifice'),
    (105,2, 'strangers have the best candy'),
    (106,1, 'goblinproofing your chicken coop'),
	(107,3, 'how to avoid huge ships'),
    (108,3, 'the loneliest ho in the world'),
    (109,3, 'divorcing a real witch');

INSERT INTO Konyvesboltok (BID, Fizetes, BoltNev, Cim)
VALUES
    (1, 2000, 'Voltegyszer hol nem volt', 'Budapest'),
    (2, 2500, 'Nem mondom meg', 'Debrecen'),
    (3, 1800, 'Konyvesboltban', 'Szeged');

-- Insert data into Tartalmazza table
INSERT INTO Tartalmazza (BID, KID, Peldany, Ar)
VALUES
    (1, 101, 10, 10),
    (1, 102, 15, 20),
    (2, 101, 20, 100),
    (2, 103, 25, 35),
    (3, 102, 30, 66);

INSERT INTO Vasarlok (Nev)
VALUES
	('Valaki'),
	('Valahol'),
	('Valamit');

INSERT INTO Vasarlasok(VID,BID,Datum,Ar)
VALUES
	(2, 2, '2024-04-17', 500),
	(2, 2, '2024-04-18', 500),
	(3, 2, '2024-04-20', 300),
	(3, 2, '2024-04-15', 4000),
	(4, 2, '2024-04-12', 50);

INSERT INTO FelhasznaloCsoport(FelhaszCsopID,Csoport)
VALUES (0,0),(1,1),(2,2)

INSERT INTO Felhasznalok(Nev, Jelszo, CsoportID)
VALUES('Admin', 'Admin', 0)

select *
from Felhasznalok