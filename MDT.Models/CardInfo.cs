using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.Models
{
    public class text
    {
        public string types { get; set; }
        public string pdesc { get; set; }
        public string desc { get; set; }
    }
    public class CardInfo
    {
        public int cid { get; set; }
        public int id { get; set; }
        public string cn_name { get; set; }
        public string jp_ruby { get; set; }
        public string jp_name { get; set; }
        public string en_name { get; set; }
        public text text { get; set; }
    }
}
