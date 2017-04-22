using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public class DispenserService
    {
        public DispenserService()
        {

        }

        public void RefillIfNeeded(List<Dispenser> dispensers, Ticket ticket)
        {
            dispensers.RemoveAll(x => x.ExternalId == ticket.DispenserId);
            //TODO: implement brewery stock logic for future scenarios.
            //if (dispenser.AvaiableLiter.Equals(0))
            //{
            //    dispenser.AvaiableLPercen = 100;
            //    dispenser.AvaiableLiter = new Dispenser().AvaiableLiter;
            //}
        }
    }
}
