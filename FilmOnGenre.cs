using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class FilmOnGenre : Form
    {
        public string genre;
        public FilmOnGenre()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Report parentForm = new Report();
            parentForm.Show();
            this.Close();
        }

        private void FilmOnGenre_Load(object sender, EventArgs e)
        {
            string query = @"SELECT Фильм.Название, Жанр.Наименование_жанра 
                             FROM Фильм 
                             INNER JOIN Фильм_по_жанру ON Фильм.Название = Фильм_по_жанру.Фильм_Название 
                             INNER JOIN Жанр ON Фильм_по_жанру.Жанр_Наименование_жанра = Жанр.Наименование_жанра 
                             WHERE Жанр.Наименование_жанра = @Genre";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Genre", genre);

                    try
                    {
                        DatabaseConnection.OpenConnection(connection);
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                    finally
                    {
                        DatabaseConnection.CloseConnection(connection);
                    }
                }
            }
        }
    }
}
