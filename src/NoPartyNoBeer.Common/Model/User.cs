using System;
using System.Collections.Generic;
using System.Text;

namespace NoBeerNoParty.Common.Model
{
    public class User:Item
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}
