using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vTools_Auth.src
{
    public class Constants
    {
        public static String settingsFolder = Environment.GetEnvironmentVariable("localappdata") + "\\Riot Games\\Riot Client";
        public static String settingsFile = settingsFolder + "\\Data\\RiotGamesPrivateSettings.yaml";
        public static String lockFile = settingsFolder + "\\Config\\lockfile";
    }
}
