using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace ERG21
{
    // Φόρμα όπου δημιουργεί τις λεπτομέρειες ενός σημείου ενδιαφέροντος
    public partial class ItemDetailsForm : BaseForm
    {
        private AppItem item;
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        bool isSpeaking = false;

        public ItemDetailsForm(AppItem item)
        {
            InitializeComponent();
            this.item = item;
            this.Text = "MyTouristGuide.GR - " + item.type;

            SetupUI();
        }
        private void ItemDetailsForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Αναλυτική Παρουσίαση " + item.type);
            }
        }
        private void SetupUI()
        {
            this.Size = new Size(850, 550);

            // Background image στο Form
            this.BackgroundImage = Image.FromFile(item.imagePath);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Label lblTitle = new Label
            {
                Text = item.title,
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(20, 30),
                Size = new Size(800, 30),
                BackColor = Color.FromArgb(120, Color.White),
            };

            this.richTextBox1.Text = item.fullDescription;
            this.Controls.Add(lblTitle);

        }
        // Speak
        private void button3_Click(object sender, EventArgs e)
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
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        // Save
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
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Help button
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εδώ μπορείτε να βρείτε περισσοτερες πληροφορίες για το σημείο ενδιαφέροντος που επιλέξατε." +
                "\n\nΟι εγγεγραμμένοι χρήστες μπορούν να αποθηκεύσουν τις πληροφορίες ή να χρησιμοποιήσουν τη λειτουργία text-to-speech", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
