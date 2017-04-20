using System;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using NoBeerNoParty.Common.Model;

namespace NoBeerNoParty.Simulation.Services
{
    /// <summary>
    /// Mapper for currency csv file configuration
    /// </summary>
    public sealed class UsersCsvMapper : CsvClassMap<User>, ICsvMapper<User>
    {
        /// <summary>
        /// UsersCsvMapper Constructor
        /// </summary>
        public UsersCsvMapper()
        {
            Map(m => m.ExternalId).Index(0);
            Map(m => m.Name).Index(3);
            Map(m => m.Age).Index(1);
            Map(m => m.Gender).Index(2);
        }
    }


}