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

        //handling buttons / GUI elements
        public mainUI()
        {
            InitializeComponent();
            listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_DoubleClick);
        }

        private void mainUI_Load(object sender, EventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(path + "/win621dl/"))
            {
                Directory.CreateDirectory(path + "/win621dl/");
            }
        }

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

        public string getRandomHeader()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            return ("win621d_" + rand);
        }


        //main data processing
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/win621dl/"; 

        private void button3_Click(object sender, EventArgs e)
        {
            tagBox.Text = tagBox.Text.Replace(" ", Environment.NewLine);
            string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string tags = string.Join("%20", lines);
            tags = tags.Replace(" ", string.Empty);

            if(System.IO.File.Exists(path + "/IDs.txt"))
            {
                skipIDs = System.IO.File.ReadAllLines(path + "/IDs.txt").ToList();
                listBox1.Items.Insert(0, "Found " + skipIDs.Count() + " IDs logged.");
            }

            Thread e6 = new Thread(() => e621dl(tags, 9999999));
            e6.IsBackground = true;
            e6.Start();
        }

        //e621 downloader
        public void e621dl(string tags, int prevLastID)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                label2.Text = "Status: Getting URL data...";
                directoryBtn.Enabled = false;
                button3.Enabled = false;
            }));

            WebClient w = new WebClient();
            w.Headers.Add("user-agent", getRandomHeader());
            byte[] dldata = w.DownloadData(@"https://e621.net/posts.json?limit=300&tags=" + tags + "+id%3A<" + prevLastID + appendLoginText);
            string dataraw = System.Text.Encoding.UTF8.GetString(dldata);
            dataraw = dataraw.Replace("\"has\":null", "\"has\":false");
            dataraw = dataraw.Replace("\"status_locked\":null", "\"status_locked\":false");
            var parsedJson = JsonConvert.DeserializeObject<RootObject>(dataraw);
            int results = parsedJson.posts.Count();
            rescount += results;
            int lastID = 0;

            for (int i = 0; i < parsedJson.posts.Count(); i++)
            {
                string url = parsedJson.posts[i].file.url;
                lastID = parsedJson.posts[i].id;
                if(url != null)
                {
                    if (url.Length > 5)
                    {
                        Urls.Add(new List<string> { url, parsedJson.posts[i].score.total.ToString(), string.Join(", ", parsedJson.posts[i].tags.artist), parsedJson.posts[i].id.ToString(), parsedJson.posts[i].description });
                    }
                }
            }

            this.Invoke(new MethodInvoker(delegate ()
            {
                resultsLbl.Text = "Results: " + rescount.ToString();
            }));

            if (results < 300)
            {
                Console.WriteLine("stopped : " + rescount);
                download();
            }
            else
            {
                e621dl(tags, lastID);
            }
        }

        public void download()
        {
            Console.WriteLine(Urls.Count());

            this.Invoke(new MethodInvoker(delegate ()
            {
                label2.Text = "Status: Downloading images...";
                progressBar.Maximum = Urls.Count();
            }));

            int dlCount = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (List<String> dat in Urls)
            {
                WebClient dlCli = new WebClient();
                string[] fileNameSplit = dat[0].Split('/');
                string fileName = fileNameSplit[fileNameSplit.Length - 1];

                this.Invoke(new MethodInvoker(delegate ()
                {
                    label3.Text = "Name: " + fileName;
                    label4.Text = "Score: " + dat[1];
                    label5.Text = "Artist: " + dat[2];
                    label6.Text = "ID: " + dat[3];
                    textBox1.Text = dat[4];
                }));

                if (!System.IO.File.Exists(path + "/" + fileName))
                {
                    dlCli.DownloadFile(dat[0], path + "/" + fileName);
                    dlCount++;
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        listBox1.Items.Insert(0, "SUCCESS: " + fileName);
                    }));
                }
                else
                {
                    dlCount++;
                    this.Invoke(new MethodInvoker(delegate ()
                    {;
                        listBox1.Items.Insert(0, "SKIPPED: " + fileName + " (File exists)");
                    }));
                }

                this.Invoke(new MethodInvoker(delegate ()
                {
                    dlLbl.Text = "Downloaded: " + dlCount;
                    progressBar.Value = dlCount;
                }));

                saveIDs.Add(dat[3]);
                System.IO.File.WriteAllLines(path + "/IDs.txt", saveIDs.ToArray());
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);

            this.Invoke(new MethodInvoker(delegate ()
            {
                done done = new done(dlCount.ToString(), elapsedTime);
                done.Show();
                label2.Text = "Status: Done!";
                directoryBtn.Enabled = true;
                button3.Enabled = true;
            }));
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

        private void cbGuest_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.Enabled = !cbGuest.Checked;
            tbUsername.Enabled = !cbGuest.Checked;
            btnLogin.Enabled = !cbGuest.Checked;
        }

        //todo: better inkbunny downloader
    }

    public class File
    {
        public int width { get; set; }
        public int height { get; set; }
        public string ext { get; set; }
        public int size { get; set; }
        public string md5 { get; set; }
        public string url { get; set; }
    }

    public class Preview
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Sample
    {
        public bool has { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string url { get; set; }
    }

    public class Score
    {
        public int up { get; set; }
        public int down { get; set; }
        public int total { get; set; }
    }

    public class Tags
    {
        public List<string> general { get; set; }
        public List<string> species { get; set; }
        public List<object> character { get; set; }
        public List<object> copyright { get; set; }
        public List<object> artist { get; set; }
        public List<object> invalid { get; set; }
        public List<object> lore { get; set; }
        public List<object> meta { get; set; }
    }

    public class Flags
    {
        public bool pending { get; set; }
        public bool flagged { get; set; }
        public bool note_locked { get; set; }
        public bool status_locked { get; set; }
        public bool rating_locked { get; set; }
        public bool deleted { get; set; }
    }

    public class Relationships
    {
        public int? parent_id { get; set; }
        public bool has_children { get; set; }
        public bool has_active_children { get; set; }
        public List<object> children { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public File file { get; set; }
        public Preview preview { get; set; }
        public Sample sample { get; set; }
        public Score score { get; set; }
        public Tags tags { get; set; }
        public List<object> locked_tags { get; set; }
        public int change_seq { get; set; }
        public Flags flags { get; set; }
        public string rating { get; set; }
        public int fav_count { get; set; }
        public List<object> sources { get; set; }
        public List<object> pools { get; set; }
        public Relationships relationships { get; set; }
        public int? approver_id { get; set; }
        public int uploader_id { get; set; }
        public string description { get; set; }
        public int comment_count { get; set; }
        public bool is_favorited { get; set; }
    }

    public class RootObject
    {
        public List<Post> posts { get; set; }
    }
}
