using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.Configuration;

namespace NoBeerNoParty.RatingProcessor
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var config = new JobHostConfiguration();

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var eventHubConfig = new EventHubConfiguration();
            var ehConnectionString = ConfigurationManager.AppSettings["Eh:ConnectionString"];
            var ehHub = "ratings";

            eventHubConfig.AddReceiver(ehHub, ehConnectionString);
            config.UseEventHub(eventHubConfig);

            JobHost host = new JobHost(config);

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
