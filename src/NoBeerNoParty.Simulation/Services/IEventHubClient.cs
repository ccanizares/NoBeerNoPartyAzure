using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public interface IEventHubClient<T>: IDisposable where T : Item
    {
        Task SendMessageAsync(T message);
    }
}
