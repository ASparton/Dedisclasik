    using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT2_Team1._1_Dédisclasik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik.Tests
{
    [TestClass()]
    public partial class SubscriberTests_US2
    {
        [TestMethod()]
        public void ListLoanTest()
        {
            // Préparation des tests
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            Subscriber subscriber;
            int subscriberID, albumID;
            DateTime expectedDate, returnDate;

            // Test report d'un emprunt n'ayant pas été rendu
            dbConnection.Open();
            subscriberID = 44;
            albumID = 78;
            subscriber = new Subscriber(subscriberID, dbConnection);
            subscriber.LoanAlbum(albumID);
            expectedDate = LoanUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection).AddMonths(1);
            subscriber.ExtendLoan(albumID);
            returnDate = LoanUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            Assert.AreEqual(expectedDate, returnDate);
            dbConnection.Close();

            // Test report d'un emprunt déjà reporté
            dbConnection.Open();
            subscriber.ExtendLoan(albumID);
            returnDate = LoanUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            Assert.AreEqual(expectedDate, returnDate);
            subscriber.ReturnLoan(albumID);
            dbConnection.Close();

            // Test report d'un emprunt déjà rendu
            dbConnection.Open();
            subscriberID = 39;
            albumID = 54;
            subscriber = new Subscriber(subscriberID, dbConnection);
            expectedDate = LoanUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            subscriber.ExtendLoan(albumID);
            returnDate = LoanUtils.GetLoanExcpectedReturnDate(subscriberID, albumID, dbConnection);
            Assert.AreEqual(expectedDate, returnDate);
            subscriber.ReturnLoan(54);

            // Test sur un client qui n'a pas de location en cours
            Subscriber sub1 = new Subscriber(47, dbConnection);
            sub1.ListLoans(dbConnection);
            Assert.AreEqual(0,sub1.nbAlbums);

            // Test sur un client qui a une location en cours
            Subscriber sub2 = new Subscriber(48, dbConnection);
            sub2.ListLoans(dbConnection);
            Assert.AreEqual(1, sub2.nbAlbums);

            dbConnection.Close();
        }
    }
}