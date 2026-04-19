using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ERG21
{
    public partial class PresentationForm : BaseForm
    {
        private List<AppItem> items = new List<AppItem>();
        int counter = 0;
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        bool isSpeaking = false;

        public PresentationForm()
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Παρουσίαση";
        }
        public PresentationForm(List<AppItem> items)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.items = items;
            this.Shown += PresentationForm_Shown;
        }

        private void PresentationForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Παρουσίαση");
            }
        }
        private void PresentationForm_Shown(object sender, EventArgs e)
        {
            counter = 0;
            ShowCurrentPicture();
        }
        // Function που εμφανίζει την εικόνα, τίτλο και περιγραφή ανάλογα με το location
        private void ShowCurrentPicture()
        {
            if (items.Count == 0) return;

            this.BackgroundImage = Image.FromFile(items[counter].imagePath);
            label1.Text = items[counter].title;
            richTextBox1.Text = items[counter].fullDescription;
            this.Text = "MyTouristGuide.GR - " + items[counter].type;
        }


        // Function για να πάει στην προηγούμενη εικόνα
        private void button3_Click_1(object sender, EventArgs e)
        {
            // Ίδια λογική για το προηγούμενο στοιχείο, αλλά προσθέτουμε το pictureList.Count γιατί αν counter = 0 και αφαιρέσουμε το -1 θα έχουμε αρνητικό counter
            counter = (counter - 1 + items.Count) % items.Count;
            ShowCurrentPicture();
        }
        // Function για να πάει στην επόμενη εικόνα
        private void button4_Click_1(object sender, EventArgs e)
        {
            counter = (counter + 1) % items.Count;
            ShowCurrentPicture();
        }
        // Function για το κουμπί auto play που ενεργοποιεί την αυτόματη αναπαραγωγή με το timer 
        private void button5_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
                timer1_Tick(this, EventArgs.Empty);
            }
        }
        // Function για το timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Counter προσθέτει 1 και αν φτάσει στο τελευταιο στοιχείο (δλδ pictureList.Count) θα κάνει reset με το modulo γιατί πχ counter = (3 + 1) % 4 -> 0
            counter = (counter + 1) % items.Count;
            ShowCurrentPicture();
        }
        // Function για να κάνουμε save την περιγράφή της φωτογραφίας
        private void button6_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Role == "User")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }
            else
            {
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Επιλογή παραλιών
        private void button7_Click(object sender, EventArgs e)
        {
            // Παίρνουμε τα δεδομένα των παραλιών
            List<AppItem> beaches = DataProvider.GetBeaches();

            // Δημιουργούμε το PresentationForm με τη λίστα
            PresentationForm presentationForm = new PresentationForm(beaches);

            // Εμφανίζουμε το νέο form
            presentationForm.Show();
            this.Hide();
        }
        // Επιλογή μνημίων
        private void button8_Click(object sender, EventArgs e)
        {
            List<AppItem> attractions = DataProvider.GetAttractions();
            PresentationForm presentationForm = new PresentationForm(attractions);
            presentationForm.Show();
            this.Hide();
        }
        // Επιλογή εκκλησιών
        private void button9_Click(object sender, EventArgs e)
        {
            List<AppItem> churches = DataProvider.GetChurches();
            PresentationForm presentationForm = new PresentationForm(churches);
            presentationForm.Show();
            this.Hide();
        }
        // Επιλογή εστιατορίων
        private void button10_Click(object sender, EventArgs e)
        {
            List<AppItem> restaurants = DataProvider.GetRestaurants();
            PresentationForm presentationForm = new PresentationForm(restaurants);
            presentationForm.Show();
            this.Hide();
        }
        // Επιλογή μπαρ
        private void button11_Click(object sender, EventArgs e)
        {
            List<AppItem> bars = DataProvider.GetBars();
            PresentationForm presentationForm = new PresentationForm(bars);
            presentationForm.Show();
            this.Hide();
        }
        // Επιλογή ξενοδοχείων
        private void button12_Click_1(object sender, EventArgs e)
        {
            List<AppItem> hotels = DataProvider.GetHotels();
            PresentationForm presentationForm = new PresentationForm(hotels);
            presentationForm.Show();
            this.Hide();
        }
        // Επιλογή camping
        private void button13_Click_1(object sender, EventArgs e)
        {
            List<AppItem> campings = DataProvider.GetCamping();
            PresentationForm presentationForm = new PresentationForm(campings);
            presentationForm.Show();
            this.Hide();
        }
        // Function για text-to-speak
        private void button14_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Role == "User")
            {
                if (!isSpeaking)
                {
                    synthesizer.SelectVoice("Microsoft Stefanos");
                    synthesizer.SpeakAsync(richTextBox1.Text);
                    isSpeaking = true;
                }
                else
                {
                    synthesizer.SpeakAsyncCancelAll();
                    isSpeaking = false;
                }
            }
            else
            {
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // help button
        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Επιλέξτε μια από τις ενότητες που σας ενδιαφέρει (παραλίες, μνημεία, ξενοδοχεία κλπ) και δείτε μέσα από μια παρουσίαση όλες τις διαθέσιμες επιλογές μας." +
                "\n▶︎‖ έναρξη παρουσίασης - αυτόματη αλλαγή φωτογραφιών" +
                "\n← προηγούμενη φωτογραφία" +
                "\n→ επόμενη φωτογραφία" +
                "\n\nΟι εγγεγραμμένοι χρήστες μπορούν να αποθηκεύσουν τις πληροφορίες ή να χρησιμοποιήσουν τη λειτουργία text-to-speech", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
