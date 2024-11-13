namespace Курсовая
{
    partial class Admin
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
            this.back = new System.Windows.Forms.Button();
            this.addfilm = new System.Windows.Forms.Button();
            this.AddDisc = new System.Windows.Forms.Button();
            this.CheckUser = new System.Windows.Forms.Button();
            this.AddFilmOnDisc = new System.Windows.Forms.Button();
            this.Report = new System.Windows.Forms.Button();
            this.ChangeGenre = new System.Windows.Forms.Button();
            this.AddGenreFilm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.back.Location = new System.Drawing.Point(3, 3);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(90, 36);
            this.back.TabIndex = 0;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // addfilm
            // 
            this.addfilm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addfilm.Location = new System.Drawing.Point(129, 116);
            this.addfilm.Name = "addfilm";
            this.addfilm.Size = new System.Drawing.Size(167, 55);
            this.addfilm.TabIndex = 1;
            this.addfilm.Text = "Изменить фильмы";
            this.addfilm.UseVisualStyleBackColor = false;
            this.addfilm.Click += new System.EventHandler(this.addfilm_Click);
            // 
            // AddDisc
            // 
            this.AddDisc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AddDisc.Location = new System.Drawing.Point(482, 116);
            this.AddDisc.Name = "AddDisc";
            this.AddDisc.Size = new System.Drawing.Size(167, 55);
            this.AddDisc.TabIndex = 2;
            this.AddDisc.Text = "Изменить касеты";
            this.AddDisc.UseVisualStyleBackColor = false;
            this.AddDisc.Click += new System.EventHandler(this.AddDisc_Click);
            // 
            // CheckUser
            // 
            this.CheckUser.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CheckUser.Location = new System.Drawing.Point(129, 212);
            this.CheckUser.Name = "CheckUser";
            this.CheckUser.Size = new System.Drawing.Size(167, 55);
            this.CheckUser.TabIndex = 5;
            this.CheckUser.Text = "Просмотр пользователей";
            this.CheckUser.UseVisualStyleBackColor = false;
            this.CheckUser.Click += new System.EventHandler(this.CheckUser_Click);
            // 
            // AddFilmOnDisc
            // 
            this.AddFilmOnDisc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AddFilmOnDisc.Location = new System.Drawing.Point(482, 212);
            this.AddFilmOnDisc.Name = "AddFilmOnDisc";
            this.AddFilmOnDisc.Size = new System.Drawing.Size(167, 55);
            this.AddFilmOnDisc.TabIndex = 6;
            this.AddFilmOnDisc.Text = "Добавить фильм на касету";
            this.AddFilmOnDisc.UseVisualStyleBackColor = false;
            this.AddFilmOnDisc.Click += new System.EventHandler(this.AddFilmOnDisc_Click);
            // 
            // Report
            // 
            this.Report.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Report.Location = new System.Drawing.Point(307, 40);
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(167, 55);
            this.Report.TabIndex = 7;
            this.Report.Text = "Отчет";
            this.Report.UseVisualStyleBackColor = false;
            this.Report.Click += new System.EventHandler(this.Report_Click);
            // 
            // ChangeGenre
            // 
            this.ChangeGenre.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChangeGenre.Location = new System.Drawing.Point(129, 309);
            this.ChangeGenre.Name = "ChangeGenre";
            this.ChangeGenre.Size = new System.Drawing.Size(167, 55);
            this.ChangeGenre.TabIndex = 8;
            this.ChangeGenre.Text = "Изменить жанры";
            this.ChangeGenre.UseVisualStyleBackColor = false;
            this.ChangeGenre.Click += new System.EventHandler(this.ChangeGenre_Click);
            // 
            // AddGenreFilm
            // 
            this.AddGenreFilm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AddGenreFilm.Location = new System.Drawing.Point(482, 309);
            this.AddGenreFilm.Name = "AddGenreFilm";
            this.AddGenreFilm.Size = new System.Drawing.Size(167, 55);
            this.AddGenreFilm.TabIndex = 9;
            this.AddGenreFilm.Text = "Добавить жанр фильму";
            this.AddGenreFilm.UseVisualStyleBackColor = false;
            this.AddGenreFilm.Click += new System.EventHandler(this.AddGenreFilm_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AddGenreFilm);
            this.Controls.Add(this.ChangeGenre);
            this.Controls.Add(this.Report);
            this.Controls.Add(this.AddFilmOnDisc);
            this.Controls.Add(this.CheckUser);
            this.Controls.Add(this.AddDisc);
            this.Controls.Add(this.addfilm);
            this.Controls.Add(this.back);
            this.Name = "Admin";
            this.Load += new System.EventHandler(this.Admin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button addfilm;
        private System.Windows.Forms.Button AddDisc;
        private System.Windows.Forms.Button CheckUser;
        private System.Windows.Forms.Button AddFilmOnDisc;
        private System.Windows.Forms.Button Report;
        private System.Windows.Forms.Button ChangeGenre;
        private System.Windows.Forms.Button AddGenreFilm;
    }
}