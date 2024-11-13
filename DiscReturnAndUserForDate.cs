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
    public partial class DiscReturnAndUserForDate : Form
    {
        public string Date;
        public DiscReturnAndUserForDate()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Report parentForm = new Report();
            parentForm.Show();
            this.Close();
        }

        private void DiscReturnAndUserForDate_Load(object sender, EventArgs e)
        {
            string query = "SELECT Пользователь.Имя, Пользователь.Фамилия, Видеокасета.Номер_касеты, Прокат.Дата_возврата " +
                            "FROM Прокат " +
                            "INNER JOIN Пользователь ON Прокат.Пользователь_id = Пользователь.id " +
                            "INNER JOIN Видеокасета ON Прокат.Видеокасета_Номер_касеты = Видеокасета.Номер_касеты " +
                            "WHERE Прокат.Дата_возврата <= @Date";

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", Date);

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
