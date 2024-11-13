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
    public partial class Registr : Form
    {
        public Registr()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            User parentForm = new User();
            parentForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string number = textBox1.Text;
            string Name = textBox2.Text;
            string surName = textBox3.Text;
            string passport = textBox4.Text;

            if (!string.IsNullOrEmpty(number) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(surName) && !string.IsNullOrEmpty(passport))
            {
                if (number.Length == 11 && double.TryParse(number, out double parsedNumber) && number.All(char.IsDigit))
                {
                    if (passport.Length == 10 && double.TryParse(passport, out double parsedPassport) && passport.All(char.IsDigit))
                    {
                        using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                        {
                            DatabaseConnection.OpenConnection(connection);
                            string query = "SELECT * FROM Пользователь WHERE Телефон = @Number OR Паспорт = @Passport";

                            using (SQLiteCommand command = new SQLiteCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Number", parsedNumber);
                                command.Parameters.AddWithValue("@Passport", parsedPassport);

                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        reader.Read();
                                        double existingNumber = reader.GetDouble(reader.GetOrdinal("Телефон"));
                                        double existingPassport = reader.GetDouble(reader.GetOrdinal("Паспорт"));

                                        if (existingNumber == parsedNumber)
                                        {
                                            MessageBox.Show("Пользователь с таким номером уже существует.");
                                        }
                                        else if (existingPassport == parsedPassport)
                                        {
                                            MessageBox.Show("Пользователь с таким паспортом уже существует.");
                                        }
                                    }
                                    else
                                    {
                                        query = "INSERT INTO Пользователь (Телефон, Фамилия, Имя, Паспорт) VALUES (@Number, @FirstName, @LastName, @Passport)";

                                        using (SQLiteCommand command2 = new SQLiteCommand(query, connection))
                                        {
                                            command2.Parameters.AddWithValue("@Number", parsedNumber);
                                            command2.Parameters.AddWithValue("@FirstName", Name);
                                            command2.Parameters.AddWithValue("@LastName", surName);
                                            command2.Parameters.AddWithValue("@Passport", parsedPassport);

                                            int rowsAffected = command2.ExecuteNonQuery();

                                            if (rowsAffected > 0)
                                            {
                                                MessageBox.Show("Пользователь успешно зарегистрирован.");
                                                Enter enter = new Enter();
                                                enter.Show();
                                                this.Hide();

                                            }
                                            else
                                            {
                                                MessageBox.Show("Ошибка при регистрации пользователя.");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, введите корректный паспорт (10 цифр).");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер (11 цифр).");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }
        }

        private void Registr_Load(object sender, EventArgs e)
        {

        }
    }
}

