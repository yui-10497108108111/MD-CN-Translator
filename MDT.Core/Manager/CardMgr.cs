using MDT.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public Dictionary<string, CardInfo> GetAllCard()
        {
            return _database;
        }
    }
}
