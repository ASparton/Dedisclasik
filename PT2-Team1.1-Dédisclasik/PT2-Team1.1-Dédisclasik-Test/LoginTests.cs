using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PT2_Team1._1_Dédisclasik.Tests
{
    [TestClass()]
    public class LoginTests
    {
        /// <summary>
        /// Tests de la fonction "GetUserLogin" de Login
        /// </summary>
        [TestMethod()]
        public void LoginAbonneTest()
        {
            KeyValuePair<int, bool> loginResult;
            loginResult = Login.GetUserLogin("login", "pass");
            Assert.AreEqual(44, loginResult.Key);

            loginResult = Login.GetUserLogin("admin", "admin");
            Assert.AreEqual(45, loginResult.Key);

            loginResult = Login.GetUserLogin("admin", "fauxmdp");
            Assert.AreEqual(-1, loginResult.Key);

            loginResult = Login.GetUserLogin("fauxlogin", "admin");
            Assert.AreEqual(-1, loginResult.Key);

            loginResult = Login.GetUserLogin("Nexist", "Pas");
            Assert.AreEqual(-1, loginResult.Key);
        }
    }
}