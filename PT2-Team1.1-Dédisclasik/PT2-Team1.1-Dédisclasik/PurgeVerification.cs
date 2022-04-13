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

namespace PT2_Team1._1_Dédisclasik
{
    public partial class PurgeVerification : Form
    {
        public Administrator admin;
        public PurgeVerification(Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            Subscriber sub;
            dbConnection.Open();
            foreach (int i in admin.GetListPurgeSubs(dbConnection))
            {
                sub = new Subscriber(i, dbConnection);
                listBox1.Items.Add(sub.FirstName + " " + sub.LastName);
            }
            dbConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbConnection.Open();
            admin.PurgeAbonne(dbConnection);
            dbConnection.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
