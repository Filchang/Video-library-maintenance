using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class ChangeFilm : Form
    {
        public ChangeFilm()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin parentForm = new Admin();
            parentForm.Show();
            this.Close();
        }

        private void ChangeFilm_Load(object sender, EventArgs e)
        {

            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "SELECT * FROM Фильм";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }

                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void AddFilm_Click(object sender, EventArgs e)
        {
            try
            {
                
                string title = dataGridView1.CurrentRow.Cells["Название"].Value.ToString();
                string year = dataGridView1.CurrentRow.Cells["Год_выпуска"].Value.ToString();
                string director = dataGridView1.CurrentRow.Cells["Режиссер"].Value.ToString();
                string duration = dataGridView1.CurrentRow.Cells["Продолжительность"].Value.ToString();
                string rating = dataGridView1.CurrentRow.Cells["Рейтинг"].Value.ToString();

                if (FilmExists(title))
                {
                    MessageBox.Show("Фильм с названием \"" + title + "\" уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    AddFilmToDatabase(title, year, director, duration, rating);
                    ChangeFilm_Load(sender, e);

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении фильма: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool FilmExists(string title)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "SELECT COUNT(*) FROM Фильм WHERE Название = @Title";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        
        private void AddFilmToDatabase(string title, string year, string director, string duration, string rating)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "INSERT INTO Фильм (Название, Год_выпуска, Режиссер, Продолжительность, Рейтинг) VALUES (@Title, @Year, @Director, @Duration, @Rating)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Director", director);
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteFilm_Click(object sender, EventArgs e)
        {
            try
            {
                string title = dataGridView1.CurrentRow.Cells["Название"].Value.ToString();

                RemoveFilmFromDisc(title);                
                Delete_Film(title);
                ChangeFilm_Load(sender, e);
                MessageBox.Show("Фильм успешно удален");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении фильма: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RemoveFilmFromDisc(string title)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string decreaseFilmCountQuery = "UPDATE Видеокасета " +
                                                "SET Количество_фильмов = Количество_фильмов - 1 " +
                                                "WHERE Номер_касеты IN (SELECT Видеокасета_Номер_касеты " +
                                                "FROM Фильм_на_касете " +
                                                "WHERE Фильм_Название = @Title)";

                using (SQLiteCommand decreaseFilmCountCommand = new SQLiteCommand(decreaseFilmCountQuery, connection))
                {
                    decreaseFilmCountCommand.Parameters.AddWithValue("@Title", title);
                    decreaseFilmCountCommand.ExecuteNonQuery();
                }

                string deleteFilmFromCassetteQuery = "DELETE FROM Фильм_на_касете WHERE Фильм_Название = @Title";
                using (SQLiteCommand deleteFilmFromCassetteCommand = new SQLiteCommand(deleteFilmFromCassetteQuery, connection))
                {
                    deleteFilmFromCassetteCommand.Parameters.AddWithValue("@Title", title);
                    deleteFilmFromCassetteCommand.ExecuteNonQuery();
                }

               
                
            }
        }

        private void Delete_Film(string title)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                
                string deleteFilmQuery = "DELETE FROM Фильм WHERE Название = @Title";
                using (SQLiteCommand deleteFilmCommand = new SQLiteCommand(deleteFilmQuery, connection))
                {
                    deleteFilmCommand.Parameters.AddWithValue("@Title", title);
                    deleteFilmCommand.ExecuteNonQuery();
                }
            }
        }
    }

}
