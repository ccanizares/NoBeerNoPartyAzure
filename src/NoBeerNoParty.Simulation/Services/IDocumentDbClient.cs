using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public interface IDocumentDbClient<T> where T: Item
    {
        Task<T> AddAsync(T value);
        Task<T> UpdateAsync(T value);
        Task Remove(string id);
        Task<T> GetByKey(string id);
        Task<T> GetAll(string text);
    }
}
