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
    public partial class BaseForm : Form
    {
        // Απλός constructor
        public BaseForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Σύνδεση event PrintPage με τη μέθοδο PrintDocument1_PrintPage -> όταν η εκτύπωση ζητάει μια σελίδα, καλούμε αυτή τη μέθοδο
            printDocument1.PrintPage += PrintDocument1_PrintPage;
        }
        // Constructor για να μπορείς να περάσεις λίστα δεδομένων List<AppItem>
        public BaseForm(List<AppItem> items)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            printDocument1.PrintPage += PrintDocument1_PrintPage;
        }

        // Menu strip - Εφαρμογή
        private void αρχικήΣελίδαToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm newMain = new MainForm();
            newMain.Show();

        }
        // Εκτύπωση - έλεγχος
        private void εκτύπωσηToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Role == "User")
            {
                PrintCurrentForm();
            }
            else
            {
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Προεπισκόπηση εκτύπωσης
        private void PrintCurrentForm()
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Δημιουργούμε ένα bitmap με όλο το περιεχόμενο του form
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

            // Ζωγραφίζουμε το bitmap στη σελίδα του εκτυπωτή
            e.Graphics.DrawImage(bmp, 0, 0);
        }
        // Έξοδος
        private void έξοδοςToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Menu strip - Αξιοθέατα
        private void παραλίεςToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParaliesForm form = new ParaliesForm();
            form.Show();
            this.Hide();
        }
        private void ιστορικάΜνημείαToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MnimiaForm form = new MnimiaForm();
            form.Show();
            this.Hide();
        }
        private void εκκλησίεςΜοναστήριαToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EklisiesForm form = new EklisiesForm();
            form.Show();
            this.Hide();
        }
        // Menu strip - Φαγητό
        private void εστιατόριαToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestaurantsForm form = new RestaurantsForm();
            form.Show();
            this.Hide();
        }
        private void μπαρΚαφέToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarsForm form = new BarsForm();
            form.Show();
            this.Hide();
        }
        // Menu strip - Διαμονή
        private void ξενοδοχείαToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotelsForm form = new HotelsForm();
            form.Show();
            this.Hide();
        }
        private void campingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CampingForm form = new CampingForm();
            form.Show();
            this.Hide();
        }
        // Menu strip - Φωτογραφίες
        private void φωτογραφίεςToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // δεν κάνει τίποτα απλά δεν μπορούσα να το αφαιρέσω
        }
        private void γκαλερίToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhotosForm form = new PhotosForm();
            form.Show();
            this.Hide();
        }
        private void παρουσίασηToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresentationForm form = new PresentationForm(DataProvider.GetBeaches());
            form.Show();
            this.Hide();
        }
        // Menu strip - Πληροφορίες
        private void ιστορικόToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Role == "User")
            {
                HistoryForm newForm = new HistoryForm();
                newForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void καιρόςLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Role == "User")
            {
                WeatherForm form = new WeatherForm();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void συχνέςΕρωτήσειςFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FaqForm form = new FaqForm();
            form.Show();
            this.Hide();
        }
        // Menu strip - Σχετικά
        private void ποιοίΕίμαστεToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Καλώς ήρθατε στο MyTouristGuide.GR!\nΤον νούμερο 1 τουριστικό βοηθό της Ελλάδας!\n" +
                "\nΕίμαστε μια ομάδα με CEO τον Ζαριφόπουλο Δημήτρη, έχουμε 28 χρόνια εμπειρίας και μοναδικό στόχο να παρέχουμε τις πιο χρήσιμες προτάσεις και συμβουλές για το ταξίδι σας στη Κρήτη." +
                "\nΜπορείτε να βρείτε:\n-Tις πιο γνωστές παραλίες, αξιοθέατα και μοναστήρια,\n-Tα πιο in εστιατόρια και μπαρ καθώς και\n-Επιλογές διαμονής για κάθε budget.", "About"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void έλεγχοςΕνημερώσεωνToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Έλεγχος ενημερώσεων...\nΠατήστε 'ΟΚ' για να συνεχίσετε", "Ενημερώσεις"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Συγχαρητήρια!\nΈχετε την τελευταία έκδοση της εφαρμογής!", "Ενημερώσεις"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Email
        private void επικοινωνίαToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string recipient = "info@mytouristguide.gr"; // το email της εφαρμογής
            string subject = Uri.EscapeDataString("Επικοινωνία από την εφαρμογή");
            string body = Uri.EscapeDataString("Καλησπέρα,\n\nΣας στέλνω αυτό το email μέσω της εφαρμογής MyTouristGuide.GR."); // default εισαγωγικό email 

            try
            {
                System.Diagnostics.Process.Start($"mailto:{recipient}?subject={subject}&body={body}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Αποτυχία ανοίγματος του προγράμματος email.\n" + ex.Message, "Αποτυχία"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Κουμπιά login, exit, about
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Καλώς ήρθατε στο MyTouristGuide.GR!\nΤον νούμερο 1 τουριστικό βοηθό της Ελλάδας!\n" +
                "\nΕίμαστε μια ομάδα με CEO τον Ζαριφόπουλο Δημήτρη, έχουμε 28 χρόνια εμπειρίας και μοναδικό στόχο να παρέχουμε τις πιο χρήσιμες προτάσεις και συμβουλές για το ταξίδι σας στη Κρήτη." +
                "\nΜπορείτε να βρείτε:\n-Tις πιο γνωστές παραλίες, αξιοθέατα και μοναστήρια,\n-Tα πιο in εστιατόρια και μπαρ καθώς και\n-Επιλογές διαμονής για κάθε budget.", "About"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void προφίλToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileForm form = new ProfileForm();
            form.Show();
            this.Hide();
        }
    }
}
