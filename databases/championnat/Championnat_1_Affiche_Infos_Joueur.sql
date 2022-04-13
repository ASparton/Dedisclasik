

-- Affiche le nom et le salaire d�un joueur a partir de son code

CREATE OR ALTER PROCEDURE AfficheInfosJoueur
	(@IdJoueur int)
AS
BEGIN
	DECLARE @Nom char(32), @Ville char(32), @Salaire int

	-- recuperation des informations
	SELECT @Nom = NOM, @Ville = VILLE, @Salaire = SALAIRE
	FROM JOUEURS INNER JOIN EQUIPES
	   ON JOUEURS.ID_EQUIPE = EQUIPES.ID_EQUIPE
	WHERE ID_JOUEUR = @IdJoueur

	-- affichage
	IF @@ROWCOUNT > 0
	BEGIN
		PRINT 'Informations sur le joueur de numero '
--			+ RTRIM(CAST(@IdJoueur AS NCHAR)) + ' :'
			+ CAST(@IdJoueur AS NCHAR) + ' :'
		PRINT '  Nom : ' + @Nom
		PRINT '  Equipe : ' + @Ville
		PRINT '  Salaire : ' + RTRIM(CAST(@Salaire AS NCHAR))
	END
	ELSE
		PRINT 'Il n''y a pas de joueur de numero ' + CAST(@IdJoueur AS NCHAR)
END

