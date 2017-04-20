using System;

namespace NoBeerNoParty.Common.Model
{
    public class Ticket:Item
    {
        public string CustomerId { get; set; }
        public string BeerId { get; set; }
        public double Price { get; set; }
        public string SellerId { get; set; }
        public DateTime Date { get; set; }
        public string DispenserId { get; set; }
        public BeerSize Size { get; set; }
        public double AvaiblePercen { get; set; }
        public double AvaiableLiter { get; set; }
    }
}
