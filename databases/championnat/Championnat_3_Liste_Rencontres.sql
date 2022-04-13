-- Liste des rencontres d'une equipe

CREATE OR ALTER PROCEDURE ListeRencontresEquipe
  ( @IdEquipe int ) AS
BEGIN
	DECLARE @Ville char(32)
	DECLARE @Equipe1 char(32), @Equipe2 char(32)
	DECLARE @Score1 int, @Score2 int

	SELECT @Ville = (SELECT VILLE FROM EQUIPES WHERE ID_EQUIPE = @IdEquipe)

	-- titre pour l’equipe
	PRINT ' '
	PRINT 'Rencontres de ' + @Ville
	PRINT '-------------------------'

	-- declaration du curseur rencontres de l’equipe
	DECLARE CurseurR CURSOR
	FOR
		SELECT E1.VILLE, E2.VILLE, SCORELOCAUX, SCOREVISITEURS
		FROM RENCONTRES
			   	INNER JOIN EQUIPES AS E1
					ON RENCONTRES.ID_EQUIPE_RECEVOIR = E1.ID_EQUIPE
			  	INNER JOIN EQUIPES AS E2
					ON RENCONTRES.ID_EQUIPE_VISITER = E2.ID_EQUIPE
		WHERE RENCONTRES.ID_EQUIPE_RECEVOIR = @IdEquipe
				OR RENCONTRES.ID_EQUIPE_VISITER = @IdEquipe
		ORDER BY E1.VILLE, E2.VILLE

	-- ouverture du curseur equipe
	OPEN CurseurR
	-- lecture de la premiere rencontre
	FETCH CurseurR INTO @Equipe1, @Equipe2, @Score1, @Score2
	-- boucle de traitement
	WHILE @@FETCH_STATUS = 0
		BEGIN
		PRINT '   ' + SUBSTRING(@Equipe1,1,LEN(@Equipe1))  + ' -- ' +
		   SUBSTRING(@Equipe2,1,LEN(@Equipe2)) + ' : ' +
		   CAST(@Score1 AS CHAR(2)) + ' - ' + CAST(@Score2 AS CHAR(2))
		-- lecture de la rencontre suivante
		FETCH CurseurR INTO @Equipe1, @Equipe2, @Score1, @Score2
		END
	-- fermeture du curseur rencontres
	CLOSE CurseurR
	-- liberation de la mémoire
	DEALLOCATE CurseurR
END
