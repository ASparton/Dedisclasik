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
    public partial class AdminView : Form
    {
        #region Propriétés

        private OleDbConnection DbConnection { get; set; }
        private Administrator Admin { get; set; }

        #endregion
        #region Pagination

        private readonly int NB_ELEMENTS_PER_LARGE_PAGE = 30;
        private readonly int NB_ELEMENTS_PER_TINY_PAGE = 10;

        private Pagination PostponedPagination { get; set; }
        private Pagination NotLoanedPagination { get; set; }
        private Pagination NotLoanedPagination2 { get; set; }
        private Pagination SubscriberPagination { get; set; }
        private Pagination LatePagination { get; set; }

        #endregion

        public AdminView()
        {
            InitializeComponent();
            DbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            DbConnection.Open();
            Admin = new Administrator(DbConnection);

            // Initialisation de la pagination
            PostponedPagination = new Pagination(Admin.GetLoanPostponedQuery(), NB_ELEMENTS_PER_LARGE_PAGE, DbConnection);
            NotLoanedPagination = new Pagination(Admin.GetListAlbumsNotLoanedSinceOneYearQuery(), NB_ELEMENTS_PER_TINY_PAGE, DbConnection);
            NotLoanedPagination2 = new Pagination(Admin.GetListAlbumsNotLoanedSinceOneYearAlreadyLoanedQuery(), NB_ELEMENTS_PER_TINY_PAGE, DbConnection);
            SubscriberPagination = new Pagination(Admin.GetListSubsQuery(), NB_ELEMENTS_PER_LARGE_PAGE, DbConnection);
            LatePagination = new Pagination(Admin.GetSubscribersWithLateLoansQuery(), NB_ELEMENTS_PER_TINY_PAGE, DbConnection);

            // Initialisation des autres éléments de la page
            LockerNumberChoice.Value = 1;
            YearChoser.Maximum = DateTime.Today.Year;
            YearChoser.Minimum = 1;
            YearChoser.Value = DateTime.Today.Year;

            // Initialisation des listes
            RefreshPostponedLoansList();
            RefreshNotLoanedSince1Year();
            RefreshSubscriberList();
            RefreshLateSubscriberList();
            RefreshMostLoanedAlbumsInYearList((int)YearChoser.Value);
        }

        #region Evenement bouttons d'infos

        /// <summary>
        /// Affiche les infos en rapport avec l'abonné qui a repoussé un emprunt.
        /// </summary>
        private void ExtendedLoanInfoButton_Click(object sender, EventArgs e)
        {
            if (ExtendedLoansList.SelectedItem != null)
            {
                int id = -1;
                string login = ExtendedLoansList.SelectedItem.ToString().Split(' ')[0];
                string sql = "SELECT * FROM ABONNÉS WHERE LOGIN_ABONNÉ = '" + login + "'";
                OleDbCommand command = new OleDbCommand(sql, DbConnection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = (int)reader[0];
                }
                new InfoLoan(id, ExtendedLoansList.SelectedItem.ToString(), DbConnection).ShowDialog();
            }
        }

        /// <summary>
        /// Affiche les infos liés à l'abonné en retard.
        /// </summary>
        private void LateLoansButton_Click(object sender, EventArgs e)
        {
            if (LateLoansList.SelectedItem != null)
            {
                int id = int.Parse(LateLoansList.SelectedItem.ToString().Split(' ')[0]);
                new InfoSub(id).ShowDialog();
            }
        }

        /// <summary>
        /// Affiche les infos liés à un album du top 10 des emprunts de l'année.
        /// </summary>
        private void Top10InfoButton_Click(object sender, EventArgs e)
        {
            if (Top10List.SelectedItem != null)
            {
                int nameIndex = Top10List.SelectedItem.ToString().IndexOf(' ') + 2;
                string select = Top10List.SelectedItem.ToString().Substring(nameIndex);
                Album album = new Album(Album.GetAlbumIDFromTitle(select, DbConnection), DbConnection);
                Subscriber sub = new Subscriber(-1, DbConnection);
                new LoansView(album, sub).ShowDialog();
            }
        }

        /// <summary>
        /// Affiche les infos sur l'album empruntés du casier.
        /// </summary>
        private void LockerInfoButton_Click(object sender, EventArgs e)
        {
            if (LockerList.SelectedItem != null)
            {
                string select = LockerList.SelectedItem.ToString();
                new LoansView(new Album(Album.GetAlbumIDFromTitle(select, DbConnection), DbConnection), 
                    new Subscriber(-1, DbConnection)).ShowDialog();
            }
        }

        /// <summary>
        /// Affiche les infos liés au subscriber sélectionné dans la liste.
        /// </summary>
        private void SubInfoButton_Click(object sender, EventArgs e)
        {
            if (ListSubList.SelectedItem != null)
            {
                int id = -1;
                string login = ListSubList.SelectedItem.ToString().Split(' ')[2];
                string sql = "select * from ABONNÉS where LOGIN_ABONNÉ = '" + login + "'";
                OleDbCommand command = new OleDbCommand(sql, DbConnection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = (int)reader[0];
                }
                new InfoSub(id).ShowDialog();
            }
        }

        /// <summary>
        /// Affiche la page de purge.
        /// </summary>
        private void PurgeButton_Click(object sender, EventArgs e) => new PurgeVerification(Admin).Show();

        /// <summary>
        /// Affiche les emprunts absents du casier courrant.
        /// </summary>
        private void LockerChoice_ValueChanged(object sender, EventArgs e)
        {
            LockerList.Items.Clear();
            int i = (int)LockerNumberChoice.Value;
            List<Album> albumByLocker = Admin.listAlbumLoanedByLocker(i);
            foreach (Album a in albumByLocker)
            {
                LockerList.Items.Add(a.Title);
            }
        }

        /// <summary>
        /// Change l'annéee du top 10.
        /// </summary>
        private void YearChoser_ValueChanged(object sender, EventArgs e) => RefreshMostLoanedAlbumsInYearList((int)YearChoser.Value);

        #endregion
        #region Refresh des listes

        /// <summary>
        /// Rafraichi la page courrante des emprunts repoussés.
        /// </summary>
        private void RefreshPostponedLoansList()
        {
            ExtendedLoansList.Items.Clear();
            foreach (KeyValuePair<int, int> i in Admin.LoanPostponed(PostponedPagination.MakePaginationQuery()))
            {
                Subscriber sub = new Subscriber(i.Value, DbConnection);
                Album album = new Album(i.Key, DbConnection);
                ExtendedLoansList.Items.Add(sub.Login.Trim() + "  " + album.Title);
            }
            CurrentPostponedPageLabel.Text = "Page " + (PostponedPagination.CurrentPage + 1).ToString() + " sur " + PostponedPagination.LastPage;
        }

        /// <summary>
        /// Rafraichi la page courrante des albums non empruntés depuis au moins 1 an.
        /// </summary>
        private void RefreshNotLoanedSince1Year()
        {
            NotLoanedList.Items.Clear();
            foreach (KeyValuePair<int, DateTime> i in Admin.ListAlbumsNotLoanedSinceOneYear(NotLoanedPagination.MakePaginationQuery(), 
                NotLoanedPagination2.MakePaginationQuery()))
            {
                NotLoanedList.Items.Add(new Album(i.Key, DbConnection));
            }
            CurrentNotLoanedLabel.Text = "Page " + (NotLoanedPagination.CurrentPage + 1).ToString() + " sur " + NotLoanedPagination.LastPage;
        }

        /// <summary>
        /// Rafraichi la page courrante des abonnés.
        /// </summary>
        private void RefreshSubscriberList()
        {
            ListSubList.Items.Clear();
            Admin.ListSubs(SubscriberPagination.MakePaginationQuery()).ForEach(sub => ListSubList.Items.Add(sub));
            CurrentSubscriberPage.Text = "Page " + (SubscriberPagination.CurrentPage + 1).ToString() + " sur " + SubscriberPagination.LastPage;
        }

        /// <summary>
        /// Rafraichi la page courrante des abonnés ayant des emprunts en retard.
        /// </summary>
        private void RefreshLateSubscriberList() => Admin.ListSubscribersWithLateLoans()
                                                    .ForEach(id => LateLoansList.Items.Add(id + " " + Loan.getSubFromID(id)));

        /// <summary>
        /// Rafraichi la page courrante des albums les plus empruntés de l'année.
        /// </summary>
        private void RefreshMostLoanedAlbumsInYearList(int year)
        {
            Top10List.Items.Clear();
            Admin.ListMostLoanedAlbumsInYear(10, year).ForEach(id => Top10List.Items.Add(id + "  " + new Album(id, DbConnection).Title));
        }

        #endregion
        #region Evenements pagination

        private void NextPostponedPageButton_Click(object sender, EventArgs e)
        {
            PostponedPagination.SetNextPage();
            if (PostponedPagination.CurrentPage < PostponedPagination.LastPage)
            {
                RefreshPostponedLoansList();
            }
        }

        private void PreviousPostponedPageButton_Click(object sender, EventArgs e)
        {
            PostponedPagination.SetPreviousPage();
            RefreshPostponedLoansList();
        }

        private void NextNotLoanedPageButton_Click(object sender, EventArgs e)
        {
            NotLoanedPagination.SetNextPage();
            NotLoanedPagination2.SetNextPage();
            if (NotLoanedPagination.CurrentPage < NotLoanedPagination.LastPage)
            {
                RefreshNotLoanedSince1Year();
            }
        }

        private void PreviousNotLoanedPageButton_Click(object sender, EventArgs e)
        {
            NotLoanedPagination.SetPreviousPage();
            NotLoanedPagination2.SetPreviousPage();
            RefreshNotLoanedSince1Year();
        }

        private void NextSubscriberPageButton_Click(object sender, EventArgs e)
        {
            SubscriberPagination.SetNextPage();
            if (SubscriberPagination.CurrentPage < SubscriberPagination.LastPage)
            {
                RefreshSubscriberList();
            }
        }

        private void PreviousSubsriberPageButton_Click(object sender, EventArgs e)
        {
            SubscriberPagination.SetPreviousPage();
            RefreshSubscriberList();
        }

        private void NextLatePageButton_Click(object sender, EventArgs e)
        {
            LatePagination.SetNextPage();
            if (LatePagination.CurrentPage < LatePagination.LastPage)
            {
                RefreshLateSubscriberList();
            }
        }

        private void PreviousLatePageButton_Click(object sender, EventArgs e)
        {
            LatePagination.SetPreviousPage();
            RefreshLateSubscriberList();
        }

        #endregion

        
    }
}
