using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestSharp;
using System.Diagnostics;
using RestSharp.Authenticators;

namespace vTools_Auth.src
{
    public class LockfileData
    {
        public string processName { get; set; }
        public int processId { get; set; }
        public int port { get; set; }
        public string code { get; set; }
        public string protocol { get; set; }
    }

    public class PostHandling
    {
        public async static Task<bool> RemoveLocalSession()
        {
            await Task.Delay(1);
            LockfileData lockfileData = GetLockfile();
            File.Delete(Constants.settingsFile);
            Process.GetProcessById(lockfileData.processId).Kill();
            if (Process.GetProcessById(lockfileData.processId) == null) return false;
            return true;

        }

        private static LockfileData GetLockfile()
        {
            using (var fileStream = new FileStream(Constants.lockFile, FileMode.Open, FileAccess.ReadWrite,
                       FileShare.ReadWrite))
            using (var sr = new StreamReader(fileStream))
            {
                var fragments = sr.ReadToEnd().Split(":");
                LockfileData lockfileData = new LockfileData()
                {
                    processName = fragments[0],
                    processId = Int32.Parse(fragments[1]),
                    port = Int32.Parse(fragments[2]),
                    code = fragments[3],
                    protocol = fragments[4],
                };
                return lockfileData;
            }

        }
    }
}
