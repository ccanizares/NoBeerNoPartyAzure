using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public class TicketService
    {
        public TicketService()
        {

        }

        public Ticket Generate(List<Dispenser> dispensers, List<User> users, List<Beer> beers, List<BeerSize> sizes)
        {
            var dispenser = dispensers[new Random().Next(dispensers.Count)]; //TODO: This logic can be improved with rating.
            var user = users[new Random().Next(users.Count)];
            var size = sizes[new Random().Next(sizes.Count)];
            var beer = beers.Where(x => x.ExternalId.Equals(dispenser.BeerId)).FirstOrDefault();

            var price = (size == BeerSize.Big) ? beer.Price * 1.5 : (size == BeerSize.Small) ? beer.Price * 0.5 : beer.Price;
            dispenser.AvaiableLiter -= ((size == BeerSize.Big) ? 1 : (size == BeerSize.Small) ? 0.5 : 1);
            if (dispenser.AvaiableLiter < 0)
                dispenser.AvaiableLiter = 0;

            dispenser.AvaiableLPercen = dispenser.AvaiableLiter * 100 / dispenser.CapacityLiters;

            return new Ticket()
            {
                BeerId = dispenser.BeerId,
                CustomerId = user.ExternalId,
                Date = DateTime.Now,
                Price = price,
                Size = size,
                SellerId = Guid.NewGuid().ToString(), //TOOD: implement seller's logic.
                DispenserId = dispenser.ExternalId,
                AvaiblePercen = dispenser.AvaiableLPercen,
                AvaiableLiter = dispenser.AvaiableLiter
            };
        }
    }
}
