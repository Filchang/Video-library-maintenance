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
    public partial class Enter : Form
    {
        
        
        public Enter()
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
            

            string number = textBox3.Text;

           

            string firstName = textBox1.Text;
            string lastName = textBox2.Text;

            if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
            if (number.Length == 11 && double.TryParse(number, out double parsedNumber) && number.All(char.IsDigit))
            {
                string query = "SELECT COUNT(*) FROM Пользователь WHERE Телефон=@Number AND Имя=@FirstName AND Фамилия=@LastName;";
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Number", number);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);

                        try
                        {
                            DatabaseConnection.OpenConnection(connection);
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            if (count > 0)
                            {
                                Finder finder = new Finder();
                                finder.PhoneNumber = number;
                                finder.UserNameSurName = textBox1.Text + " " + textBox2.Text;
                                finder.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Пользователь не найден. Зарегестрируйтесь");
                            }
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
            else 
            { 
                MessageBox.Show("Пожалуйста, введите корректный номер (11 цифр)."); 
            }
            

        }
        
        private void Enter_Load(object sender, EventArgs e)
        {

        }
    }
}

