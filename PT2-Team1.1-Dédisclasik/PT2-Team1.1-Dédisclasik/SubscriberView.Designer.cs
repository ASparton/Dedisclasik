namespace PT2_Team1._1_Dédisclasik
{
    partial class SubscriberView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubscriberView));
            this.ExtendLoan = new System.Windows.Forms.Button();
            this.LoanedAlbumsList = new System.Windows.Forms.ListBox();
            this.ExtendAllLoansButton = new System.Windows.Forms.Button();
            this.AlbumsList = new System.Windows.Forms.ListBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.UserInfosLabel = new System.Windows.Forms.Label();
            this.CurrentLoansLabel = new System.Windows.Forms.Label();
            this.AlbumsLabel = new System.Windows.Forms.Label();
            this.LoanedAlbumsInfoList = new System.Windows.Forms.ListBox();
            this.ReturnLoanButton = new System.Windows.Forms.Button();
            this.LoanedPaginationLayout = new System.Windows.Forms.TableLayoutPanel();
            this.CurrentLoanPageLabel = new System.Windows.Forms.Label();
            this.NextLoanButton = new System.Windows.Forms.Button();
            this.PreviousLoanButton = new System.Windows.Forms.Button();
            this.AvailablePaginationLayout = new System.Windows.Forms.TableLayoutPanel();
            this.CurrentAlbumPage = new System.Windows.Forms.Label();
            this.NextAlbumPageButton = new System.Windows.Forms.Button();
            this.PreviousAlbumPageButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Pochette = new System.Windows.Forms.PictureBox();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.LoanedPaginationLayout.SuspendLayout();
            this.AvailablePaginationLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pochette)).BeginInit();
            this.SuspendLayout();
            // 
            // ExtendLoan
            // 
            this.ExtendLoan.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.ExtendLoan.Location = new System.Drawing.Point(12, 608);
            this.ExtendLoan.Name = "ExtendLoan";
            this.ExtendLoan.Size = new System.Drawing.Size(289, 38);
            this.ExtendLoan.TabIndex = 1;
            this.ExtendLoan.Text = "Prolonger emprunt";
            this.ExtendLoan.UseVisualStyleBackColor = true;
            this.ExtendLoan.Click += new System.EventHandler(this.ExtendLoanButton_Click);
            // 
            // LoanedAlbumsList
            // 
            this.LoanedAlbumsList.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.LoanedAlbumsList.FormattingEnabled = true;
            this.LoanedAlbumsList.HorizontalScrollbar = true;
            this.LoanedAlbumsList.ItemHeight = 19;
            this.LoanedAlbumsList.Location = new System.Drawing.Point(12, 125);
            this.LoanedAlbumsList.Name = "LoanedAlbumsList";
            this.LoanedAlbumsList.Size = new System.Drawing.Size(289, 441);
            this.LoanedAlbumsList.TabIndex = 9;
            this.LoanedAlbumsList.SelectedIndexChanged += new System.EventHandler(this.LoanedAlbumsList_SelectedIndexChanged);
            // 
            // ExtendAllLoansButton
            // 
            this.ExtendAllLoansButton.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.ExtendAllLoansButton.Location = new System.Drawing.Point(322, 608);
            this.ExtendAllLoansButton.Name = "ExtendAllLoansButton";
            this.ExtendAllLoansButton.Size = new System.Drawing.Size(289, 38);
            this.ExtendAllLoansButton.TabIndex = 11;
            this.ExtendAllLoansButton.Text = "Prolonger tous les emprunts ";
            this.ExtendAllLoansButton.UseVisualStyleBackColor = true;
            this.ExtendAllLoansButton.Click += new System.EventHandler(this.ButtonExtendAll_Click);
            // 
            // AlbumsList
            // 
            this.AlbumsList.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.AlbumsList.FormattingEnabled = true;
            this.AlbumsList.ItemHeight = 19;
            this.AlbumsList.Location = new System.Drawing.Point(898, 110);
            this.AlbumsList.Name = "AlbumsList";
            this.AlbumsList.Size = new System.Drawing.Size(289, 441);
            this.AlbumsList.TabIndex = 12;
            this.AlbumsList.SelectedIndexChanged += new System.EventHandler(this.AlbumsList_SelectedIndexChanged);
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Poor Richard", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.TitleLabel.Location = new System.Drawing.Point(-41, 35);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(1243, 56);
            this.TitleLabel.TabIndex = 15;
            this.TitleLabel.Text = "Gestion des emprunts";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserInfosLabel
            // 
            this.UserInfosLabel.AutoSize = true;
            this.UserInfosLabel.BackColor = System.Drawing.Color.Transparent;
            this.UserInfosLabel.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserInfosLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.UserInfosLabel.Location = new System.Drawing.Point(8, 9);
            this.UserInfosLabel.Name = "UserInfosLabel";
            this.UserInfosLabel.Size = new System.Drawing.Size(70, 22);
            this.UserInfosLabel.TabIndex = 16;
            this.UserInfosLabel.Text = "UserInfo";
            // 
            // CurrentLoansLabel
            // 
            this.CurrentLoansLabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentLoansLabel.Font = new System.Drawing.Font("Poor Richard", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLoansLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.CurrentLoansLabel.Location = new System.Drawing.Point(14, 91);
            this.CurrentLoansLabel.Name = "CurrentLoansLabel";
            this.CurrentLoansLabel.Size = new System.Drawing.Size(582, 31);
            this.CurrentLoansLabel.TabIndex = 17;
            this.CurrentLoansLabel.Text = "Emprunts en cours";
            this.CurrentLoansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlbumsLabel
            // 
            this.AlbumsLabel.BackColor = System.Drawing.Color.Transparent;
            this.AlbumsLabel.Font = new System.Drawing.Font("Poor Richard", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlbumsLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.AlbumsLabel.Location = new System.Drawing.Point(597, 236);
            this.AlbumsLabel.Name = "AlbumsLabel";
            this.AlbumsLabel.Size = new System.Drawing.Size(189, 31);
            this.AlbumsLabel.TabIndex = 18;
            this.AlbumsLabel.Text = "Liste des albums";
            this.AlbumsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoanedAlbumsInfoList
            // 
            this.LoanedAlbumsInfoList.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.LoanedAlbumsInfoList.FormattingEnabled = true;
            this.LoanedAlbumsInfoList.ItemHeight = 19;
            this.LoanedAlbumsInfoList.Location = new System.Drawing.Point(307, 125);
            this.LoanedAlbumsInfoList.Name = "LoanedAlbumsInfoList";
            this.LoanedAlbumsInfoList.Size = new System.Drawing.Size(289, 441);
            this.LoanedAlbumsInfoList.TabIndex = 10;
            // 
            // ReturnLoanButton
            // 
            this.ReturnLoanButton.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.ReturnLoanButton.Location = new System.Drawing.Point(19, 55);
            this.ReturnLoanButton.Name = "ReturnLoanButton";
            this.ReturnLoanButton.Size = new System.Drawing.Size(169, 33);
            this.ReturnLoanButton.TabIndex = 19;
            this.ReturnLoanButton.Text = "Rendre un album";
            this.ReturnLoanButton.UseVisualStyleBackColor = true;
            this.ReturnLoanButton.Click += new System.EventHandler(this.ReturnLoanButton_Click);
            // 
            // LoanedPaginationLayout
            // 
            this.LoanedPaginationLayout.BackColor = System.Drawing.Color.Transparent;
            this.LoanedPaginationLayout.ColumnCount = 3;
            this.LoanedPaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.LoanedPaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.LoanedPaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.LoanedPaginationLayout.Controls.Add(this.CurrentLoanPageLabel, 1, 0);
            this.LoanedPaginationLayout.Controls.Add(this.NextLoanButton, 2, 0);
            this.LoanedPaginationLayout.Controls.Add(this.PreviousLoanButton, 0, 0);
            this.LoanedPaginationLayout.Font = new System.Drawing.Font("Old English Text MT", 12F);
            this.LoanedPaginationLayout.ForeColor = System.Drawing.Color.MistyRose;
            this.LoanedPaginationLayout.Location = new System.Drawing.Point(12, 573);
            this.LoanedPaginationLayout.Name = "LoanedPaginationLayout";
            this.LoanedPaginationLayout.RowCount = 1;
            this.LoanedPaginationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LoanedPaginationLayout.Size = new System.Drawing.Size(289, 32);
            this.LoanedPaginationLayout.TabIndex = 20;
            // 
            // CurrentLoanPageLabel
            // 
            this.CurrentLoanPageLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLoanPageLabel.Location = new System.Drawing.Point(46, 0);
            this.CurrentLoanPageLabel.Name = "CurrentLoanPageLabel";
            this.CurrentLoanPageLabel.Size = new System.Drawing.Size(196, 32);
            this.CurrentLoanPageLabel.TabIndex = 22;
            this.CurrentLoanPageLabel.Text = "Current Page";
            this.CurrentLoanPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextLoanButton
            // 
            this.NextLoanButton.ForeColor = System.Drawing.Color.Black;
            this.NextLoanButton.Location = new System.Drawing.Point(248, 3);
            this.NextLoanButton.Name = "NextLoanButton";
            this.NextLoanButton.Size = new System.Drawing.Size(37, 26);
            this.NextLoanButton.TabIndex = 3;
            this.NextLoanButton.Text = ">";
            this.NextLoanButton.UseVisualStyleBackColor = true;
            this.NextLoanButton.Click += new System.EventHandler(this.NextLoanButton_Click);
            // 
            // PreviousLoanButton
            // 
            this.PreviousLoanButton.ForeColor = System.Drawing.Color.Black;
            this.PreviousLoanButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousLoanButton.Name = "PreviousLoanButton";
            this.PreviousLoanButton.Size = new System.Drawing.Size(37, 26);
            this.PreviousLoanButton.TabIndex = 0;
            this.PreviousLoanButton.Text = "<";
            this.PreviousLoanButton.UseVisualStyleBackColor = true;
            this.PreviousLoanButton.Click += new System.EventHandler(this.PreviousLoanButton_Click);
            // 
            // AvailablePaginationLayout
            // 
            this.AvailablePaginationLayout.BackColor = System.Drawing.Color.Transparent;
            this.AvailablePaginationLayout.ColumnCount = 3;
            this.AvailablePaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.AvailablePaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.AvailablePaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.AvailablePaginationLayout.Controls.Add(this.CurrentAlbumPage, 1, 0);
            this.AvailablePaginationLayout.Controls.Add(this.NextAlbumPageButton, 2, 0);
            this.AvailablePaginationLayout.Controls.Add(this.PreviousAlbumPageButton, 0, 0);
            this.AvailablePaginationLayout.Location = new System.Drawing.Point(898, 557);
            this.AvailablePaginationLayout.Name = "AvailablePaginationLayout";
            this.AvailablePaginationLayout.RowCount = 1;
            this.AvailablePaginationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.AvailablePaginationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.AvailablePaginationLayout.Size = new System.Drawing.Size(289, 32);
            this.AvailablePaginationLayout.TabIndex = 21;
            // 
            // CurrentAlbumPage
            // 
            this.CurrentAlbumPage.BackColor = System.Drawing.Color.Transparent;
            this.CurrentAlbumPage.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentAlbumPage.ForeColor = System.Drawing.Color.MistyRose;
            this.CurrentAlbumPage.Location = new System.Drawing.Point(46, 0);
            this.CurrentAlbumPage.Name = "CurrentAlbumPage";
            this.CurrentAlbumPage.Size = new System.Drawing.Size(196, 32);
            this.CurrentAlbumPage.TabIndex = 3;
            this.CurrentAlbumPage.Text = "Current Page";
            this.CurrentAlbumPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextAlbumPageButton
            // 
            this.NextAlbumPageButton.Location = new System.Drawing.Point(248, 3);
            this.NextAlbumPageButton.Name = "NextAlbumPageButton";
            this.NextAlbumPageButton.Size = new System.Drawing.Size(37, 26);
            this.NextAlbumPageButton.TabIndex = 2;
            this.NextAlbumPageButton.Text = ">";
            this.NextAlbumPageButton.UseVisualStyleBackColor = true;
            this.NextAlbumPageButton.Click += new System.EventHandler(this.NextAlbumPageButton_Click);
            // 
            // PreviousAlbumPageButton
            // 
            this.PreviousAlbumPageButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousAlbumPageButton.Name = "PreviousAlbumPageButton";
            this.PreviousAlbumPageButton.Size = new System.Drawing.Size(37, 26);
            this.PreviousAlbumPageButton.TabIndex = 1;
            this.PreviousAlbumPageButton.Text = "<";
            this.PreviousAlbumPageButton.UseVisualStyleBackColor = true;
            this.PreviousAlbumPageButton.Click += new System.EventHandler(this.PreviousAlbumPageButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // Pochette
            // 
            this.Pochette.BackColor = System.Drawing.Color.White;
            this.Pochette.Location = new System.Drawing.Point(322, 301);
            this.Pochette.Name = "Pochette";
            this.Pochette.Size = new System.Drawing.Size(250, 250);
            this.Pochette.TabIndex = 24;
            this.Pochette.TabStop = false;
            // 
            // SearchBox
            // 
            this.SearchBox.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.SearchBox.Location = new System.Drawing.Point(672, 270);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(205, 23);
            this.SearchBox.TabIndex = 20;
            this.SearchBox.Text = "Rechercher un titre d\'album:";
            this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchBox.Enter += new System.EventHandler(this.SearchBox_Enter);
            this.SearchBox.Leave += new System.EventHandler(this.SearchBox_Leave);
            // 
            // SubscriberView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1214, 651);
            this.Controls.Add(this.Pochette);
            this.Controls.Add(this.AvailablePaginationLayout);
            this.Controls.Add(this.LoanedPaginationLayout);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.ReturnLoanButton);
            this.Controls.Add(this.AlbumsLabel);
            this.Controls.Add(this.CurrentLoansLabel);
            this.Controls.Add(this.UserInfosLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.AlbumsList);
            this.Controls.Add(this.ExtendAllLoansButton);
            this.Controls.Add(this.LoanedAlbumsInfoList);
            this.Controls.Add(this.LoanedAlbumsList);
            this.Controls.Add(this.ExtendLoan);
            this.DoubleBuffered = true;
            this.Name = "SubscriberView";
            this.Text = "AbonneView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AbonneView_FormClosing);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SubscriberView_MouseClick);
            this.LoanedPaginationLayout.ResumeLayout(false);
            this.AvailablePaginationLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pochette)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ExtendLoan;
        private System.Windows.Forms.ListBox LoanedAlbumsList;
        private System.Windows.Forms.Button ExtendAllLoansButton;
        private System.Windows.Forms.ListBox AlbumsList;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label UserInfosLabel;
        private System.Windows.Forms.Label CurrentLoansLabel;
        private System.Windows.Forms.Label AlbumsLabel;
        private System.Windows.Forms.ListBox LoanedAlbumsInfoList;
        private System.Windows.Forms.Button ReturnLoanButton;
        private System.Windows.Forms.TableLayoutPanel LoanedPaginationLayout;
        private System.Windows.Forms.TableLayoutPanel AvailablePaginationLayout;
        private System.Windows.Forms.Button PreviousLoanButton;
        private System.Windows.Forms.Button PreviousAlbumPageButton;
        private System.Windows.Forms.Button NextAlbumPageButton;
        private System.Windows.Forms.Button NextLoanButton;
        private System.Windows.Forms.Label CurrentLoanPageLabel;
        private System.Windows.Forms.Label CurrentAlbumPage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Pochette;
        private System.Windows.Forms.TextBox SearchBox;
    }
}
