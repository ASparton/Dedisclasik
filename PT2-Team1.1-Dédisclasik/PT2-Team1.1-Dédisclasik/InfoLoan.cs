using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class InfoLoan : Form
    {
        public InfoLoan(int id, string albumTitle, OleDbConnection dbConnection)
        {
            InitializeComponent();
            albumTitle = albumTitle.Replace(albumTitle.Split(' ')[0] + "  ", "");
            Console.WriteLine(albumTitle);
            Subscriber subscriber = new Subscriber(id, dbConnection);
            LastNameLabel.Text = subscriber.LastName;
            FirstNameLabel.Text = subscriber.FirstName;
            LoginLabel.Text = subscriber.Login;
            LoanDateLabel.Text = subscriber.GetLoanDate(albumTitle).ToString().Split(' ')[0];
            ExpectedReturnDateLabel.Text = subscriber.GetExpectedReturnDate(albumTitle).ToString().Split(' ')[0];
        }
    }
}
