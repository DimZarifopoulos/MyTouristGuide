using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ERG21
{
    public partial class VideosForm : BaseForm
    {
        private IWMPPlaylist playlist;
        public VideosForm()
        {
            InitializeComponent();

            // Σύνδεση του event SelectedIndexChanged του listBox1 με τη μέθοδο listBox1_SelectedIndexChanged.
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.Load += VideoForm_Load;

            // Σύνδεση του event VisibleChanged με τη μέθοδο VideosForm_VisibleChanged
            this.VisibleChanged += VideosForm_VisibleChanged;
            this.Text = "MyTouristGuide.GR - Βίντεο";

        }
        private void VideoForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Βίντεο");
            }

            string folderPath = @"images/gallery/videos";

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("Ο φάκελος δεν υπάρχει: " + folderPath);
                return;
            }

            string[] videoFiles = Directory.GetFiles(folderPath, "*.mp4");

            // Δημιουργία νέας playlist
            playlist = axWindowsMediaPlayer1.newPlaylist("MyVideos", "");

            foreach (string file in videoFiles)
            {
                IWMPMedia media = axWindowsMediaPlayer1.newMedia(file);
                playlist.appendItem(media);

                listBox1.Items.Add(Path.GetFileName(file));
            }

            axWindowsMediaPlayer1.currentPlaylist = playlist;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
        // Μέθοδος για να αλλάζει το βίντεο από το listbox1
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                axWindowsMediaPlayer1.Ctlcontrols.playItem(
                    playlist.get_Item(listBox1.SelectedIndex)
                );
            }
        }
        // Επόμενο βίντεο
        private void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = listBox1.SelectedIndex;

            if (currentIndex < playlist.count - 1)
            {
                listBox1.SelectedIndex = currentIndex + 1;
                axWindowsMediaPlayer1.Ctlcontrols.playItem(
                    playlist.get_Item(listBox1.SelectedIndex)
                );
            }
        }
        // Προηγούμενο βίντεο
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int currentIndex = listBox1.SelectedIndex;

            if (currentIndex > 0)
            {
                listBox1.SelectedIndex = currentIndex - 1;
                axWindowsMediaPlayer1.Ctlcontrols.playItem(
                    playlist.get_Item(listBox1.SelectedIndex)
                );
            }
        }
        // Κουμπί για φωτογραφίες
        private void button4_Click(object sender, EventArgs e)
        {
            PhotosForm photos = new PhotosForm();
            photos.Show();
            this.Hide();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
        }
        // Μέθοδος για να σταματάει το βίντεο αν ανοίξουμε κάποια άλλη φόρμα
        private void VideosForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }
        // Βοήθεια
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εδώ μπορείτε να δείτε τα βίντεο της συλλογής μας!\nΠεριηγηθείτε στα διαθέσιμα βίντεο από τη λίστα δεξιά του media player." +
                "\nΚάντε κλικ πάνω στο εικονίδιο της φωτογραφίας για να επιστρέψετε στη συλλογή με τις φωτογραφίες.", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
