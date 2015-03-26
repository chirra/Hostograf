using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Tester
{
    public class DBController
    {
        
        /// <summary>
        /// Get collection of host from DataBase and add in List &lt;Tester.Host&gt;
        /// </summary>
        /// <returns></returns>
        public IList<Host> GetHosts()
        {
            IList<Host> hostlist = new List<Host>();
            try
            {
                using (var repository = new HostografRepository())
                {
                    //Get Hosts from Database
                    var dbhostlist = repository.GetHosts();
                    
                    foreach (var host in dbhostlist)
                    {
                        IList<TestFactory> testCollection = new List<TestFactory>();
                        foreach (var icmpTest in host.Test.OfType<TestICMP>())
                            testCollection.Add(new TestFactory_ICMP(icmpTest.Id, icmpTest.Enabled, icmpTest.Address));
                        
                        foreach (var tcpTest in host.Test.OfType<TestTCP>())
                            testCollection.Add(new TestFactory_TCP(tcpTest.Id, tcpTest.Enabled, tcpTest.Address, tcpTest.Port));
                        
                        hostlist.Add(new Host(host.Id, host.Description, testCollection, host.Enabled));
                    }
                } //using

                return hostlist;
            }
            catch (Exception e)
            {
                ErrorHandler.ErrorHandlerToFile(this, e);
                return null;
            }
        }

        public void RemoveHost(Host host)
        {
        try
            {
                using (var repository = new HostografRepository())
                {
                    repository.RemoveHost(host.Id);
                    repository.Save();
                }
            }
            catch (Exception e)
            {
                ErrorHandler.ErrorHandlerToFile(this, e);
            }
        }


      public void AddOrUpdateHost(Host host)
        {
            DAL.Host dbHost = new DAL.Host();
            dbHost.Id = host.Id;
            dbHost.Description = host.Description;
            dbHost.Enabled = host.Enabled;

            foreach (var icmpTest in host.TestCollection.OfType<TestFactory_ICMP>())
                dbHost.Test.Add(new TestICMP() {Id = icmpTest.Id, Address = icmpTest.Address, Enabled = icmpTest.Enabled });
            foreach (var tcpTest in host.TestCollection.OfType<TestFactory_TCP>())
                dbHost.Test.Add(new TestTCP() { Id = tcpTest.Id, Address = tcpTest.Address, Port = tcpTest.Port, Enabled = tcpTest.Enabled });

            try
            {
                using (var repository = new HostografRepository())
                {
                    repository.UpdateHost(dbHost);
                    repository.Save();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                ErrorHandler.ErrorHandlerToFile(this, e);
            }
        }

    }
}
