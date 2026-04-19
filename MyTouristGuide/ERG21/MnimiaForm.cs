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
    public partial class MnimiaForm : ItemListForm
    {
        // Το constructor καλεί τον constructor της μητρικής φόρμας ItemListForm και του περνάω μια λίστα με αντικείμενα List<AppItem>
        // (Το DataProvider.GetAttractions() επιστρέφει μια List<AppItem>) 
        public MnimiaForm() : base(DataProvider.GetAttractions())
        {
            InitializeComponent();
            this.Text = "MyTouristGuide.GR - Μνημεία";
        }

        private void MnimiaForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Μνημεία");
            }
        }
        // Help button
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εδώ μπορείτε να βρείτε τα σημαντικότερα μνημεία της Κρήτης!\nΚάντε κλικ πάνω σε μια φωτογραφία ή ένα όνομα για δείτε περισσοτερες πληροφορίες.", "Βοήθεια"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
