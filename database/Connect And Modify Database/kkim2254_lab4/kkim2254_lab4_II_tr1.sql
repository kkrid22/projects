USE lab3_tabla

GO

CREATE TRIGGER trg_SzerzoTorles
ON Szerzok
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @pSzerzoID INT

	SELECT @pSzerzoID = D.SZID 
	FROM deleted AS D 
	JOIN Szerzok AS S ON S.SZID = D.SZID
	WHERE D.SzerzoNev = S.SzerzoNev AND CAST(D.Szuletett AS DATE) = S.Szuletett

	UPDATE Konyvek
	SET SZID = NULL
	FROM Konyvek AS K
	WHERE K.SZID = @pSzerzoID

	DELETE FROM Szerzok
	FROM Szerzok AS S
	WHERE S.SZID = @pSzerzoID

END

GO	