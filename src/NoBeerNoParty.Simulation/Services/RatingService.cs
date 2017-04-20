using NoBeerNoParty.Common.Model;
using NoPartyNoBeer.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public class RatingService
    {
        public RatingService()
        {

        }

        public Rate Generate(List<Ticket> tickets, List<Rate> ratings)
        {
            var ticket = tickets[new Random().Next(tickets.Count)];
            var rate = new Rate()
            {
                BeerId = ticket.BeerId,
                Date = DateTime.Now,
                ExternalId = Guid.NewGuid().ToString(),
                Points = new Random().Next(5),
                UserId = ticket.CustomerId, 
                DispenserId = ticket.DispenserId
            };

            //remove ticket from list
            tickets.Remove(ticket);
            //add rating to the ticket
            ratings.Add(rate);
            rate.Average = (ratings.Where(x => x.BeerId.Equals(rate.BeerId)).Select(x => x.Points).Sum() / ratings.Count);

            return rate;
        }
    }
}
