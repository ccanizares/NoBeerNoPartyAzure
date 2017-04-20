using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NoBeerNoParty.Simulation.Services
{
    /// <summary>
    /// Allows to read a CSV file and parse to a typed object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvStaticReferenceReader<T> : IStaticReferenceReader<T>
    {
        private readonly ICsvMapper<T> _mapper;
        /// <summary>
        /// CsvStaticReferenceReader constructor
        /// </summary>
        /// <param name="mapper">The mapping class information related with T</param>
        public CsvStaticReferenceReader(ICsvMapper<T> mapper)
        {
            _mapper = mapper;
        }


        /// <summary>
        /// Reads a Csv file passing a delimiter
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public List<T> Read(string pathToFile, string delimiter)
        {
            try
            {
                List<T> result = new List<T>();
                using (TextReader reader = File.OpenText(pathToFile))
                {
                    CsvConfiguration config = new CsvConfiguration();
                    config.Delimiter = delimiter;
                    config.SkipEmptyRecords = true;
                    config.HasHeaderRecord = true;
                    config.RegisterClassMap(_mapper.GetType());

                    var csv = new CsvReader(reader, config);
                    result = csv.GetRecords<T>().ToList<T>();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Default implementation for Read a Csv File.
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public List<T> Read(string pathToFile)
        {
            return this.Read(pathToFile, ",");
        }
    }
}