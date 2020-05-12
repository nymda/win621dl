using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace win621dl
{
    class InkbunnyHndl
    {
        public mainUI parent { get; set; }
        public List<List<string>> Urls = new List<List<string>>();
        public List<String> saveIDs = new List<String> { };
        public int rescount = 0;
        public string appendLoginText = "";
        public string path = "";
        public int totalBytes = 0;
        public string sessionID = "";
        public int numPages = 0;
        public dlSetting dls;

        public InkbunnyHndl(mainUI parent, string path, string sessionID, dlSetting dls)
        {
            this.parent = parent;
            this.path = path;
            this.sessionID = sessionID;
            this.dls = dls;
        }

        public void begin(string tags)
        {

                //get number of pages

                parent.Invoke(new MethodInvoker(delegate ()
                {
                    parent.label13.Text = "Status: Getting URL data...";
                }));

                WebClient w = new WebClient();

                byte[] tmpObtainList = new byte[0];

                if (dls == dlSetting.Tags)
                {
                    tmpObtainList = w.DownloadData(String.Format("https://inkbunny.net/api_search.php?output_mode=json&sid={0}&text={1}&page=1", sessionID, tags));
                }
                else if(dls == dlSetting.Gallery)
                {
                    tmpObtainList = w.DownloadData(String.Format("https://inkbunny.net/api_search.php?output_mode=json&sid={0}&username={1}&page=1", sessionID, tags));
                }

                string tmpObtainString = Encoding.UTF8.GetString(tmpObtainList);
                var parsedJson = JsonConvert.DeserializeObject<RootobjectIB>(tmpObtainString);
                numPages = parsedJson.pages_count;

                for (int i = 0; i < numPages; i++)
                {
                    byte[] dlDataByte = new byte[0];

                    if (dls == dlSetting.Tags)
                    {
                        dlDataByte = w.DownloadData(String.Format("https://inkbunny.net/api_search.php?output_mode=json&sid={0}&text={1}&page=1", sessionID, tags));
                    }
                    else if (dls == dlSetting.Gallery)
                    {
                        dlDataByte = w.DownloadData(String.Format("https://inkbunny.net/api_search.php?output_mode=json&sid={0}&username={1}&page=1", sessionID, tags));
                    }
                    string dlDataString = Encoding.UTF8.GetString(dlDataByte);
                    var parsedPageData = JsonConvert.DeserializeObject<RootobjectIB>(dlDataString);
                    foreach (Submission sb in parsedPageData.submissions)
                    {
                        Urls.Add(new List<String> { sb.file_url_full, sb.title, sb.username, sb.submission_id});
                    }
                }

                Console.WriteLine(Urls.Count());
                download();
            

        }

        public void download()
        {
            Console.WriteLine(Urls.Count());

            parent.Invoke(new MethodInvoker(delegate ()
            {
                parent.label2.Text = "Status: Downloading images...";
                parent.label14.Text = "Results: " + Urls.Count();
                parent.progressBar1.Maximum = Urls.Count();
            }));

            int dlCount = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (List<String> dat in Urls)
            {
                WebClient dlCli = new WebClient();
                string[] fileNameSplit = dat[0].Split('/');
                string fileName = fileNameSplit[fileNameSplit.Length - 1];

                parent.Invoke(new MethodInvoker(delegate ()
                {
                    parent.label12.Text = "Name: " + dat[1];
                    parent.label10.Text = "Artist: " + dat[2];
                    parent.label9.Text = "ID: " + dat[3];
                }));

                if (!System.IO.File.Exists(path + "/" + fileName))
                {
                    dlCli.DownloadFile(dat[0], path + "/" + fileName);
                    totalBytes += (int)new FileInfo(path + "/" + fileName).Length;
                    dlCount++;
                    parent.Invoke(new MethodInvoker(delegate ()
                    {
                        parent.listBox2.Items.Insert(0, "SUCCESS: " + fileName);
                    }));
                }
                else
                {
                    dlCount++;
                    parent.Invoke(new MethodInvoker(delegate ()
                    {
                        parent.listBox2.Items.Insert(0, "SKIPPED: " + fileName + " (File exists)");
                    }));
                }

                parent.Invoke(new MethodInvoker(delegate ()
                {
                    parent.label15.Text = "Downloaded: " + dlCount;
                    parent.progressBar1.Value = dlCount;
                }));

                saveIDs.Add(dat[3]);
                System.IO.File.WriteAllLines(path + "/IDs.txt", saveIDs.ToArray());
            }

        }
    }
}
