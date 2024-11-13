namespace Курсовая
{
    partial class User
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
            this.enter = new System.Windows.Forms.Button();
            this.registr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.back.Location = new System.Drawing.Point(3, 2);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(90, 36);
            this.back.TabIndex = 1;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // enter
            // 
            this.enter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.enter.Location = new System.Drawing.Point(193, 88);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(124, 41);
            this.enter.TabIndex = 2;
            this.enter.Text = "Войти";
            this.enter.UseVisualStyleBackColor = false;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // registr
            // 
            this.registr.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.registr.Location = new System.Drawing.Point(165, 153);
            this.registr.Name = "registr";
            this.registr.Size = new System.Drawing.Size(183, 41);
            this.registr.TabIndex = 3;
            this.registr.Text = "Зарегестрироваться";
            this.registr.UseVisualStyleBackColor = false;
            this.registr.Click += new System.EventHandler(this.registr_Click);
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 324);
            this.Controls.Add(this.registr);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.back);
            this.Name = "User";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.User_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Button registr;
    }
}