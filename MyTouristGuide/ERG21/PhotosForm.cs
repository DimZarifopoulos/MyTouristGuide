using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ERG21
{
    public partial class PhotosForm : BaseForm
    {
        public PhotosForm()
        {
            InitializeComponent();
            this.Load += PhotosForm_Load;
            this.Text = "MyTouristGuide.GR - Γκαλερί";
        }

        private void PhotosForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Γκαλερί");
            }

            string folderPath = @"images/gallery/pictures";
            string[] imageFiles = Directory.GetFiles(folderPath, "*.png");

            // Δημιουργία TableLayoutPanel
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                ColumnCount = 3,
                RowCount = (int)Math.Ceiling(imageFiles.Length / 3.0), // στρογγυλοποιεί
            };

            this.Controls.Add(table);

            int col = 0;
            int row = 0;

            // Για κάθε εικόνα png που υπάρχει -> δημιουργούμε ενα PictureBox και το προσθέτουμε στο table
            foreach (string file in imageFiles)
            {
                PictureBox pic = new PictureBox
                {
                    Image = Image.FromFile(file),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                    Size = new Size(270, 120),
                    Margin = new Padding(5),
                };

                // Τοποθετεί την εικόνα στο table στη θέση col / row 
                table.Controls.Add(pic, col, row);

                // αυξάνει το col για να πάει στην επόμενη στήλη - αν πάνω από 3 το μηδενίζει
                col++;
                if (col >= table.ColumnCount)
                {
                    col = 0;
                    row++;
                }
            }
        }
        // Κουμπί για βίντεο
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentUser.Role == "User")
            {
                VideosForm videos = new VideosForm();
                videos.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Αυτή η λειτουργία είναι διαθέσιμη μόνο για εγγεγραμμένους χρήστες.", "Περιορισμένες Λειτουργίες"
                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        // help button
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εδώ μπορείτε να βρείτε όλες τις φωτογραφίες της εφαρμογής μας!\nΟι εγγεγραμμένοι χρήστες έχουν τη δυνατότητα να δουν και βίντεο με το πάτημα του κουμπίου της κάμερας.", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
