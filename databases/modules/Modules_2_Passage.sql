USE [Modules]
GO


/* Procédure qui réalise le "passage" des étudiants fin d'année
   Moyenne > 11 ---> passe en année supérieure
   Moyenne < 8  ---> exclus et supprimé de la table ETUDIANTS
   Sinon        ---> redouble   
   ************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[PASSAGE] AS
BEGIN
	DECLARE @moyetd float
	DECLARE @numetd int
	DECLARE curseurPAS CURSOR
	FOR SELECT NUM_ETU FROM ETUDIANTS 
	OPEN curseurPAS
    FETCH curseurPAS INTO @numetd
    WHILE @@FETCH_STATUS = 0
		BEGIN
        select @moyetd = dbo.MOYENNE(@numetd)
        IF @moyetd > 11
			BEGIN
          UPDATE ETUDIANTS
          SET ANNEE_ETU = ANNEE_ETU + 1
          WHERE NUM_ETU = @numetd
			END
        ELSE IF @moyetd < 8
			BEGIN
          DELETE FROM NOTES WHERE NUM_ETU = @numetd
          DELETE FROM ETUDIANTS WHERE NUM_ETU = @numetd
			END
		FETCH curseurPAS INTO @numetd
		END
	CLOSE curseurPAS
	DEALLOCATE curseurPAS
END
