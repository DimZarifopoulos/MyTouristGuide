using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERG21
{
    public partial class ProfileForm : BaseForm
    {
        public ProfileForm()
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Προφίλ";
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user + αλλαγή προφίλ
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Προφίλ");


                // Απόκρυψη επιλογών επισκέπτη
                panel1.Visible = false;


                // Δημιουργία panel για τα στοιχεία
                Panel pnlProfile = new Panel
                {
                    Location = new Point(250, 100),
                    AutoSize = true,
                    BackColor = Color.White
                };

                // Δημιουργία labels
                Label lblTitle = new Label
                {
                    Text = "Στοιχεία Χρήστη",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    Location = new Point(70, 10)
                };

                Label lblName = new Label
                {
                    Text = $"Username: {CurrentUser.Username}",
                    Font = new Font("Arial", 14, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent,
                    Location = new Point(10, 50)
                };

                Label lblEmail = new Label
                {
                    Text = $"Email: {CurrentUser.Email}",
                    Font = new Font("Arial", 14, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent,
                    Location = new Point(10, 90)
                };

                Label lblPhone = new Label
                {
                    Text = $"Phone: {CurrentUser.phone}",
                    Font = new Font("Arial", 14, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent,
                    Location = new Point(10, 130)
                };

                Label lblRole = new Label
                {
                    Text = "Ρόλος: Εγγεγραμμένος Χρήστης",
                    Font = new Font("Arial", 14, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent,
                    Location = new Point(10, 170)
                };

                // Κουμπί αποσύνδεσης
                Button btnLogout = new Button
                {
                    Text = "Αποσύνδεση",
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Size = new Size(150, 40),
                    Location = new Point(10, 220),
                    BackColor = Color.WhiteSmoke,
                    ForeColor = Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };

                Button btnHistory = new Button
                {
                    Text = "Ιστορικό",
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Size = new Size(150, 40),
                    Location = new Point(170, 220),
                    BackColor = Color.WhiteSmoke,
                    ForeColor = Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };

                // Προσθήκη στο panel
                pnlProfile.Controls.Add(lblTitle);
                pnlProfile.Controls.Add(lblName);
                pnlProfile.Controls.Add(lblEmail);
                pnlProfile.Controls.Add(lblPhone);
                pnlProfile.Controls.Add(lblRole);
                pnlProfile.Controls.Add(btnLogout);
                pnlProfile.Controls.Add(btnHistory);

                // Προσθήκη panel στη φόρμα
                this.Controls.Add(pnlProfile);
                btnLogout.Click += btnLogout_Click;
                btnHistory.Click += btnHistory_Click;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        // σύνδεση
        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.Show();
            this.Hide();
        }
        // εγγραφή
        private void button4_Click(object sender, EventArgs e)
        {
            SignupForm form = new SignupForm();
            form.Show();
            this.Hide();
        }
        // κουμπί αποσύνδεσης
        private void btnLogout_Click(object sender, EventArgs e)
        {
            CurrentUser.Reset();
            MessageBox.Show("Αποσυνδεθήκατε με επιτυχία.", "Επιτυχής Αποσύνδεση"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);

            ProfileForm profile = new ProfileForm();
            profile.Show();
            this.Hide();
        }
        // Εμφάνιση ιστορικού
        private void btnHistory_Click(object sender, EventArgs e)
        {
            HistoryForm newForm = new HistoryForm();
            newForm.Show();
            this.Hide();
        }
    }
}
