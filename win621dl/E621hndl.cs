using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace win621dl
{
    internal class E621hndl
    {
        public mainUI parent { get; set; }
        public List<List<string>> Urls = new List<List<string>>();
        public List<string> saveIDs = new List<string> { };
        public int rescount = 0;
        public string appendLoginText = "";
        public string path = "";
        public int totalBytes = 0;
        public E621hndl(mainUI parent, string path)
        {
            this.parent = parent;
            this.path = path;
        }
        public void e621dl(string tags, int prevLastID, string appendLoginText)
        {
            parent.Invoke(new MethodInvoker(delegate ()
            {
                parent.label2.Text = "Status: Getting URL data...";
                parent.directoryBtn.Enabled = false;
                parent.button3.Enabled = false;
            }));

            WebClient w = new WebClient();
            w.Headers.Add("user-agent", getRandomHeader());
            byte[] dldata = w.DownloadData(@"https://e621.net/posts.json?limit=300&tags=" + tags + "+id%3A<" + prevLastID + appendLoginText);
            string dataraw = System.Text.Encoding.UTF8.GetString(dldata);
            dataraw = dataraw.Replace("\"has\":null", "\"has\":false");
            dataraw = dataraw.Replace("\"status_locked\":null", "\"status_locked\":false");
            RootObject parsedJson = JsonConvert.DeserializeObject<RootObject>(dataraw);
            int results = parsedJson.posts.Count();
            rescount += results;
            int lastID = 0;

            for (int i = 0; i < parsedJson.posts.Count(); i++)
            {
                string url = parsedJson.posts[i].file.url;
                lastID = parsedJson.posts[i].id;
                if (url != null)
                {
                    if (url.Length > 5)
                    {
                        Urls.Add(new List<string> { url, parsedJson.posts[i].score.total.ToString(), string.Join(", ", parsedJson.posts[i].tags.artist), parsedJson.posts[i].id.ToString(), parsedJson.posts[i].description });
                    }
                }
            }

            parent.Invoke(new MethodInvoker(delegate ()
            {
                parent.resultsLbl.Text = "Results: " + rescount.ToString();
            }));

            if (results < 300)
            {
                Console.WriteLine("stopped : " + rescount);
                download();
            }
            else
            {
                e621dl(tags, lastID, appendLoginText);
            }
        }

        public void download()
        {
            Console.WriteLine(Urls.Count());

            parent.Invoke(new MethodInvoker(delegate ()
            {
                parent.label2.Text = "Status: Downloading images...";
                parent.progressBar.Maximum = Urls.Count();
            }));

            int dlCount = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (List<string> dat in Urls)
            {
                WebClient dlCli = new WebClient();
                string[] fileNameSplit = dat[0].Split('/');
                string fileName = fileNameSplit[fileNameSplit.Length - 1];

                parent.Invoke(new MethodInvoker(delegate ()
                {
                    parent.label3.Text = "Name: " + fileName;
                    parent.label4.Text = "Score: " + dat[1];
                    parent.label5.Text = "Artist: " + dat[2];
                    parent.label6.Text = "ID: " + dat[3];
                    parent.textBox1.Text = dat[4];
                }));

                if (!System.IO.File.Exists(path + "/" + fileName))
                {
                    dlCli.DownloadFile(dat[0], path + "/" + fileName);
                    totalBytes += (int)new FileInfo(path + "/" + fileName).Length;
                    dlCount++;
                    parent.Invoke(new MethodInvoker(delegate ()
                    {
                        parent.listBox1.Items.Insert(0, "SUCCESS: " + fileName);
                    }));
                }
                else
                {
                    dlCount++;
                    parent.Invoke(new MethodInvoker(delegate ()
                    {
                        parent.listBox1.Items.Insert(0, "SKIPPED: " + fileName + " (File exists)");
                    }));
                }

                parent.Invoke(new MethodInvoker(delegate ()
                {
                    parent.dlLbl.Text = "Downloaded: " + dlCount;
                    parent.progressBar.Value = dlCount;
                }));

                saveIDs.Add(dat[3]);
                System.IO.File.WriteAllLines(path + "/IDs.txt", saveIDs.ToArray());
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);

            parent.Invoke(new MethodInvoker(delegate ()
            {
                parent.listBox1.Items.Insert(0, "####################");
                parent.listBox1.Items.Insert(0, "Downloaded " + SizeSuffix(totalBytes, 2));
                parent.listBox1.Items.Insert(0, "Done in " + elapsedTime);
                parent.listBox1.Items.Insert(0, "####################");
                parent.label2.Text = "Status: Done!";
                parent.directoryBtn.Enabled = true;
                parent.button3.Enabled = true;
            }));
        }

        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }
            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }

        public string getRandomHeader()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            return ("win621d_" + rand);
        }
    }
}
