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
    public partial class AddGenreOnFilm : Form
    {
        public AddGenreOnFilm()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin parentForm = new Admin();
            parentForm.Show();
            this.Close();
        }

        private void AddGenreOnFilm_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            string query = "SELECT * FROM Фильм_по_жанру";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
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

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка, выбраны ли элементы в обоих ComboBox
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите фильм и жанр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string filmTitle = comboBox1.SelectedItem.ToString();
                string genre = comboBox2.SelectedItem.ToString();

                if (!FilmGenreRelationExists(filmTitle, genre))
                {
                    string query = "INSERT INTO Фильм_по_жанру (Фильм_Название, Жанр_Наименование_жанра) VALUES (@FilmTitle, @Genre)";
                    using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FilmTitle", filmTitle);
                            command.Parameters.AddWithValue("@Genre", genre);

                            DatabaseConnection.OpenConnection(connection);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Жанр успешно добавлен фильму.");
                                AddGenreOnFilm_Load(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось добавить фильму жанр.");
                            }
                            DatabaseConnection.CloseConnection(connection);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Такая запись уже существует в базе данных.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении фильма в жанр: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool FilmGenreRelationExists(string filmTitle, string genre)
        {
            string query = "SELECT COUNT(*) FROM Фильм_по_жанру WHERE Фильм_Название = @FilmTitle AND Жанр_Наименование_жанра = @Genre";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilmTitle", filmTitle);
                    command.Parameters.AddWithValue("@Genre", genre);

                    try
                    {
                        DatabaseConnection.OpenConnection(connection);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при выполнении запроса к базе данных: " + ex.Message);
                        return false;
                    }
                    finally
                    {
                        DatabaseConnection.CloseConnection(connection);
                    }
                }
            }
        }

        private void LoadComboBox()
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string filmQuery = "SELECT Название FROM Фильм";
                using (SQLiteCommand filmCmd = new SQLiteCommand(filmQuery, connection))
                {
                    using (SQLiteDataReader filmReader = filmCmd.ExecuteReader())
                    {
                        while (filmReader.Read())
                        {
                            comboBox1.Items.Add(filmReader["Название"].ToString());
                        }
                    }
                }

                string genreQuery = "SELECT Наименование_жанра FROM Жанр";
                using (SQLiteCommand genreCmd = new SQLiteCommand(genreQuery, connection))
                {
                    using (SQLiteDataReader genreReader = genreCmd.ExecuteReader())
                    {
                        while (genreReader.Read())
                        {
                            comboBox2.Items.Add(genreReader["Наименование_жанра"].ToString());
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataGridViewCell cell = selectedRow.Cells["Фильм_Название"];

                if (cell.Value != null)
                {
                    string filmTitle = cell.Value.ToString();

                    string query = "DELETE FROM Фильм_по_жанру WHERE Фильм_Название = @FilmTitle";
                    using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FilmTitle", filmTitle);

                            try
                            {
                                DatabaseConnection.OpenConnection(connection);
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Фильм успешно удален из жанра.");
                                    AddGenreOnFilm_Load(sender, e);
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось удалить фильм из жанра.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при выполнении запроса к базе данных: " + ex.Message);
                            }
                            finally
                            {
                                DatabaseConnection.CloseConnection(connection);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Значение ячейки 'Фильм_Название' равно null.");
                }
            }
        }
    }
}
