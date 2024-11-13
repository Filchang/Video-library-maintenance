namespace Курсовая
{
    partial class ChangeDisc
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
            this.AddDisc = new System.Windows.Forms.Button();
            this.DeleteDisc = new System.Windows.Forms.Button();
            this.UodatePrice = new System.Windows.Forms.Button();
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
            this.dataGridView1.Location = new System.Drawing.Point(56, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(631, 375);
            this.dataGridView1.TabIndex = 2;
            // 
            // AddDisc
            // 
            this.AddDisc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddDisc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AddDisc.Location = new System.Drawing.Point(693, 62);
            this.AddDisc.Name = "AddDisc";
            this.AddDisc.Size = new System.Drawing.Size(96, 40);
            this.AddDisc.TabIndex = 3;
            this.AddDisc.Text = "Добавить ";
            this.AddDisc.UseVisualStyleBackColor = false;
            this.AddDisc.Click += new System.EventHandler(this.AddDisc_Click);
            // 
            // DeleteDisc
            // 
            this.DeleteDisc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DeleteDisc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DeleteDisc.Location = new System.Drawing.Point(693, 120);
            this.DeleteDisc.Name = "DeleteDisc";
            this.DeleteDisc.Size = new System.Drawing.Size(96, 40);
            this.DeleteDisc.TabIndex = 4;
            this.DeleteDisc.Text = "Удалить";
            this.DeleteDisc.UseVisualStyleBackColor = false;
            this.DeleteDisc.Click += new System.EventHandler(this.DeleteDisc_Click);
            // 
            // UodatePrice
            // 
            this.UodatePrice.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UodatePrice.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UodatePrice.Location = new System.Drawing.Point(693, 180);
            this.UodatePrice.Name = "UodatePrice";
            this.UodatePrice.Size = new System.Drawing.Size(96, 52);
            this.UodatePrice.TabIndex = 5;
            this.UodatePrice.Text = "Обновить цену";
            this.UodatePrice.UseVisualStyleBackColor = false;
            this.UodatePrice.Click += new System.EventHandler(this.UodatePrice_Click);
            // 
            // ChangeDisc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UodatePrice);
            this.Controls.Add(this.DeleteDisc);
            this.Controls.Add(this.AddDisc);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.back);
            this.Name = "ChangeDisc";
            this.Text = "ChangeDisc";
            this.Load += new System.EventHandler(this.ChangeDisc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button AddDisc;
        private System.Windows.Forms.Button DeleteDisc;
        private System.Windows.Forms.Button UodatePrice;
    }
}