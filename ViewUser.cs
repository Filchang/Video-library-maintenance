using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Курсовая
{
    public partial class ViewUser : Form
    {
        public ViewUser()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin parentForm = new Admin();
            parentForm.Show();
            this.Close();
        }

        private void ViewUser_Load(object sender, EventArgs e)
        {
            SQLiteConnection connection = DatabaseConnection.GetConnection();
            DatabaseConnection.OpenConnection(connection);

            string query = "SELECT * FROM Пользователь";

            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            DatabaseConnection.CloseConnection(connection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Введите корректный ID пользователя.");
                return;
            }

            string query = "SELECT * FROM Пользователь WHERE id = @ID";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = null;

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с указанным ID не найден.");
                        }
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления.");
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        List<int> rentedTapes = new List<int>();
                        string selectRentedTapesQuery = "SELECT Видеокасета_Номер_касеты FROM Прокат WHERE Пользователь_id = @UserID";
                        using (SQLiteCommand command = new SQLiteCommand(selectRentedTapesQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@UserID", userId);
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    rentedTapes.Add(reader.GetInt32(0));
                                }
                            }
                        }

                        if (rentedTapes.Count > 0)
                        {
                            string updateTapesQuery = $"UPDATE Видеокасета SET Состояние = 1 WHERE Номер_касеты IN ({string.Join(",", rentedTapes)})";
                            using (SQLiteCommand command = new SQLiteCommand(updateTapesQuery, connection, transaction))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                        string deleteRentQuery = "DELETE FROM Прокат WHERE Пользователь_id = @UserID";
                        using (SQLiteCommand command = new SQLiteCommand(deleteRentQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@UserID", userId);
                            command.ExecuteNonQuery();
                        }

                        string deleteUserQuery = "DELETE FROM Пользователь WHERE id = @UserID";
                        using (SQLiteCommand command = new SQLiteCommand(deleteUserQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@UserID", userId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        ViewUser_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка удаления пользователя: {ex.Message}");
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }
        }
    }
}
   

