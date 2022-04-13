## Projet exemple pour illustrer les mises à jour d'une base de données via OLEDB

* Base concernée : `Championnat`.
* Opérations de mise à jour : ajout, modification ou suppression de tuples dans la table `Joueurs`.

***Note 1 :*** Il ne s'agit pas d'un outil "complet", mais d'une simple illustration. Il ne permet ainsi que d'effectuer des opérations de mise à jour sur la table `Joueurs` de la base de données `Championnat`, et uniquement pour les joueurs de l'équipe ayant pour `Id_Equipe` la valeur `1`.

Pour tester ce projet, vous devrez adapter le fichier `app.config` (nom de votre serveur).

***Note 2 :*** Vous devrez éventuellement adapter le code dans `Form1.cs` si vous n'avez pas d'équipe ayant `1` pour identifiant.
