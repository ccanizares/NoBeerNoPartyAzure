using CsvHelper.TypeConversion;
using NoBeerNoParty.Common.Model;
using System;

namespace NoBeerNoParty.Simulation.Services
{
    public class BeerTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            return true;
        }

        public bool CanConvertTo(Type type)
        {
            return true;
        }

        public object ConvertFromString(TypeConverterOptions options, string text)
        {
            int value = 1;
            int.TryParse(text, out value);

            return (BeerStyle)value;
        }

        public string ConvertToString(TypeConverterOptions options, object value)
        {
            return (value != null) ? value.ToString() : "";
        }

    }
}
