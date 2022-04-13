using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PT2_Team1._1_Dédisclasik
{
    /// <summary>
    /// Offre les services que l'administrateur a besoin d'avoir.
    /// </summary>
    public class Administrator
    {
        private readonly OleDbConnection DbConnection;   // La connexion vers la base de données

        /// <summary>
        /// Instancie un Administrator avec une connexion vers la base de données
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// Alexandre Sparton
        public Administrator(OleDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        #region Listage des albums non emprûntés depuis plus d'un an

        /// <summary>
        /// Liste tous les albums qui n'ont pas été emprûntés depuis au moins un an.
        /// </summary>
        /// <returns>Retourne un dictionnaire contenant le code des albums non empruntés depuis plus d'un an et leur dernière date d'emprunt.</returns>
        /// Alexandre Sparton
        public Dictionary<int, DateTime> ListAlbumsNotLoanedSinceOneYear(string thisPaginationQuery = "", string other = "")
        {
            // Contient le code des albums déjà emprûntés
            Dictionary<int, DateTime> albumNotLoanedSince1Year = ListAlbumsNotLoanedSinceOneYearAlreadyLoaned(other);

            try
            {
                // Parcours du reste des albums
                string getAlbumsQuery = GetListAlbumsNotLoanedSinceOneYearQuery();
                if (thisPaginationQuery.Length > 0)
                {
                    getAlbumsQuery = thisPaginationQuery;
                }

                OleDbCommand getAlbumsCommand = new OleDbCommand(getAlbumsQuery, DbConnection);
                getAlbumsCommand.Prepare();
                OleDbDataReader albumsReader = getAlbumsCommand.ExecuteReader();
                while (albumsReader.Read())
                {
                    if (!albumNotLoanedSince1Year.ContainsKey(albumsReader.GetInt32(0)))
                        albumNotLoanedSince1Year.Add(albumsReader.GetInt32(0), new DateTime(2, 2, 2));
                }
                albumsReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return albumNotLoanedSince1Year.Where(lastLoan => lastLoan.Value != new DateTime(1, 1, 1))
                .ToDictionary(value => value.Key, value => value.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetListAlbumsNotLoanedSinceOneYearQuery()
        {
            return "SELECT CODE_ALBUM, TITRE_ALBUM FROM ALBUMS";
        }

        /// <summary>
        /// Liste tous les albums non empruntés depuis plus d'un an mais déjà ajouté dans le passé.
        /// </summary>
        /// <returns>Un dictionnaire contenant le code de tous les albums déjà emprûntés avec leur dernière date de retour.</returns>
        /// Alexandre Sparton
        private Dictionary<int, DateTime> ListAlbumsNotLoanedSinceOneYearAlreadyLoaned(string paginationQuery = "")
        {
            // ¨Garde le code de tous albums emprûntés.
            Dictionary<int, DateTime> albumNotLoanedSince1Year = new Dictionary<int, DateTime>();

            try
            {
                // Parcours des emprûnts
                string getLoansQuery = GetListAlbumsNotLoanedSinceOneYearAlreadyLoanedQuery();
                if (paginationQuery.Length > 0)
                {
                    getLoansQuery = paginationQuery;
                }

                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, DbConnection);
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    // Si l'emprûnt date de plus d'un an, on l'affiche
                    if (!loansReader.IsDBNull(1) &&
                        DateUtils.AtLeastMoreThanAYearAgo(loansReader.GetDateTime(1)) &&
                        !albumNotLoanedSince1Year.ContainsKey(loansReader.GetInt32(0)))
                    {
                        albumNotLoanedSince1Year.Add(loansReader.GetInt32(0), loansReader.GetDateTime(1));
                    }
                    else if (!albumNotLoanedSince1Year.ContainsKey(loansReader.GetInt32(0)))
                    {
                        albumNotLoanedSince1Year.Add(loansReader.GetInt32(0), new DateTime(1, 1, 1));
                    }
                }
                loansReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }

            return albumNotLoanedSince1Year;
        }

        public string GetListAlbumsNotLoanedSinceOneYearAlreadyLoanedQuery()
        {
            return "SELECT EMPRUNTER.CODE_ALBUM, EMPRUNTER.DATE_RETOUR, ALBUMS.TITRE_ALBUM " +
                   "FROM EMPRUNTER INNER JOIN ALBUMS ON EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM";
        }

        #endregion
        #region Purge abonnés

        /// <summary>
        /// Supprime les abonnés n'ayant pas emprunté depuis plus de 1 an
        /// </summary>
        public void PurgeAbonne(OleDbConnection DbConnection)
        {
            // Garde les id des abonnés ayant des emprunts en cours
            List<int> keep = new List<int>();
            string sql = "select distinct CODE_ABONNÉ from EMPRUNTER where DATE_RETOUR IS NULL";
            OleDbCommand command = new OleDbCommand(sql, DbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                keep.Add((int)reader[0]);
            }
            reader.Close();

            // Stock les CODE_ABONNÉ dans nb
            List<int> nb = NbAbonne();
            // Pour chaque CODE_ABONNÉ
            foreach (int n in nb)
            {
                try
                {
                    if (!keep.Contains(n))
                    {
                        DateTime date;
                        string delete = "DELETE FROM EMPRUNTER WHERE CODE_ABONNÉ = " + n +
                        " DELETE FROM ABONNÉS WHERE CODE_ABONNÉ = " + n;
                        string derniereDate = "SELECT MAX(DATE_EMPRUNT) " +
                        "FROM EMPRUNTER WHERE CODE_ABONNÉ = " + n;

                        command = new OleDbCommand(derniereDate, DbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            date = reader.GetDateTime(0);
                            command = new OleDbCommand(delete, DbConnection);
                            // Supprime ssi DATE_EMPRUNT date de plus de 1 an
                            if (DateUtils.AtLeastMoreThanAYearAgo(date) && GetListPurgeSubs(DbConnection).Contains(n))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        reader.Close();
                    }
                }
                catch (InvalidCastException ex)
                {
                    ex.ToString();
                }
            }
            if (GetListPurgeSubs(DbConnection).Count > 0)
            {
                MessageBox.Show("Suppression effectué");
            } else
            {
                MessageBox.Show("Pas d'abonnés à supprimer");
            }
        }

        /// <summary>
        /// Renvoi la liste des abonnés n'ont pas emprunté depuis 1 an et qui n'ont pas d'album loué
        /// </summary>
        /// <param name=""> La connection </param>
        /// <returns> la liste d'abonnés </returns>
        public List<int> GetListPurgeSubs(OleDbConnection DbConnection)
        {
            List<int> listPurge = new List<int>();
            // Garde les id des abonnés ayant des emprunts en cours
            List<int> keep = new List<int>();
            string sql = "select distinct CODE_ABONNÉ from EMPRUNTER where DATE_RETOUR IS NULL";
            OleDbCommand command = new OleDbCommand(sql, DbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                keep.Add((int)reader[0]);
            }
            reader.Close();

            // Stock les CODE_ABONNÉ dans nb
            List<int> nb = NbAbonne();
            DateTime date;
            // Pour chaque CODE_ABONNÉ
            foreach (int n in nb)
            {
                try
                {
                    if (!keep.Contains(n))
                    {
                        sql = "SELECT MAX(DATE_EMPRUNT) FROM EMPRUNTER WHERE CODE_ABONNÉ = " + n;
                        command = new OleDbCommand(sql, DbConnection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            date = reader.GetDateTime(0);
                            command = new OleDbCommand(sql, DbConnection);
                            // Supprime ssi DATE_EMPRUNT date de plus de 1 an
                            if (DateUtils.AtLeastMoreThanAYearAgo(date))
                            {
                                listPurge.Add(n);
                            }

                        }
                        reader.Close();
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return listPurge;
        }

        /// <summary>
        /// Stock CODE_ABONNÉ
        /// </summary>
        /// <returns>Les CODE_ABONNÉ</returns>
        private List<int> NbAbonne()
        {
            List<int> nb = new List<int>();

            try
            {
                string nombre = "SELECT CODE_ABONNÉ FROM ABONNÉS";
                OleDbCommand command = new OleDbCommand(nombre, DbConnection);
                //DbConnection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nb.Add(reader.GetInt32(0));
                }
                reader.Close();
                //DbConnection.Close();
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }

            return nb;
        }

        #endregion
        #region Liste des report des emprûnts

        /// <summary>
        /// Liste de tous les albums repoussés.
        /// <returns>Une liste contenant tous les ID des albums repoussés</returns>
        /// </summary>
        public Dictionary<int, int> LoanPostponed(string paginationQuery = "")
        {
            Dictionary<int, int> postponedAlbums = new Dictionary<int, int>();

            try
            {
                string getLoansQuery = GetLoanPostponedQuery();
                if (paginationQuery.Length > 0)
                {
                    getLoansQuery = paginationQuery;
                }

                // Préparation de la requête
                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, DbConnection);
                getLoansCommand.Prepare();

                // Récupération des albums
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    if (DateUtils.HasBeenPostpone(loansReader.GetInt32(1), loansReader.GetInt32(0)))
                    {
                        if (!postponedAlbums.ContainsKey(loansReader.GetInt32(0)))
                            postponedAlbums.Add(loansReader.GetInt32(0), loansReader.GetInt32(1));
                    }
                }
                loansReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }

            return postponedAlbums;
        }

        /// <summary>
        /// Obtient la requête qui liste les emprunts en retard
        /// </summary>
        /// <returns>La requête qui liste les emprunts en retard</returns>
        public string GetLoanPostponedQuery()
        {
            return "SELECT CODE_ALBUM, CODE_ABONNÉ FROM EMPRUNTER " +
                   "WHERE DATE_RETOUR IS NULL ORDER BY DATE_EMPRUNT DESC";
        }

        #endregion
        #region Liste des albums les plus emprûntés

        /// <summary>
        /// Obtient le nombre désiré d'albums les plus empruntés de l'année donnée.
        /// </summary>
        /// <param name="numberOfAlbumsToList">Le nombre d'albums les plus emprûnter que l'on veut afficher</param>
        /// <param name="year">L'année dont on veut obtenir les albums les plus emprûntés</param>
        /// <returns>Une liste contentant les codes des albums les plus empruntés de l'année donnée</returns>
        /// Alexandre Sparton
        public List<int> ListMostLoanedAlbumsInYear(int numberOfAlbumsToList, int year)
        {
            List<int> mostLoanedAlbums = new List<int>();

            // Contient le nombre d'emprûnts par albums pour l'année [year]
            Dictionary<int, int> loanedAlbumsNumber = GetNbLoanedPerAlbumInYear(year);

            // Réduit le nombre d'albums à afficher si le nombre n'est pas suffisant pour l'année donnée
            if (loanedAlbumsNumber.Count < numberOfAlbumsToList)
                numberOfAlbumsToList = loanedAlbumsNumber.Count;

            // Affiche les [numberOfAlbumsToList] albums les plus empruntés de l'année [year]
            int counter = 0;

            foreach (KeyValuePair<int, int> albumLoans in loanedAlbumsNumber.OrderByDescending(key => key.Value))
            {
                if (counter == numberOfAlbumsToList)
                {
                    break;
                }
               
                mostLoanedAlbums.Add(albumLoans.Key);
                counter++;
            }

            return mostLoanedAlbums;
        }

        /// <summary>
        /// Determine le nombre d'emprpûnts par albums dans l'année donnée.
        /// </summary>
        /// <param name="year">L'année en question</param>
        /// <returns>le nombre d'emprpûnts par albums dans l'année donnée</returns>
        /// Alexandre Sparton
        public Dictionary<int, int> GetNbLoanedPerAlbumInYear(int year)
        {

            // Contient le nombre d'emprûnts par albums
            Dictionary<int, int> loanedAlbumsNumber = new Dictionary<int, int>();

            try
            {
                // Parcour des emprunts
                string getLoansQuery = "SELECT EMPRUNTER.CODE_ALBUM, DATE_EMPRUNT " +
                                       "FROM EMPRUNTER";
                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, DbConnection);
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    if (loansReader.GetDateTime(1).Year == year)
                    {
                        // On augmente le nombre d'emprûnts à chaque fois qu'on retrouve le même ID
                        if (loanedAlbumsNumber.ContainsKey(loansReader.GetInt32(0)))
                            loanedAlbumsNumber[loansReader.GetInt32(0)]++;
                        else
                            loanedAlbumsNumber.Add(loansReader.GetInt32(0), 1);
                    }
                }
                loansReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }

            return loanedAlbumsNumber;
        }

        #endregion
        #region Liste des emprûnts en retard

        /// <summary>
        /// Retourne la liste des ID des abonnés avec un retard de 10 jours sur un des albums
        /// </summary>
        /// <returns> La liste d'ID des abonnés</returns>
        /// Ethan Krakowski
        public List<int> ListSubscribersWithLateLoans()
        {
            List<int> subscribersWithLateLoans = new List<int>();

            try
            {
                // Préparation de la requête de parcours des emprunts de l'abonné
                string getSubscriberLoansQuery = GetSubscribersWithLateLoansQuery();

                OleDbCommand getSubscriberLoansCommand = new OleDbCommand(getSubscriberLoansQuery, DbConnection);
                getSubscriberLoansCommand.Prepare();

                // Parcours des emprunts de l'abonné
                OleDbDataReader subscriberLoansReader = getSubscriberLoansCommand.ExecuteReader();
                while (subscriberLoansReader.Read())
                {
                    if (DateUtils.IsLate(subscriberLoansReader.GetDateTime(1).ToString()))
                    {
                        subscribersWithLateLoans.Add(subscriberLoansReader.GetInt32(0));
                    }
                }
                subscriberLoansReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return subscribersWithLateLoans;
        }

        public string GetSubscribersWithLateLoansQuery()
        {
            return "SELECT CODE_ABONNÉ, DATE_RETOUR_ATTENDUE FROM EMPRUNTER WHERE DATE_RETOUR IS NULL";
        }

        #endregion
        #region Liste des Abonnés
        /// <summary>
        /// Permet d'afficher la liste des abonnés, avec leur prénom, leur nom, leur login et leur pays.
        /// </summary>
        public List<string> ListSubs(string paginationQuery)
        {
            List<string> result = new List<string>();
            try
            {
                //Permet de récupérer les informations surles abonnés.
                OleDbCommand command = new OleDbCommand(paginationQuery, DbConnection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader.GetString(0).Trim() + " " + reader.GetString(1).Trim() + " " + reader.GetString(2).Trim() + " " + reader.GetString(3).Trim());
                }
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }
            return result;
        }

        public string GetListSubsQuery()
        {
            return "SELECT ABONNÉS.NOM_ABONNÉ, ABONNÉS.PRÉNOM_ABONNÉ, ABONNÉS.LOGIN_ABONNÉ, PAYS.NOM_PAYS " +
                   "FROM ABONNÉS " +
                   "INNER JOIN PAYS ON ABONNÉS.CODE_PAYS = PAYS.CODE_PAYS " +
                   "ORDER BY NOM_ABONNÉ ASC";
        }

        #endregion
        #region Liste des Albums par leur numéro de casier
        /// <summary>
        /// Permet de lister les album empruntés à partir de leur numéro de casier.
        /// </summary>
        /// <param name="numLocker">Numéro du casier</param>
        /// <returns>La liste des album obtenu</returns>
        public List<Album> listAlbumLoanedByLocker(int numLocker)
        {
            List<Album> result = new List<Album>();
            string albumsByLockerQuery = "select ALBUMS.CODE_ALBUM from EMPRUNTER " +
                         "inner join ALBUMS on EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM " +
                         "where ALBUMS.CASIER_ALBUM = " + numLocker + "AND EMPRUNTER.DATE_RETOUR is null";
            OleDbCommand command = new OleDbCommand(albumsByLockerQuery, DbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Album(reader.GetInt32(0), DbConnection));
            }
            return result;
        }
        #endregion
    }

}
