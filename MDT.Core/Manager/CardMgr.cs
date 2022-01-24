using MDT.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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
    }
}
