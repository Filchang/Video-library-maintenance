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
    public partial class ChangeGenre : Form
    {
        public ChangeGenre()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin parentForm = new Admin();
            parentForm.Show();
            this.Close();
        }

        private void ChangeGenre_Load(object sender, EventArgs e)
        {
            string query = "SELECT Наименование_жанра FROM Жанр";

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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataGridViewCell cell = selectedRow.Cells["Наименование_жанра"];

                if (cell != null && cell.Value != null)
                {
                    try
                    {
                        string newGenre = cell.Value.ToString().Trim();

                        if (!string.IsNullOrWhiteSpace(newGenre))
                        {
                            if (!GenreExists(newGenre))
                            {
                                string query = "INSERT INTO Жанр (Наименование_жанра) VALUES (@Genre)";
                                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                                {
                                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                                    {
                                        command.Parameters.AddWithValue("@Genre", newGenre);

                                        try
                                        {
                                            DatabaseConnection.OpenConnection(connection);
                                            int rowsAffected = command.ExecuteNonQuery();
                                            if (rowsAffected > 0)
                                            {
                                                MessageBox.Show("Новый жанр успешно добавлен.");
                                                ChangeGenre_Load(sender, e);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Не удалось добавить новый жанр.");
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
                                MessageBox.Show("Жанр уже существует в базе данных.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите жанр для добавления.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при обработке значения ячейки: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Ячейка Наименование_жанра пуста или не существует.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для добавления в базу данных.");
            }
        }
            



        private bool GenreExists(string genre)
        {
            string query = "SELECT COUNT(*) FROM Жанр WHERE Наименование_жанра = @Genre";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Genre", genre);

                    try
                    {
                        DatabaseConnection.OpenConnection(connection);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                        return false;
                    }
                    finally
                    {
                        DatabaseConnection.CloseConnection(connection);
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления из базы данных.");
                return;
            }

            string genreToDelete = ""; 

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataGridViewCell cell = selectedRow.Cells["Наименование_жанра"];

                if (cell.Value != null)
                {
                    genreToDelete = cell.Value.ToString(); 
                }
                else
                {
                    MessageBox.Show("Значение ячейки Наименование_жанра равно null.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении названия жанра: " + ex.Message);
                return;
            }

            string queryDeleteFilms = "DELETE FROM Фильм_по_жанру WHERE Жанр_Наименование_жанра = @Genre";
            string queryDeleteGenre = "DELETE FROM Жанр WHERE Наименование_жанра = @Genre";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(queryDeleteFilms, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Genre", genreToDelete);
                            command.ExecuteNonQuery();
                        }

                        using (SQLiteCommand command = new SQLiteCommand(queryDeleteGenre, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Genre", genreToDelete);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Жанр и связанные с ним фильмы успешно удалены.");
                        ChangeGenre_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }
    }
    
}
