USE lab3_tabla

GO
CREATE OR ALTER PROCEDURE regisztracio(@pNev VARCHAR(50),@pTelefon VARCHAR(13), @pFelhasznalo VARCHAR(50), @pJelszo VARCHAR(255), @pSalt VARCHAR(255))
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Vasarlok(Nev,Telefon)
	VALUES(@pNev, @pTelefon)

	INSERT INTO Felhasznalok(Nev, Jelszo, Salt, CsoportID)
	VALUES(@pFelhasznalo, @pJelszo, @pSalt, 2)

END
