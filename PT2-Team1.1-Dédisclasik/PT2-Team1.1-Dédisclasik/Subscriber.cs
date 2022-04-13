using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Drawing;

namespace PT2_Team1._1_Dédisclasik
{
    /// <summary>
    /// Représente une connection d'un abonné vers la base données.
    /// Offre des services permettant de gérer les actions que peuvent effectuées un abonné.
    /// </summary>
    public class Subscriber
    {
        #region Propriétés

        public int ID { get; private set; } // Le code de l'abonné dans la base
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Login { get; private set; }
        public int NbAlbums { get; set; } // Le nombre d'albums en location
        private OleDbConnection DbConnection { get; set; }  // La connexion vers la base de donnée

        #endregion

        /// <summary>
        /// Construit un abonné à partir du code de l'abonné dans la base et de la connexion donnée.
        /// </summary>
        /// <param name="subscriberID">Le code de l'abonné</param>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// Gregory Simon & Alexandre Sparton
        public Subscriber(int subscriberID, OleDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            ID = subscriberID;
            FirstName = GetFirstName();
            LastName = GetLastName();
            Login = GetLogin();
            NbAlbums = 0;
        }

        #region Réaliser un emprûnt

        /// <summary>
        /// Réalise l'emprûnt d'un album correspondant au code donné.
        /// </summary>
        /// <param name="album">Le code de l'album à emprûnter</param>
        /// <returns>True si l'emprûnt s'est réalisé sans problèmes, false sinon</returns>
        /// Alexandre Sparton & Guillaume Froidcourt
        public bool LoanAlbum(Album album)
        {
            try
            {
                if (album.IsCurrentlyLoaned())
                {
                    return false;
                }

                // Suppression de l'ancien emprunt si l'abonné a déjà emprunté cet album
                string deletePreviousLoanQuery = "DELETE FROM EMPRUNTER WHERE CODE_ABONNÉ = " + ID + " AND CODE_ALBUM = " + album.ID;
                OleDbCommand deletePreviousLoanCommand = new OleDbCommand(deletePreviousLoanQuery, DbConnection);
                deletePreviousLoanCommand.Prepare();
                deletePreviousLoanCommand.ExecuteNonQuery();

                // Ajoute le nouvel emprunt
                string addLoanQuery = "INSERT INTO EMPRUNTER ([CODE_ABONNÉ],[CODE_ALBUM],[DATE_EMPRUNT],[DATE_RETOUR_ATTENDUE]) VALUES (?,?,?,?)";
                OleDbCommand addLoanCommand = new OleDbCommand(addLoanQuery, DbConnection);
                addLoanCommand.Parameters.AddWithValue("@CODE_ABONNÉ", ID);
                addLoanCommand.Parameters.AddWithValue("@CODE_ALBUM", album.ID);
                addLoanCommand.Parameters.AddWithValue("@DATE_EMPRUNT", DateTime.Today);
                addLoanCommand.Parameters.AddWithValue("@DATE_RETOUR_ATTENDUE", DateTime.Today.AddDays(album.Delay));
                addLoanCommand.ExecuteNonQuery();
                return true;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        #endregion
        #region Lister les emprûnts

        /// <summary>
        /// Méthode permettant de consulter la liste d'emprunt par abonné.
        /// </summary>
        /// <param name="paginationQuery">La requête avec la pagination (optionnel)</param>
        /// <returns>Une liste contenant le code de chaque album empruntés en cours d'emprunt</returns>
        /// Wijdan Abderrahmani & Thomas Occelli & Alexandre Sparton (pagination)
        public List<int> ListLoans(string paginationQuery = "")
        {
            List<int> loans = new List<int>();

            // Requête permettant de lister la liste d'emprunt par login d'abonné
            string emprunt = GetListLoansQuery();
            if (paginationQuery.Length > 0)
            {
                emprunt = paginationQuery;
            }

            // Connection et affichage de la liste d'emprunt dans la console
            OleDbCommand command = new OleDbCommand(emprunt, DbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                loans.Add(reader.GetInt32(0));
                NbAlbums++;
            }
            reader.Close();

            return loans;
        }

        /// <summary>
        /// Obtient la requête qui retourne les emprunts de l'abonné.
        /// </summary>
        /// <returns>La requête qui retourne les emprunts de l'abonné</returns>
        public string GetListLoansQuery()
        {
            return "SELECT CODE_ALBUM FROM EMPRUNTER " +
                   "WHERE DATE_RETOUR IS NULL AND CODE_ABONNÉ = " + ID;
        }

        /// <summary>
        /// Un dictionnaire contenant les albums empruntables avec les suggestions marqué en valeur.
        /// </summary>
        /// <param name="paginationQuery">La query de pagination</param>
        /// <param name="isAtPageOne">True si on est à la première page de la pagination</param>
        /// <param name="nbElements">Le nombre d'éléments à ajouter à la liste</param>
        /// <returns>Un dictionnaire contenant les albums empruntables avec les suggestions marqué en valeur</returns>
        /// Alexandre Sparton
        public Dictionary<int, bool> GetAvailableAlbumsWithPreferences(string paginationQuery, bool isAtPageOne, int nbElements)
        {
            HashSet<int> allAlbums = Album.GetAllAlbums(DbConnection, paginationQuery);
            HashSet<int> preferences = ResearchIDAlbumByPreferedType();
            allAlbums.ExceptWith(preferences);

            // Création du dictionnaire
            Dictionary<int, bool> albums = new Dictionary<int, bool>();
            int counter = 0;
            if (isAtPageOne)
            {
                foreach (int albumID in preferences)
                {
                    if (counter >= nbElements)
                    {
                        break;
                    }
                    else
                    {
                        counter++;
                    }
                    albums.Add(albumID, true);
                }
            }

            if (counter < nbElements)
            {
                foreach (int albumID in allAlbums)
                {
                    if (counter >= nbElements)
                    {
                        break;
                    }
                    else
                    {
                        counter++;
                    }
                    albums.Add(albumID, false);
                }
            }
            
            return albums;
        }

        #endregion
        #region Repousser les emprûnts

        /// <summary>
        /// Ajoute un mois à l'emprunt d'un album donné.
        /// </summary>
        /// <param name="idAlbum">Le code de l'album dont l'emprunt doit être allongé</param>
        /// <returns>True si la prolongation s'est bien passée, false sinon</returns>
        /// Gregory Simon & Alexandre Sparton
        public bool ExtendLoan(int idAlbum)
        {
            if (DateUtils.HasBeenPostpone(ID, idAlbum))
            {
                return false;
            }
            if (!Album.IsAlbumCurrentlyLoaned(idAlbum, DbConnection))
            {
                return false;
            }

            try
            {
                // Obtention de la date retour attendue de l'album
                string getLoanQuery = "SELECT DATE_RETOUR_ATTENDUE FROM EMPRUNTER" +
                                     " INNER JOIN ALBUMS on EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM" +
                                     " WHERE EMPRUNTER.CODE_ABONNÉ = " + ID +
                                     " AND EMPRUNTER.CODE_ALBUM = " + idAlbum;
                OleDbCommand getLoanCommand = new OleDbCommand(getLoanQuery, DbConnection);
                getLoanCommand.Prepare();
                OleDbDataReader loanReader = getLoanCommand.ExecuteReader();
                DateTime datePlusMonth = DateTime.Today;
                while (loanReader.Read())
                {
                    datePlusMonth = loanReader.GetDateTime(0).AddMonths(1);    // On ajoute un mois à cette date
                }

                // On updaye la base de donnée avec la nouvelle date
                string newDate = DateUtils.ConstructDate(datePlusMonth.Year, datePlusMonth.Month, datePlusMonth.Day);
                string updateQuery = "UPDATE EMPRUNTER SET DATE_RETOUR_ATTENDUE = CAST('" + newDate + "' AS DATETIME) " +
                                         "WHERE EMPRUNTER.CODE_ABONNÉ = " + ID + " AND EMPRUNTER.CODE_ALBUM = " + idAlbum;
                OleDbCommand updateCommand = new OleDbCommand(updateQuery, DbConnection);
                updateCommand.Prepare();
                updateCommand.ExecuteNonQuery();
                loanReader.Close();
                return true;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        /// <summary>
        /// Permet d'tendre la durée d'emprunt d'un mois de tous les emprunts.
        /// </summary>
        /// <returns>True si l'opération s'est bien passée, false sinon</returns>
        /// Gregory Simon & Alexandre Sparton
        public bool ExtendAllLoans()
        {
            try
            {
                // Parcours de tous les albums emprunté par l'abonné
                string getAlbumsQuery = "SELECT CODE_ALBUM FROM EMPRUNTER " +
                                        "WHERE EMPRUNTER.DATE_RETOUR IS NULL AND CODE_ABONNÉ = " + ID;
                OleDbCommand getAlbumsCommand = new OleDbCommand(getAlbumsQuery, DbConnection);
                getAlbumsCommand.Prepare();

                OleDbDataReader IdReader = getAlbumsCommand.ExecuteReader();
                while (IdReader.Read())
                {
                    ExtendLoan(IdReader.GetInt32(0));   // On repousse l'emprunt de l'album (ne fait rien si déjà repoussé)
                }
                IdReader.Close();
                return true;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        #endregion
        #region Liste préférences

        /// <summary>
        /// Permet de proposer une liste d'albums des genres les plus écoutés de l'abonné.
        /// </summary>
        /// Gregory Simon
        public Dictionary<int,string> ResearchByPreferedType()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            try
            {
                ArrayList tab = new ArrayList();
                //Permet de récupere la liste de tout les albums en fonction du top 3 des genres écoutés
                string query = "SELECT ALBUMS.TITRE_ALBUM,  ALBUMS.CODE_ALBUMS" +
                               "FROM ALBUMS " +
                               "INNER JOIN GENRES on ALBUMS.CODE_GENRE = GENRES.CODE_GENRE " +
                               "WHERE GENRES.CODE_GENRE IN " +
                               "(SELECT TOP 3 Genres.CODE_GENRE from EMPRUNTER " +
                               "INNER JOIN ALBUMS on EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM " +
                               "INNER JOIN GENRES on ALBUMS.CODE_GENRE = GENRES.CODE_GENRE " +
                               (HasAlreadyLoaned() ? "WHERE EMPRUNTER.CODE_ABONNÉ = " + ID : "") +
                               "GROUP BY GENRES.CODE_GENRE " +
                               "ORDER BY count(GENRES.LIBELLÉ_GENRE) DESC)";
                OleDbCommand getIdGenres = new OleDbCommand(query, DbConnection);
                OleDbDataReader reader = getIdGenres.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0),reader.GetString(1));
                    tab.Add(reader.GetString(1));
                }
                Random random = new Random();
                for (int j = 0; j < 11; j++)
                {
                    int r = random.Next(5, tab.Count);
                    Console.WriteLine(tab[r]);
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return result;
        }


        /// <summary>
        /// Permet de proposer une liste d'albums des genres les plus écoutés de l'abonné.
        /// </summary>
        /// Alexandre Sparton
        public HashSet<int> ResearchIDAlbumByPreferedType()
        {
            HashSet<int> result = new HashSet<int>();
            try
            {
                List<int> tab = new List<int>();
                //Permet de récupere la liste de tout les albums en fonction du top 3 des genres écoutés
                string query = "SELECT ALBUMS.CODE_ALBUM " +
                               "FROM ALBUMS " +
                               "inner join GENRES on ALBUMS.CODE_GENRE = GENRES.CODE_GENRE " +
                               "where GENRES.CODE_GENRE IN " +
                               "(SELECT TOP 3 Genres.CODE_GENRE from EMPRUNTER " +
                               "INNER JOIN ALBUMS on EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM " +
                               "INNER JOIN GENRES on ALBUMS.CODE_GENRE = GENRES.CODE_GENRE " +
                               (HasAlreadyLoaned() ? "WHERE EMPRUNTER.CODE_ABONNÉ = " + ID : "") +
                               "GROUP BY GENRES.CODE_GENRE " +
                               "ORDER BY count(GENRES.LIBELLÉ_GENRE) DESC)";
                OleDbCommand getIdGenres = new OleDbCommand(query, DbConnection);
                OleDbDataReader reader = getIdGenres.ExecuteReader();
                while (reader.Read())
                {
                    tab.Add(reader.GetInt32(0));
                }
                Random random = new Random();
                for (int j = 0; j < 11; j++)
                {
                    int r = random.Next(5, tab.Count);
                    result.Add(tab[r]);
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return result;
        }

        #endregion
        #region Rendre un album

        /// <summary>
        /// Permet à l'abonné de pouvoir rendre un album.
        /// </summary>
        /// <param name="albumID">le code de l'album à rendre</param>
        public void ReturnLoan(int albumID)
        {
            try
            {
                // Réaliste le rendu
                string returnLoanQuery = "UPDATE EMPRUNTER " +
                                         "SET DATE_RETOUR = CAST('" +
                                         DateUtils.ConstructDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                                         + "' as datetime) " +
                                         "WHERE CODE_ABONNÉ = " + ID + " AND CODE_ALBUM = " + albumID;
                OleDbCommand returnLoanCommand = new OleDbCommand(returnLoanQuery, DbConnection);
                returnLoanCommand.Prepare();
                returnLoanCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        #endregion
        #region Récupérer les infos de l'abonné

        /// <summary>
        /// Obtient le nom de famille de l'abonné
        /// </summary>
        /// <returns>Le nom de famille de l'abonné</returns>
        private string GetLastName()
        {
            // Préparation de la requête
            string getLastNameQuery = "SELECT NOM_ABONNÉ FROM ABONNÉS WHERE CODE_ABONNÉ = " + ID;
            OleDbCommand getLastNameCommand = new OleDbCommand(getLastNameQuery, DbConnection);
            getLastNameCommand.Prepare();
            string name = null;

            // Récupération du nom de famille
            OleDbDataReader lastNameReader = getLastNameCommand.ExecuteReader();
            while (lastNameReader.Read())
            {
                name = lastNameReader.GetString(0);
            }
            lastNameReader.Close();
            return name;
        }


        /// <summary>
        /// Obtient le prénom de l'abonné
        /// </summary>
        /// <returns>Le prénom de l'abonné</returns>
        private string GetFirstName()
        {
            // Préparation de la requête
            string getFirstNameQuery = "SELECT PRÉNOM_ABONNÉ FROM ABONNÉS WHERE CODE_ABONNÉ = " + ID;
            OleDbCommand getFirstNameCommand = new OleDbCommand(getFirstNameQuery, DbConnection);
            string firstName = null;
            getFirstNameCommand.Prepare();

            // Récupération du prénom
            OleDbDataReader firstNameReader = getFirstNameCommand.ExecuteReader();
            while (firstNameReader.Read())
            {
                firstName = firstNameReader.GetString(0);
            }
        
            firstNameReader.Close();
            return firstName;
        }


        /// <summary>
        /// Obtient le login de l'abonné
        /// </summary>
        /// <returns>Le login de l'abonné</returns>
        private string GetLogin()
        {
            // Préparation de la requête
            string getLoginQuery = "SELECT LOGIN_ABONNÉ FROM ABONNÉS WHERE CODE_ABONNÉ = " + ID;
            OleDbCommand getLoginCommand = new OleDbCommand(getLoginQuery, DbConnection);
            getLoginCommand.Prepare();
            string login = null;

            // Récupération du login
            OleDbDataReader loginReader = getLoginCommand.ExecuteReader();
            while (loginReader.Read())
            {
                login = loginReader.GetString(0);
            }
            loginReader.Close();
            return login;
        }


        /// <summary>
        /// Détermine si l'abonné a déjà réalisé au moins un emprûnt.
        /// </summary>
        /// <returns>True si l'abonné a déjà réalisé au moins un emprûnt, false sinon</returns>
        /// Alexandre Sparton
        private bool HasAlreadyLoaned()
        {
            try
            {
                // Préparation de la requête
                string getLoansQuery = "SELECT CODE_ABONNÉ FROM EMPRUNTER";
                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, DbConnection);
                getLoansCommand.Prepare();

                // Parcours des emprûnts
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    // Si on retrouve l'abonné dans un des emprûnts, alors il a déjà emprûnté
                    if (loansReader.GetInt32(0) == ID)
                    {
                        loansReader.Close();
                        return true;
                    }
                }

                loansReader.Close();
                return false;
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
                return false;
            }
        }

        #endregion
        #region Récupérer les dates en rapport avec un emprunt

        /// <summary>
        /// Obtient la date d'emprunt en fonction du titre d'un album.
        /// </summary>
        /// <param name="albumTitle">Le titre de l'album emprunté</param>
        /// <returns>La date d'emprunt en fonction du titre d'un album</returns>
        public DateTime GetLoanDate(string albumTitle)
        {
            DateTime loanDate = DateTime.Today;

            try
            {
                string query = "SELECT EMPRUNTER.DATE_EMPRUNT FROM EMPRUNTER INNER JOIN ALBUMS " +
                               "ON EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM WHERE CODE_ABONNÉ = " + ID + 
                               " AND ALBUMS.TITRE_ALBUM = '" + albumTitle + "'";
                OleDbCommand command = new OleDbCommand(query, DbConnection);
                command.Prepare();
                OleDbDataReader loanDateReader = command.ExecuteReader();
                while (loanDateReader.Read())
                {
                    loanDate = loanDateReader.GetDateTime(0);
                }
                loanDateReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return loanDate;
        }

        /// <summary>
        /// Obtient la date de retour attendue en fonction du titre d'un album.
        /// </summary>
        /// <param name="albumTitle">Le titre de l'album emprunté</param>
        /// <returns>La date de retour attendue en fonction du titre d'un album</returns>
        public DateTime GetExpectedReturnDate(string albumTitle)
        {
            DateTime loanDate = DateTime.Today;

            try
            {
                string query = "SELECT EMPRUNTER.DATE_RETOUR_ATTENDUE FROM EMPRUNTER INNER JOIN ALBUMS " +
                               "ON EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM WHERE CODE_ABONNÉ = " + ID +
                               " AND ALBUMS.TITRE_ALBUM = '" + albumTitle + "'";
                OleDbCommand command = new OleDbCommand(query, DbConnection);
                command.Prepare();
                OleDbDataReader loanDateReader = command.ExecuteReader();
                while (loanDateReader.Read())
                {
                    loanDate = loanDateReader.GetDateTime(0);
                }
                loanDateReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return loanDate;
        }

        #endregion

        /// <summary>
        /// Retourne le prénom suivit d'un espace et du nom de l'abonné.
        /// </summary>
        /// <returns>Le prénom suivit d'un espace et du nom de l'abonné.</returns>
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }

}