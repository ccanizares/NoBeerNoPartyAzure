using System;
using System.Collections.Generic;
using System.Text;

namespace NoBeerNoParty.Common.Model
{
    public class Beer:Item
    {
        public string Name { get; set; }
        public string BreweryId { get; set; }
        public BeerStyle Style { get; set; } 
        public double Price { get; set; }
    }
}
