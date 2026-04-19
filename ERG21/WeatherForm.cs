using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace ERG21
{
    public partial class WeatherForm : BaseForm
    {
        private WebView2 webView21;

        public WeatherForm()
        {
            InitializeComponent();
            this.Load += WeatherForm_Load;
            this.Text = "MyTouristGuide.GR - Καιρός";
        }

        private void WeatherForm_Load(object sender, EventArgs e)
        {
            // καταγραφή ιστορικού αν είναι user
            if (CurrentUser.Role == "User")
            {
                CurrentUser.History.Add("Καιρός");
            }

            webView21 = new WebView2
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(webView21);

            // Φόρτωση live weather map (Windy zoomed στην Ελλάδα)
            webView21.Source = new Uri("https://www.windy.com/?37,23,6");
        }
    }
}
