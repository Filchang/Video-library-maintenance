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
    public partial class DiscPrice : Form
    {
        public int price;
        public string Operator;
        public DiscPrice()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Report parentForm = new Report();
            parentForm.Show();
            this.Close();
        }

        private void DiscPrice_Load(object sender, EventArgs e)
        {
            
            string query = $"SELECT Номер_касеты, Стоимость_видеокасеты FROM Видеокасета WHERE Стоимость_видеокасеты {Operator} @Price";

            
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Price", price);

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
