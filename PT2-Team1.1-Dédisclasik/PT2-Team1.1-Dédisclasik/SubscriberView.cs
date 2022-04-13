using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class SubscriberView : Form
    {
        #region Constantes (nombre d'éléments par page)

        int ELEMENTS_PER_PAGE = 10;
        string SEARCH_SENTENCE = "Rechercherun titre d'album";

        #endregion
        #region Propriétés

        private OleDbConnection DbConnection { get; set; }
        private Subscriber Subscriber { get; set; }
        private Pagination LoanedAlbumsPagination { get; set; }
        private Pagination AlbumsPagination { get; set; }
        private List<Album> pageBeforeSearch { get; set; }
        private List<string> loanedAlbums { get; set; }

        #endregion

        /// <summary>
        /// Créer l'interface de gestion d'emprunts.
        /// </summary>
        /// <param name="subscriberID">Le code de l'abonné qui va gérer ses emprunts</param>
        public SubscriberView(int subscriberID)
        {
            InitializeComponent();
            DbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            DbConnection.Open();

            // Initialisation de l'abonné et des paginations
            Subscriber = new Subscriber(subscriberID, DbConnection);
            LoanedAlbumsPagination = new Pagination(Subscriber.GetListLoansQuery(), ELEMENTS_PER_PAGE, DbConnection);
            AlbumsPagination = new Pagination(Album.GetAllAlbumsQuery(), ELEMENTS_PER_PAGE, DbConnection);

            // Initialisation de la liste des albums empruntés
            loanedAlbums = Album.GetLoanedAlbums(DbConnection);

            // Initialisation du backup de la page courrante
            pageBeforeSearch = new List<Album>();
            pageBeforeSearch.Clear();
            foreach (object album in AlbumsList.Items)
            {
                pageBeforeSearch.Add((Album)album);
            }

            // Initialisation de la fenêtre
            UserInfosLabel.Text = "Bienvenue " + Subscriber.ToString();
            RefreshLoanList();
            RefreshAlbumsList();
        }

        #region Refresh des listes

        /// <summary>
        /// Ajoute les albums en cours d'emprunts de la page courrante à la liste et supprime les anciens éléments de la liste.
        /// </summary>
        private void RefreshLoanList()
        {
            List<int> albumIDs = Subscriber.ListLoans(LoanedAlbumsPagination.MakePaginationQuery());

            if (albumIDs.Count > 0)
            {
                LoanedAlbumsList.Items.Clear();
                CurrentLoanPageLabel.Text = "Page " + (LoanedAlbumsPagination.CurrentPage + 1).ToString() +
                    " sur " + LoanedAlbumsPagination.LastPage;
                albumIDs.ForEach(albumID => LoanedAlbumsList.Items.Add(new Album(albumID, DbConnection)));
            }
            else
            {
                LoanedAlbumsPagination.SetPreviousPage();
            }
        }

        /// <summary>
        /// Affiche les informations relatives à l'album actuellement sélectionné.
        /// </summary>
        private void LoanedAlbumsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoanedAlbumsInfoList.Items.Clear();
            Album selectedAlbum = (Album)LoanedAlbumsList.SelectedItem;
            ByteArrayToImage(selectedAlbum);
            if (selectedAlbum != null)
            {
                LoanedAlbumsInfoList.Items.Add("Vous avez empruter l'album: ");
                LoanedAlbumsInfoList.Items.Add(selectedAlbum.ToString().Split('(')[0]);
                LoanedAlbumsInfoList.Items.Add("Le: " + selectedAlbum.GetLoanDate(Subscriber.ID).ToString().Split(' ')[0] + ".");
                LoanedAlbumsInfoList.Items.Add("Vous devez le rendre le: " + selectedAlbum.GetExpectedReturnDate(Subscriber.ID).ToString().Split(' ')[0]);
            }
        }

        /// <summary>
        /// Ajoute les albums disponibles et les préférences à la liste des albums disponibles par page.
        /// </summary>
        private void RefreshAlbumsList()
        {
            bool atPageOne = false;
            if (AlbumsPagination.CurrentPage == 0)
            {
                atPageOne = true;
            }

            Dictionary<int, bool> albums = Subscriber.GetAvailableAlbumsWithPreferences(
                AlbumsPagination.MakePaginationQuery(), atPageOne, ELEMENTS_PER_PAGE);

            if (albums.Count > 0)
            {
                AlbumsList.Items.Clear();
                CurrentAlbumPage.Text = "Page " + (AlbumsPagination.CurrentPage + 1).ToString() + " sur " + AlbumsPagination.LastPage;
                foreach (KeyValuePair<int, bool> albumIDAndSuggestion in albums)
                {
                    Album album = new Album(albumIDAndSuggestion.Key, DbConnection);
                    if (albumIDAndSuggestion.Value)
                    {
                        album.IsSuggested = true;
                    }
                    AlbumsList.Items.Add(album);
                }
            }
            else
            {
                AlbumsPagination.SetPreviousPage();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'emprunt lié à l'album sélectionné
        /// </summary>
        private void AlbumsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Album album = (Album)AlbumsList.SelectedItem;
                DialogResult result = new LoansView(album, Subscriber).ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoanedAlbumsList.Items.Add(album);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Album album = new Album(Album.GetAlbumIDFromTitle(AlbumsList.SelectedItem.ToString(), DbConnection), DbConnection);
                DialogResult result = new LoansView(album, Subscriber).ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoanedAlbumsList.Items.Add(album);
                }
            }

        }

        #endregion
        #region Gestion emprunts en cours

        /// <summary>
        /// Reporte le rendu de l'album actuellement sélectionné.
        /// </summary>
        private void ExtendLoanButton_Click(object sender, EventArgs e)
        {
            Album album = (Album)LoanedAlbumsList.SelectedItem;
            var result = MessageBox.Show("Êtes-vous sûr de vouloir prolonger l'emprunt de " + album.Title.Trim() + " de 1 mois ?",
                "Valider", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                MessageBox.Show("Opération annulé", "Annulé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bool succeded = Subscriber.ExtendLoan(album.ID);
                if (succeded)
                {
                    MessageBox.Show("L'emprunt de " + album.Title.Trim() + " a bien été prolongé de 1 mois.", "Emprunt prolongé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("L'emprunt de " + album.Title.Trim() + " a déjà été prolongé.", "Emprunt déjà prolongé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                LoanedAlbumsList_SelectedIndexChanged(sender, e);
            }
        }

        /// <summary>
        /// Repousse la date de retour de tous les emprunts si confirmation.
        /// </summary>
        private void ButtonExtendAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir prolonger tous vos emprunts de 1 mois ?",
                "Valider", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Opération annulé", "Opération annulé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Subscriber.ExtendAllLoans();
                MessageBox.Show("Tous vos emprunts non prolongés l'ont été.", "Emprunts prolongés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoanedAlbumsList_SelectedIndexChanged(sender, e);
            }
        }

        /// <summary>
        /// Rend un album emprunté.
        /// </summary>
        private void ReturnLoanButton_Click(object sender, EventArgs e)
        {
            Album selectedAlbum = (Album)LoanedAlbumsList.SelectedItem;
            var result = MessageBox.Show("Êtes-vous sûr de vouloir rendre l'album " + LoanedAlbumsList.SelectedItem.ToString().Trim() + "?",
                "Valider", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                MessageBox.Show("Opération annulé", "Annulé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Subscriber.ReturnLoan(selectedAlbum.ID);
            RefreshLoanList();
            LoanedAlbumsList.SelectedItem = null;
        }

        #endregion
        #region Evènements pagination

        /// <summary>
        /// Charge la page suivante des albums.
        /// </summary>
        private void NextAlbumPageButton_Click(object sender, EventArgs e)
        {
            AlbumsPagination.SetNextPage();
            RefreshAlbumsList();
        }

        /// <summary>
        /// Charge la page des albums précédentes
        /// </summary>
        private void PreviousAlbumPageButton_Click(object sender, EventArgs e)
        {
            if (AlbumsPagination.CurrentPage > 0)
            {
                AlbumsPagination.SetPreviousPage();
                RefreshAlbumsList();
            }
        }

        /// <summary>
        /// Charge la page suivante des emprunts en cours.
        /// </summary>
        private void NextLoanButton_Click(object sender, EventArgs e)
        {
            LoanedAlbumsPagination.SetNextPage();
            RefreshLoanList();
        }

        /// <summary>
        /// Charge la page précédente des emprunts en cours.
        /// </summary>
        private void PreviousLoanButton_Click(object sender, EventArgs e)
        {
            if (LoanedAlbumsPagination.CurrentPage > 0)
            {
                LoanedAlbumsPagination.SetPreviousPage();
                RefreshLoanList();
            }
        }

        #endregion
        #region Evènements recherche

        /// <summary>
        /// Actualise l'album list avec le texte contenu dans le recherche.
        /// </summary>
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchBox.Text.Length > 0 && SearchBox.Text != SEARCH_SENTENCE)
            {
                string titre = SearchBox.Text;
                titre = titre.Replace("'", "_");
                titre = titre.Replace(" ", "%");
                List<string> albumTitle = Album.ReshearshAlbumByName(DbConnection, titre, loanedAlbums);

                AlbumsList.Items.Clear();
                foreach (string Title in albumTitle)
                {
                    AlbumsList.Items.Add(Title);
                }
                SetAlbumsPaginationButtonsEnabled(false);
            }
            else
            {
                SetAlbumsPaginationButtonsEnabled(true);
                RefreshAlbumsList();
            }
        }

        /// <summary>
        /// Remet le texte initiale dans la textBox.
        /// </summary>
        private void SearchBox_Leave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Remet le focus ailleur que sur la textBox
        /// </summary>
        private void SubscriberView_MouseClick(object sender, MouseEventArgs e)
        {
            if (SearchBox.Text.Length == 0)
            {
                LoanedAlbumsInfoList.Focus();
            }
        }

        /// <summary>
        /// Efface le message quand on entre dans la textBox
        /// </summary>
        private void SearchBox_Enter(object sender, EventArgs e)
        {
            SearchBox.Text = "";
        }

        /// <summary>
        /// Active les bouttons de paginations ou les désactives
        /// </summary>
        /// <param name="enabled">True pour activer, false pour désactiver</param>
        private void SetAlbumsPaginationButtonsEnabled(bool enabled)
        {
            NextAlbumPageButton.Enabled = enabled;
            PreviousAlbumPageButton.Enabled = enabled;
        }

        public void ByteArrayToImage(Album album)
        {
            MemoryStream ms = new MemoryStream(album.GetImage());
            Image returnImage = Image.FromStream(ms);
            Pochette.Image = new Bitmap(returnImage, Pochette.Size);
        }

        #endregion

        /// <summary>
        /// Ferme la connexion avec la base de données.
        /// </summary>
        private void AbonneView_FormClosing(object sender, FormClosingEventArgs e) => DbConnection.Close();
    }
}
