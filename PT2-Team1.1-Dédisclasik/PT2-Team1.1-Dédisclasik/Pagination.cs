using System;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik
{
    public class Pagination
    {
        #region Propriétés

        private string Query { get; set; }
        private int NbElementsToDisplay { get; set; }
        public int CurrentPage { get; private set; }
        public int LastPage { get; private set; }
        public bool IsAtLastPage { get; set; }

        #endregion

        /// <summary>
        /// Créer une instance de pagination pouvant éxécuter la requête donnée au fur et à mesure.
        /// </summary>
        /// <param name="query">La chaine de caractère contenant la requête</param>
        /// <param name="nbElementsToDisplay">Le nombre d'éléments par pages</param>
        /// <param name="dbConnection">La connexion vers la base de données</param>
        public Pagination(string query, int nbElementsToDisplay, OleDbConnection dbConnection)
        {
            Query = QueryWithOrderBy(query);
            NbElementsToDisplay = nbElementsToDisplay;
            CurrentPage = 0;
            LastPage = GetLastPageNumber(dbConnection);
            IsAtLastPage = false;
        }

        /// <summary>
        /// Créer la commande sql permettant d'obtenir la requête à la page voulue.
        /// </summary>
        /// <param name="page">Le numéro de page dont on veut le résultat (optionnel sinon page courrante)</param>
        /// <returns>La requête à la page voulue</returns>
        public string MakePaginationQuery(int page = -1)
        {
            return Query + " OFFSET " + (page == -1 ? CurrentPage : page) * NbElementsToDisplay + " ROWS FETCH NEXT " + NbElementsToDisplay + " ROWS ONLY";
        }

        /// <summary>
        /// Augmente le numéro de la page qui sera affichée.
        /// </summary>
        public void SetNextPage()
        {
            if (CurrentPage < LastPage - 1)
                CurrentPage++;
        }

        /// <summary>
        /// Réduit le numéro de la page qui sera affichée.
        /// </summary>
        public void SetPreviousPage()
        {
            if (CurrentPage >= 1)
                CurrentPage--;
        }

        /// <summary>
        /// Rajoute un order by null si la requête n'en contient pas
        /// </summary>
        /// <param name="query">La requête</param>
        /// <returns>La requête ajouté d'un order by (select null) si elle ne contient pas d'order by</returns>
        private string QueryWithOrderBy(string query)
        {
            return query + (query.ToUpper().Contains("ORDER BY") ? "" : " ORDER BY (SELECT NULL)");
        }

        /// <summary>
        /// Calcule et retourne le numéro de la dernière page en fonction de la requête et du nombre d'éléments à retenir par requête.
        /// </summary>
        /// <returns>Le numéro de la dernière page de la requête</returns>
        private int GetLastPageNumber(OleDbConnection dbConnection)
        {
            int lastPageNumber = -1;

            try
            {
                // Préparation de la requête
                string[] fromSeperator = { "FROM" };
                string[] orderBySeparator = { "ORDER BY" };
                string getCountQuery = Query.Split(' ')[0] + " COUNT(" + Query.Split(' ')[1].Replace(",", "") + ") FROM" 
                    + Query.Split(fromSeperator, StringSplitOptions.None)[1].Split(orderBySeparator, StringSplitOptions.None)[0];
                OleDbCommand getCountCommand = new OleDbCommand(getCountQuery, dbConnection);
                getCountCommand.Prepare();

                // Récupération de la somme
                OleDbDataReader countReader = getCountCommand.ExecuteReader();
                while (countReader.Read())
                {
                    int sum = countReader.GetInt32(0); ;
                    if (sum % NbElementsToDisplay > 0)
                    {
                        lastPageNumber = sum / NbElementsToDisplay + 1;
                    }
                    else
                    {
                        lastPageNumber = sum / NbElementsToDisplay;
                    }
                }
                countReader.Close();
            }
            catch (OleDbException exception)
            {
                Console.Error.WriteLine(exception);
            }

            return lastPageNumber;
        }
    }
}
