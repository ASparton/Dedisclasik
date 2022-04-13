using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class LoansView : Form
    {
        #region Propriétés

        private Album ConsultedAlbum { get; set; }
        private Subscriber Subscriber { get; set; }

        #endregion

        /// <summary>
        /// Créer une fenêtre d'emprunt d'un album.
        /// </summary>
        /// <param name="album">L'album à possiblement emprunter</param>
        /// <param name="subscriber">L'abonné qui souhaite l'emprunter</param>
        public LoansView(Album album, Subscriber subscriber)
        {
            InitializeComponent();

            Subscriber = subscriber;
            ConsultedAlbum = album;
            if (album.IsCurrentlyLoaned())
            {
                LoanButton.Enabled = false;
                LoanButton.Text = ("Cet album est déjà emprunté.");
            }
            TitleLabel.Text = album.Title;
            DateLabel.Text = album.ProductionDate;
            PriceLabel.Text = album.Price;
            PlaceLabel.Text = album.Section;
            if (subscriber.ID == -1)
            {
                LoanButton.Enabled = false;
                LoanButton.Visible = false;
            }
            ByteArrayToImage(album);
        }

        /// <summary>
        /// Demande la confirmation de l'emprunt et le réalise.
        /// </summary>
        /// Alexandre Sparton
        private void LoanButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Confirmer ?", "Confirmation d'emprunt",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                bool succeded = Subscriber.LoanAlbum(ConsultedAlbum);
                if (succeded)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        public void ByteArrayToImage(Album album)
        {
            try
            {
                label1.Text = "";
                MemoryStream ms = new MemoryStream(album.GetImage());
                Image returnImage = Image.FromStream(ms);
                Pochette.Image = new Bitmap(returnImage, Pochette.Size);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                label1.Text = "Pochette indisponible";
            }
            
        }
    }
}
