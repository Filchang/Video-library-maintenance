using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class Finder : Form
    {
       
        public string UserNameSurName { get; set; }
        public string PhoneNumber { get; set; }

        public Finder()
        {
            InitializeComponent();
            
        }

        private void back_Click(object sender, EventArgs e)
        {
            User parentForm = new User();
            parentForm.Show();
            this.Close();
        }

        private void Finder_Load(object sender, EventArgs e)
        {
            
                AddAllFilmOnLstBox();
                AutoCompleteTxt1();
                LoadColumnNames();
                label1.Text = UserNameSurName;
            
            

        }
        private void LoadColumnNames()
        {
            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    // Запрос к базе данных для выбора названий колонок из таблицы Фильм
                    string query = "PRAGMA table_info(Фильм)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Пропускаем 3 столбец
                                if (reader.GetInt32(0) == 3)
                                    continue;

                                string columnName = reader["name"].ToString();
                                comboBox1.Items.Add(columnName);
                            }
                        }
                    }

                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Ошибка загрузки данных названий столбцов: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string columnName = comboBox1.SelectedItem.ToString();
                string searchText = textBox1.Text;

                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "";


                    if (columnName == "Рейтинг")
                    {

                        Convert.ToDouble(searchText);
                        query = $"SELECT Название FROM Фильм WHERE Рейтинг >= @SearchText";
                    }
                    else if (columnName == "Жанр")
                    {

                        query = "SELECT Фильм.Название FROM Фильм " +
                                "INNER JOIN Фильм_по_жанру ON Фильм.Название = Фильм_по_жанру.Фильм_Название " +
                                "INNER JOIN Жанр ON Фильм_по_жанру.Жанр_Наименование_жанра = Жанр.Наименование_жанра " +
                                "WHERE Жанр.Наименование_жанра LIKE '%' || @SearchText || '%'; ";

                    }
                    else
                    {
                        
                        query = $"SELECT Название FROM Фильм WHERE (@ColumnName = '{columnName}' AND {columnName} LIKE '%' || @SearchText || '%')";
                    }

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        if (columnName == "Год_выпуска" && searchText.Length < 4)
                        {

                            listBox1.Items.Clear();
                        }
                        else
                        {
                            if (columnName == "Рейтинг")
                            {

                                command.Parameters.AddWithValue("@SearchText", Convert.ToDouble(searchText));
                            }

                            else
                            {

                                command.Parameters.AddWithValue("@ColumnName", columnName);
                                command.Parameters.AddWithValue("@SearchText", searchText);
                            }

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                listBox1.Items.Clear();
                                while (reader.Read())
                                {
                                    listBox1.Items.Add(reader["Название"].ToString());
                                }
                            }
                        }
                    }
                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Ошибка: " + ex.Message);
            }

        }
        private void AutoCompleteTxt1()
        {
            

            // Создаем источник данных для автозаполнения
            AutoCompleteStringCollection autoCompleteMovies = new AutoCompleteStringCollection();
            AutoCompleteStringCollection autoCompleteDirectors = new AutoCompleteStringCollection();

            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string queryFilms = "SELECT Название FROM Фильм";
                 
                    string queryDirectors = "SELECT DISTINCT Режиссер FROM Фильм";

                    
                    switch (comboBox1.SelectedItem?.ToString())
                    {
                        case "Режиссер":
                        {
                            using (SQLiteCommand command = new SQLiteCommand(queryDirectors, connection))
                            using (SQLiteDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                    autoCompleteDirectors.Add(reader["Режиссер"].ToString());

                            textBox1.AutoCompleteCustomSource = autoCompleteDirectors;
                            break;
                        }
                        case "Название":
                        {
                            using (SQLiteCommand command = new SQLiteCommand(queryFilms, connection))
                            using (SQLiteDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                    autoCompleteMovies.Add(reader["Название"].ToString());

                            textBox1.AutoCompleteCustomSource = autoCompleteMovies;
                            break;
                        }
                       
                            default: { break; }
                    }

                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void AddAllFilmOnLstBox()
        {
            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);


                    string query = "SELECT Название FROM Фильм";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            listBox1.Items.Clear();
                            while (reader.Read())
                            {
                                listBox1.Items.Add(reader["Название"].ToString());
                            }
                        }
                    }

                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка загрузки фильмов: " + ex.Message);
            }
        }

        private void Film_info_Click(object sender, EventArgs e)
        {
            var selectedItems = listBox1.SelectedItems;

            FilmInfo infoForm = new FilmInfo();

            infoForm.FilmName = selectedItems.Cast<string>().ToList();

            infoForm.Show();
            this.Hide();
        }

        private void All_Open_disc_Click(object sender, EventArgs e)
        {
            AllDisc allDisc = new AllDisc();
            allDisc.PhoneNumber = PhoneNumber;
            allDisc.Show();
            this.Hide();
        }

        private void MyRent_Click(object sender, EventArgs e)
        {
            UserRentDisc UserRentDisc = new UserRentDisc();
            UserRentDisc.PhoneNumber = PhoneNumber;
            UserRentDisc.Show();
            this.Hide();
        }

        private void open_disc_WithFilm_Click(object sender, EventArgs e)
        {
            var selectedItems = listBox1.SelectedItems;
            DiscWithChooseFilm DiscWithChooseFilm = new DiscWithChooseFilm();
            DiscWithChooseFilm.PhoneNumber = PhoneNumber;
            DiscWithChooseFilm.FilmName = selectedItems.Cast<string>().ToList();
            DiscWithChooseFilm.Show();
            this.Hide();
        }
    }
}
