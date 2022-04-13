namespace EF_MAJ_CHAMPIONNAT
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxJOUEURS = new System.Windows.Forms.ListBox();
            this.textBoxNom = new System.Windows.Forms.TextBox();
            this.textBoxSalaire = new System.Windows.Forms.TextBox();
            this.comboBoxEquipe = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.boutonSupprimer = new System.Windows.Forms.Button();
            this.boutonAjouter = new System.Windows.Forms.Button();
            this.boutonModifier = new System.Windows.Forms.Button();
            this.boutonRafraîchir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxJOUEURS
            // 
            this.listBoxJOUEURS.FormattingEnabled = true;
            this.listBoxJOUEURS.ItemHeight = 20;
            this.listBoxJOUEURS.Location = new System.Drawing.Point(41, 62);
            this.listBoxJOUEURS.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxJOUEURS.Name = "listBoxJOUEURS";
            this.listBoxJOUEURS.Size = new System.Drawing.Size(270, 524);
            this.listBoxJOUEURS.Sorted = true;
            this.listBoxJOUEURS.TabIndex = 0;
            this.listBoxJOUEURS.SelectedIndexChanged += new System.EventHandler(this.listBoxJOUEURS_SelectedIndexChanged);
            // 
            // textBoxNom
            // 
            this.textBoxNom.Location = new System.Drawing.Point(510, 100);
            this.textBoxNom.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.Size = new System.Drawing.Size(284, 26);
            this.textBoxNom.TabIndex = 1;
            // 
            // textBoxSalaire
            // 
            this.textBoxSalaire.Location = new System.Drawing.Point(508, 152);
            this.textBoxSalaire.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalaire.Name = "textBoxSalaire";
            this.textBoxSalaire.Size = new System.Drawing.Size(284, 26);
            this.textBoxSalaire.TabIndex = 2;
            // 
            // comboBoxEquipe
            // 
            this.comboBoxEquipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEquipe.FormattingEnabled = true;
            this.comboBoxEquipe.Location = new System.Drawing.Point(510, 200);
            this.comboBoxEquipe.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxEquipe.Name = "comboBoxEquipe";
            this.comboBoxEquipe.Size = new System.Drawing.Size(282, 28);
            this.comboBoxEquipe.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(348, 100);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(146, 19);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "Nom joueur :";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(348, 152);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(146, 19);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "Salaire :";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(348, 205);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(146, 19);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "Equipe :";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // boutonSupprimer
            // 
            this.boutonSupprimer.Location = new System.Drawing.Point(719, 316);
            this.boutonSupprimer.Margin = new System.Windows.Forms.Padding(2);
            this.boutonSupprimer.Name = "boutonSupprimer";
            this.boutonSupprimer.Size = new System.Drawing.Size(147, 52);
            this.boutonSupprimer.TabIndex = 8;
            this.boutonSupprimer.Text = "Supprimer";
            this.boutonSupprimer.UseVisualStyleBackColor = true;
            this.boutonSupprimer.Click += new System.EventHandler(this.boutonSupprimer_Click);
            // 
            // boutonAjouter
            // 
            this.boutonAjouter.Location = new System.Drawing.Point(371, 316);
            this.boutonAjouter.Margin = new System.Windows.Forms.Padding(2);
            this.boutonAjouter.Name = "boutonAjouter";
            this.boutonAjouter.Size = new System.Drawing.Size(139, 52);
            this.boutonAjouter.TabIndex = 9;
            this.boutonAjouter.Text = "Ajouter";
            this.boutonAjouter.UseVisualStyleBackColor = true;
            this.boutonAjouter.Click += new System.EventHandler(this.boutonAjouter_Click);
            // 
            // boutonModifier
            // 
            this.boutonModifier.Location = new System.Drawing.Point(534, 316);
            this.boutonModifier.Margin = new System.Windows.Forms.Padding(2);
            this.boutonModifier.Name = "boutonModifier";
            this.boutonModifier.Size = new System.Drawing.Size(157, 52);
            this.boutonModifier.TabIndex = 10;
            this.boutonModifier.Text = "Modifier";
            this.boutonModifier.UseVisualStyleBackColor = true;
            this.boutonModifier.Click += new System.EventHandler(this.boutonModifier_Click);
            // 
            // boutonRafraîchir
            // 
            this.boutonRafraîchir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonRafraîchir.Location = new System.Drawing.Point(719, 247);
            this.boutonRafraîchir.Name = "boutonRafraîchir";
            this.boutonRafraîchir.Size = new System.Drawing.Size(147, 39);
            this.boutonRafraîchir.TabIndex = 11;
            this.boutonRafraîchir.Text = "Rafraîchir";
            this.boutonRafraîchir.UseVisualStyleBackColor = true;
            this.boutonRafraîchir.Click += new System.EventHandler(this.boutonRafraîchir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 733);
            this.Controls.Add(this.boutonRafraîchir);
            this.Controls.Add(this.boutonModifier);
            this.Controls.Add(this.boutonAjouter);
            this.Controls.Add(this.boutonSupprimer);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.comboBoxEquipe);
            this.Controls.Add(this.textBoxSalaire);
            this.Controls.Add(this.textBoxNom);
            this.Controls.Add(this.listBoxJOUEURS);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxJOUEURS;
        private System.Windows.Forms.TextBox textBoxNom;
        private System.Windows.Forms.TextBox textBoxSalaire;
        private System.Windows.Forms.ComboBox comboBoxEquipe;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button boutonSupprimer;
        private System.Windows.Forms.Button boutonAjouter;
        private System.Windows.Forms.Button boutonModifier;
        private System.Windows.Forms.Button boutonRafraîchir;
    }
}

