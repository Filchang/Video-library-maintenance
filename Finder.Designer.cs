namespace Курсовая
{
    partial class Finder
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Film_info = new System.Windows.Forms.Button();
            this.open_disc_WithFilm = new System.Windows.Forms.Button();
            this.All_Open_disc = new System.Windows.Forms.Button();
            this.MyRent = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.back.Location = new System.Drawing.Point(3, 3);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(90, 36);
            this.back.TabIndex = 2;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(715, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(191, 122);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(680, 424);
            this.listBox1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(191, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(610, 26);
            this.textBox1.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Жанр"});
            this.comboBox1.Location = new System.Drawing.Point(12, 71);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 28);
            this.comboBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(820, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 38);
            this.button1.TabIndex = 7;
            this.button1.Text = "Поиск";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Film_info
            // 
            this.Film_info.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Film_info.Location = new System.Drawing.Point(3, 122);
            this.Film_info.Name = "Film_info";
            this.Film_info.Size = new System.Drawing.Size(182, 74);
            this.Film_info.TabIndex = 8;
            this.Film_info.Text = "О фильме(-ах)";
            this.Film_info.UseVisualStyleBackColor = false;
            this.Film_info.Click += new System.EventHandler(this.Film_info_Click);
            // 
            // open_disc_WithFilm
            // 
            this.open_disc_WithFilm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.open_disc_WithFilm.Location = new System.Drawing.Point(3, 202);
            this.open_disc_WithFilm.Name = "open_disc_WithFilm";
            this.open_disc_WithFilm.Size = new System.Drawing.Size(182, 77);
            this.open_disc_WithFilm.TabIndex = 9;
            this.open_disc_WithFilm.Text = "Доступные кассеты с выбранным(-и) фильмом(-ами)";
            this.open_disc_WithFilm.UseVisualStyleBackColor = false;
            this.open_disc_WithFilm.Click += new System.EventHandler(this.open_disc_WithFilm_Click);
            // 
            // All_Open_disc
            // 
            this.All_Open_disc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.All_Open_disc.Location = new System.Drawing.Point(3, 285);
            this.All_Open_disc.Name = "All_Open_disc";
            this.All_Open_disc.Size = new System.Drawing.Size(182, 77);
            this.All_Open_disc.TabIndex = 10;
            this.All_Open_disc.Text = "Показать все доступные кассеты";
            this.All_Open_disc.UseVisualStyleBackColor = false;
            this.All_Open_disc.Click += new System.EventHandler(this.All_Open_disc_Click);
            // 
            // MyRent
            // 
            this.MyRent.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MyRent.Location = new System.Drawing.Point(3, 368);
            this.MyRent.Name = "MyRent";
            this.MyRent.Size = new System.Drawing.Size(182, 77);
            this.MyRent.TabIndex = 11;
            this.MyRent.Text = "Мои арендованные кассеты";
            this.MyRent.UseVisualStyleBackColor = false;
            this.MyRent.Click += new System.EventHandler(this.MyRent_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Фильтры";
            // 
            // Finder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(929, 558);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MyRent);
            this.Controls.Add(this.All_Open_disc);
            this.Controls.Add(this.open_disc_WithFilm);
            this.Controls.Add(this.Film_info);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back);
            this.Name = "Finder";
            this.Text = "Finder";
            this.Load += new System.EventHandler(this.Finder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Film_info;
        private System.Windows.Forms.Button open_disc_WithFilm;
        private System.Windows.Forms.Button All_Open_disc;
        private System.Windows.Forms.Button MyRent;
        private System.Windows.Forms.Label label2;
    }
}