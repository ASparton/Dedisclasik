
-- Fonction qui calcule la masse salariale d'une equipe

CREATE OR ALTER FUNCTION MasseSalariale
  ( @numeq int ) RETURNS int AS
BEGIN
	DECLARE @somme int
	select @somme = ( SELECT SUM(Salaire)
		FROM EQUIPES INNER JOIN JOUEURS
			ON EQUIPES.ID_EQUIPE = JOUEURS.ID_EQUIPE
		WHERE EQUIPES.ID_EQUIPE = @numeq )

	return @somme
END
GO
