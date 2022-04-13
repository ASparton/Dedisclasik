using System;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace PT2_Team1._1_Dédisclasik
{
    public static class Register
    {
        /// <summary>
        /// Ajoute un abonné dans la base
        /// </summary>
        /// <param name="codePays">le code du pays</param>
        /// <param name="nom">le nom de l'abonné</param>
        /// <param name="prenom">le prénom de l'abonné</param>
        /// <param name="login">le login de l'abonné</param>
        /// <param name="password">le mot de passe de l'abonné</param>
        public static void RegisterSubscriber(int codePays, string nom, string prenom, string login, string password)
        {
            MD5 saltForHash = MD5.Create(); // Premet de hacher un mot de passe
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();

            try
            {
                bool exist = false;

                string getSubscriberLoginQuery = "SELECT LOGIN_ABONNÉ FROM ABONNÉS WHERE LOGIN_ABONNÉ = '" + login + "'";
                OleDbCommand getSubscriberLoginCommand = new OleDbCommand(getSubscriberLoginQuery, dbConnection);
                OleDbDataReader reader = getSubscriberLoginCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        exist = true;
                        MessageBox.Show("Le login existe déjà", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dbConnection.Close();

                if (!exist)
                {
                    string insertQuery = "INSERT INTO ABONNÉS (" + (codePays != -1 ? "[CODE_PAYS]," : "")  + 
                                         "[NOM_ABONNÉ],[PRÉNOM_ABONNÉ],[LOGIN_ABONNÉ],[PASSWORD_ABONNÉ]) " +
                                         "VALUES (" + (codePays != -1 ? "?," : "") + "?,?,?,?)";
                    OleDbCommand insertCommand = new OleDbCommand(insertQuery, dbConnection);
                    if (codePays != -1)
                    {
                        insertCommand.Parameters.AddWithValue("@CODE_PAYS", codePays);
                    }
                    insertCommand.Parameters.AddWithValue("@NOM_ABONNÉ", nom);
                    insertCommand.Parameters.AddWithValue("@PRÉNOM_ABONNÉ", prenom);
                    insertCommand.Parameters.AddWithValue("@LOGIN_ABONNÉ", login);

                    // Algorithme de hachage du mot de passe afin qu'il n'apparaisse pas en clair dans la base de donnée.
                    var salting = saltForHash.ComputeHash(new UTF8Encoding().GetBytes(password));
                    string hash = BitConverter.ToString(salting).Replace("-", string.Empty);
                    insertCommand.Parameters.AddWithValue("@PASSWORD_ABONNÉ", hash);
                    dbConnection.Open();
                    insertCommand.ExecuteNonQuery();
                    dbConnection.Close();

                    MessageBox.Show("Vous avez bien été ajotué à la base.");
                }
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine("Erreur dans la création de la ligne" + exception.ToString());
            }
        }

        /// <summary>
        /// Méthode permettant de récupérer le code d'un pays en fonction de son nom.
        /// </summary>
        /// <returns> Le code pays associé au nom du pays</returns>
        public static int GetCountryCode(String CountryName)
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

            try
            {
                string sql = "SELECT CODE_PAYS from PAYS where NOM_PAYS = '" + CountryName + "'";
                OleDbCommand getSubscriberCommand = new OleDbCommand(sql, dbConnection);
                getSubscriberCommand.Connection.Open();
                OleDbDataReader reader = getSubscriberCommand.ExecuteReader();
                reader.Read();
                int countryID = (int)reader[0];
                reader.Close();
                getSubscriberCommand.Connection.Close();
                return countryID;
            }
            catch (OleDbException exception)
            {
                Console.WriteLine(exception);
                return -1;
            }
        }
    }
}
