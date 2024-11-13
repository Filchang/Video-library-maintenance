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
    public partial class AllDisc : Form
    {
        public string PhoneNumber { get; set; }
        public AllDisc()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Finder parentForm = new Finder();
            parentForm.Show();
            this.Close();
        }

        private void AllDisc_Load(object sender, EventArgs e)
        {
            
            DataTable cassetteTable = new DataTable();
            cassetteTable.Columns.Add("Номер_касеты");
            cassetteTable.Columns.Add("Стоимость");
            cassetteTable.Columns.Add("Фильмы");

            List<string[]> cassetteInfoList = GetCasseteInfo();

            foreach (string[] info in cassetteInfoList)
            {
                DataRow row = cassetteTable.NewRow();
                row["Номер_касеты"] = info[0];
                row["Стоимость"] = info[1];
                row["Фильмы"] = info[2];

                cassetteTable.Rows.Add(row);
            }

            dataGridView1.DataSource = cassetteTable;
        }

        private List<string[]> GetCasseteInfo()
        {
            List<string[]> cassetteInfoList = new List<string[]>();

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "SELECT Видеокасета.Номер_касеты, Видеокасета.Стоимость_видеокасеты, GROUP_CONCAT(Фильм.Название, ', ') AS Названия_фильмов " +
                               "FROM Видеокасета " +
                               "INNER JOIN Фильм_на_касете ON Видеокасета.Номер_касеты = Фильм_на_касете.Видеокасета_Номер_касеты " +
                               "INNER JOIN Фильм ON Фильм_на_касете.Фильм_Название = Фильм.Название " +
                               "WHERE Видеокасета.Состояние = '1' " +
                               "GROUP BY Видеокасета.Номер_касеты, Видеокасета.Стоимость_видеокасеты";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cassetteInfoArray = new string[3];
                            cassetteInfoArray[0] = reader["Номер_касеты"].ToString();
                            cassetteInfoArray[1] = reader["Стоимость_видеокасеты"].ToString();
                            cassetteInfoArray[2] = reader["Названия_фильмов"].ToString();
                            cassetteInfoList.Add(cassetteInfoArray);
                        }
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }

            return cassetteInfoList;
        }

        private void Rent_Click(object sender, EventArgs e)
        {
            List<string> cassetteNumbers = new List<string>();



            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string cassetteNumber = row.Cells["Номер_касеты"].Value.ToString();
                    cassetteNumbers.Add(cassetteNumber);
                }

           
                Rent rent = new Rent(this.Name);
                rent.DiscNumbers = cassetteNumbers;
                rent.PhoneNumber = PhoneNumber;
                rent.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Выберите строку для передачи номера кассеты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
