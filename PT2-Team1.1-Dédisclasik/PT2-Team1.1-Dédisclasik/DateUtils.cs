using System;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik
{
    /// <summary>
    /// Offre plusieurs services pour gérer les dates et avoir des informations impliquant des dates de la base.
    /// </summary>
    /// Gregory Simon & Alexandre Sparton & Ethan Krakowski
    public static class DateUtils
    {
        public static readonly char DATE_SEPARATOR = '/';

        /// <summary>
        /// Converti une date en nombre de jour.
        /// </summary>
        /// <param name="date">La date en chaîne de caractères</param>
        /// <returns>Le nombre de jour associé à la date</returns>
        /// Gregory Simon & Alexandre Sparton
        public static int ConvertDateInDays(string date)
        {
            string dateYMD = date.Split(' ')[0];
            int day = int.Parse(dateYMD.Split(DATE_SEPARATOR)[0]);
            int month = int.Parse(dateYMD.Split(DATE_SEPARATOR)[1]) * 30;
            int year = int.Parse(dateYMD.Split(DATE_SEPARATOR)[2]) * 365;

            return day + month + year;
        }

        /// <summary>
        /// Construit une date en chaîne de caractère à partir des nombres donnés (année, mois, jour)
        /// </summary>
        /// <param name="year">Le numéro de l'année</param>
        /// <param name="month">Le numéro du mois</param>
        /// <param name="day">Le numéro du jour</param>
        /// <returns>Une date en chaîne de caractère à partir des nombres donnés (année, mois, jour)</returns>
        /// Gregory Simon & Alexandre Sparton
        public static string ConstructDate(int year, int month, int day)
        {
            return year.ToString() + '-'
                + day.ToString() + '-'
                + month.ToString();
        }

        /// <summary>
        /// Construit une date en chaîne de caractère à partir des nombres donnés (année, mois, jour)
        /// </summary>
        /// <param name="dateToConstruct">La date à construire</param>
        /// <returns>Une date en chaîne de caractère à partir des nombres donnés (année, mois, jour)</returns>
        /// Gregory Simon & Alexandre Sparton
        public static string ConstructDate(DateTime dateToConstruct)
        {
            return dateToConstruct.Year.ToString() + '-'
                + dateToConstruct.Day.ToString() + '-'
                + dateToConstruct.Month.ToString();
        }

        /// <summary>
        /// Vérifie si la date de retour de l'emprunt d'un album donné par son code a déjà été repoussée.
        /// </summary>
        /// <param name="idAbo">L'id de l'abonnée</param>
        /// <param name="idAlbum">Le code de l'album</param>
        /// <returns>true si la date de retour a été repoussée, false sinon.</returns>
        /// Gregory Simon & Alexandre Sparton
        public static bool HasBeenPostpone(int idAbo, int idAlbum)
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();

            try
            {
                int loanDate = 0, expectedReturnDate = 0, delay = 0;
                // Récupération des dates
                string getLoanQuery = "SELECT Date_Emprunt, Date_Retour_Attendue FROM Emprunter" +
                                     " INNER JOIN Albums on EMPRUNTER.Code_Album = ALBUMS.Code_Album" +
                                     " WHERE EMPRUNTER.Code_Abonné = " + idAbo +
                                     " AND EMPRUNTER.Code_Album = " + idAlbum;

                OleDbCommand getLoan = new OleDbCommand(getLoanQuery, dbConnection);
                OleDbDataReader loanReader = getLoan.ExecuteReader();
                while (loanReader.Read())
                {
                    loanDate = ConvertDateInDays(loanReader.GetDateTime(0).ToString());
                    expectedReturnDate = ConvertDateInDays(loanReader.GetDateTime(1).ToString());
                }
                loanReader.Close();

                // Récupération du délai
                delay = Album.GetAlbumDelay(idAlbum, dbConnection);

                dbConnection.Close();
                return expectedReturnDate - loanDate > delay;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        /// <summary>
        /// Vérifie si une date est en retard de 10 jours ou plus
        /// </summary>
        /// <param name="date"> La date </param>
        /// <returns> un booléen vrai si la date est en retard de 10 jours, faux sinon</returns>
        public static bool IsLate(String date)
        {
            DateTime actualDate = DateTime.Now.AddDays(-10);
            int ms = date.LastIndexOf('.');
            if (ms >= 0)
            {
                date = date.Substring(ms);
            }
            if (DateTime.Compare(actualDate, DateTime.Parse(date)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// Determine wether a given date is at least one year older from today.
        /// </summary>
        /// <param name="date">the date</param>
        /// <returns>true if the date is at least one year older, false otherwise</returns>
        /// Alexandre Sparton
        public static bool AtLeastMoreThanAYearAgo(DateTime date)
        {
            return DateTime.Today.AddYears(-1) > date;
        }
    }
}
