USE [Modules]
GO


/* Fonction qui calcule la moyenne (avec coefficients) d'un étudiant 
   *****************************************************************/
   
CREATE OR ALTER FUNCTION [dbo].[MOYENNE]
  ( @numetd int ) RETURNS float AS
BEGIN
	DECLARE @somme float
	DECLARE @sommecoeff float
	DECLARE @note float
	DECLARE @coeff float
	select @somme = 0
	select @sommecoeff = 0

	DECLARE curseurMOY CURSOR
	FOR SELECT NOTE, COEFF_MAT FROM NOTES, MATIERES
        WHERE NOTES.CODE_MAT = MATIERES.CODE_MAT
            AND NOTES.NUM_ETU = @numetd
    OPEN curseurMOY
	FETCH curseurMOY into @note,@coeff
	WHILE @@FETCH_STATUS = 0
		BEGIN
		select @somme = @somme + @note*@coeff
		select @sommecoeff = @sommecoeff + @coeff
		FETCH curseurMOY into @note,@coeff
		END
	CLOSE curseurMOY
	DEALLOCATE curseurMOY
	IF @sommecoeff = 0
		BEGIN
		select @sommecoeff = 1
		END
	return @somme/@sommecoeff
END
GO
