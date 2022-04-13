/* Creation de la base */

drop database Championnat;

create database Championnat;

use Championnat;

/* les tables */

create table EQUIPES
  (
     ID_EQUIPE int identity (1, 1),
     VILLE char(32)  not null,
     constraint PK_EQUIPES primary key (ID_EQUIPE)
  ) ;

create table JOUEURS
  (
     ID_JOUEUR int identity (1, 1),
     ID_EQUIPE int  not null,
     NOM char(32)  not null,
     SALAIRE int  not null,
     constraint PK_JOUEURS primary key (ID_JOUEUR)
  ) ;

create table RENCONTRES
  (
     ID_RENCONTRE int identity (1, 1),
     ID_EQUIPE_RECEVOIR int  not null,
     ID_EQUIPE_VISITER int  not null,
     SCORELOCAUX char(32)  null,
     SCOREVISITEURS char(32)  null,
     constraint PK_RENCONTRES primary key (ID_RENCONTRE)
  ) ;

/* contraintes referentielles */

alter table JOUEURS
     add constraint FK_JOUEURS_EQUIPES foreign key (ID_EQUIPE)
               references EQUIPES (ID_EQUIPE) ;

alter table RENCONTRES
     add constraint FK_RENCONTRES_EQUIPES foreign key (ID_EQUIPE_RECEVOIR)
               references EQUIPES (ID_EQUIPE) ;

alter table RENCONTRES
     add constraint FK_RENCONTRES_EQUIPES1 foreign key (ID_EQUIPE_VISITER)
               references EQUIPES (ID_EQUIPE) ;

go
use Championnat;

/* Remplissage */

/* Equipes */
INSERT INTO EQUIPES (VILLE) VALUES ('Bordeaux')
INSERT INTO EQUIPES (VILLE) VALUES ('Toulouse')
INSERT INTO EQUIPES (VILLE) VALUES ('Nantes')

INSERT INTO JOUEURS (ID_EQUIPE, NOM, SALAIRE) VALUES (1, 'Tom', 10000)
INSERT INTO JOUEURS (ID_EQUIPE, NOM, SALAIRE) VALUES (1, 'Jerry', 15000)

/* Joueurs */
INSERT INTO JOUEURS (ID_EQUIPE, NOM, SALAIRE) VALUES (2, 'Laurel', 20000)
INSERT INTO JOUEURS (ID_EQUIPE, NOM, SALAIRE) VALUES (2, 'Hardy', 25000)

INSERT INTO JOUEURS (ID_EQUIPE, NOM, SALAIRE) VALUES (3, 'Boule', 30000)
INSERT INTO JOUEURS (ID_EQUIPE, NOM, SALAIRE) VALUES (3, 'Bill', 35000)

/* Rencontres */
INSERT INTO RENCONTRES (ID_EQUIPE_RECEVOIR, ID_EQUIPE_VISITER, SCORELOCAUX, SCOREVISITEURS)
   VALUES (1, 2, 2, 0)

INSERT INTO RENCONTRES (ID_EQUIPE_RECEVOIR, ID_EQUIPE_VISITER, SCORELOCAUX, SCOREVISITEURS)
   VALUES (1, 3, 3, 0)

INSERT INTO RENCONTRES (ID_EQUIPE_RECEVOIR, ID_EQUIPE_VISITER, SCORELOCAUX, SCOREVISITEURS)
   VALUES (2, 3, 2, 2)

INSERT INTO RENCONTRES (ID_EQUIPE_RECEVOIR, ID_EQUIPE_VISITER, SCORELOCAUX, SCOREVISITEURS)
   VALUES (2, 1, 2, 0)

INSERT INTO RENCONTRES (ID_EQUIPE_RECEVOIR, ID_EQUIPE_VISITER, SCORELOCAUX, SCOREVISITEURS)
   VALUES (3, 1, 2, 5)

