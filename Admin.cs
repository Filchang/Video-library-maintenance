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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Form1 parentForm = new Form1();
            parentForm.Show();
            this.Close();
        }

       
        private void CheckUser_Click(object sender, EventArgs e)
        {
            ViewUser view = new ViewUser();
            view.Show();
            this.Hide();
        }

        private void addfilm_Click(object sender, EventArgs e)
        {
            ChangeFilm changefilm = new ChangeFilm();
            changefilm.Show();
            this.Hide();
        }

        private void AddDisc_Click(object sender, EventArgs e)
        {
            ChangeDisc changeDisc = new ChangeDisc();
            changeDisc.Show();
            this.Hide();
        }

        private void AddFilmOnDisc_Click(object sender, EventArgs e)
        {
            AddFilmOnDisc addFilmOnDisc = new AddFilmOnDisc();
            addFilmOnDisc.Show();
            this.Hide();
        }
        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void Report_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Hide();
        }

        private void ChangeGenre_Click(object sender, EventArgs e)
        {
            ChangeGenre changegenre = new ChangeGenre();
            changegenre.Show();
            this.Hide();
        }

        private void AddGenreFilm_Click(object sender, EventArgs e)
        {
            AddGenreOnFilm add = new AddGenreOnFilm();
            add.Show();
            this.Hide();
        }
    }
}
