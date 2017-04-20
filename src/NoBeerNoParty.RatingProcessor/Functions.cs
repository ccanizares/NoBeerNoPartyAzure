using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Configuration;
using RedDog.Search.Model;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using NoPartyNoBeer.Common.Model;
using NoBeerNoParty.Common.Services;

namespace NoBeerNoParty.RatingProcessor
{
    public class Functions
    {
        private static string SearchResourceName;
        private static string SearchKey;

        public static void Trigger([EventHubTrigger("ratings")] EventData message)
        {
            string data = Encoding.UTF8.GetString(message.GetBytes());

            SearchResourceName = ConfigurationManager.AppSettings["SearchResourceName"];
            SearchKey = ConfigurationManager.AppSettings["SearchMgtKey"];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Message received. Data: '{data}'");
            Console.ResetColor();

            var rate = JsonConvert.DeserializeObject<Rate>(data);
            Console.WriteLine($"Updating search index for document id: {rate.DispenserId}");

            IndexOperation[] searchDoc = { new IndexOperation(IndexOperationType.Merge, "id", rate.DispenserId)
                    .WithProperty("rate", rate.Average) };

            var searchSvc = new SearchService(SearchResourceName, SearchKey);

            var response = searchSvc.PopulateIndexAsync("beer", searchDoc).GetAwaiter().GetResult();
            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(response.Error.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Index updated!");
            }

            Console.ResetColor();
        }
    }
}
