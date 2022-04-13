USE [Modules]
GO

CREATE OR ALTER TRIGGER [dbo].[UPD_ETU] ON [dbo].[ETUDIANTS]
INSTEAD OF UPDATE AS
	
	DECLARE @num int
	DECLARE @nom varchar(20)
	DECLARE @prenom varchar(20)
	DECLARE @newAnnee int
	DECLARE @oldAnnee int

BEGIN
	DECLARE curseurUPD CURSOR
	FOR select NUM_ETU, NOM_ETU, PRENOM_ETU, ANNEE_ETU from inserted    
	OPEN curseurUPD
	FETCH curseurUPD into @num, @nom, @prenom, @newAnnee
	WHILE @@FETCH_STATUS = 0
		BEGIN
		select @oldAnnee = (select ANNEE_ETU from deleted where NUM_ETU = @num)
		IF @newAnnee < @oldAnnee OR @newAnnee > @oldAnnee + 1
			BEGIN
			select @newAnnee = @oldAnnee
			END
		UPDATE ETUDIANTS
			SET NOM_ETU = @nom, PRENOM_ETU = @prenom, ANNEE_ETU = @newAnnee
		WHERE NUM_ETU = @num
		FETCH curseurUPD into @num, @nom, @prenom, @newAnnee
		END
	CLOSE curseurUPD
	DEALLOCATE curseurUPD
END
GO
	

/* Trigger sauvegarde dans ETUD_OLD des étudiants supprimés de ETUDIANTS 
   *********************************************************************/

CREATE OR ALTER TRIGGER [dbo].[DEL_ETU] ON [dbo].[ETUDIANTS]
AFTER DELETE AS
BEGIN
	insert into ETUD_OLD(NUM_OLD, NOM_OLD, PRENOM_OLD, DATE_SORTIE)
		select NUM_ETU, NOM_ETU, PRENOM_ETU, getdate() from deleted
END
GO
