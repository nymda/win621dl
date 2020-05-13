using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace win621dl
{
    public partial class mainUI : Form
    {
        public List<List<string>> Urls = new List<List<string>>();
        public List<String> skipIDs = new List<String> { };
        public List<String> saveIDs = new List<String> { };
        public int rescount = 0;
        public string login = "";
        public string key = "";
        public string appendLoginText = "";

        //universial
        public mainUI()
        {
            InitializeComponent();
            listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_DoubleClick);
            listBox2.MouseDoubleClick += new MouseEventHandler(listBox2_DoubleClick);
        }

        private void mainUI_Load(object sender, EventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(path + "/win621dl/"))
            {
                Directory.CreateDirectory(path + "/win621dl/");
            }
        }

        //E621 only content
        private void listBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string filename = listBox1.SelectedItem.ToString();
                filename = filename.Replace(" SUCCESS ", string.Empty);
                string[] filenameArray = filename.Split(new string[] { " " }, StringSplitOptions.None);
                filename = filenameArray[1];
                Process.Start(path + "/" + filename);
            }
        }

        private void directoryBtn_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    path = fbd.SelectedPath;
                }
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Form about = new about();
            about.Show();
        }

        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/win621dl/"; 
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            tagBox.Text = tagBox.Text.Replace(" ", Environment.NewLine);
            string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string tags = string.Join("%20", lines);
            tags = tags.Replace(" ", string.Empty);

            if(System.IO.File.Exists(path + "/IDs.txt"))
            {
                skipIDs = System.IO.File.ReadAllLines(path + "/IDs.txt").ToList();
                listBox1.Items.Insert(0, "Found " + skipIDs.Count() + " IDs logged.");
            }

            E621hndl e6h = new E621hndl(this, path);
            Thread e6 = new Thread(() => e6h.e621dl(tags, 9999999, appendLoginText));
            e6.IsBackground = true;
            e6.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new apiSet())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    login = form.login;
                    key = form.api_key;
                    appendLoginText = "&login=" + login + "&api_key=" + key;
                    button1.ForeColor = Color.Green;
                }
            }
        }

        //Inkbunny only content
        public WebClient w = new WebClient();
        public string sessionID = "";
        public string pathIB = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/win621dl/";
        private void cbGuest_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.Enabled = !cbGuest.Checked;
            tbUsername.Enabled = !cbGuest.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbGuest.Checked)
                {
                    byte[] dat = w.DownloadData("https://inkbunny.net/api_login.php?output_mode=json&username=guest");
                    string raw = Encoding.UTF8.GetString(dat);
                    var parsedJson = JsonConvert.DeserializeObject<sessionData>(raw);
                    sessionID = parsedJson.sid;
                    Console.WriteLine(parsedJson.ratingsmask);

                    //set this session to allow all content
                    w.DownloadData(string.Format("https://inkbunny.net/api_userrating.php?output_mode=json&sid={0}&tag[0]=yes&tag[1]=yes&tag[2]=yes&tag[3]=yes&tag[4]=yes&tag[5]=yes", sessionID));
                }
                else
                {
                    string username = tbUsername.Text;
                    string password = tbPassword.Text;

                    byte[] dat = w.DownloadData(String.Format("https://inkbunny.net/api_login.php?output_mode=json&username={0}&password={1}", username, password));
                    string raw = Encoding.UTF8.GetString(dat);
                    var parsedJson = JsonConvert.DeserializeObject<sessionData>(raw);
                    sessionID = parsedJson.sid;
                    Console.WriteLine(parsedJson.ratingsmask);

                    //set this session to allow all content
                    w.DownloadData(string.Format("https://inkbunny.net/api_userrating.php?output_mode=json&sid={0}&tag[0]=yes&tag[1]=yes&tag[2]=yes&tag&tag[3]=yes&tag[4]=yes&tag[5]=yes", sessionID));
                }
                btnLogin.Enabled = false;
                listBox2.Items.Insert(0, "Logged in. " + sessionID);
                Clipboard.SetText(sessionID);
                cbGuest.Enabled = false;
            }
            catch
            {
                listBox2.Items.Insert(0, "Login details invalid.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form about = new about();
            about.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] lines = rbTags.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string tags = string.Join("%20", lines);

            dlSetting set = dlSetting.Gallery;

            if (rbGallery.Checked)
            {
                set = dlSetting.Gallery;
            }
            else if (rbTag.Checked)
            {
                set = dlSetting.Tags;
            }

            InkbunnyHndl ibhndl = new InkbunnyHndl(this, pathIB, sessionID, set);
            Thread ib = new Thread(() => ibhndl.begin(tags));
            ib.IsBackground = true;
            ib.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    pathIB = fbd.SelectedPath;
                }
            }
        }

        private void listBox2_DoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string filename = listBox2.SelectedItem.ToString();
                filename = filename.Replace(" SUCCESS ", string.Empty);
                string[] filenameArray = filename.Split(new string[] { " " }, StringSplitOptions.None);
                filename = filenameArray[1];
                Process.Start(pathIB + "/" + filename);
            }
        }
    }
}
