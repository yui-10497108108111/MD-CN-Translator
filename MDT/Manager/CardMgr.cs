using System.Collections.Generic;
using System.IO;

namespace MDT.Manager
{
    internal class CardMgr:BaseManager<CardMgr>
    {
        private Dictionary<string, CardInfo> _database;
        public CardMgr()
        {
            string cardStr = File.ReadAllText("cards.json");
            _database = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, CardInfo>>(cardStr);
        }
        public CardInfo GetCardInfo(string id)
        {
            if (_database.ContainsKey(id))
                return _database[id];
            return null;
        }
    }
}
