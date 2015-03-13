using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IHostografRepository : IDisposable
    {
        IQueryable<Host> GetHosts();
        Host GetHostByID(Guid hostId);
        void AddHost(Host host);
        void RemoveHost(Guid hostId);
        void UpdateHost(Host host);
        void Save();


    }
}
