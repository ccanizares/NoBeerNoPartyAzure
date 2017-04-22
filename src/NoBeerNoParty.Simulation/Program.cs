using Newtonsoft.Json;
using NoBeerNoParty.Common.Model;
using NoBeerNoParty.Simulation.Services;
using NoBeerNoParty.Common.Services;
using RedDog.Search.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NoBeerNoParty.Simulation
{
    class Program
    {
        private static string EhConnectionString;
        private static string EhTicketsPath;
        private static string EhRatingsPath;
        private static string DbEndPointUrl;
        private static string DbAccessKey;
        private static string DbName;
        private static string SearchResourceName;
        private static string SearchKey;

        private static Simulator _simulator;

        static void Main(string[] args)
        {
            EhConnectionString = ConfigurationManager.AppSettings["Eh:ConnectionString"];
            EhTicketsPath = ConfigurationManager.AppSettings["Eh:TicketsPath"];
            EhRatingsPath = ConfigurationManager.AppSettings["Eh:RatingsPath"];
            DbEndPointUrl = ConfigurationManager.AppSettings["Db:EndpointUrl"];
            DbAccessKey = ConfigurationManager.AppSettings["Db:AccessKey"];
            DbName = ConfigurationManager.AppSettings["Db:Name"];
            SearchResourceName = ConfigurationManager.AppSettings["SearchResourceName"];
            SearchKey = ConfigurationManager.AppSettings["SearchMgtKey"];

            OptionSelector(args);
        }

        private static void OptionSelector(string[] args)
        {
            Console.WriteLine("Welcome to NoBeer No Party Seed Project, type your choice");
            Console.WriteLine("1 - Seed Db (this should be done once)");
            Console.WriteLine("2 - Create Beer Search Index (this should be done once)");
            Console.WriteLine("3 - Populate Beer Search Index (this should be done once)");
            Console.WriteLine("4 - Init simulation");

            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1": SeedDbAsync(args).GetAwaiter().GetResult(); break;
                case "2": CreateBeerSearchIndexAsync(args).GetAwaiter().GetResult(); break;
                case "3": PopulateBeerIndexAsync(args).GetAwaiter().GetResult(); break;
                case "4": TicketSimulationAsync(args).GetAwaiter().GetResult(); break;
            }
        }

        private static async Task TicketSimulationAsync(string[] args)
        {
            if (_simulator == null)
                _simulator = new Simulator(EhConnectionString, EhTicketsPath, EhRatingsPath);

            await _simulator.Start();
        }

        private static async Task CreateBeerSearchIndexAsync(string[] args)
        {
            var searchSvc = new SearchService(SearchResourceName, SearchKey);
            var index = new Index("beer")
                .WithStringField("id", f => f.IsKey().IsRetrievable())
                .WithStringField("beerName", f => f.IsRetrievable().IsSearchable())
                .WithStringField("brewery", f => f.IsRetrievable().IsSearchable().IsFilterable())
                .WithStringField("beerType", f=> f.IsRetrievable().IsSearchable().IsFacetable())
                .WithDoubleField("rate", f => f.IsRetrievable().IsFilterable().IsSortable())
                .WithStringField("stand", f => f.IsRetrievable().IsFilterable())
                .WithDoubleField("price", f => f.IsRetrievable())
                .WithDoubleField("quantityPercen", f => f.IsRetrievable().IsFilterable().IsSortable());

            var response = await searchSvc.CreateIndexAsync(index);
            if (response.Error != null)
                Console.WriteLine(response.Error.Message);
            else
                Console.WriteLine("Indice de búsqueda creado!");

            Console.ReadLine();
            OptionSelector(args);
        }

        private static void GenerateRandomAges()
        {
            for (var i = 0; i <= 1000; i++)
            {
                Console.WriteLine(new Random().Next(18,70));
            }
            Console.ReadLine();
        }

        private static void GenerateGuids()
        {
            for (var i = 0; i <= 1000; i++)
            {
                Console.WriteLine(Guid.NewGuid());
            }
            Console.ReadLine();
        }

        private static void SerialiceTicket()
        {
            var ticket = new Ticket()
            {
                BeerId = "beerId",
                CustomerId = "customerId",
                Date = DateTime.Now,
                DispenserId = "dispenserId",
                ExternalId = "externalId",
                Price = 7,
                SellerId = "sellerId",
                Size = BeerSize.Big
            };

            var json = JsonConvert.SerializeObject(ticket);
            Console.WriteLine(json);
            Console.ReadLine();
        }

        private static void GenerateRandomePrices()
        {
            for (var i = 0; i <= 100; i++)
            {
                Console.WriteLine(new Random().Next(10));
            }
            Console.ReadLine();
        }

        private static async Task PopulateBeerIndexAsync(string[] args)
        {
            var searchSvc = new SearchService(SearchResourceName, SearchKey);
            var dispensers = new CsvStaticReferenceReader<Dispenser>(new DispenserCsvMapper()).Read("Csv\\Dispensers.csv");
            var beers = new CsvStaticReferenceReader<Beer>(new BeersCsvMapper()).Read("Csv\\Beers.csv");
            var breweries = new CsvStaticReferenceReader<Brewery>(new BreweriesCsvMapper()).Read("Csv\\Breweries.csv");

            var idxData = dispensers.Select(x =>
            {
                var beer = beers.Where(b => b.ExternalId.Equals(x.BeerId)).First();
                return new IndexOperation(IndexOperationType.MergeOrUpload, "id", x.ExternalId)
                    .WithProperty("beerName", beer.Name)
                    .WithProperty("brewery", breweries.Where(b => b.ExternalId.Equals(beer.BreweryId)).First().Name)
                    .WithProperty("beerType", beer.Style.ToString())
                    .WithProperty("rate", 0)
                    .WithProperty("stand", x.Stand)
                    .WithProperty("price", beer.Price)
                    .WithProperty("quantityPercen", 100);
                }
            ).ToArray();

            var response = await searchSvc.PopulateIndexAsync("beer", idxData);
            if (response.Error != null)
                Console.WriteLine(response.Error.Message);
            else
                Console.WriteLine("Index populated!");

            Console.ReadLine();
            OptionSelector(args);
        }

        private static async Task SeedDbAsync(string[] args)
        {
            var userClient = new DocumentDbClient<User>(DbEndPointUrl, DbAccessKey, DbName);
            var beerClient = new DocumentDbClient<Beer>(DbEndPointUrl, DbAccessKey, DbName);
            var breweryClient = new DocumentDbClient<Brewery>(DbEndPointUrl, DbAccessKey, DbName);
            var dispenserClient = new DocumentDbClient<Dispenser>(DbEndPointUrl, DbAccessKey, DbName);

            //Read Csv
            var users = new CsvStaticReferenceReader<User>(new UsersCsvMapper()).Read("Csv\\Users.csv");
            var beers = new CsvStaticReferenceReader<Beer>(new BeersCsvMapper()).Read("Csv\\Beers.csv");
            var breweries = new CsvStaticReferenceReader<Brewery>(new BreweriesCsvMapper()).Read("Csv\\Breweries.csv");
            var dispensers = new CsvStaticReferenceReader<Dispenser>(new DispenserCsvMapper()).Read("Csv\\Dispensers.csv");

            //Write DocumentDb
            foreach (var user in users)
            {
                await userClient.AddAsync(user);
            }

            foreach (var beer in beers)
            {
                await beerClient.AddAsync(beer);
            }

            foreach (var brewery in breweries)
            {
                await breweryClient.AddAsync(brewery);
            }

            foreach (var dispenser in dispensers)
            {
                await dispenserClient.AddAsync(dispenser);
            }

            Console.Write("Seed Finished");
            Console.ReadLine();
            OptionSelector(args);
        }
    }
}
