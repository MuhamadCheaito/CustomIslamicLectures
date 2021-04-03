using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IslamicConfPlayer
{
    public partial class frmMain : MaterialForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        string VideoId;
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void headerPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            VideoId = e.Item.ToolTipText.Trim();
            var embed = "<html><head>" +
            "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
            "</head><body style=\"background-color:black;\">" +
            "<iframe width=\"" + (videoContent.Width - 25) + "\" height=\"" + (videoContent.Height - 25) + "\" src=\"{0}\"" +
            "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
            "</body></html>";
            var url = $"https://www.youtube.com/embed/{VideoId}?rel=0&autoplay=1";
            this.videoContent.DocumentText = string.Format(embed, url);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\Data\videos.json");
            JObject videos = JObject.Parse(File.ReadAllText(path));
            foreach(var video in videos["playlist"]["videos"])
            {
                ListViewItem item = new ListViewItem();
                item.ToolTipText = video["id"].ToString();
                item.Text = video["name"].ToString();
                listView1.Items.Add(item);
            }
        }
    }
}
