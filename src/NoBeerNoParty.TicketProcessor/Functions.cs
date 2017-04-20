using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using RedDog.Search.Model;
using NoBeerNoParty.Common.Services;
using NoBeerNoParty.Common.Model;
using System.Configuration;

namespace NoBeerNoParty.TicketProcessor
{
    public class Functions
    {
        private static string SearchResourceName;
        private static string SearchKey;

        public static void Trigger([EventHubTrigger("tickets")] EventData message)
        {
            string data = Encoding.UTF8.GetString(message.GetBytes());
            SearchResourceName = ConfigurationManager.AppSettings["SearchResourceName"];
            SearchKey = ConfigurationManager.AppSettings["SearchMgtKey"];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Message received. Data: '{data}'");
            Console.ResetColor();

            var ticket = JsonConvert.DeserializeObject<Ticket>(data);
            Console.WriteLine($"Updating search index for document id: {ticket.DispenserId}");
                
            IndexOperation[] searchDoc = { new IndexOperation(IndexOperationType.Merge, "id", ticket.DispenserId)
                    .WithProperty("quantityPercen", ticket.AvaiblePercen) };

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
