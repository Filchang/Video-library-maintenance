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
    public partial class CheckRental : Form
    {
        public string Passport;
        public CheckRental()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Report parentForm = new Report();
            parentForm.Show();
            this.Close();
        }

        private void CheckRental_Load(object sender, EventArgs e)
        {

            {
                string query = @"SELECT 
                Пользователь.Имя, 
                Пользователь.Фамилия, 
                Пользователь.Паспорт,
                Прокат.Дата_аренды,
                Прокат.Дата_возврата,
                Видеокасета.Номер_касеты
            FROM 
                Прокат
            INNER JOIN 
                Пользователь ON Прокат.Пользователь_id = Пользователь.id
            INNER JOIN 
                Видеокасета ON Прокат.Видеокасета_Номер_касеты = Видеокасета.Номер_касеты
            WHERE 
                Пользователь.Паспорт = @Паспорт";

                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Паспорт", Passport);

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
}
