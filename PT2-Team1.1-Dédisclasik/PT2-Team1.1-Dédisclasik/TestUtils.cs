using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik
{
    /// <summary>
    /// Contient des méthodes utiles pour les tests
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Insert un album emprunté il y a plus d'un an dans la base.
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de donnée</param>
        /// <param name="subscriberID">Le code de l'abonné qui va emprunter</param>
        /// <param name="albumToLoan">Le code de l'album à emprunter</param>
        /// Thomas Ocelli & Alexandre Sparton
        public static void InsertAlbumNotLoanedSince1Year(OleDbConnection dbConnection, int subscriberID, int albumToLoan)
        {
            try
            {
                // Préparation de la requête
                string insertQuery = "INSERT INTO EMPRUNTER " +
                                     "([CODE_ABONNÉ], [CODE_ALBUM], [DATE_EMPRUNT], [DATE_RETOUR_ATTENDUE], [DATE_RETOUR]) " +
                                     "VALUES (?,?,?,?,?)";
                OleDbCommand insertCommand = new OleDbCommand(insertQuery, dbConnection);
                DateTime loanDate = new DateTime(2000, 6, 1), expectedReturnDate = loanDate.AddDays(Album.GetAlbumDelay(albumToLoan, dbConnection));
                insertCommand.Parameters.Add("@CODE_ABONNÉ", OleDbType.Integer);
                insertCommand.Parameters.Add("@CODE_ALBUM", OleDbType.Integer);
                insertCommand.Parameters.Add("@DATE_EMPRUNT", OleDbType.Date);
                insertCommand.Parameters.Add("@DATE_RETOUR_ATTENDUE", OleDbType.Date);
                insertCommand.Parameters.Add("@DATE_RETOUR", OleDbType.Date);

                // Ajout des valeurs désirées
                insertCommand.Parameters[0].Value = subscriberID;
                insertCommand.Parameters[1].Value = albumToLoan;
                insertCommand.Parameters[2].Value = loanDate;
                insertCommand.Parameters[3].Value = expectedReturnDate;
                insertCommand.Parameters[4].Value = expectedReturnDate;
                insertCommand.Prepare();

                // Exécution de la requête
                insertCommand.ExecuteNonQuery();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }
        }

        #region Utils abonnés

        /// <summary>
        /// Ajoute abonné "killPurg" dans la base.
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <returns>L'ID de l'utilisateur crée</returns>
        public static int AddSubscriber(OleDbConnection dbConnection)
        {
            int code = -1;

            try
            {
                string createUser = "INSERT INTO ABONNÉS(NOM_ABONNÉ,PRÉNOM_ABONNÉ,LOGIN_ABONNÉ,PASSWORD_ABONNÉ)"
                        + "VALUES('killPurge', 'killPurge', 'killPurge', 'killPurge')";
                string selectUser = "SELECT MAX(CODE_ABONNÉ) FROM ABONNÉS";


                OleDbCommand createCommand = new OleDbCommand(createUser, dbConnection);
                OleDbCommand command = new OleDbCommand(selectUser, dbConnection);
                createCommand.Prepare();
                createCommand.ExecuteNonQuery();

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    code = reader.GetInt32(0);
                }

            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }
        
            return code;
        }

        /// <summary>
        /// Supprime l'abonné correspondant au login donné de la base et ses emprunts.
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <param name="subscriberLogin">Le login de l'abonné à supprimer</param>
        public static void DeleteSubscriberAndHisLoans(OleDbConnection dbConnection, string subscriberLogin)
        {
            try
            {
                // Loans deletion
                string deleteQuery = "DELETE FROM EMPRUNTER WHERE EMPRUNTER.LOGIN_ABONNÉ = " + subscriberLogin;
                OleDbCommand deleteCommand = new OleDbCommand(deleteQuery, dbConnection);
                deleteCommand.Prepare();
                deleteCommand.ExecuteNonQuery();

                // Subscriber deletion
                deleteQuery = "DELETE FROM ABONNÉS WHERE LOGIN_ABONNÉ = " + subscriberLogin;
                deleteCommand = new OleDbCommand(deleteQuery, dbConnection);
                deleteCommand.Prepare();
                deleteCommand.ExecuteNonQuery();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }
        }

        /// <summary>
        /// Détermine si l'abonné avec le code donné existe dans la base.
        /// </summary>
        /// <param name="subscriberID">Le code de l'abonné à rhercher</param>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <returns>True si l'abonné existe, false sinon</returns>
        public static bool ResearshSubscriber(int subscriberID, OleDbConnection dbConnection)
        {
            bool exists = false;

            try
            {
                // Préparation de la requête
                string searchSubscriberQuery = "SELECT CODE_ABONNÉ " +
                         "FROM ABONNÉS WHERE CODE_ABONNÉ=" + subscriberID;
                OleDbCommand searchSubscriberCommand = new OleDbCommand(searchSubscriberQuery, dbConnection);
                searchSubscriberCommand.Prepare();

                // Obtention ou non d'un résultat
                OleDbDataReader reader = searchSubscriberCommand.ExecuteReader();
                if (reader.Read())
                {
                    exists = true;
                }
                reader.Close();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return exists;
        }

        #endregion
        #region Utils emprunts

        /// <summary>
        /// Crée un emprunt de l'utisateur donné.
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <param name="ID">Le code de l'utisateur</param>
        public static void CreateLoan(OleDbConnection dbConnection, int ID)
        {
            try
            {
                string createLoan = "INSERT INTO EMPRUNTER(CODE_ABONNÉ, CODE_ALBUM, DATE_EMPRUNT, DATE_RETOUR_ATTENDUE, DATE_RETOUR) " +
                "VALUES(" + ID + ", 699, '2019-09-09', '2019-12-12', '2019-12-12')";
                OleDbCommand createCommand = new OleDbCommand(createLoan, dbConnection);
                createCommand.Prepare();
                createCommand.ExecuteNonQuery();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }
        }

        /// <summary>
        /// Supprime un emprunt dans la base.
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        /// <param name="subscriberID">Le code de l'abonné</param>
        /// <param name="albumID">Le code de l'album</param>
        /// Thomas Ocelli & Alexandre Sparton
        public static void DeleteLoan(OleDbConnection dbConnection, int subscriberID, int albumID)
        {
            try
            {
                string deleteQuery = "DELETE FROM EMPRUNTER " +
                                     "WHERE CODE_ABONNÉ = " + subscriberID + " AND CODE_ALBUM = " + albumID;
                OleDbCommand deleteCommand = new OleDbCommand(deleteQuery, dbConnection);
                deleteCommand.Prepare();
                deleteCommand.ExecuteNonQuery();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }
        }

        /// <summary>
        /// Retourne la date de retour expecté d'un emprûnt à partir du code de l'album et l'abonné donné.
        /// </summary>
        /// <param name="subscriberID">Le code de l'abonné</param>
        /// <param name="albumID">Le code de l'album</param>
        /// <returns>La date de retour expected</returns>
        public static DateTime GetLoanExcpectedReturnDate(int subscriberID, int albumID, OleDbConnection dbConnection)
        {
            try
            {
                // Préparation de la requête
                string getReturnDateQuery = "SELECT DATE_RETOUR_ATTENDUE FROM EMPRUNTER " +
                                            "WHERE EMPRUNTER.CODE_ABONNÉ = " + subscriberID +
                                            " AND EMPRUNTER.CODE_ALBUM = " + albumID;
                OleDbCommand getReturnDateCommand = new OleDbCommand(getReturnDateQuery, dbConnection);
                getReturnDateCommand.Prepare();

                // Récupération de la date
                OleDbDataReader getReturnDateReader = getReturnDateCommand.ExecuteReader();
                getReturnDateReader.Read();
                DateTime expectedReturnDate = getReturnDateReader.GetDateTime(0);
                getReturnDateReader.Close();
                return expectedReturnDate;
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
                return new DateTime(1, 1, 1);
            }
        }

        /// <summary>
        /// Obtient une liste contentant le code des albums empruntés cet année.
        /// </summary>
        /// <param name="dbConnection">La connexion vers la base de donnée</param>
        /// <returns>Un HashSet contentant le code des albums empruntés cet année.</returns>
        public static HashSet<int> GetAlbumLoanedDuringThisYear(OleDbConnection dbConnection)
        {
            HashSet<int> loanedThisYear = new HashSet<int>();

            try
            {
                // Parcours des emprûnts
                string getLoansQuery = "SELECT EMPRUNTER.CODE_ALBUM, EMPRUNTER.DATE_RETOUR " +
                                       "FROM EMPRUNTER INNER JOIN ALBUMS ON EMPRUNTER.CODE_ALBUM = ALBUMS.CODE_ALBUM";
                OleDbCommand getLoansCommand = new OleDbCommand(getLoansQuery, dbConnection);
                OleDbDataReader loansReader = getLoansCommand.ExecuteReader();
                while (loansReader.Read())
                {
                    // Si l'emprûnt date de plus d'un an, on l'ajoute
                    if (loansReader.IsDBNull(1) ||
                        !DateUtils.AtLeastMoreThanAYearAgo(loansReader.GetDateTime(1)))
                    {
                        loanedThisYear.Add(loansReader.GetInt32(0));
                    }
                }
                loansReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
            }

            return loanedThisYear;
        }

        #endregion
    }
}
