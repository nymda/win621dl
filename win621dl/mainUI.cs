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
        }

        public WebClient client = new WebClient();
        public int counter = 0;
        public int subcounter = 0;
        public List<String> urls = new List<String> { };
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/win621dl/";
        public int downloadcounter = 0;
        public string current = "";



        public void downloader()
        {
            int i = 0;
            for(; ; )
            {
                try
                {
                    Console.WriteLine(path  + urls[i]);
                    string[] url = urls[i].Split('/');
                    client.DownloadFile(urls[i], path + "/" + url[6]);
                    current = path + "/" + url[6];
                    i++;
                    downloadcounter++;
                    //int perc = ((downloadcounter * 100 / counter * 100) / 100);
                    //Console.WriteLine(perc);
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        dlLbl.Text = "Downloaded: " + downloadcounter;
                        progressBar.Maximum = counter;
                        progressBar.Value = downloadcounter;
                        picBox.Image = Bitmap.FromFile(path + "/" + url[6]);
                    }));
                }
                catch(Exception e)
                {

                }
            }
        }

        public void newthread(string uri, int mode)
        {

            bool cont = true;
            while (cont)
            {
                string dlstring = uri;
                byte[] data;
                try
                {
                    data = client.DownloadData(dlstring);
                    Console.WriteLine("data ended");
                }
                catch (Exception e)
                {
                    Console.WriteLine("403 ERROR");
                    return;
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
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                Console.WriteLine(final);
                            }));
                            if (subcounter == 300)
                            {
                                subcounter = 0;
                                Console.WriteLine("300");
                                Thread.Sleep(2500);
                                Random random = new Random();
                                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                                string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
                                client.Headers.Add("user-agent", "win621d_" + rand);
                                string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                                string tags = string.Join("%20", lines);
                                tags = tags.Replace(" ", string.Empty);
                                string dlstring2 = "https://e621.net/post/index.json?limit=300&before_id=" + final + "&tags=" + tags;
                                Thread t = new Thread(() => newthread(dlstring2, 0));
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
                            if (mode == 0)
                            {

                            }
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
            client.Headers.Add("user-agent", "win621d_" + rand);
            string[] lines = tagBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string tags = string.Join("%20", lines);
            tags = tags.Replace(" ", string.Empty);
            string dlstring = "https://e621.net/post/index.json?limit=300&&tags=" + tags;
            Thread t = new Thread(() => newthread(dlstring, 0));
            t.IsBackground = true;
            t.Start();

            Thread d = new Thread(() => downloader());
            d.IsBackground = true;
            d.Start();
        }

        private void mainUI_Load(object sender, EventArgs e)
        {
            //client.Headers.Add("user-agent", "win621dl");
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if(!Directory.Exists(path + "/win621dl/"))
            {
                Directory.CreateDirectory(path + "/win621dl/");
            }
        }

        private void infoButton_Click(object sender, EventArgs e)
        {

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
            Process.Start(current);
        }
    }
}
