/*************************************************************/
/* Masse salariale */

SELECT EQUIPES.ID_EQUIPE, VILLE, SUM(SALAIRE) AS "Masse salariale"
FROM EQUIPES INNER JOIN JOUEURS 
	ON EQUIPES.ID_EQUIPE = JOUEURS.ID_EQUIPE
GROUP BY EQUIPES.ID_EQUIPE, VILLE

-- 1. GROUP BY permet d'avoir une seule ligne par équipe, ainsi
-- SUM(SALAIRE) calcule la somme des salaires de chaque GROUP (équipe)
-- ce qui est bien la somme des salaires de ses joueurs
--
-- 2. On a gardé ID_EQUIPE au cas où deux équipes seraient dans la même ville
--
-- 3. Si une équipe n'a aucun joueur, elle n'apparaîtra pas ici (jointure interne)


/*************************************************************/
/* Victoires a domicile */

SELECT EQUIPES.ID_EQUIPE, EQUIPES.VILLE, 		
		COUNT(RENCONTRES.ID_RENCONTRE) AS Victoires
FROM EQUIPES LEFT OUTER JOIN RENCONTRES 
	   ON (EQUIPES.ID_EQUIPE = RENCONTRES.ID_EQUIPE_RECEVOIR 
			AND SCOREVISITEURS < SCORELOCAUX) 
GROUP BY EQUIPES.ID_EQUIPE, EQUIPES.VILLE
ORDER BY Victoires DESC

-- 1. OUTER JOIN : cette jointure externe permet de conserver les équipes
-- qui n'ont jamais gagné... le COUNT vaudra alors 0
--
-- ATTENTION : si on utilise un "WHERE SCOREVISITEURS < SCORELOCAUX" plutôt
-- que la condition dans le ON, cela ne marche pas : en effet, on "élimine"
-- les équipes sans victoires avant de faire les groupes, et donc la jointure
-- externe devient sans effet...




/*************************************************************/
/* Victoires a l'exterieur */

SELECT EQUIPES.ID_EQUIPE, EQUIPES.VILLE, 		
		COUNT(RENCONTRES.ID_RENCONTRE) AS Victoires
FROM EQUIPES LEFT OUTER JOIN RENCONTRES 
	   ON (EQUIPES.ID_EQUIPE = RENCONTRES.ID_EQUIPE_VISITER 
			AND SCOREVISITEURS > SCORELOCAUX)
GROUP BY EQUIPES.ID_EQUIPE, EQUIPES.VILLE
ORDER BY Victoires DESC

-- très similaire à la solution précédente...


/*************************************************************/
/* Victoires */

SELECT EQUIPES.ID_EQUIPE, EQUIPES.VILLE, 		
		COUNT(RENCONTRES.ID_RENCONTRE) AS Victoires
FROM EQUIPES LEFT OUTER JOIN RENCONTRES 
	   ON ((EQUIPES.ID_EQUIPE = RENCONTRES.ID_EQUIPE_RECEVOIR 
			AND SCOREVISITEURS < SCORELOCAUX) 
	   OR (EQUIPES.ID_EQUIPE = RENCONTRES.ID_EQUIPE_VISITER 
			AND SCOREVISITEURS > SCORELOCAUX) )
GROUP BY EQUIPES.ID_EQUIPE, EQUIPES.VILLE
ORDER BY Victoires DESC

-- à nouveau, très similaire à la solution précédente...






