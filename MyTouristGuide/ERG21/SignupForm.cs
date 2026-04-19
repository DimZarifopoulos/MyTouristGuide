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
    public partial class SignupForm : BaseForm
    {
        public SignupForm()
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Εγγραφή";
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Εγγραφή");
            }
        }
        // Μέθοδος για να κάνουμε hash το password πριν την αποθύκευση
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash); // Αποθηκεύουμε ως Base64 string
            }
        }
        // Εγγραφή
        private void button3_Click(object sender, EventArgs e)
        {
            // Καθαρίζουμε όλα τα προηγούμενα μηνύματα λάθους
            usernameErrorLabel.Text = "";
            passwordErrorLabel.Text = "";
            confirmPasswordErrorLabel.Text = "";
            emailErrorLabel.Text = "";
            phoneErrorLabel.Text = "";

            // Μεταβλητή για να παρακολουθούμε αν η φόρμα είναι έγκυρη
            bool isFormValid = true;

            // Ελέγχοι
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                usernameErrorLabel.Text = "Το πεδίο είναι υποχρεωτικό.";
                isFormValid = false;
            }
            // Έλεγχος για το μήκος του username
            else if (textBoxUsername.Text.Length <= 6)
            {
                usernameErrorLabel.Text = "Πάνω από 6 χαρακτήρες.";
                isFormValid = false;
            }

            // Έλεγχος για κενό password
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                passwordErrorLabel.Text = "Το πεδίο είναι υποχρεωτικό.";
                isFormValid = false;
            }

            // Έλεγχος αν οι κωδικοί είναι ίδιοι
            if (!textBoxPassword.Text.Equals(textBoxPassword2.Text))
            {
                confirmPasswordErrorLabel.Text = "Οι κωδικοί δεν ταιριάζουν.";
                isFormValid = false;
            }

            // Έλεγχος ότι το τηλέφωνο περιέχει μόνο αριθμούς
            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                phoneErrorLabel.Text = "Το πεδίο είναι υποχρεωτικό.";
                isFormValid = false;
            }
            else if (!textBoxPhone.Text.All(char.IsDigit))
            {
                phoneErrorLabel.Text = "Μόνο αριθμοί επιτρέπονται.";
                isFormValid = false;
            }

            // Έλεγχος για τη μορφή του email
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                emailErrorLabel.Text = "Το πεδίο είναι υποχρεωτικό.";
                isFormValid = false;
            }
            else
            {
                try
                {
                    // προσπαθούμε να δημιουργήσουμε email -> αν η μορφή είναι λάθος -> θα προκαλέσει σφάλμα.
                    var emailAddress = new System.Net.Mail.MailAddress(textBoxEmail.Text);
                }
                catch (FormatException)
                {
                    emailErrorLabel.Text = "Μη έγκυρη μορφή email.";
                    isFormValid = false;
                }
            }
            // Αν η φόρμα δεν είναι έγκυρη -> σταματάμε την εκτέλεση
            if (!isFormValid)
            {
                return;
            }

            String connectionString = "Data source=final.db;Version=3";

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            // Έλεγχος αν υπάρχει ήδη ο χρήστης
            string checkIfExistsQuery = "SELECT 1 FROM user WHERE username = @username OR email = @email OR phone = @phone LIMIT 1";
            using (SQLiteCommand checkCmd = new SQLiteCommand(checkIfExistsQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                checkCmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                checkCmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);

                using (SQLiteDataReader reader = checkCmd.ExecuteReader())
                {
                    // Αν true -> υπάρχει ήδη εγγραφή
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Υπάρχει ήδη λογαριασμός με αυτό το όνομα χρήστη, email ή τηλέφωνο.", "Σφάλμα Εγγραφής", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // μετατρέπουμε το password σε hashed για να έχουμε ασφάλεια στη βάση δεδομένων
            string hashedPassword = HashPassword(textBoxPassword.Text);

            SQLiteCommand command = new SQLiteCommand("Insert into user(username, password_hash, email, phone) " +
                "values(@username, @password_hash, @email, @phone)", connection);

            command.Parameters.AddWithValue("username", textBoxUsername.Text);
            command.Parameters.AddWithValue("password_hash", hashedPassword);
            command.Parameters.AddWithValue("email", textBoxEmail.Text);
            command.Parameters.AddWithValue("phone", textBoxPhone.Text);



            int count = command.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("Δημιουργία χρήστη επιτυχής!", "Επιτυχής Δημιουργία"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProfileForm profileForm = new ProfileForm();
                profileForm.Show();
                this.Hide();
            }
            command.Dispose();
            connection.Close();
        }
    }
}
