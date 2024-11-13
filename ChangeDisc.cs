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
    public partial class ChangeDisc : Form
    {
        public ChangeDisc()
        {
            InitializeComponent();
        }

        private void ChangeDisc_Load(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "SELECT * FROM Видеокасета";

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


        private void back_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }

        private void AddDisc_Click(object sender, EventArgs e)
        {
            try
            {
                string cassetteNumber = dataGridView1.CurrentRow.Cells["Номер_касеты"].Value.ToString();
                string cassettePrice = dataGridView1.CurrentRow.Cells["Стоимость_видеокасеты"].Value.ToString();
                string condition = dataGridView1.CurrentRow.Cells["Состояние"].Value.ToString();
                string filmCount = dataGridView1.CurrentRow.Cells["Количество_фильмов"].Value.ToString();

                if (DiscExists(cassetteNumber))
                {
                    MessageBox.Show("Видеокассета с номером \"" + cassetteNumber + "\" уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (Convert.ToInt32(condition) != 1)
                {
                    MessageBox.Show("Состояние видеокасеты при ее добавление, всегда должно быть равным 1");
                }
                else if (Convert.ToInt32(filmCount) !=0)
                {
                    MessageBox.Show("Количество фильмов на видеокасете при добавление должно равняться нулю");
                }
                else
                {
                    AddDiscToDatabase(cassetteNumber, condition, cassettePrice, filmCount);
                    ChangeDisc_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении видеокассеты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool DiscExists(string cassetteNumber)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "SELECT COUNT(*) FROM Видеокасета WHERE Номер_касеты = @CassetteNumber";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        private void AddDiscToDatabase(string cassetteNumber, string condition, string cassettePrice, string filmCount)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "INSERT INTO Видеокасета (Номер_касеты, Состояние, Стоимость_видеокасеты, Количество_фильмов) VALUES (@CassetteNumber, @Condition, @CassettePrice, @FilmCount)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.Parameters.AddWithValue("@Condition", condition);
                    cmd.Parameters.AddWithValue("@CassettePrice", cassettePrice);
                    cmd.Parameters.AddWithValue("@FilmCount", filmCount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteDisc_Click(object sender, EventArgs e)
        {
            try
            {
                string cassetteNumber = dataGridView1.CurrentRow.Cells["Номер_касеты"].Value.ToString();
                DeleteProkatFromDisc(cassetteNumber);
                DeleteFilmsFromDisc(cassetteNumber);
                DeleteDiscFromDatabase(cassetteNumber);
                ChangeDisc_Load(sender, e);
                MessageBox.Show("Видеокасета успешно удалена");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении видеокассеты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteProkatFromDisc(string cassetteNumber)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "DELETE FROM Прокат WHERE Видеокасета_Номер_касеты = @CassetteNumber";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void DeleteFilmsFromDisc(string cassetteNumber)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "DELETE FROM Фильм_на_касете WHERE Видеокасета_Номер_касеты = @CassetteNumber";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteDiscFromDatabase(string cassetteNumber)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "DELETE FROM Видеокасета WHERE Номер_касеты = @CassetteNumber";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UodatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                string cassetteNumber = dataGridView1.CurrentRow.Cells["Номер_касеты"].Value.ToString();
                string newPrice = dataGridView1.CurrentRow.Cells["Стоимость_видеокасеты"].Value.ToString();
   
                UpdateCassettePrice(cassetteNumber, newPrice);
                ChangeDisc_Load(sender, e);
                MessageBox.Show("Стоимость касеты успешно обновлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при изменении стоимости видеокассеты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateCassettePrice(string cassetteNumber, string newPrice)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string updateCassettePriceQuery = "UPDATE Видеокасета SET Стоимость_видеокасеты = @NewPrice WHERE Номер_касеты = @CassetteNumber";
                using (SQLiteCommand updateCassettePriceCommand = new SQLiteCommand(updateCassettePriceQuery, connection))
                {
                    updateCassettePriceCommand.Parameters.AddWithValue("@NewPrice", newPrice);
                    updateCassettePriceCommand.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                    updateCassettePriceCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
