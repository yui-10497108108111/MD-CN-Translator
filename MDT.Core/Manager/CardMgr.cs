using MDT.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MDT.Core.Manager
{
    public class CardMgr:BaseManager<CardMgr>
    {
        private Dictionary<string, CardInfo> _database;
        public CardMgr()
        {
            string cardStr = File.ReadAllText("cards.json");

            _database = JsonConvert.DeserializeObject<Dictionary<string, CardInfo>>(cardStr);
        }
        public CardInfo GetCardInfo(string id)
        {
            if (_database.ContainsKey(id))
                return _database[id];
            return null;
        }
        public CardInfo GetCardInfoByEnName(string name)
        {
            name = Regex.Replace(name, "[^A-Z^a-z^0-9^ ]","",RegexOptions.None).ToLower().Replace(" ","");
            System.Console.WriteLine($"name: {name}");
            return _database.FirstOrDefault(x => Regex.Replace(x.Value.en_name,"[^A-Z^a-z^0-9^ ]", "", RegexOptions.None).ToLower().Replace(" ", "").Contains(name)).Value;
        }
        public Dictionary<string, CardInfo> GetAllCard()
        {
            return _database;
        }
    }
}
