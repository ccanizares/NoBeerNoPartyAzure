using System;
using System.Collections.Generic;
using System.Text;

namespace NoBeerNoParty.Common.Model
{
    public class Dispenser:Item
    {
        public Dispenser()
        {
            AvaiableLPercen = 100;
            AvaiableLiter = 40;
            CapacityLiters = 40;
        }

        public string BeerId { get; set; }

        public string Stand { get; set; }

        public double AvaiableLPercen { get; set; }

        public double AvaiableLiter { get; set; }

        public double CapacityLiters { get; set; }
    }
}
