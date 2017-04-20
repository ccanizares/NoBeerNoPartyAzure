using CsvHelper.Configuration;
using NoBeerNoParty.Common.Model;

namespace NoBeerNoParty.Simulation.Services
{
    /// <summary>
    /// Mapper for currency csv file configuration
    /// </summary>
    public sealed class BeersCsvMapper: CsvClassMap<Beer>, ICsvMapper<Beer>
    {
        /// <summary>
        /// CurrencyCsvMapper Constructor
        /// </summary>
        public BeersCsvMapper()
        {
            Map(m => m.ExternalId).Index(0);
            Map(m => m.Name).Index(3);
            Map(m => m.BreweryId).Index(1);
            Map(m => m.Price).Index(2);
            Map(m => m.Style).Index(4).TypeConverter(new BeerTypeConverter());
        }
    }
}