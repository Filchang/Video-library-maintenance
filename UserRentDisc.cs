using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class UserRentDisc : Form
    {
        public string PhoneNumber { get; set; }
        public UserRentDisc()
        {
            InitializeComponent();

        }

        private void back_Click(object sender, EventArgs e)
        {
            Finder parentForm = new Finder();
            parentForm.Show();
            this.Close();
        }

        private void UserRentDisc_Load(object sender, EventArgs e)
        {
            try
            {
                int userId = GetUserID(PhoneNumber);

                DataTable UserRentTable = new DataTable();
                UserRentTable.Columns.Add("Номер_касеты");
                UserRentTable.Columns.Add("Стоимость");
                UserRentTable.Columns.Add("Дата аренды");
                UserRentTable.Columns.Add("Дата возврата");
                UserRentTable.Columns.Add("Фильмы");
                UserRentTable.Columns.Add("Просрочка (дней)"); // Новый столбец для просрочки

                List<string[]> UserRentDisc = GetUserRentDisc(userId);

                foreach (string[] Rent in UserRentDisc)
                {
                    DataRow row = UserRentTable.NewRow();
                    row["Номер_касеты"] = Rent[0];
                    row["Стоимость"] = Rent[1];
                    row["Дата аренды"] = Rent[2];
                    row["Дата возврата"] = Rent[3];
                    row["Фильмы"] = Rent[4];

                    DateTime returnDate;
                    if (DateTime.TryParse(Rent[3], out returnDate))
                    {
                        int overdueDays = (DateTime.Now.Date - returnDate.Date).Days;
                        row["Просрочка (дней)"] = overdueDays > 0 ? overdueDays : 0;
                    }
                    else
                    {
                        row["Просрочка (дней)"] = "Некорректная дата";
                    }

                    UserRentTable.Rows.Add(row);
                }

                dataGridView1.DataSource = UserRentTable;
                dataGridView1.CellFormatting += CellColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
            }
        }
        private void CellColor(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Просрочка (дней)")
            {
                int overdueDays;
                if (int.TryParse(e.Value?.ToString(), out overdueDays))
                {
                    if (overdueDays > 0)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.Green;
                        e.CellStyle.ForeColor = Color.White;
                    }
                }
            }
        }


        private List<string[]> GetUserRentDisc(int userId)
        {
            List<string[]> UserRentDisc = new List<string[]>();

            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "SELECT Видеокасета.Номер_касеты, Видеокасета.Стоимость_видеокасеты, " +
                                   "Прокат.Дата_аренды, Прокат.Дата_возврата, GROUP_CONCAT(Фильм.Название, ', ') AS Названия_фильмов " +
                                   "FROM Видеокасета " +
                                   "INNER JOIN Фильм_на_касете ON Видеокасета.Номер_касеты = Фильм_на_касете.Видеокасета_Номер_касеты " +
                                   "INNER JOIN Фильм ON Фильм_на_касете.Фильм_Название = Фильм.Название " +
                                   "INNER JOIN Прокат ON Видеокасета.Номер_касеты = Прокат.Видеокасета_Номер_касеты " +
                                   "WHERE Прокат.Пользователь_ID = @UserId " +
                                   "GROUP BY Видеокасета.Номер_касеты, Видеокасета.Стоимость_видеокасеты, Прокат.Дата_аренды, Прокат.Дата_возврата";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string[] UserRent = new string[5];
                                UserRent[0] = reader["Номер_касеты"].ToString();
                                UserRent[1] = reader["Стоимость_видеокасеты"].ToString();
                                UserRent[2] = reader["Дата_аренды"].ToString();
                                UserRent[3] = reader["Дата_возврата"].ToString();
                                UserRent[4] = UserRent[4] = reader["Названия_фильмов"].ToString();

                                UserRentDisc.Add(UserRent); 
                            }

                        }

                        DatabaseConnection.CloseConnection(connection);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении данных из базы данных: " + ex.Message);
            }

            return UserRentDisc;
        }

        private int GetUserID(string PhoneNumber)
        {
            int UserId = 0;

            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "SELECT id FROM Пользователь WHERE Телефон = @phoneNumber";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@phoneNumber", PhoneNumber);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserId = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }

                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении id: " + ex.Message);
            }

            return UserId;
        }

        private void EndRent_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string cassetteNumber = selectedRow.Cells["Номер_касеты"].Value.ToString();

                try
                {
                    using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                    {
                        DatabaseConnection.OpenConnection(connection);

                        string deleteQuery = "DELETE FROM Прокат WHERE Видеокасета_Номер_касеты = @CassetteNumber";

                        using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                            deleteCommand.ExecuteNonQuery();
                        }

                        string updateQuery = "UPDATE Видеокасета SET Состояние = 1 WHERE Номер_касеты = @CassetteNumber";

                        using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                            updateCommand.ExecuteNonQuery();
                        }

                        DatabaseConnection.CloseConnection(connection);
                    }

                    dataGridView1.Rows.Remove(selectedRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении записи из базы данных: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите строку с видеокасетой для отмены аренды.");
            }
        }
    }
}
