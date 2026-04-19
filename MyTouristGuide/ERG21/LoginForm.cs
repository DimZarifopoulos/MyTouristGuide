using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ERG21
{
    public partial class LoginForm : BaseForm
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Σύνδεση";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Σύνδεση");
            }
        }
        // Μέθοδος για να κάνουμε hash το password πριν την αποθήκευση
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        // Σύνδεση
        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            String connectionString = "Data source=final.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            // Κάνουμε πρώτα hash τον password και μετά τον συγκρίνουμε με τον hashed password που είναι ήδη αποθηκευμένος στη βάση μας
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM user WHERE username=@username AND password_hash=@password_hash", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password_hash", HashPassword(password));

            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                // γεμίζουμε το CurrentUser
                CurrentUser.Role = "User";
                CurrentUser.Id = reader.GetInt32(0);
                CurrentUser.Username = reader.GetString(1);
                CurrentUser.Email = reader.GetString(3);
                CurrentUser.phone = reader.GetString(4);

                MessageBox.Show("Επιτυχής είσοδος!", "Επιτυχής Είσοδος"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
                ProfileForm newForm = new ProfileForm();
                newForm.Show();
            }
            else
            {
                MessageBox.Show("Λάθος username ή password!", "Αποτυχία Σύνδεσης"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
