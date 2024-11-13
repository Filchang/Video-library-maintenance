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
using System.Windows.Forms.VisualStyles;

namespace Курсовая
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin parentForm = new Admin();
            parentForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime Date;

            if (!DateTime.TryParse(textBox1.Text, out Date))
            {
                MessageBox.Show("Пожалуйста, введите корректную дату.");
                return;

            }
            else
            {
                DiscReturnAndUserForDate date = new DiscReturnAndUserForDate();
                date.Date = textBox1.Text;
                date.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите жанр");
            }
            else
            {
                FilmOnGenre filmOnGenre = new FilmOnGenre();
                filmOnGenre.genre = comboBox1.Text;
                filmOnGenre.Show();
                this.Hide();
            }
        }

        private void Report_Load(object sender, EventArgs e)
        {

            string query = "SELECT Наименование_жанра FROM Жанр";
            

            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        DatabaseConnection.OpenConnection(connection);
                        SQLiteDataReader reader = command.ExecuteReader();

                        comboBox1.Items.Clear();

                        while (reader.Read())
                        {
                            string genreName = reader["Наименование_жанра"].ToString();
                            comboBox1.Items.Add(genreName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                    
                }
                string userQuery = "SELECT Имя || ' ' || Фамилия || ' ' || Паспорт AS UserDetails FROM Пользователь";

                
                using (SQLiteCommand userCommand = new SQLiteCommand(userQuery, connection))
                {
                    try
                    {
                        SQLiteDataReader userReader = userCommand.ExecuteReader();

                        comboBox3.Items.Clear();

                        while (userReader.Read())
                        {
                            string userDetails = userReader["UserDetails"].ToString();
                            comboBox3.Items.Add(userDetails);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }

                }
             
                 DatabaseConnection.CloseConnection(connection);
                
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int price;
            if (!int.TryParse(textBox2.Text, out price))
            {
                MessageBox.Show("Введите корректную цену.");
                return;
            }
            else
            {
                string condition = comboBox2.SelectedItem.ToString();
                string Operator = "";
                switch (condition)
                {
                    case "Равно":
                        Operator = "=";
                        break;
                    case "Меньше":
                        Operator = "<";
                        break;
                    case "Меньше или равно":
                        Operator = "<=";
                        break;
                    case "Больше":
                        Operator = ">";
                        break;
                    case "Больше или равно":
                        Operator = ">=";
                        break;
                    default:
                        MessageBox.Show("Выберите корректное условие.");
                        return;
                }

                DiscPrice discprice = new DiscPrice();
                discprice.Operator = Operator;
                discprice.price = int.Parse(textBox2.Text);
                discprice.Show();
                this.Hide();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            if (comboBox3.SelectedItem != null)
            {
                string UserDetails = comboBox3.SelectedItem.ToString();
                string Passport = UserDetails.Split(' ')[2]; // Извлекаем третий элемент (паспорт)
                CheckRental rental = new CheckRental();
                rental.Passport = Passport;
                rental.Show();
                this.Hide();
            }
            
        }
    
    }
}
