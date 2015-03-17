using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    
    /// <summary>
    /// Repository of simple operations with DB
    /// </summary>
    public class HostografRepository : IHostografRepository
    {

        private HostografContainer context;

        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public HostografRepository()
        {
            context = new HostografContainer();
        }
    
     
        /// <summary>
        /// Get host collection from DataBase
        /// </summary>
        /// <returns>Host Collection</returns>
        public IQueryable<Host> GetHosts()
        {
            return context.Host;
        }


        /// <summary>
        /// Get host from DataBase
        /// </summary>
        /// <param name="hostId"></param>
        /// <returns></returns>
        public Host GetHostByID(Guid hostId)
        {
            var query = from host in context.Host where host.Id==hostId select host;
            return query.First();
        }


        private bool _disposed = false;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Delete host from DataBase
        /// </summary>
        /// <param name="hostId"></param>
        public void RemoveHost(Guid hostId)
        {
           //var hostId = host.Id;
            
            // Select  and delete all tests for selected host
            var queryTest = from test in context.Test where test.Host.Id == hostId select test;
            var queryHost = from myhost in context.Host where myhost.Id == hostId select myhost;
            context.Test.RemoveRange(queryTest);
            context.Host.RemoveRange(queryHost);
        }


        /// <summary>
        /// Modify host in Database
        /// </summary>
        /// <param name="host"></param>
        public void UpdateHost(Host host)
        {
            // Get and update host in DataBase
            var dbHost = context.Host.Find(host.Id);    
            if (dbHost == null)
            {
                this.AddHost(host);
                return;
            }
            context.Entry(dbHost).CurrentValues.SetValues(host);

            // Get and update test in DataBase
            foreach (var test in host.Test)
            {
                var dbTest = context.Test.FirstOrDefault(t => t.Id == test.Id); // Get test from database
                if (dbTest != null)
                    context.Entry(dbTest).CurrentValues.SetValues(test); // Update test
                else
                {
                    test.Host = dbHost;                 // Or add if not exist
                    context.Test.Add(test);
                }
            }
          
            // Delete test from database if it does not exist in the host
            foreach (var dbTest in dbHost.Test.ToList())
            {
                if (host.Test.All(t => t.Id != dbTest.Id))
                    context.Test.Remove(dbTest);
            }
          
        }


        /// <summary>
        /// Applies the changes
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }


        /// <summary>
        /// Add Host to DataBase
        /// </summary>
        /// <param name="host"></param>
        public void AddHost(Host host)
        {
            context.Host.Add(host);
        }


       
     
    }
}
