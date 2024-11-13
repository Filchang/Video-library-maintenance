using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    
    public partial class Rent : Form
    {
        private string previousFormName;
        public List<string> DiscNumbers = new List<string>();
        public string PhoneNumber { get; set; }
        public Rent(string previousFormName)
        {
            InitializeComponent();
            this.previousFormName = previousFormName;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            int userId = GetUserID(PhoneNumber);
            string rentDate = GetTodayDate();
            string returnDate = textBox2.Text;
            if (PhoneNumber == null)
            {
                MessageBox.Show("Отсутствует номер телефона");
            }
            else
            {
                DateTime parsedDate;
                if (!DateTime.TryParseExact(returnDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    MessageBox.Show("Неверный формат даты\n(yyyy-MM-dd)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TimeSpan rentDuration = parsedDate - DateTime.ParseExact(rentDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    if (rentDuration.Days < 1 || rentDuration.Days > 10)
                    {
                        MessageBox.Show("Дата возврата должна быть не менее чем через 1 день и не более чем через 10 дней после даты аренды", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        AddRentalToDatabase(rentDate, returnDate, userId);
                        MessageBox.Show("Видеокасета успешно арендована");
                        UserRentDisc userrentdisc = new UserRentDisc();
                        userrentdisc.PhoneNumber = PhoneNumber;
                        userrentdisc.Show();
                        this.Close();
                    }
                }
            }
        }

        private void AddRentalToDatabase(string rentDate, string returnDate, int userId)
        {
            using (SQLiteConnection connection = DatabaseConnection.GetConnection())
            {
                DatabaseConnection.OpenConnection(connection);
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string insertQuery = "INSERT INTO Прокат (Дата_аренды, Дата_возврата, Видеокасета_Номер_касеты, Пользователь_id) " +
                                        "VALUES (@RentDate, @ReturnDate, @CassetteNumber, @UserId)";

                    string updateQuery = "UPDATE Видеокасета SET Состояние = 0 WHERE Номер_касеты = @CassetteNumber";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection, transaction))
                    {
                        foreach (string cassetteNumber in DiscNumbers)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                            command.Parameters.AddWithValue("@RentDate", rentDate);
                            command.Parameters.AddWithValue("@ReturnDate", returnDate);
                            command.Parameters.AddWithValue("@UserId", userId);
                            command.ExecuteNonQuery();

                            using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@CassetteNumber", cassetteNumber);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                }
                DatabaseConnection.CloseConnection(connection);
            }
        }
        private string GetTodayDate()
        {

            DateTime today = DateTime.Today;

            string formattedDate = today.ToString("yyyy-MM-dd");

            return formattedDate;
        }
        
        private void back_Click(object sender, EventArgs e)
        {
                    if (previousFormName == "AllDisc")
                    {
                        AllDisc allDiscForm = new AllDisc();
                        allDiscForm.Show();
                    }
                    else if (previousFormName == "DiscWithChooseFilm")
                    {
                        DiscWithChooseFilm discWithChooseFilmForm = new DiscWithChooseFilm();
                        discWithChooseFilmForm.Show();
                    }
            
                    else
                    {
               
                        MessageBox.Show("Не удалось определить предидущую форму");
                    }
                    this.Close(); 
        }
        private void Rent_Load(object sender, EventArgs e)
        {
            MessageBox.Show(PhoneNumber);
        }

        
        private int GetUserID(string PhoneNumber)
        {
            int UserId = 0;

            try
            {
                using (SQLiteConnection connection = DatabaseConnection.GetConnection())
                {
                    DatabaseConnection.OpenConnection(connection);

                    string query = "SELECT id FROM Пользователь WHERE Телефон = @phoneNumber";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@phoneNumber", PhoneNumber);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserId = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }

                    DatabaseConnection.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении id: " + ex.Message);
            }

            return UserId;
        }
    }
}
