using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class InfoSub : Form
    {
        Subscriber sub;
        OleDbConnection dbConnection;
        MD5 saltForHash;
        public InfoSub(int id)
        {
            InitializeComponent();
            dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            sub = new Subscriber(id, dbConnection);
            name.Text = sub.LastName;
            firstName.Text = sub.FirstName;
            login.Text = sub.Login;
            saltForHash = MD5.Create();
            foreach (int i in sub.ListLoans())
            {
                Album album = new Album(i, dbConnection);
                listLoan.Items.Add(album.Title);
            }
            dbConnection.Close();
        }

        private void switchPassword_Click(object sender, EventArgs e)
        {
            bool passwordCondition = newPassword.Text.Length > 5 && newPassword.Text.Length < 32 && newPassword.Text.Any(char.IsUpper)
                && newPassword.Text.Any(char.IsLower) && !newPassword.Text.Contains(" ");
            if (passwordCondition)
            {
                DialogResult result = MessageBox.Show("Etes-vous sure de vouloir modifié le mot de passe de cet abonné.", "Confirmation", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    dbConnection.Open();
                    var salting = saltForHash.ComputeHash(new UTF8Encoding().GetBytes(newPassword.Text));
                    string hash = BitConverter.ToString(salting).Replace("-", string.Empty);
                    string updateQuery = "Update ABONNÉS Set PASSWORD_ABONNÉ = '" + hash + "'" +
                                         "where CODE_ABONNÉ = " + sub.ID;
                    OleDbCommand cmd = new OleDbCommand(updateQuery, dbConnection);
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                    MessageBox.Show("Le mot de passe a bien été changé.", "Confirmation", MessageBoxButtons.OK);
                }
            }

        }
    }
}
