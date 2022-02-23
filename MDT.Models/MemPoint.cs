using System;
using System.Collections.Generic;
using System.Text;

namespace MDT.Models
{
    public class MemPoint
    {
        public string ModuleName { get; set; }
        public int Address { get; set; }
        public int[] Offset { get; set; }
    }
}
