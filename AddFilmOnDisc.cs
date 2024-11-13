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
    public partial class AddFilmOnDisc : Form
    {
        public AddFilmOnDisc()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }
        

        private void AddFilmOnDisc_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "SELECT * FROM Фильм_на_касете";

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

                string cassetteQuery = "SELECT Номер_касеты FROM Видеокасета";
                using (SQLiteCommand cassetteCmd = new SQLiteCommand(cassetteQuery, connection))
                {
                    using (SQLiteDataReader cassetteReader = cassetteCmd.ExecuteReader())
                    {
                        while (cassetteReader.Read())
                        {
                            comboBox2.Items.Add(cassetteReader["Номер_касеты"].ToString());
                        }
                    }
                }
            }
        }
        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedItem == null || comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите фильм и номер видеокассеты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cassetteNumber = comboBox2.SelectedItem.ToString();
                string filmTitle = comboBox1.SelectedItem.ToString();

                if (FilmExistsOnDisc(cassetteNumber, filmTitle))
                {
                    MessageBox.Show("Этот фильм уже есть на данной кассете.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    AddFilmToDiscInDatabase(cassetteNumber, filmTitle);
                    FilmCountPlus(cassetteNumber);
                    AddFilmOnDisc_Load(sender, e);
                    MessageBox.Show("Добавление: успешно");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении фильма на кассету: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool FilmExistsOnDisc(string cassetteNumber, string filmTitle)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "SELECT COUNT(*) FROM Фильм_на_касете WHERE Видеокасета_Номер_касеты = @CassetteNumber AND Фильм_Название = @FilmTitle";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.Parameters.AddWithValue("@FilmTitle", filmTitle);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        private void AddFilmToDiscInDatabase(string cassetteNumber, string Title)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "INSERT INTO Фильм_на_касете (Видеокасета_Номер_касеты, Фильм_Название) VALUES (@CassetteNumber, @FilmTitle)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.Parameters.AddWithValue("@FilmTitle", Title);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void FilmCountPlus(string cassetteNumber)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "UPDATE Видеокасета SET Количество_фильмов = Количество_фильмов + 1 WHERE Номер_касеты = @CassetteNumber";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string cassetteNumber = dataGridView1.CurrentRow.Cells["Видеокасета_Номер_касеты"].Value.ToString();
                string filmTitle = dataGridView1.CurrentRow.Cells["Фильм_Название"].Value.ToString();

                RemoveFilmOnDiscInDatabase(cassetteNumber, filmTitle);
                FilmCountMinus(cassetteNumber);
                AddFilmOnDisc_Load(sender, e);
                MessageBox.Show("Удаление: успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении фильма с кассеты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RemoveFilmOnDiscInDatabase(string cassetteNumber, string filmTitle)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string deleteQuery = "DELETE FROM Фильм_на_касете WHERE Видеокасета_Номер_касеты = @CassetteNumber AND Фильм_Название = @FilmTitle";
                using (SQLiteCommand deleteCmd = new SQLiteCommand(deleteQuery, connection))
                {
                    deleteCmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    deleteCmd.Parameters.AddWithValue("@FilmTitle", filmTitle);
                    deleteCmd.ExecuteNonQuery();
                }
            }
        }
        private void FilmCountMinus(string cassetteNumber)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string updateQuery = "UPDATE Видеокасета SET Количество_фильмов = Количество_фильмов - 1 WHERE Номер_касеты = @CassetteNumber";
                using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    updateCmd.ExecuteNonQuery();
                }
            }
        }


    }
}
