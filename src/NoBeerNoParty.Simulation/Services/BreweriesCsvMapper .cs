using CsvHelper.Configuration;
using NoBeerNoParty.Common.Model;

namespace NoBeerNoParty.Simulation.Services
{
    /// <summary>
    /// Mapper for currency csv file configuration
    /// </summary>
    public sealed class BreweriesCsvMapper: CsvClassMap<Brewery>, ICsvMapper<Brewery>
    {
        /// <summary>
        /// BreweriesCsvMapper Constructor
        /// </summary>
        public BreweriesCsvMapper()
        {
            Map(m => m.ExternalId).Index(0);
            Map(m => m.Name).Index(1);
        }
    }
}