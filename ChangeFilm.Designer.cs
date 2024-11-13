namespace Курсовая
{
    partial class ChangeFilm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AddFilm = new System.Windows.Forms.Button();
            this.DeleteFilm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.back.Location = new System.Drawing.Point(2, 2);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(90, 36);
            this.back.TabIndex = 1;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(57, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(628, 362);
            this.dataGridView1.TabIndex = 2;
            // 
            // AddFilm
            // 
            this.AddFilm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddFilm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AddFilm.Location = new System.Drawing.Point(691, 61);
            this.AddFilm.Name = "AddFilm";
            this.AddFilm.Size = new System.Drawing.Size(105, 39);
            this.AddFilm.TabIndex = 3;
            this.AddFilm.Text = "Добавить фильм";
            this.AddFilm.UseVisualStyleBackColor = false;
            this.AddFilm.Click += new System.EventHandler(this.AddFilm_Click);
            // 
            // DeleteFilm
            // 
            this.DeleteFilm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DeleteFilm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DeleteFilm.Location = new System.Drawing.Point(691, 106);
            this.DeleteFilm.Name = "DeleteFilm";
            this.DeleteFilm.Size = new System.Drawing.Size(105, 39);
            this.DeleteFilm.TabIndex = 4;
            this.DeleteFilm.Text = "Удалить фильм";
            this.DeleteFilm.UseVisualStyleBackColor = false;
            this.DeleteFilm.Click += new System.EventHandler(this.DeleteFilm_Click);
            // 
            // ChangeFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DeleteFilm);
            this.Controls.Add(this.AddFilm);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.back);
            this.Name = "ChangeFilm";
            this.Text = "ChangeFilm";
            this.Load += new System.EventHandler(this.ChangeFilm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button AddFilm;
        private System.Windows.Forms.Button DeleteFilm;
    }
}