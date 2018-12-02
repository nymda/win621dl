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

namespace win621dl
{
    public partial class mainUI : Form
    {
        public mainUI()
        {
            InitializeComponent();
            listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_DoubleClick);
        }

        public WebClient client = new WebClient();
        public WebClient downloadClient = new WebClient();
        public WebClient backupClient = new WebClient();
        public int counter = 0;
        public int subcounter = 0;
        public List<String> urls = new List<String> { };
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/win621dl/";
        public int downloadcounter = 0;
        public string current = "";
        public int disposecounter = 0;
        Bitmap b = new Bitmap(10, 10);
        public int tres = 0;
        public int page = 1;
        public int IBallresults = 0;
        public bool invalidSid;

        public void downloader()
        {
            int i = 0;
            for(; ; )
            {
                try
                {
                    Console.WriteLine(path  + urls[i]);
                    string[] url = urls[i].Split('/');
                    downloadClient.DownloadFile(urls[i], path + "/" + url[6]);
                    current = path + "/" + url[6];
                    downloadcounter++;
                    disposecounter++;
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        dlLbl.Text = "Downloaded: " + downloadcounter;
                        int perc = ((downloadcounter * 100 / counter * 100) / 100);
                        progressBar.Value = perc * 2;
                        listBox1.Items.Insert(0, "SUCCESS " + url[6]);
                    }));
                    i++;
                    
                }
                catch(ArgumentOutOfRangeException)
                {
                    Thread.Sleep(10);
                }
                catch
                {
                    i++;
                    Console.WriteLine("malformed url");
                }
            }
        }

        public void downloaderIB()
        {
            if (invalidSid)
            {
                return;
            }
            int i = 0;
            for (; ; )
            {

                if(downloadcounter == IBallresults && downloadcounter > 0)
                {
                    return;
                }

                try
                {
                    Console.WriteLine(path + urls[i]);
                    string[] url = urls[i].Split('/');
                    downloadClient.DownloadFile(urls[i], path + "/" + url[6]);
                    current = path + "/" + url[6];
                    downloadcounter++;
                    disposecounter++;
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        try
                        {
                            dlLbl.Text = "Downloaded: " + downloadcounter;
                            int perc = ((downloadcounter * 100 / IBallresults * 100) / 100);
                            progressBar.Value = perc;
                            listBox1.Items.Insert(0, "SUCCESS " + url[6]);
                        }
                        catch
                        {

                        }
                    }));
                    i++;

                }
                catch (ArgumentOutOfRangeException)
                {
                    Thread.Sleep(10);
                }
                catch
                {
                    i++;
                    Console.WriteLine("malformed url");
                }
            }
        }

        public void e621(string uri)
        {
            bool cont = true;
            while (cont)
            {
                string dlstring = uri;
                byte[] data = { 00 };
                if (client.IsBusy)
                {
                    try
                    {
                        data = backupClient.DownloadData(dlstring);
                    }
                    catch
                    {
                        return;
                    }
                }
                if(!client.IsBusy)
                {
                    try
                    {
                        data = client.DownloadData(dlstring);
                    }
                    catch
                    {
                        return;
                    }
                }

                string dataraw = System.Text.Encoding.UTF8.GetString(data);
                string[] datarawsplit = dataraw.Split(new string[] { "]}" }, StringSplitOptions.None);

                for (int i = 0; i < datarawsplit.Count(); i++)
                {
                    string[] current = datarawsplit[i].Split(',');

                    for (int o = 0; o < current.Count(); o++)
                    {
                        if (current[o].Contains("\"id\":"))
                        {
                            string[] current2 = current[o].Split(new string[] { ":" }, StringSplitOptions.None);
                            string final = current2[1].Replace("\"", string.Empty);
                            counter++;
                            subcounter++;
                            if (subcounter == 300)
                            {
                                Console.WriteLine(subcounter);
                                subcounter = 0;
                                Console.WriteLine("300");
                                Thread.Sleep(600);
                                Random random = new Random();
                                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                                string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
                                client.Headers.Add("user-agent", "win621d_" + rand);
                                string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                                string tags = string.Join("%20", lines);
                                tags = tags.Replace(" ", string.Empty);
                                string dlstring2 = "https://e621.net/post/index.json?limit=300&before_id=" + final + "&tags=" + tags;
                                Thread t = new Thread(() => e621(dlstring2));
                                t.IsBackground = true;
                                t.Start();
                            }
                        }
                    }
                }

                for (int i = 0; i < datarawsplit.Count(); i++)
                {
                    string[] current = datarawsplit[i].Split(',');
                    for (int o = 0; o < current.Count(); o++)
                    {
                        if (current[o].Contains("file_url"))
                        {
                            string[] current2 = current[o].Split(new string[] { "\":\"" }, StringSplitOptions.None);
                            string final = current2[1].Replace("\"", string.Empty);
                            counter++;
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                //Console.WriteLine(final);
                                urls.Add(final);
                                resultsLbl.Text = "Results: " + (counter / 2).ToString();
                            }));
                        }
                    }
                }
            }
        }

        public void inkbunny(string uri)
        {
            bool cont = true;
            bool got_pages_count = false;
            bool got_rid = false;
            bool set_results = false;
            int c = -1;
            int pages = 0;
            string rid = "";
            int currentpageresults = 0;

            while (cont)
            {
                string dlstring = uri;
                byte[] data = { 00 };
                if (client.IsBusy)
                {
                    try
                    {
                        data = backupClient.DownloadData(dlstring);
                    }
                    catch
                    {
                        return;
                    }
                }
                if (!client.IsBusy)
                {
                    try
                    {
                        data = client.DownloadData(dlstring);
                    }
                    catch
                    {
                        return;
                    }
                }

                if(System.Text.Encoding.UTF8.GetString(data).Contains("Invalid Session ID") || System.Text.Encoding.UTF8.GetString(data).Contains("No Session ID"))
                {
                    Console.WriteLine("INVALID SID");

                    invalidSid = true;

                    string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    string tags = string.Join("%20", lines);
                    tags = tags.Replace(" ", string.Empty);
                    byte[] sidbytes = client.DownloadData("https://inkbunny.net/api_login.php?username=guest&password=");
                    string siddatastr = System.Text.Encoding.UTF8.GetString(sidbytes);
                    string[] siddata1 = siddatastr.Split(',');
                    string[] siddata2 = siddata1[0].Split(':');
                    string siddata = siddata2[1];
                    string sid = siddata.Replace("\"", string.Empty);
                    Console.WriteLine(sid);

                    invalidSid = false;

                    string dlstring2 = "https://inkbunny.net/api_search.php?sid=" + sid + "&text=" + tags + "&orderby=views&get_rid=yes";

                    Thread t = new Thread(() => inkbunny(dlstring2));
                    t.IsBackground = true;
                    t.Start();

                    Thread d = new Thread(() => downloaderIB());
                    d.IsBackground = true;
                    d.Start();
                }

                Console.WriteLine(data.Length);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
                string dataraw = System.Text.Encoding.UTF8.GetString(data);
                string[] datarawsplit = dataraw.Split(new string[] { "},{" }, StringSplitOptions.None);

                for (int i = 0; i < datarawsplit.Count(); i++)
                {
                    string[] current = datarawsplit[i].Split(',');
                    for (int o = 0; o < current.Count(); o++)
                    {
                        if (current[o].Contains("pages_count"))
                        {
                            string[] current2 = current[o].Split(new string[] { "\":" }, StringSplitOptions.None);
                            string final = current2[1].Replace("\"", string.Empty);
                            pages = Int32.Parse(final);
                            got_pages_count = true;
                            Console.WriteLine(final);
                        }

                        if (current[o].Contains("rid\":"))
                        {
                            string[] current2 = current[o].Split(new string[] { "\":" }, StringSplitOptions.None);
                            string final = current2[1].Replace("\"", string.Empty);
                            rid = final;
                            got_rid = true;
                            Console.WriteLine(final);
                        }

                        if (current[o].Contains("results_count_thispage\":"))
                        {
                            string[] current2 = current[o].Split(new string[] { "\":" }, StringSplitOptions.None);
                            string final = current2[1].Replace("\"", string.Empty);
                            currentpageresults = Int32.Parse(final);
                            Console.WriteLine(final);
                        }

                        if (current[o].Contains("results_count_all\":") && set_results == false)
                        {
                            Console.WriteLine(current[o]);
                            string[] current2 = current[o].Split(new string[] { "\":" }, StringSplitOptions.None);
                            string final = current2[1].Replace("\"", string.Empty);
                            IBallresults = Int32.Parse(final);
                            set_results = true;
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                resultsLbl.Text = "Results: " + IBallresults.ToString();
                            }));
                        }

                        if (current[o].Contains("file_url_full"))
                        {
                            try
                            {
                                tres++;
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    //listBox1.Items.Add(tres.ToString());
                                }));
                                string[] current2 = current[o].Split(new string[] { "\":\"" }, StringSplitOptions.None);
                                string final = current2[1].Replace("\"", string.Empty);
                                final = final.Replace(@"\/", @"/");
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    urls.Add(final);
                                    //resultsLbl.Text = "Results: " + (counter / 2).ToString();
                                }));
                                Console.WriteLine(final);
                                c++;
                            }
                            catch
                            {

                            }
                        }

                        if (current[o].Contains("}]}"))
                        {
                            Console.WriteLine(current[o]);
                            page++;
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                //listBox1.Items.Add(page);
                            }));
                            Thread t = new Thread(() => inkbunny(uri + "&page=" + page));
                            t.IsBackground = true;
                            t.Start();
                            return;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            directoryBtn.Enabled = false;
            button3.Enabled = false;
            tagBox.Enabled = false;
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string tags = string.Join("%20", lines);
            tags = tags.Replace(" ", string.Empty);

            if (radioButton1.Checked)
            {
                client.Headers.Add("user-agent", "win621d_" + rand);

                string dlstring = "https://e621.net/post/index.json?limit=300&&tags=" + tags;

                Thread t = new Thread(() => e621(dlstring));
                t.IsBackground = true;
                t.Start();

                Thread d = new Thread(() => downloader());
                d.IsBackground = true;
                d.Start();
            }
            if (radioButton2.Checked)
            {
                invalidSid = false;
                byte[] sidbytes = client.DownloadData("https://inkbunny.net/api_login.php?username=guest&password=");
                string siddatastr = System.Text.Encoding.UTF8.GetString(sidbytes);
                string[] siddata1 = siddatastr.Split(',');
                string[] siddata2 = siddata1[0].Split(':');
                string siddata = siddata2[1];
                string sid = siddata.Replace("\"", string.Empty);

                string dlstring = "https://inkbunny.net/api_search.php?sid=" + sid + "&text=" + tags + "&orderby=views&get_rid=yes";

                Thread t = new Thread(() => inkbunny(dlstring));
                t.IsBackground = true;
                t.Start();

                Thread d = new Thread(() => downloaderIB());
                d.IsBackground = true;
                d.Start();
            }
        }

        private void mainUI_Load(object sender, EventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if(!Directory.Exists(path + "/win621dl/"))
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

        private void picBox_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(current);
            }
            catch
            {

            }
        }
    }
}
