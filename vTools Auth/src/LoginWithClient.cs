using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace vTools_Auth.src
{
    public partial class Session
    {
        public String id { get; set; }
        public String ssid { get; set; }
        public String csid { get; set; }
        public String clid { get; set; }
        public String puuid { get; set; }
        public String shard { get; set; }
    }

    class LoginWithClient
    {
        public String fileContent = "";
        public Session sess;
        public bool isSessionReady()
        {
            String settingsFile = Constants.settingsFile;
            StreamReader rd;
            using(FileStream privateSettings = File.Open(settingsFile, FileMode.Open, FileAccess.ReadWrite))
            {
                rd = new StreamReader(privateSettings);
                fileContent = (String)rd.ReadToEnd().Clone();
            }
            rd.Close();
            if (!this.fileContent.Contains("ssid")) return false;
            return true;
        }

        public Session obtainSession()
        {
            Session session = new Session();
            if (
                !this.fileContent.Contains("ssid") &&
                !this.fileContent.Contains("csid") &&
                !this.fileContent.Contains("clid") &&
                !this.fileContent.Contains("sub")
                ) { return session; }
            String[] contentArr = this.fileContent.Split("domain");
            for (int i = 0; i < contentArr.Length; i++)
            {
                switch(contentArr[i])
                {
                    case String entry when entry.Contains("ssid"):
                        session.ssid = entry.Split("value: \"")[1].Split("\"")[0];
                        break;
                    case String entry when entry.Contains("clid"):
                        session.clid = entry.Split("value: \"")[1].Split("\"")[0];
                        break;
                    case String entry when entry.Contains("csid"):
                        session.csid = entry.Split("value: \"")[1].Split("\"")[0];
                        break;
                    case String entry when entry.Contains("sub"):
                        session.puuid = entry.Split("value: \"")[1].Split("\"")[0];
                        break;
                }
            }
            this.sess = session;
            return session;
        }
    }
}
