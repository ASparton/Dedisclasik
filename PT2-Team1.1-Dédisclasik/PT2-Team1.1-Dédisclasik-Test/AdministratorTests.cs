using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik.Tests
{
    [TestClass()]
    public class AdministratorTests
    {
        /// <summary>
        /// Tests du listage des emprunts repoussés par abonné.
        /// </summary>
        [TestMethod()]
        public void LoanPostponedTest()
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            Dictionary<int, int> result;

            Subscriber subscriber = new Subscriber(59, dbConnection);
            //On ajoute deux emprunts ou l'on va étendre la date de retour.
            subscriber.LoanAlbum(new Album(77, dbConnection));
            subscriber.LoanAlbum(new Album(78, dbConnection));
            subscriber.ExtendLoan(77);
            subscriber.ExtendLoan(78);
            //On ajoute un emprunt qu'on ne repoussera pas.
            subscriber.LoanAlbum(new Album(79, dbConnection));
            //On test si le repoussement des emprunt se passe bien.
            Assert.IsTrue(DateUtils.HasBeenPostpone(59, 77));
            Assert.IsTrue(DateUtils.HasBeenPostpone(59, 78));
            Assert.IsFalse(DateUtils.HasBeenPostpone(59, 79));

            Administrator admin = new Administrator(dbConnection);
            result = admin.LoanPostponed();
            //Test des deux emprunt repoussés.
            Assert.IsTrue(result.Keys.Contains(77));
            Assert.IsTrue(result.Keys.Contains(78));
            //Test de l'emprunt qui n'a pas été repoussé.
            Assert.IsFalse(result.Keys.Contains(79));
            //On rend les albums empruntés durant le test afin de ne pas trop perturber la base de donné.
            subscriber.ReturnLoan(77);
            subscriber.ReturnLoan(78);
            subscriber.ReturnLoan(79);

            dbConnection.Close();
        }

        /// <summary>
        /// Tests de la purge des abonnés.
        /// </summary>
        [TestMethod()]
        public void PurgeAbonneTest()
        {
            //Préparation tests
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            int code = TestUtils.AddSubscriber(dbConnection);
            TestUtils.CreateLoan(dbConnection, code);

            Administrator administrator = new Administrator(dbConnection);
            Subscriber subscriber = new Subscriber(49, dbConnection);
            Subscriber test1 = new Subscriber(code, dbConnection);
            Subscriber test2 = new Subscriber(278, dbConnection);

            administrator.PurgeAbonne(dbConnection);

            // Test purge 6 mois 
            Assert.AreEqual(true, TestUtils.ResearshSubscriber(subscriber.ID, dbConnection));
            // Test purge 1 ans
            Assert.AreEqual(false, TestUtils.ResearshSubscriber(test1.ID, dbConnection));
            // Test purge 1 ans en retard
            Assert.AreEqual(true, TestUtils.ResearshSubscriber(test2.ID, dbConnection));

            dbConnection.Close();
        }

        /// <summary>
        /// Tests de la liste des albums les plus vendus de l'année.
        /// </summary>
        [TestMethod()]
        public void ListMostLoanedAlbumsInYearTest()
        {
            // Préparation des tests
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            Administrator adminTest = new Administrator(dbConnection);
            dbConnection.Open();
            adminTest.ListMostLoanedAlbumsInYear(10, 2021);
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(2));            
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(48));
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(54));
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(71));
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(72));
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(78));
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(33));
            Assert.IsTrue(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(100));
            Assert.IsFalse(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(-1));
            Assert.IsFalse(adminTest.ListMostLoanedAlbumsInYear(10, 2021).Contains(88));

            dbConnection.Close();
        }

        /// <summary>
        /// Tests de la liste des albums non empruntés depuis plus d'un an.
        /// </summary>
        [TestMethod()]
        public void ListAlbumsNotLoanedSinceOneYearTest()
        {
            // Préparation de la requête
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            Administrator administrator;
            Dictionary<int, DateTime> albumNotLoanedSince1Year;
            HashSet<int> albumLoanedThisYear;

            // Suppression des emprunts
            TestUtils.DeleteLoan(dbConnection, 44, 256);
            TestUtils.DeleteLoan(dbConnection, 44, 255);
            TestUtils.DeleteLoan(dbConnection, 44, 254);
            TestUtils.DeleteLoan(dbConnection, 44, 253);

            // Test avec aucun album emprunté ayant été emprunté il y a un plus d'un an
            administrator = new Administrator(dbConnection);
            albumNotLoanedSince1Year = administrator.ListAlbumsNotLoanedSinceOneYear();
            albumLoanedThisYear = TestUtils.GetAlbumLoanedDuringThisYear(dbConnection);

            foreach (KeyValuePair<int, DateTime> loan in albumNotLoanedSince1Year)
            {
                Assert.IsFalse(albumLoanedThisYear.Contains(loan.Key));
            }

            // Test avec un seul album emprunté il y a plus d'un an (le reste jamais emprunté)
            TestUtils.InsertAlbumNotLoanedSince1Year(dbConnection, 44, 256);
            albumNotLoanedSince1Year = administrator.ListAlbumsNotLoanedSinceOneYear();
            albumLoanedThisYear = TestUtils.GetAlbumLoanedDuringThisYear(dbConnection);

            int isAlbumInside = 0;
            foreach (KeyValuePair<int, DateTime> loan in albumNotLoanedSince1Year)
            {
                Assert.IsFalse(albumLoanedThisYear.Contains(loan.Key));
                if (loan.Value != new DateTime(2, 2, 2))
                    isAlbumInside++;
            }
            Assert.AreEqual(1, isAlbumInside);

            // Test avec plusieurs albums emprunté il y a plus d'un an (le reste jamais emprunté)
            TestUtils.InsertAlbumNotLoanedSince1Year(dbConnection, 44, 253);
            TestUtils.InsertAlbumNotLoanedSince1Year(dbConnection, 44, 255);
            TestUtils.InsertAlbumNotLoanedSince1Year(dbConnection, 44, 254);
            albumNotLoanedSince1Year = administrator.ListAlbumsNotLoanedSinceOneYear();
            albumLoanedThisYear = TestUtils.GetAlbumLoanedDuringThisYear(dbConnection);

            isAlbumInside = 0;
            foreach (KeyValuePair<int, DateTime> loan in albumNotLoanedSince1Year)
            {
                Assert.IsFalse(albumLoanedThisYear.Contains(loan.Key));
                if (loan.Value != new DateTime(2, 2, 2))
                    isAlbumInside++;
            }
            Assert.AreEqual(4, isAlbumInside);

            // Suppression des emprunts
            TestUtils.DeleteLoan(dbConnection, 44, 256);
            TestUtils.DeleteLoan(dbConnection, 44, 255);
            TestUtils.DeleteLoan(dbConnection, 44, 254);
            TestUtils.DeleteLoan(dbConnection, 44, 253);

            dbConnection.Close();
        }

        /// <summary>
        /// Tests de la liste des abonnés ayant un ou plusieurs emprunts en retard.
        /// </summary>
        [TestMethod()]
        public void ListSubscriberLateTest()
        {
            // Préparation des tests
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            Administrator admin = new Administrator(dbConnection);
            Register.RegisterSubscriber(14, "Test", "US5", "TestUS5", "mdp");
            int subscriberID = 315;

            // Test d'un abonné qui n'a pas d'emprunt en retard
            Assert.IsFalse(admin.ListSubscribersWithLateLoans().Contains(subscriberID));

            string sql = "INSERT INTO EMPRUNTER ([CODE_ABONNÉ],[CODE_ALBUM],[DATE_EMPRUNT],[DATE_RETOUR_ATTENDUE]) " +
                         "VALUES (" + subscriberID + ", 62, '" + DateTime.Now + "' ,'" + DateTime.Now.AddDays(-10) + "')";
            OleDbCommand addLoanCommand = new OleDbCommand(sql, dbConnection);
            addLoanCommand.ExecuteNonQuery();

            // Test d'un abonné avec un emprunt en retard
            Assert.IsTrue(admin.ListSubscribersWithLateLoans().Contains(subscriberID));

            // Suppression de l'abonné et ses emprunts
            TestUtils.DeleteLoan(dbConnection, 315, 62);
            //TestUtils.DeleteSubscriberAndHisLoans(dbConnection, "TestUS5"); ;
            dbConnection.Close();
        }
    }
}
