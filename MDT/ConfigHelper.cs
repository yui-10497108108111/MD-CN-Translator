using MDT.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace MDT
{
    public static class ConfigHelper
    {
        public static MemPoint ReadMemPoint(string type)
        {
            return JsonConvert.DeserializeObject<MemPoint>(ReadAppSettings(type));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">duel or deck</param>
        /// <param name="memAddr"></param>
        /// <returns></returns>
        public static void SaveMemPoint(string type, MemPoint memAddr)
        {
            WriteAppSettings(type, JsonConvert.SerializeObject(memAddr));
        }

        public static string ReadAppSettings(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
        public static void WriteAppSettings(string keyName, string newValue)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings.Remove(keyName);
            cfa.AppSettings.Settings.Add(keyName, newValue);
            cfa.Save();
        }
    }
}
