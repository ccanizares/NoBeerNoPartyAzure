using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPartyNoBeer.Common.Model
{
    public class Rate:Item
    {
        public int Points { get; set; }
        public string UserId { get; set; }
        public string BeerId { get; set; }
        public DateTime Date { get; set; }
        public double Average { get; set; }

        public string DispenserId { get; set; }
    }
}
