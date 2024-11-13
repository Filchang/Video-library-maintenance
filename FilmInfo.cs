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
    public partial class FilmInfo : Form
    {
        public List<string> FilmName { get; set; }
        public FilmInfo()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {

            Finder parentForm = new Finder();
            parentForm.Show();
           
            this.Close();
        }

        private void FilmInfo_Load(object sender, EventArgs e)
        {
            DataTable FilmTable = new DataTable();
            FilmTable.Columns.Add("Название");
            FilmTable.Columns.Add("Режиссер");
            FilmTable.Columns.Add("Год выпуска");
            FilmTable.Columns.Add("Продолжительность");
            FilmTable.Columns.Add("Рейтинг");
            FilmTable.Columns.Add("Жанр");

            foreach (string Film in FilmName)
            {
                string[] filmInfoArray = GetFilmInfoFromDatabase(Film);
                string Genre = GetGenreForFilm(Film);
                filmInfoArray[5] += Genre;

                
                if (filmInfoArray.Length == 6)
                {
                    FilmTable.Rows.Add(filmInfoArray);
                }
                else
                {
                    
                    Console.WriteLine($"Неправильное количество атрибутов для фильма {Film}");
                }
            }

            dataGridView1.DataSource = FilmTable;


        }
        private string[] GetFilmInfoFromDatabase(string FilmName)
        {

            string[] filmInfoArray = new string[6]; 

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = "SELECT Год_выпуска, Режиссер, Продолжительность," +
                    " Рейтинг FROM Фильм WHERE Название = @FilmName";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilmName", FilmName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            filmInfoArray[0] = FilmName;
                            filmInfoArray[1] = reader["Режиссер"].ToString();
                            filmInfoArray[2] = reader["Год_выпуска"].ToString();
                            filmInfoArray[3] = reader["Продолжительность"].ToString();
                            filmInfoArray[4] = reader["Рейтинг"].ToString();
                        }
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }

            return filmInfoArray;
        }
        private string GetGenreForFilm(string FilmName)
        {
            string genre = string.Empty;

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);

                string query = @"
                                SELECT f.Название, GROUP_CONCAT(g.Наименование_жанра, ', ') AS Жанры
                                FROM Фильм f 
                                JOIN Фильм_по_жанру fg ON f.Название = fg.Фильм_Название 
                                JOIN Жанр g ON fg.Жанр_Наименование_жанра = g.Наименование_жанра 
                                WHERE f.Название = @FilmName
                                GROUP BY f.Название";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilmName", FilmName);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            genre = reader["Жанры"].ToString();
                        }
                    }
                }

                DatabaseConnection.CloseConnection(connection);
            }

            return genre;
        }
    }
    
}
