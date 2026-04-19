using System;
using System.Drawing;
using System.Windows.Forms;

namespace ERG21
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Αρχική Σελίδα";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Αρχική Σελίδα");
            }

            // Background
            this.BackgroundImage = Image.FromFile("images/backgrounds/main.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //this.panel1.BackColor = Color.White; // ημιδιαφανές

            // Τίτλος
            Label lblTitle = new Label
            {
                Text = "MyTouristGuide.GR",
                Font = new Font("Arial", 28, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(50, 50)
            };
            this.Controls.Add(lblTitle);

            // Περιγραφή
            Label lblDescription = new Label
            {
                Text = "Ο απόλυτος τουριστικός οδηγός για την Κρήτη!\nΑνακαλύψτε παραλίες, μνημεία, εκκλησίες, ταβέρνες, μπαρ και καταλύματα.",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(50, 110)
            };
            this.Controls.Add(lblDescription);
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        // Παραλίες
        private void button3_Click(object sender, EventArgs e)
        {
            ParaliesForm paraliesForm = new ParaliesForm();
            paraliesForm.Show();
            this.Hide();
        }
        // Μνημεία
        private void button4_Click(object sender, EventArgs e)
        {
            MnimiaForm mnimiaForm = new MnimiaForm();
            mnimiaForm.Show();
            this.Hide();
        }
        // Εκκλησίες
        private void button5_Click(object sender, EventArgs e)
        {
            EklisiesForm eklisiesForm = new EklisiesForm();
            eklisiesForm.Show();
            this.Hide();
        }
        // Εστιατόρια
        private void button6_Click(object sender, EventArgs e)
        {
            RestaurantsForm form = new RestaurantsForm();
            form.Show();
            this.Hide();
        }
        // Μπαρ
        private void button7_Click(object sender, EventArgs e)
        {
            BarsForm barsForm = new BarsForm();
            barsForm.Show();
            this.Hide();
        }
        // Ξενοδοχεία
        private void button8_Click(object sender, EventArgs e)
        {
            HotelsForm hotelsForm = new HotelsForm();
            hotelsForm.Show();
            this.Hide();
        }
        // Κάμπινκ
        private void button9_Click(object sender, EventArgs e)
        {
            CampingForm campingForm = new CampingForm();
            campingForm.Show();
            this.Hide();
        }
        // Προφίλ
        private void button10_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm();
            profileForm.Show();
            this.Hide();
        }
        // Γκαλερί
        private void button11_Click(object sender, EventArgs e)
        {
            PhotosForm photosForm = new PhotosForm();
            photosForm.Show();
            this.Hide();
        }
    }
}
