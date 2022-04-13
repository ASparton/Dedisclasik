using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik
{
    /// <summary>
    /// Caractérise un emprunt avec un album et un abonné.
    /// </summary>
    public class Loan
    {
        #region Propriétés

        public Subscriber Subscriber { get; private set; }

        public Album Album { get; private set; }

        #endregion

        /// <summary>
        /// Garde les informations d'un emprunt
        /// </summary>
        /// <param name="subscriber">L'abonné ayant réalisé l'emprunt</param>
        /// <param name="album">L'album emprunté</param>
       public Loan(Subscriber subscriber, Album album)
        {
            Subscriber = subscriber;
            Album = album;
        }

        /// <summary>
        /// Retourne le titre de l'album emprunté puis le nom et prénom de l'abonné.
        /// </summary>
        /// <returns>Le titre de l'album emprunté puis le nom et prénom de l'abonné</returns>
        public override string ToString()
        {
            return Album.Title + " " + Subscriber.ToString();
        }

        /// <summary>
        /// Retourn les informations d'un abonné à l'aide d'un id
        /// </summary>
        /// <param name="id">l'id de l'abonné</param>
        /// <returns> une string avec le nom et le prénom</returns>
        public static string getSubFromID(int id)
        {
            string sub = "";
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            string sql = "select * from ABONNÉS where CODE_ABONNÉ = '" + id + "'";
            OleDbCommand getLoansCommand = new OleDbCommand(sql, dbConnection);
            dbConnection.Open();
            OleDbDataReader reader = getLoansCommand.ExecuteReader();
            while (reader.Read())
            {
                sub = reader[2].ToString().Trim() + " " + reader[3].ToString();
            }
            reader.Close();
            dbConnection.Close();
            return sub;
        }
    }
}
