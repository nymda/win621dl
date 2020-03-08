using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace win621dl
{
    public partial class done : Form
    {
        public string itm = "";
        public string tim = "";

        public done(string items, string time)
        {
            InitializeComponent();
            itm = items;
            tim = time;
        }

        private void done_Load(object sender, EventArgs e)
        {
            label1.Text = "Download finished. \n\n Items downloaded:" + itm + "\nTime elapsed: " + tim;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
