using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public class EventHubClient<T> : IEventHubClient<T> where T: Item
    {
        private readonly EventHubClient _client;

        public EventHubClient(string connectionString, string entityPath)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(connectionString)
            {
                EntityPath = entityPath
            };

            _client = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }

        public void Dispose()
        {
            _client.Close();
        }

        public async Task SendMessageAsync(T item)
        {
            var message = JsonConvert.SerializeObject(item);
            await _client.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }
    }
}
