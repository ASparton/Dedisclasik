using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik
{
    /// <summary>
    /// Caractérise un album avec toutes les informations et méthodes pouvant être utiles.
    /// </summary>
    public class Album
    {
        #region Propriétés

        private readonly OleDbConnection DbConnection;
        public int ID { get; private set; } // Le code de l'album dans la base
        public string Title { get; private set; }   // Le titre de l'album
        public string ProductionDate { get; private set; }    // L'année de production de l'album
        public string Price { get; private set; }   // Le prix du vinyle
        public string Section { get; private set; }   // L'allée (le rayon) de l'album dans le magasin
        public int Delay { get; private set; }  // Le délai en fonction de son genre
        public bool IsSuggested { get; set; }   // Est-ce que l'album est une suggestion ou non ?
        public string TextLoaned { get; set; }

        #endregion

        /// <summary>
        /// Créer une instance d'album.
        /// </summary>
        /// <param name="idAlbum">Le code de l'album dans la base</param>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        public Album(int idAlbum, OleDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            ID = idAlbum;
            Title = GetTitle();
            ProductionDate = GetProductionDate();
            Price = GetLoanPrice();
            Section = GetSection();
            Delay = GetDelay();
            IsSuggested = false;
            TextLoaned = "   (Indisponible)";
        }

        #region Récupération des infos de l'album

        /// <summary>
        /// Retourne le titre d'un album.
        /// </summary>
        /// <returns>Le titre de l'album en chaîne de caractère</returns>
        public string GetTitle()
        {
            string title = "";

            try
            {
                // Préparation de la requête
                string getTitleQuery = "SELECT TITRE_ALBUM FROM ALBUMS " +
                                       "WHERE CODE_ALBUM = " + ID;
                OleDbCommand getTitleCommand = new OleDbCommand(getTitleQuery, DbConnection);
                getTitleCommand.Prepare();

                // Récupération du titre
                OleDbDataReader titleReader = getTitleCommand.ExecuteReader();
                while (titleReader.Read())
                {
                    title = titleReader.GetString(0);
                }
                titleReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return title;
        }

        public string GetProductionDate()
        {
            string date = "";

            try
            {
                // Préparation de la requête
                string getDateQuery = "SELECT ANNÉE_ALBUM FROM ALBUMS " +
                                      "WHERE CODE_ALBUM = " + ID;
                OleDbCommand getDateCommand = new OleDbCommand(getDateQuery, DbConnection);
                getDateCommand.Prepare();

                // Récupération de la l'année
                OleDbDataReader getDateReader = getDateCommand.ExecuteReader();
                while (getDateReader.Read())
                {
                    date = getDateReader.GetInt32(0).ToString();
                }
                getDateReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return date;
        }

        /// <summary>
        /// Obtient le prix de location de l'album.
        /// </summary>
        /// <returns>Le prix de l'album</returns>
        public string GetLoanPrice()
        {
            string price = "";

            try
            {
                // Préparation de la requête
                string getLoanPriceQuery = "SELECT PRIX_ALBUM FROM ALBUMS " +
                                      "WHERE CODE_ALBUM = " + ID;
                OleDbCommand getLoanPriceCommand = new OleDbCommand(getLoanPriceQuery, DbConnection);
                getLoanPriceCommand.Prepare();

                // Récupération du prix
                OleDbDataReader getLoanPriceReader = getLoanPriceCommand.ExecuteReader();
                while (getLoanPriceReader.Read())
                {
                    price = getLoanPriceReader.GetDecimal(0).ToString() + "$";
                }
                getLoanPriceReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return price;
        }

        /// <summary>
        /// Obtient l'allée de l'album.
        /// </summary>
        /// <returns>L'allée de l'album</returns>
        /// Alexandre Sparton
        public string GetSection()
        {
            string place = "";

            try
            {
                string getSectionQuery = "SELECT ALLÉE_ALBUM FROM ALBUMS " +
                                      "WHERE CODE_ALBUM = " + ID;
                OleDbCommand getSectionCommand = new OleDbCommand(getSectionQuery, DbConnection);
                getSectionCommand.Prepare();
                OleDbDataReader getSectionReader = getSectionCommand.ExecuteReader();
                while (getSectionReader.Read())
                {
                    place = getSectionReader.GetString(0);
                }
                getSectionReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return place;
        }

        /// <summary>
        /// Obtient le délai de l'album.
        /// </summary>
        /// <returns>Le délai d'emprunt l'album</returns>
        /// Alexandre Sparton
        public int GetDelay()
        {
            try
            {
                int delay = -1;
                // Préparation de la requête
                string getDelayQuery = "SELECT DÉLAI FROM ALBUMS " +
                                       "INNER JOIN GENRES ON GENRES.CODE_GENRE = ALBUMS.CODE_GENRE " +
                                       "WHERE ALBUMS.CODE_ALBUM = " + ID;
                OleDbCommand getDelayCommand = new OleDbCommand(getDelayQuery, DbConnection);
                getDelayCommand.Prepare();

                // Récupération du délai
                OleDbDataReader delayReader = getDelayCommand.ExecuteReader();
                while (delayReader.Read())
                {
                    delay = delayReader.GetInt32(0);
                }
                delayReader.Close();

                return delay;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return -1;
            }
        }

        #endregion
        #region Services liés aux emprunts

        /// <summary>
        /// Détermine si l'album est actuellement emprunté par un abonné.
        /// </summary>
        /// <returns>True si l'album est actuellement emprunté, false sinon</returns>
        public bool IsCurrentlyLoaned()
        {
            try
            {
                // Préparation de la requête
                string getLoansQuery = "SELECT CODE_ALBUM, DATE_RETOUR FROM EMPRUNTER";
                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, DbConnection);
                getLoansCommand.Prepare();

                // Parcours des emprûnts
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    // Si on retrouve le code et que la date de retour n'est pas renseignée, c'est qu'il est emprûnté
                    if (loansReader.GetInt32(0) == ID && loansReader.IsDBNull(1))
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
                Console.WriteLine(exception);
                return false;
            }
        }

        /// <summary>
        /// Obtient la date d'emprunt de cet album pour un abonné donné.
        /// </summary>
        /// <param name="subscriberID">Le code de l'abonné</param>
        /// <returns>La date d'emprunt de l'album par cet abonné</returns>
        public DateTime GetLoanDate(int subscriberID)
        {
            DateTime loanDate = new DateTime(1, 1, 1);

            try
            {
                // Préparation de la requête
                string getLoanDateQuery = "SELECT DATE_EMPRUNT FROM EMPRUNTER " +
                                          "WHERE CODE_ALBUM = " + ID + " AND CODE_ABONNÉ = " + subscriberID;
                OleDbCommand getLoanDateCommand = new OleDbCommand(getLoanDateQuery, DbConnection);
                getLoanDateCommand.Prepare();

                // Récupération de la date d'emprunt
                OleDbDataReader loanDateReader = getLoanDateCommand.ExecuteReader();
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
        /// Obtient la date de retour attendue de cet album pour un abonné donné.
        /// </summary>
        /// <param name="subscriberID">Le code de l'abonné</param>
        /// <returns>La date de retour attendue de l'album par cet abonné</returns>
        public DateTime GetExpectedReturnDate(int subscriberID)
        {
            DateTime expectedReturnDate = new DateTime(1, 1, 1);

            try
            {
                // Préparation de la requête
                string getExpectedReturnDateQuery = "SELECT DATE_RETOUR_ATTENDUE FROM EMPRUNTER " +
                                          "WHERE CODE_ALBUM = " + ID + " AND CODE_ABONNÉ = " + subscriberID;
                OleDbCommand getExpectedReturnDateCommand = new OleDbCommand(getExpectedReturnDateQuery, DbConnection);
                getExpectedReturnDateCommand.Prepare();

                // Récupération de la date de retour attendue
                OleDbDataReader expectedReturnDateReader = getExpectedReturnDateCommand.ExecuteReader();
                while (expectedReturnDateReader.Read())
                {
                    expectedReturnDate = expectedReturnDateReader.GetDateTime(0);
                }
                expectedReturnDateReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return expectedReturnDate;
        }

        public byte[] GetImage()
        {
            byte[] octets = null;
            try
            {
                string sql = "SELECT POCHETTE from ALBUMS WHERE CODE_ALBUM = " + ID;
                OleDbCommand cmd = new OleDbCommand(sql, DbConnection);
                cmd.Prepare();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    octets = (byte[])reader.GetValue(0);
                }
                reader.Close();
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex);
            }
            return octets;
        }

        #endregion
        #region Utils

        /// <summary>
        /// Obtient le code d'un album à partir de son titre.
        /// </summary>
        /// <param name="title">Le titre de l'album dont on veut obtenir le code</param>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <returns>Le code de l'album correspondant au titre donné.</returns>
        public static int GetAlbumIDFromTitle(string title, OleDbConnection dbConnection)
        {
            if (title.Contains(" [Indisponible]"))
            {
                title = title.Replace(" [Indisponible]", "");
            }
            try
            {
                // Préparation de la requête
                string getIDQuery = "SELECT CODE_ALBUM FROM ALBUMS WHERE TITRE_ALBUM LIKE '%" + title + "%'";
                OleDbCommand getIDCommand = new OleDbCommand(getIDQuery, dbConnection);
                getIDCommand.Prepare();

                // Récupération du code
                OleDbDataReader reader = getIDCommand.ExecuteReader();
                reader.Read();
                int albumID = reader.GetInt32(0);
                reader.Close();
                return albumID;
            }
            catch (OleDbException excpetion)
            {
                Console.Error.WriteLine(excpetion);
                return -1;
            }
        }

        /// <summary>
        /// Détermine si un album est en train d'être loué à ce jour ou nom.
        /// </summary>
        /// <param name="albumID">Le code de l'album</param>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <returns>True si l'album est actuellement emprûnté, false sinon</returns>
        /// Alexandre Sparton & Guillaume Froidcourt
        public static bool IsAlbumCurrentlyLoaned(int albumID, OleDbConnection dbConnection)
        {
            try
            {
                // Préparation de la requête
                string getLoansQuery = "SELECT CODE_ALBUM, DATE_RETOUR FROM EMPRUNTER";
                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, dbConnection);
                getLoansCommand.Prepare();

                // Parcours des emprûnts
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    // Si on retrouve le code et que la date de retour n'est pas renseignée, c'est qu'il est emprûnté
                    if (loansReader.GetInt32(0) == albumID && loansReader.IsDBNull(1))
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
                Console.WriteLine(exception);
                return false;
            }
        }

        /// <summary>
        /// Retourne tous les albums empruntables
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <param name="paginationQuery">La requête pour la pagination (optionnel)</param>
        /// <returns>Tous les albums empruntables</returns>
        /// Alexandre Sparton
        public static HashSet<int> GetAllAlbums(OleDbConnection dbConnection, string paginationQuery = "")
        {
            HashSet<int> allAlbums = new HashSet<int>();

            try
            {
                // Préparation de la requête des albums jamais empruntés disponibles
                string getAllAvailableQuery = "SELECT CODE_ALBUM FROM ALBUMS";
                if (paginationQuery.Length > 0)
                {
                    getAllAvailableQuery = paginationQuery;
                }

                OleDbCommand getAllAvailableCommand = new OleDbCommand(getAllAvailableQuery, dbConnection);
                getAllAvailableCommand.Prepare();

                // Récupération des albums empruntés disponibles
                OleDbDataReader availableAlbumsReader = getAllAvailableCommand.ExecuteReader();
                while (availableAlbumsReader.Read())
                {
                    if (!allAlbums.Contains(availableAlbumsReader.GetInt32(0)))
                        allAlbums.Add(availableAlbumsReader.GetInt32(0));
                }
                availableAlbumsReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return allAlbums;
        }

        /// <summary>
        /// Obtient la requête qui permet de lister tous les albums.
        /// </summary>
        /// <returns>La requête qui permet de lister tous les albums.</returns>
        public static string GetAllAlbumsQuery()
        {
            return "SELECT CODE_ALBUM FROM ALBUMS";
        }

        /// <summary>
        /// Détermine le délai d'emprûnt d'un album à partir de son code.
        /// </summary>
        /// <param name="idAlbum">Le code de l'album</param>
        /// <param name="dbConnection">La connection vers la base de données</param>
        /// <returns>Le nombre de jour correspondant au délai d'emprûnt de l'album; -1 si une erreur est survenue</returns>
        /// Alexandre Sparton & Thoms Ocelli
        public static int GetAlbumDelay(int idAlbum, OleDbConnection dbConnection)
        {
            try
            {
                // Préparation de la requête
                string getDelayQuery = "SELECT DÉLAI FROM ALBUMS " +
                                   "INNER JOIN GENRES ON GENRES.CODE_GENRE = ALBUMS.CODE_GENRE " +
                                   "WHERE ALBUMS.CODE_ALBUM = " + idAlbum;
                OleDbCommand getDelayCommand = new OleDbCommand(getDelayQuery, dbConnection);
                getDelayCommand.Prepare();

                // Récupération du délai
                OleDbDataReader delayReader = getDelayCommand.ExecuteReader();
                int delay = 0;

                while (delayReader.Read())
                {
                    delay = delayReader.GetInt32(0);
                }
                delayReader.Close();

                return delay;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// Retourne le titre de l'album et une étoile si l'album est suggéré.
        /// </summary>
        /// <returns>Le titre de l'album et une étoile si l'album est suggéré.</returns>
        /// Alexandre Sparton
        public override string ToString()
        {
            if (this.IsCurrentlyLoaned())
            {
                return (IsSuggested ? "*   " : "") + Title.Trim() + TextLoaned;
            }
            return (IsSuggested ? "*   " : "") + Title.Trim();
        }

        public int GetLoanCount()
        {
            return 0;
        }
        /// <summary>
        /// Permet de rechercher un album par son nom
        /// </summary>
        /// <param name="DbConnection"> La connection</param>
        /// <param name="titre">Le titre de l'album</param>
        /// <returns></returns>
        public static List<string> ReshearshAlbumByName(OleDbConnection DbConnection, string titre, List<string> currentLoaned)
        {
            List<string> Recherche = new List<string>();
            try
            {
                //Permet d'afficher les albums contenant la recherche entré par l'utilisateur
                string sql = "SELECT TITRE_ALBUM FROM ALBUMS WHERE TITRE_ALBUM LIKE '" + titre + "%' OR TITRE_ALBUM LIKE '% " + titre + "%'";
                OleDbCommand command = new OleDbCommand(sql, DbConnection);
                command.Prepare();

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //Recherche.Add(reader.GetString(0));
                    if (currentLoaned.Contains(reader.GetString(0)))
                    {
                        Recherche.Add(reader.GetString(0).Trim() + " [Indisponible]");
                    } else
                    {
                        Recherche.Add(reader.GetString(0));
                    }
                }
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }
            return Recherche;
        }

        public static List<string> GetLoanedAlbums(OleDbConnection DbConnection)
        {
            List<string> loanedAlbums = new List<string>();
            string sql = "select distinct TITRE_ALBUM from ALBUMS inner join EMPRUNTER on ALBUMS.CODE_ALBUM = EMPRUNTER.CODE_ALBUM where EMPRUNTER.DATE_RETOUR IS NULL";
            OleDbCommand command = new OleDbCommand(sql, DbConnection);

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                loanedAlbums.Add(reader[0].ToString());
            }
            reader.Close();
            return loanedAlbums;
        }
    }
}
