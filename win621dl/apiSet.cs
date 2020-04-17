using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace win621dl
{
    public partial class apiSet : Form
    {
        public apiSet()
        {
            InitializeComponent();
        }

        public WebClient w = new WebClient();

        public string api_key { get; set; }
        public string login { get; set; }

        private void apiSet_Load(object sender, EventArgs e)
        {
            w.Headers.Add("user-agent", "win621dl - api login test");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] t = w.DownloadData(@"https://e621.net/posts.json?limit=1&login=" + tb_login.Text + "&api_key=" + tb_key.Text);
                api_key = tb_key.Text;
                login = tb_login.Text;
                btnTest.ForeColor = Color.Green;
            }
            catch
            {
                btnTest.ForeColor = Color.Red;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            api_key = tb_key.Text;
            login = tb_login.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
