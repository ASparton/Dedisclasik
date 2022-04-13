using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.OleDb;

namespace PT2_Team1._1_Dédisclasik.Tests
{
    [TestClass()]
    public class RegisterTests
    {
        /// <summary>
        /// Tests d'enregistrement d'un abonné dans la base.
        /// </summary>
        [TestMethod()]
        public void RegisterSubscriberTest()
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

            Register.RegisterSubscriber(1, "nom", "prenom", "existe", "mdp");
            string sql = "select * from ABONNÉS where LOGIN_ABONNÉ = 'existe'";
            OleDbCommand cmd = new OleDbCommand(sql, dbConnection);
            cmd.Connection.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Assert.AreEqual("1", reader[1].ToString());
                Assert.AreEqual("nom", reader[2].ToString().Trim());
                Assert.AreEqual("prenom", reader[3].ToString().Trim());
                Assert.AreEqual("existe", reader[4].ToString().Trim());
            }
            reader.Close();
            TestUtils.DeleteSubscriberAndHisLoans(dbConnection, "existe");
            dbConnection.Close();
        }

        /// <summary>
        /// Tests de la fonction "GetCodePays" de la classe Register.
        /// </summary>
        [TestMethod()]
        public void GetCodePaysTest()
        {
            Assert.AreEqual("1", Register.GetCountryCode("France").ToString());
            Assert.AreEqual("6", Register.GetCountryCode("USA").ToString());
            Assert.AreEqual("42", Register.GetCountryCode("Mexique").ToString());
            Assert.AreNotEqual("2", Register.GetCountryCode("France").ToString());
            Assert.AreNotEqual("Bonjour", Register.GetCountryCode("USA").ToString());
            Assert.AreNotEqual("99999", Register.GetCountryCode("Mexique").ToString());
        }
    }
}