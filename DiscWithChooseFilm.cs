using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class DiscWithChooseFilm : Form
    {
        public string PhoneNumber { get; set; }
        public List<string> FilmName { get; set; }
        public DiscWithChooseFilm()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Finder parentForm = new Finder();
            parentForm.Show();
            this.Close();
        }

        private void DiscWithChooseFilm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataTable cassetteTable = new DataTable();
            cassetteTable.Columns.Add("Номер_касеты");
            cassetteTable.Columns.Add("Стоимость");
            cassetteTable.Columns.Add("Фильмы");

            List<string[]> cassetteInfoList = GetCassetteNumbersForFilms(FilmName);
            if (cassetteInfoList == null)
            {
                dataGridView1 = null;
            }
            else
            {
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
        }

        public List<string[]> GetCassetteNumbersForFilms(List<string> FilmName)
        {
            List<string[]> cassetteInfoList = new List<string[]>();

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "SELECT Видеокасета.Номер_касеты, Видеокасета.Стоимость_видеокасеты " +
                               "FROM Видеокасета " +
                               "INNER JOIN Фильм_на_касете ON Видеокасета.Номер_касеты = Фильм_на_касете.Видеокасета_Номер_касеты " +
                               "INNER JOIN Фильм ON Фильм_на_касете.Фильм_Название = Фильм.Название " +
                               "WHERE Фильм.Название = @FilmName AND Видеокасета.Состояние = 1";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    if (FilmName == null)
                    {
                        return null;
                    }
                    else
                    {
                        foreach (var filmName in FilmName)
                        {
                            command.Parameters.AddWithValue("@FilmName", filmName);


                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string[] cassetteInfoArray = new string[3];
                                    cassetteInfoArray[0] = reader["Номер_касеты"].ToString();
                                    cassetteInfoArray[1] = reader["Стоимость_видеокасеты"].ToString();
                                    cassetteInfoArray[2] = GetFilmsOnCassette(cassetteInfoArray[0]);
                                    cassetteInfoList.Add(cassetteInfoArray);
                                }
                            }
                        }
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }

            return cassetteInfoList;
        }

        public string GetFilmsOnCassette(string cassetteNumber)
        {
            string filmsOnCassette = string.Empty;

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = @"SELECT group_concat(Фильм.Название, ', ') AS Фильмы
                         FROM Фильм
                         INNER JOIN Фильм_на_касете ON Фильм.Название = Фильм_на_касете.Фильм_Название
                         WHERE Фильм_на_касете.Видеокасета_Номер_касеты = @CassetteNumber";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filmsOnCassette = reader["Фильмы"].ToString();
                        }
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }

            return filmsOnCassette;
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
