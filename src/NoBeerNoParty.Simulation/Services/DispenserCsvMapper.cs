using CsvHelper.Configuration;
using NoBeerNoParty.Common.Model;

namespace NoBeerNoParty.Simulation.Services
{
    /// <summary>
    /// Mapper for currency csv file configuration
    /// </summary>
    public sealed class DispenserCsvMapper: CsvClassMap<Dispenser>, ICsvMapper<Dispenser>
    {
        /// <summary>
        /// BreweriesCsvMapper Constructor
        /// </summary>
        public DispenserCsvMapper()
        {
            Map(m => m.ExternalId).ConvertUsing(x => x.GetField(0).ToString());
            Map(m => m.BeerId).Index(1);
            //Map(m => m.BreweryId).Index(2);
            Map(m => m.Stand).Index(2);
        }
    }
}