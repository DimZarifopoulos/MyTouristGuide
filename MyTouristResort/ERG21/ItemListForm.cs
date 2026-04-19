using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ERG21
{
    // Form που παίρνει μια λίστα List<AppItem> με δεδομένα όπως αξιοθέατα κλπ
    // και δημιουργεί μια νέα λίστα με panels μέσα σε ένα FlowLayoutPanel
    // και με κλικ πάνω σε κάθε κάρτα εμφανίζει μια φόρμα με τις λεπτομέρειες (ItemDetailsForm)

    // Κληρονομεί το BaseForm
    public partial class ItemListForm : BaseForm
    {
        private List<AppItem> items = new List<AppItem>();

        public ItemListForm()
        {
            InitializeComponent();
        }

        public ItemListForm(List<AppItem> items)
        {
            InitializeComponent();
            this.items = items;
        }

        // Δημιουργεί τη λίστα με τις κάρτες όταν γίνεται load
        private void ItemListForm_Load(object sender, EventArgs e)
        {
            PopulateItems();
        }

        private void PopulateItems()
        {
            flowLayoutPanel1.Controls.Clear(); // αν χρειάζεται reset
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;

            foreach (var item in items)
            {
                Panel card = CreateItemCard(item.title, item.shortDescription, Image.FromFile(item.imagePath), item);
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private Panel CreateItemCard(string title, string description, Image image, AppItem item)
        {
            Panel panel = new Panel
            {
                Size = new Size(651, 140),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            PictureBox pic = new PictureBox
            {
                Image = image,
                Location = new Point(3, 3),
                Size = new Size(222, 132),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(350, 24),
                Location = new Point(300, 3),
                Cursor = Cursors.Hand
            };

            TextBox lblDesc = new TextBox
            {
                Text = description,
                Font = new Font("Arial", 16),
                Location = new Point(238, 30),
                Size = new Size(404, 105),
                Multiline = true,
                ReadOnly = true
            };

            // με κλικ ανοίγει το item details form
            void openDetails(object sender, EventArgs e)
            {
                ItemDetailsForm details = new ItemDetailsForm(item);
                details.Show();
                this.Hide();
            }

            pic.Click += openDetails;
            lblTitle.Click += openDetails;
            panel.Click += openDetails;

            // Προσθήκη στο panel
            panel.Controls.Add(pic);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblDesc);

            return panel;
        }
    }
}
