using Microsoft.Azure.Documents;
using NoBeerNoParty.Common.Model;
using NoPartyNoBeer.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public class Simulator
    {
        private readonly string _EhConnectionString;
        private readonly string _EhTicketsPath;
        private readonly string _EhRatingsPath;
        private readonly List<Common.Model.User> _users;
        private readonly List<Beer> _beers;
        private readonly List<BeerSize> _sizes;
        private readonly List<Dispenser> _dispensers;
        private readonly List<Ticket> _tickets;
        private readonly TicketService _ticketService;
        private readonly DispenserService _dispenserService;
        private readonly RatingService _ratingService;
        private readonly EventHubClient<Ticket> _ticketsEhClient;
        private readonly EventHubClient<Rate> _ratingsEhClient;

        public Simulator(string EhConnectionString, string EhTicketsPath, string EhRatingsPath)
        {
            _EhConnectionString = EhConnectionString;
            _EhTicketsPath = EhTicketsPath;
            _EhRatingsPath = EhRatingsPath;

            _dispensers = new CsvStaticReferenceReader<Dispenser>(new DispenserCsvMapper()).Read("Csv\\Dispensers.csv");
            _users = new CsvStaticReferenceReader<Common.Model.User>(new UsersCsvMapper()).Read("Csv\\Users.csv");
            _beers = new CsvStaticReferenceReader<Beer>(new BeersCsvMapper()).Read("Csv\\Beers.csv");
            _sizes = Enum.GetValues(typeof(BeerSize)).Cast<BeerSize>().ToList();

            _tickets = new List<Ticket>();
            _ticketService = new TicketService();
            _dispenserService = new DispenserService();
            _ratingService = new RatingService();
            _ticketsEhClient = new EventHubClient<Ticket>(_EhConnectionString, _EhTicketsPath);
            _ratingsEhClient = new EventHubClient<Rate>(_EhConnectionString, _EhRatingsPath);
        }

        public async Task Start()
        {
            var rating = 0;
            var ratings = new List<Rate>();
            Console.WriteLine("... Don't close the application or you'll need to regenerate index ... Press Enter to pause/start the simulation");
            //There is no return from this simulation XD..
            while (true)
            {
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        if (!rating.Equals(5))
                        {
                            try
                            {
                                var ticket = _ticketService.Generate(_dispensers, _users, _beers, _sizes);
                                _tickets.Add(ticket);
                                Console.WriteLine($"Sending message: {ticket.Date.Ticks.ToString()}");
                                await _ticketsEhClient.SendMessageAsync(ticket);

                                _dispenserService.RefillIfNeeded(_dispensers, ticket);
                                rating++;
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                            }
                        }
                        else
                        {
                            try
                            {
                                var rate = _ratingService.Generate(_tickets, ratings);
                                Console.WriteLine($"Sending message: {rate.Date.Ticks.ToString()}");
                                await _ratingsEhClient.SendMessageAsync(rate);
                                rating = 0;
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                            }
                        }
                        await Task.Delay(100);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
                Console.WriteLine("Simulation Paused...");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                {
                    Console.ReadKey();
                }
            }
        }
        
    }
}
