using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik
{
    public static class Login
    {
        private static readonly int ADMIN_ID = 45;

        /// <summary>
        /// Obtient le code de l'utilisateur si la connexion est valide, et détermine s'il s'agit de l'admin ou non.
        /// </summary>
        /// <param name="user">Le login de l'utilisateur</param>
        /// <param name="password">Le mot de passe de l'utilisateur</param>
        /// <returns>Une pair de valeur contenant le code du connecté, et s'il s'agit de l'administrateur ou non</returns>
        /// Guillaume Froidcourt & Alexandre Sparton
        public static KeyValuePair<int, bool> GetUserLogin(string user, string password)
        {
            MD5 saltForHash = MD5.Create(); // Premet de hacher un mot de passe
            OleDbConnection dbConnection = new OleDbConnection(
                ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            KeyValuePair<int, bool> loginResult = new KeyValuePair<int, bool>(-1, false);

            try
            {
                // Algorithme de hachage du mot de passe afin qu'il n'apparaisse pas en clair dans la base de donnée.
                byte[] salting = saltForHash.ComputeHash(new UTF8Encoding().GetBytes(password));
                string hashedPassword = BitConverter.ToString(salting).Replace("-", string.Empty);

                // Préparation de la requête
                string getUserCodeQuery = "SELECT CODE_ABONNÉ FROM ABONNÉS " +
                             "WHERE LOGIN_ABONNÉ = '" + user + "' and PASSWORD_ABONNÉ = '" + hashedPassword + "'";
                OleDbCommand getUserCodeCommand = new OleDbCommand(getUserCodeQuery, dbConnection);
                dbConnection.Open();

                // Récupération du code de l'utilisateur
                OleDbDataReader reader = getUserCodeCommand.ExecuteReader();

                if (reader.Read())
                {
                    int userId = reader.GetInt32(0);

                    //  S'il s'agit de l'ID d'un abonné autre que l'admin
                    if (reader.HasRows && userId != ADMIN_ID)
                    {
                        loginResult = new KeyValuePair<int, bool>(userId, false);
                    }
                    // S'il s'agit de l'ID de l'admin (45)
                    else if (reader.HasRows && userId == ADMIN_ID)
                    {
                        loginResult = new KeyValuePair<int, bool>(userId, true);
                    }
                }

                reader.Close();
                dbConnection.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return loginResult;
        }
    }
}
