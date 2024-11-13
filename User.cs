using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Form1 parentForm = new Form1();
            parentForm.Show();
            this.Close();
        }

        private void enter_Click(object sender, EventArgs e)
        {
            Enter enter = new Enter();
            enter.Show();
            this.Hide();
        }

        private void registr_Click(object sender, EventArgs e)
        {
            Registr registr = new Registr();
            registr.Show();
            this.Hide();
        }

        private void User_Load(object sender, EventArgs e)
        {

        }
    }
}
