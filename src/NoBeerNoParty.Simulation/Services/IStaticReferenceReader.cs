using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    /// <summary>
    /// Interface that defines a reader for static content files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStaticReferenceReader<T>
    {
        /// <summary>
        /// Reads a file and returns a typed list representing the content of the file. 
        /// </summary>
        /// <param name="pathToFile">the path to file</param>
        /// <param name="delimiter">the delimiter</param>
        /// <returns></returns>
        List<T> Read(string pathToFile, string delimiter);
    }
}
