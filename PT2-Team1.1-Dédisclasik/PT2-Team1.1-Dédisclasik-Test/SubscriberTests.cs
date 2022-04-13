using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik.Tests
{
    [TestClass()]
    public class SubscriberTests
    {
        /// <summary>
        /// Tests du report d'un album.
        /// </summary>
        [TestMethod()]
        public void ExtendLoanTest()
        {
            // Préparation des tests
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            Subscriber subscriber;
            int subscriberID, albumID;
            DateTime expectedDate, returnDate;

            // Test report d'un emprunt n'ayant pas été rendu
            dbConnection.Open();
            subscriberID = 285;
            albumID = 100;
            subscriber= new Subscriber(subscriberID, dbConnection);
            subscriber.LoanAlbum(new Album(albumID, dbConnection));
            expectedDate = TestUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection).AddMonths(1);
            subscriber.ExtendLoan(albumID);
            returnDate = TestUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            Assert.AreEqual(expectedDate, returnDate); 
            dbConnection.Close();

            // Test report d'un emprunt déjà reporté
            dbConnection.Open();
            subscriber.ExtendLoan(albumID);
            returnDate = TestUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            Assert.AreEqual(expectedDate, returnDate);
            subscriber.ReturnLoan(albumID);
            dbConnection.Close();

            // Test report d'un emprunt déjà rendu
            dbConnection.Open();
            subscriberID = 39;
            albumID = 54;
            subscriber = new Subscriber(subscriberID, dbConnection);
            expectedDate = TestUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            subscriber.ExtendLoan(albumID);
            returnDate = TestUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            Assert.AreEqual(expectedDate, returnDate);
            subscriber.ReturnLoan(54);

            // Test sur un client qui n'a pas de location en cours
            Subscriber sub1 = new Subscriber(47, dbConnection);
            sub1.ListLoans();
            Assert.AreEqual(0, sub1.NbAlbums);

            dbConnection.Close();
        }

        /// <summary>
        /// Tests du report de tous les albums d'un abonné.
        /// </summary>
        [TestMethod()]
        public void ExtendAllLoansTest()
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            Subscriber subscriber = new Subscriber(292, dbConnection);

            // On emprunte des album.
            subscriber.LoanAlbum(new Album(575, dbConnection));
            subscriber.LoanAlbum(new Album(605, dbConnection));

            // On vérifie que leur date d'emprunt de soit pas repousser.
            Assert.IsFalse(DateUtils.HasBeenPostpone(292, 575)); //problème
            Assert.IsFalse(DateUtils.HasBeenPostpone(292, 605)); //problème

            // On applique la fonction qu'on veut tester.
            subscriber.ExtendAllLoans();

            // On vérifie alors si la date de retour attendue à été repoussée d'un mois.
            Assert.IsTrue(DateUtils.HasBeenPostpone(292, 575));
            Assert.IsTrue(DateUtils.HasBeenPostpone(292, 605));

            // On rend les emprunts de test
            subscriber.ReturnLoan(575);
            subscriber.ReturnLoan(605);
        }

        /// <summary>
        /// Tests suggestions
        /// </summary>
        [TestMethod()]
        public void ResearchByPreferedTypeTest()
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            Subscriber subscriber = new Subscriber(47, dbConnection);
            Dictionary<int,string> albums = subscriber.ResearchByPreferedType();

            foreach (int code in albums.Keys)
            {
                string typeQuery = "SELECT GENRES.CODE_GENRE from ALBUMS " +
                         "INNER JOIN GENRES on ALBUMS.CODE_GENRE = GENRES.CODE_GENRE " +
                         "WHERE ALBUMS.CODE_ALBUM = '" + code;

                OleDbCommand getIdGenres = new OleDbCommand(typeQuery, dbConnection);
                OleDbDataReader reader = getIdGenres.ExecuteReader();
                int idType = 0;
                while (reader.Read())
                {
                    idType = reader.GetInt32(0);
                }
                Assert.AreEqual(3, idType);
            }
            dbConnection.Close();
        }

        /// <summary>
        /// Tests rendu d'un emprunt.
        /// </summary>
        [TestMethod()]
        public void ReturnLoanTest()
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            // Test si un abonné a bien rendu un album
            Subscriber subscriber = new Subscriber(39, dbConnection);
            subscriber.ReturnLoan(90);
            string sql = "SELECT DATE_RETOUR from EMPRUNTER WHERE CODE_ABONNÉ = " + subscriber.ID + " AND CODE_ALBUM = " + 90;
            OleDbCommand cmd = new OleDbCommand(sql, dbConnection);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Si l'abonné a rendu un album, la date de retour n'est pas NULL
                Assert.IsFalse(reader.IsDBNull(0));
            }
            reader.Close();

            Subscriber sub2 = new Subscriber(412, dbConnection);
            sql = "SELECT DATE_RETOUR from EMPRUNTER WHERE CODE_ABONNÉ = " + sub2.ID + " AND CODE_ALBUM = " + 5;
            OleDbCommand cmd2 = new OleDbCommand(sql, dbConnection);
            OleDbDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                // Si l'abonné n'a pas rendu d'album, la date de retour est encore NULL.
                Assert.IsTrue(reader2.IsDBNull(0));
            }

            reader2.Close();

            dbConnection.Close();
        }

    }
}