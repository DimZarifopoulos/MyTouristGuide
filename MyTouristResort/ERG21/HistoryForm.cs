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
    public partial class HistoryForm : BaseForm
    {
        // Λίστα με όλα τα Forms
        List<string> allForms = new List<string>
        {
            "Αρχική Σελίδα", "Παραλίες", "Μνημεία", "Εκκλησίες/ Μοναστήρια", "Εστιατόρια", "Μπαρ",
            "Ξενοδοχεία", "Κάμπινκ", "Ιστορικό", "Καιρός", "Γκαλερί", "Βίντεο", "Συχνές ερωτήσεις", "Παρουσίαση",
            "Προφίλ", "Σύνδεση", "Εγγραφή", "Αναλυτική Παρουσίαση Παραλίες", "Αναλυτική Παρουσίαση Μνημεία",
            "Αναλυτική Παρουσίαση Εκκλησίες/ Μοναστήρια", "Αναλυτική Παρουσίαση Εστιατόρια", "Αναλυτική Παρουσίαση Μπαρ",
            "Αναλυτική Παρουσίαση Ξενοδοχεία", "Αναλυτική Παρουσίαση Κάμπινκ"
        };

        public HistoryForm()
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Ιστορικό";
        }
        private void HistoryForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("images/backgrounds/main.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Ιστορικό");
            }
            // Τίτλος
            Label lblTitle = new Label
            {
                Text = "Ιστορικό",
                Font = new Font("Arial", 16, FontStyle.Bold),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(320, 25) // πάνω από το panel
            };
            this.Controls.Add(lblTitle);

            // Panel όπου θα εμφανίζονται όλα
            Panel pnlHistory = new Panel
            {
                AutoScroll = true,
                Size = new Size(450, 310),
                Location = new Point(170, 60),
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(pnlHistory);

            int y = 10;

            // Δημιουργούμε label για κάθε φόρμα
            foreach (string formName in allForms)
            {
                Label lbl = new Label
                {
                    Text = formName,
                    AutoSize = false,
                    Width = 400,
                    Height = 30,
                    Location = new Point(10, y),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5),
                };


                // Αλλαγή χρώματος ανάλογα αν την έχει επισκεφτεί
                if (CurrentUser.History.Contains(formName))
                {
                    lbl.BackColor = Color.LightGreen;   // έχει επισκεφτεί
                }
                else
                {
                    lbl.BackColor = Color.Transparent; // δεν έχει επισκεφτεί
                }

                pnlHistory.Controls.Add(lbl);
                y += 35;
            }
        }

        // κουμπί για διαγραφή ιστορικού
        private void button3_Click(object sender, EventArgs e)
        {
            CurrentUser.History.Clear();
            MessageBox.Show("Το ιστορικό διαγράφηκε.", "Διαγραφή Ιστορικού"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            HistoryForm historyForm = new HistoryForm();    // reload το form
            historyForm.Show();
            this.Hide();
        }
        // Help button
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εδώ μπορείτε να βρείτε όλες τις σελίδες που έχετε επισκεφτεί στην εφαρμογή μας", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
