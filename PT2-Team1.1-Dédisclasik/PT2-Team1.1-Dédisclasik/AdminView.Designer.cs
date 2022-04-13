
namespace PT2_Team1._1_Dédisclasik
{
    partial class AdminView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminView));
            this.PurgeButton = new System.Windows.Forms.Button();
            this.ExtendedLoanLabel = new System.Windows.Forms.Label();
            this.LateLoansLabel = new System.Windows.Forms.Label();
            this.Top10Label = new System.Windows.Forms.Label();
            this.ExtendedLoansList = new System.Windows.Forms.ListBox();
            this.LateLoansList = new System.Windows.Forms.ListBox();
            this.Top10List = new System.Windows.Forms.ListBox();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.ExtendedLoanInfoButton = new System.Windows.Forms.Button();
            this.LateLoansButton = new System.Windows.Forms.Button();
            this.Top10InfoButton = new System.Windows.Forms.Button();
            this.ListSubList = new System.Windows.Forms.ListBox();
            this.ListSubLabel = new System.Windows.Forms.Label();
            this.SubInfoButton = new System.Windows.Forms.Button();
            this.LockerList = new System.Windows.Forms.ListBox();
            this.LockerLabel = new System.Windows.Forms.Label();
            this.LockerNumberChoice = new System.Windows.Forms.NumericUpDown();
            this.LockerInfoButton = new System.Windows.Forms.Button();
            this.NotLoanedList = new System.Windows.Forms.ListBox();
            this.NotLoanedLabel = new System.Windows.Forms.Label();
            this.PostponedPaginationLayout = new System.Windows.Forms.TableLayoutPanel();
            this.CurrentPostponedPageLabel = new System.Windows.Forms.Label();
            this.NextPostponedPageButton = new System.Windows.Forms.Button();
            this.PreviousPostponedPageButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CurrentNotLoanedLabel = new System.Windows.Forms.Label();
            this.NextNotLoanedPageButton = new System.Windows.Forms.Button();
            this.PreviousNotLoanedPageButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CurrentSubscriberPage = new System.Windows.Forms.Label();
            this.NextSubscriberPageButton = new System.Windows.Forms.Button();
            this.PreviousSubsriberPageButton = new System.Windows.Forms.Button();
            this.YearChoser = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.LockerNumberChoice)).BeginInit();
            this.PostponedPaginationLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearChoser)).BeginInit();
            this.SuspendLayout();
            // 
            // PurgeButton
            // 
            this.PurgeButton.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.PurgeButton.Location = new System.Drawing.Point(958, 19);
            this.PurgeButton.Name = "PurgeButton";
            this.PurgeButton.Size = new System.Drawing.Size(214, 41);
            this.PurgeButton.TabIndex = 2;
            this.PurgeButton.Text = "Purger abonnés inactifs";
            this.PurgeButton.UseVisualStyleBackColor = true;
            this.PurgeButton.Click += new System.EventHandler(this.PurgeButton_Click);
            // 
            // ExtendedLoanLabel
            // 
            this.ExtendedLoanLabel.AutoSize = true;
            this.ExtendedLoanLabel.BackColor = System.Drawing.Color.Transparent;
            this.ExtendedLoanLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtendedLoanLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.ExtendedLoanLabel.Location = new System.Drawing.Point(22, 100);
            this.ExtendedLoanLabel.Name = "ExtendedLoanLabel";
            this.ExtendedLoanLabel.Size = new System.Drawing.Size(162, 23);
            this.ExtendedLoanLabel.TabIndex = 6;
            this.ExtendedLoanLabel.Text = "Emprunts prolongés :";
            // 
            // LateLoansLabel
            // 
            this.LateLoansLabel.AutoSize = true;
            this.LateLoansLabel.BackColor = System.Drawing.Color.Transparent;
            this.LateLoansLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LateLoansLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.LateLoansLabel.Location = new System.Drawing.Point(348, 100);
            this.LateLoansLabel.Name = "LateLoansLabel";
            this.LateLoansLabel.Size = new System.Drawing.Size(156, 23);
            this.LateLoansLabel.TabIndex = 8;
            this.LateLoansLabel.Text = "Emprunts en retard :";
            // 
            // Top10Label
            // 
            this.Top10Label.AutoSize = true;
            this.Top10Label.BackColor = System.Drawing.Color.Transparent;
            this.Top10Label.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Top10Label.ForeColor = System.Drawing.Color.MistyRose;
            this.Top10Label.Location = new System.Drawing.Point(600, 99);
            this.Top10Label.Name = "Top10Label";
            this.Top10Label.Size = new System.Drawing.Size(196, 23);
            this.Top10Label.TabIndex = 10;
            this.Top10Label.Text = "Top 10 albums de l\'année :";
            // 
            // ExtendedLoansList
            // 
            this.ExtendedLoansList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ExtendedLoansList.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ExtendedLoansList.FormattingEnabled = true;
            this.ExtendedLoansList.ItemHeight = 14;
            this.ExtendedLoansList.Location = new System.Drawing.Point(27, 129);
            this.ExtendedLoansList.Name = "ExtendedLoansList";
            this.ExtendedLoansList.Size = new System.Drawing.Size(273, 452);
            this.ExtendedLoansList.TabIndex = 12;
            // 
            // LateLoansList
            // 
            this.LateLoansList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LateLoansList.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.LateLoansList.FormattingEnabled = true;
            this.LateLoansList.ItemHeight = 14;
            this.LateLoansList.Location = new System.Drawing.Point(353, 129);
            this.LateLoansList.Name = "LateLoansList";
            this.LateLoansList.Size = new System.Drawing.Size(232, 186);
            this.LateLoansList.TabIndex = 13;
            // 
            // Top10List
            // 
            this.Top10List.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Top10List.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Top10List.FormattingEnabled = true;
            this.Top10List.ItemHeight = 14;
            this.Top10List.Location = new System.Drawing.Point(611, 129);
            this.Top10List.Name = "Top10List";
            this.Top10List.Size = new System.Drawing.Size(289, 186);
            this.Top10List.TabIndex = 14;
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.BackColor = System.Drawing.Color.Transparent;
            this.WelcomeLabel.Font = new System.Drawing.Font("Poor Richard", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.WelcomeLabel.Location = new System.Drawing.Point(17, 12);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(402, 47);
            this.WelcomeLabel.TabIndex = 15;
            this.WelcomeLabel.Text = "Bienvenu Administrateur";
            // 
            // ExtendedLoanInfoButton
            // 
            this.ExtendedLoanInfoButton.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.ExtendedLoanInfoButton.Location = new System.Drawing.Point(101, 594);
            this.ExtendedLoanInfoButton.Name = "ExtendedLoanInfoButton";
            this.ExtendedLoanInfoButton.Size = new System.Drawing.Size(93, 35);
            this.ExtendedLoanInfoButton.TabIndex = 16;
            this.ExtendedLoanInfoButton.Text = "INFO";
            this.ExtendedLoanInfoButton.UseVisualStyleBackColor = true;
            this.ExtendedLoanInfoButton.Click += new System.EventHandler(this.ExtendedLoanInfoButton_Click);
            // 
            // LateLoansButton
            // 
            this.LateLoansButton.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.LateLoansButton.Location = new System.Drawing.Point(425, 321);
            this.LateLoansButton.Name = "LateLoansButton";
            this.LateLoansButton.Size = new System.Drawing.Size(93, 35);
            this.LateLoansButton.TabIndex = 17;
            this.LateLoansButton.Text = "INFO";
            this.LateLoansButton.UseVisualStyleBackColor = true;
            this.LateLoansButton.Click += new System.EventHandler(this.LateLoansButton_Click);
            // 
            // Top10InfoButton
            // 
            this.Top10InfoButton.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.Top10InfoButton.Location = new System.Drawing.Point(706, 321);
            this.Top10InfoButton.Name = "Top10InfoButton";
            this.Top10InfoButton.Size = new System.Drawing.Size(93, 35);
            this.Top10InfoButton.TabIndex = 18;
            this.Top10InfoButton.Text = "INFO";
            this.Top10InfoButton.UseVisualStyleBackColor = true;
            this.Top10InfoButton.Click += new System.EventHandler(this.Top10InfoButton_Click);
            // 
            // ListSubList
            // 
            this.ListSubList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ListSubList.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ListSubList.FormattingEnabled = true;
            this.ListSubList.ItemHeight = 14;
            this.ListSubList.Location = new System.Drawing.Point(931, 129);
            this.ListSubList.Name = "ListSubList";
            this.ListSubList.Size = new System.Drawing.Size(259, 452);
            this.ListSubList.TabIndex = 19;
            // 
            // ListSubLabel
            // 
            this.ListSubLabel.AutoSize = true;
            this.ListSubLabel.BackColor = System.Drawing.Color.Transparent;
            this.ListSubLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListSubLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.ListSubLabel.Location = new System.Drawing.Point(927, 100);
            this.ListSubLabel.Name = "ListSubLabel";
            this.ListSubLabel.Size = new System.Drawing.Size(143, 23);
            this.ListSubLabel.TabIndex = 20;
            this.ListSubLabel.Text = "Liste des abonnés :";
            // 
            // SubInfoButton
            // 
            this.SubInfoButton.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.SubInfoButton.Location = new System.Drawing.Point(1009, 594);
            this.SubInfoButton.Name = "SubInfoButton";
            this.SubInfoButton.Size = new System.Drawing.Size(93, 35);
            this.SubInfoButton.TabIndex = 21;
            this.SubInfoButton.Text = "INFO";
            this.SubInfoButton.UseVisualStyleBackColor = true;
            this.SubInfoButton.Click += new System.EventHandler(this.SubInfoButton_Click);
            // 
            // LockerList
            // 
            this.LockerList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LockerList.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.LockerList.FormattingEnabled = true;
            this.LockerList.ItemHeight = 14;
            this.LockerList.Location = new System.Drawing.Point(620, 404);
            this.LockerList.Name = "LockerList";
            this.LockerList.Size = new System.Drawing.Size(280, 186);
            this.LockerList.TabIndex = 22;
            // 
            // LockerLabel
            // 
            this.LockerLabel.AutoSize = true;
            this.LockerLabel.BackColor = System.Drawing.Color.Transparent;
            this.LockerLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockerLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.LockerLabel.Location = new System.Drawing.Point(624, 375);
            this.LockerLabel.Name = "LockerLabel";
            this.LockerLabel.Size = new System.Drawing.Size(219, 23);
            this.LockerLabel.TabIndex = 23;
            this.LockerLabel.Text = "Albums emprunté du casier :";
            // 
            // LockerNumberChoice
            // 
            this.LockerNumberChoice.Location = new System.Drawing.Point(855, 381);
            this.LockerNumberChoice.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.LockerNumberChoice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LockerNumberChoice.Name = "LockerNumberChoice";
            this.LockerNumberChoice.Size = new System.Drawing.Size(38, 20);
            this.LockerNumberChoice.TabIndex = 24;
            this.LockerNumberChoice.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.LockerNumberChoice.ValueChanged += new System.EventHandler(this.LockerChoice_ValueChanged);
            // 
            // LockerInfoButton
            // 
            this.LockerInfoButton.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.LockerInfoButton.Location = new System.Drawing.Point(706, 594);
            this.LockerInfoButton.Name = "LockerInfoButton";
            this.LockerInfoButton.Size = new System.Drawing.Size(93, 35);
            this.LockerInfoButton.TabIndex = 25;
            this.LockerInfoButton.Text = "INFO";
            this.LockerInfoButton.UseVisualStyleBackColor = true;
            this.LockerInfoButton.Click += new System.EventHandler(this.LockerInfoButton_Click);
            // 
            // NotLoanedList
            // 
            this.NotLoanedList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NotLoanedList.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.NotLoanedList.FormattingEnabled = true;
            this.NotLoanedList.ItemHeight = 14;
            this.NotLoanedList.Location = new System.Drawing.Point(320, 404);
            this.NotLoanedList.Name = "NotLoanedList";
            this.NotLoanedList.Size = new System.Drawing.Size(280, 144);
            this.NotLoanedList.TabIndex = 27;
            // 
            // NotLoanedLabel
            // 
            this.NotLoanedLabel.AutoSize = true;
            this.NotLoanedLabel.BackColor = System.Drawing.Color.Transparent;
            this.NotLoanedLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotLoanedLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.NotLoanedLabel.Location = new System.Drawing.Point(315, 371);
            this.NotLoanedLabel.Name = "NotLoanedLabel";
            this.NotLoanedLabel.Size = new System.Drawing.Size(267, 23);
            this.NotLoanedLabel.TabIndex = 28;
            this.NotLoanedLabel.Text = "Albums pas empruntés depuis 1 an :";
            // 
            // PostponedPaginationLayout
            // 
            this.PostponedPaginationLayout.BackColor = System.Drawing.Color.Transparent;
            this.PostponedPaginationLayout.ColumnCount = 3;
            this.PostponedPaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.PostponedPaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.PostponedPaginationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.PostponedPaginationLayout.Controls.Add(this.CurrentPostponedPageLabel, 1, 0);
            this.PostponedPaginationLayout.Controls.Add(this.NextPostponedPageButton, 2, 0);
            this.PostponedPaginationLayout.Controls.Add(this.PreviousPostponedPageButton, 0, 0);
            this.PostponedPaginationLayout.Location = new System.Drawing.Point(27, 555);
            this.PostponedPaginationLayout.Name = "PostponedPaginationLayout";
            this.PostponedPaginationLayout.RowCount = 1;
            this.PostponedPaginationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PostponedPaginationLayout.Size = new System.Drawing.Size(273, 32);
            this.PostponedPaginationLayout.TabIndex = 29;
            // 
            // CurrentPostponedPageLabel
            // 
            this.CurrentPostponedPageLabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentPostponedPageLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentPostponedPageLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.CurrentPostponedPageLabel.Location = new System.Drawing.Point(43, 0);
            this.CurrentPostponedPageLabel.Name = "CurrentPostponedPageLabel";
            this.CurrentPostponedPageLabel.Size = new System.Drawing.Size(185, 32);
            this.CurrentPostponedPageLabel.TabIndex = 22;
            this.CurrentPostponedPageLabel.Text = "Current Page";
            this.CurrentPostponedPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextPostponedPageButton
            // 
            this.NextPostponedPageButton.Location = new System.Drawing.Point(234, 3);
            this.NextPostponedPageButton.Name = "NextPostponedPageButton";
            this.NextPostponedPageButton.Size = new System.Drawing.Size(36, 26);
            this.NextPostponedPageButton.TabIndex = 3;
            this.NextPostponedPageButton.Text = ">";
            this.NextPostponedPageButton.UseVisualStyleBackColor = true;
            this.NextPostponedPageButton.Click += new System.EventHandler(this.NextPostponedPageButton_Click);
            // 
            // PreviousPostponedPageButton
            // 
            this.PreviousPostponedPageButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousPostponedPageButton.Name = "PreviousPostponedPageButton";
            this.PreviousPostponedPageButton.Size = new System.Drawing.Size(34, 26);
            this.PreviousPostponedPageButton.TabIndex = 0;
            this.PreviousPostponedPageButton.Text = "<";
            this.PreviousPostponedPageButton.UseVisualStyleBackColor = true;
            this.PreviousPostponedPageButton.Click += new System.EventHandler(this.PreviousPostponedPageButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.CurrentNotLoanedLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.NextNotLoanedPageButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.PreviousNotLoanedPageButton, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(320, 557);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(280, 32);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // CurrentNotLoanedLabel
            // 
            this.CurrentNotLoanedLabel.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentNotLoanedLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.CurrentNotLoanedLabel.Location = new System.Drawing.Point(45, 0);
            this.CurrentNotLoanedLabel.Name = "CurrentNotLoanedLabel";
            this.CurrentNotLoanedLabel.Size = new System.Drawing.Size(185, 32);
            this.CurrentNotLoanedLabel.TabIndex = 22;
            this.CurrentNotLoanedLabel.Text = "Current Page";
            this.CurrentNotLoanedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextNotLoanedPageButton
            // 
            this.NextNotLoanedPageButton.Location = new System.Drawing.Point(241, 3);
            this.NextNotLoanedPageButton.Name = "NextNotLoanedPageButton";
            this.NextNotLoanedPageButton.Size = new System.Drawing.Size(36, 26);
            this.NextNotLoanedPageButton.TabIndex = 3;
            this.NextNotLoanedPageButton.Text = ">";
            this.NextNotLoanedPageButton.UseVisualStyleBackColor = true;
            this.NextNotLoanedPageButton.Click += new System.EventHandler(this.NextNotLoanedPageButton_Click);
            // 
            // PreviousNotLoanedPageButton
            // 
            this.PreviousNotLoanedPageButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousNotLoanedPageButton.Name = "PreviousNotLoanedPageButton";
            this.PreviousNotLoanedPageButton.Size = new System.Drawing.Size(34, 26);
            this.PreviousNotLoanedPageButton.TabIndex = 0;
            this.PreviousNotLoanedPageButton.Text = "<";
            this.PreviousNotLoanedPageButton.UseVisualStyleBackColor = true;
            this.PreviousNotLoanedPageButton.Click += new System.EventHandler(this.PreviousNotLoanedPageButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.CurrentSubscriberPage, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.NextSubscriberPageButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.PreviousSubsriberPageButton, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(931, 558);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(259, 32);
            this.tableLayoutPanel2.TabIndex = 31;
            // 
            // CurrentSubscriberPage
            // 
            this.CurrentSubscriberPage.Font = new System.Drawing.Font("Poor Richard", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentSubscriberPage.ForeColor = System.Drawing.Color.MistyRose;
            this.CurrentSubscriberPage.Location = new System.Drawing.Point(41, 0);
            this.CurrentSubscriberPage.Name = "CurrentSubscriberPage";
            this.CurrentSubscriberPage.Size = new System.Drawing.Size(175, 32);
            this.CurrentSubscriberPage.TabIndex = 22;
            this.CurrentSubscriberPage.Text = "Current Page";
            this.CurrentSubscriberPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextSubscriberPageButton
            // 
            this.NextSubscriberPageButton.Location = new System.Drawing.Point(222, 3);
            this.NextSubscriberPageButton.Name = "NextSubscriberPageButton";
            this.NextSubscriberPageButton.Size = new System.Drawing.Size(34, 26);
            this.NextSubscriberPageButton.TabIndex = 3;
            this.NextSubscriberPageButton.Text = ">";
            this.NextSubscriberPageButton.UseVisualStyleBackColor = true;
            this.NextSubscriberPageButton.Click += new System.EventHandler(this.NextSubscriberPageButton_Click);
            // 
            // PreviousSubsriberPageButton
            // 
            this.PreviousSubsriberPageButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousSubsriberPageButton.Name = "PreviousSubsriberPageButton";
            this.PreviousSubsriberPageButton.Size = new System.Drawing.Size(32, 26);
            this.PreviousSubsriberPageButton.TabIndex = 0;
            this.PreviousSubsriberPageButton.Text = "<";
            this.PreviousSubsriberPageButton.UseVisualStyleBackColor = true;
            this.PreviousSubsriberPageButton.Click += new System.EventHandler(this.PreviousSubsriberPageButton_Click);
            // 
            // YearChoser
            // 
            this.YearChoser.Location = new System.Drawing.Point(825, 103);
            this.YearChoser.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.YearChoser.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.YearChoser.Name = "YearChoser";
            this.YearChoser.Size = new System.Drawing.Size(52, 20);
            this.YearChoser.TabIndex = 32;
            this.YearChoser.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.YearChoser.ValueChanged += new System.EventHandler(this.YearChoser_ValueChanged);
            // 
            // AdminView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1214, 651);
            this.Controls.Add(this.YearChoser);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.PostponedPaginationLayout);
            this.Controls.Add(this.NotLoanedLabel);
            this.Controls.Add(this.NotLoanedList);
            this.Controls.Add(this.LockerInfoButton);
            this.Controls.Add(this.LockerNumberChoice);
            this.Controls.Add(this.LockerLabel);
            this.Controls.Add(this.LockerList);
            this.Controls.Add(this.SubInfoButton);
            this.Controls.Add(this.ListSubLabel);
            this.Controls.Add(this.ListSubList);
            this.Controls.Add(this.Top10InfoButton);
            this.Controls.Add(this.LateLoansButton);
            this.Controls.Add(this.ExtendedLoanInfoButton);
            this.Controls.Add(this.WelcomeLabel);
            this.Controls.Add(this.Top10List);
            this.Controls.Add(this.LateLoansList);
            this.Controls.Add(this.ExtendedLoansList);
            this.Controls.Add(this.Top10Label);
            this.Controls.Add(this.LateLoansLabel);
            this.Controls.Add(this.ExtendedLoanLabel);
            this.Controls.Add(this.PurgeButton);
            this.DoubleBuffered = true;
            this.Name = "AdminView";
            this.Text = "AdminView";
            ((System.ComponentModel.ISupportInitialize)(this.LockerNumberChoice)).EndInit();
            this.PostponedPaginationLayout.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.YearChoser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button PurgeButton;
        private System.Windows.Forms.Label ExtendedLoanLabel;
        private System.Windows.Forms.Label LateLoansLabel;
        private System.Windows.Forms.Label Top10Label;
        private System.Windows.Forms.ListBox ExtendedLoansList;
        private System.Windows.Forms.ListBox LateLoansList;
        private System.Windows.Forms.ListBox Top10List;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Button ExtendedLoanInfoButton;
        private System.Windows.Forms.Button LateLoansButton;
        private System.Windows.Forms.Button Top10InfoButton;
        private System.Windows.Forms.ListBox ListSubList;
        private System.Windows.Forms.Label ListSubLabel;
        private System.Windows.Forms.Button SubInfoButton;
        private System.Windows.Forms.ListBox LockerList;
        private System.Windows.Forms.Label LockerLabel;
        private System.Windows.Forms.NumericUpDown LockerNumberChoice;
        private System.Windows.Forms.Button LockerInfoButton;
        private System.Windows.Forms.ListBox NotLoanedList;
        private System.Windows.Forms.Label NotLoanedLabel;
        private System.Windows.Forms.TableLayoutPanel PostponedPaginationLayout;
        private System.Windows.Forms.Label CurrentPostponedPageLabel;
        private System.Windows.Forms.Button NextPostponedPageButton;
        private System.Windows.Forms.Button PreviousPostponedPageButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label CurrentNotLoanedLabel;
        private System.Windows.Forms.Button NextNotLoanedPageButton;
        private System.Windows.Forms.Button PreviousNotLoanedPageButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label CurrentSubscriberPage;
        private System.Windows.Forms.Button NextSubscriberPageButton;
        private System.Windows.Forms.Button PreviousSubsriberPageButton;
        private System.Windows.Forms.NumericUpDown YearChoser;
    }
}