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
    public partial class FaqForm : BaseForm
    {
        public FaqForm()
        {
            InitializeComponent();

            // Συνδέει τη FaqForm_Load με το event Load
            this.Load += FaqForm_Load;
            this.Text = "MyTouristGuide.GR - Συχνές ερωτήσεις";
        }

        private void FaqForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Συχνές ερωτήσεις");
            }

            // Panel όπου θα μπει κάθε ερώτηση
            Panel panel = new Panel
            {
                Size = new Size(686, 357),
                Location = new Point(12, 81),
                AutoScroll = true
            };
            // Προσθήκη panel πάνω στη φόρμα
            this.Controls.Add(panel);

            // Προσθήκη FAQs
            AddFaq(panel, "Τι είναι το MyTouristGuide;",
                "Το MyTouristGuide είναι μια εφαρμογή που συγκεντρώνει παραλίες, αξιοθέατα, μοναστήρια, εστιατόρια, μπαρ και επιλογές διαμονής στην Κρήτη.");

            AddFaq(panel, "Πώς μπορώ να βρω τις καλύτερες παραλίες;",
                "Στο μενού Αξιοθέατα → Παραλίες θα βρείτε λίστα με τις πιο γνωστές παραλίες της Κρήτης, με περιγραφές και φωτογραφίες.");

            AddFaq(panel, "Υπάρχουν πληροφορίες για ξενοδοχεία;",
                "Ναι, στο μενού Διαμονή μπορείτε να δείτε ξενοδοχεία, camping και άλλες επιλογές για κάθε budget.");

            AddFaq(panel, "Μπορώ να δω φωτογραφίες και βίντεο;",
                "Στο μενού Φωτογραφίες → Γκαλερί θα βρείτε φωτογραφίες, ενώ στο Βίντεο μπορείτε να δείτε σχετικά βίντεο μέσα από την εφαρμογή.");

            AddFaq(panel, "Υπάρχει η δυνατότητα να αποθηκεύσω τις πληροφορίες για ένα αξιοθέατο;",
                "Φυσικά! Αφού κλικάρετε το αξιοθέατο που σας ενδιαφέρει → θα δείτε ένα κουμπί με το εικονίδιο μιας δισκέτας.");

            AddFaq(panel, "Μπορώ να φτιάξω λογαριασμό;",
                "Φυσικά! Από το μενού Προφίλ → Εγγραφή.");

            AddFaq(panel, "Τι διαφορά έχει με τον απλό επισκέπτη;",
                "Ένας εγγεγραμμένος χρήστης έχει πολλές περισσότερες δυνατότητες από έναν απλό επισκέπτη." +
                " Ο επισκέπτης δεν μπορεί να αποθηκεύσει ή να εκτυπώσει τις πληροφορίες ενός αξιοθέατου. Επίσης, δεν έχει πρόσβαση στα βίντεο, το text-to-speech αλλά ούτε στο ιστορικό!");

            AddFaq(panel, "Γιατί δεν μπορώ να δω τα βίντεο;",
                "Δυστυχώς, μόνο οι εγγεγραμμένοι χρήστες μπορούν να δούν τα βίντεο.");

            AddFaq(panel, "Υπάρχει ιστορικό;",
                "Φυσικά! ");
        }
        // Μεταβλητή που κρατάει το ύψος όπου θα τοποθετηθεί το επόμενο GroupBox
        // με κάθε ερώτηση -> αυξάνεται κατά 10 
        private int currentY = 10;

        private void AddFaq(Panel panel, string question, string answer)
        {
            GroupBox groupBox = new GroupBox
            {
                Text = question,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Width = panel.Width - 40,
                Height = 120,
                Top = currentY,
                Left = 10,
            };

            Label lblAnswer = new Label
            {
                Text = answer,
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                MaximumSize = new Size(groupBox.Width - 20, 0),
                Location = new Point(10, 25)
            };

            groupBox.Controls.Add(lblAnswer);
            panel.Controls.Add(groupBox);

            currentY += groupBox.Height + 10;
        }
        // Help button
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εδώ μπορείτε να βρείτε τις πιο συχνές ερωτήσεις για τη λειτουργία της εφαρμογής.", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
