using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            TestICMP testicmp = new TestICMP() {Address = "bbn.siam.ru"};
            TestTCP testtcp = new TestTCP() {Address = "bbn.siam.ru", Port = "3389"};
            ICollection<Test> tests = new List<Test>();
            tests.Add(testicmp);
            tests.Add(testtcp);

            //TestTCP testtcp = new TestTCP(){Address = "bbn.siamoil.ru", Port = "3389"};
            Host host = new Host() { Description = "BBN", Enabled = true, Test = tests};

            Test [] testforss = new Test[1];
            testforss[0] = new TestICMP(){Address = "ss.siam.ru"};
            Host host2 = new Host(){Description = "SS", Enabled = true, Test = testforss};

            
            using (IHostografRepository repository = new HostografRepository())
            {


                repository.AddHost(host);
                repository.AddHost(host2);
                repository.Save();
               
               // repository.RemoveHost(3);
               // repository.Save();

               // Host host3 = repository.GetHostByID(4);
               // host3.Description = "Moon";
               // repository.UpdateHost(host3);
               // repository.Save();

                var hosts = repository.GetHosts();
                
                TestICMP icmpElement;
                TestTCP tcpElement;

                foreach (var item in hosts)
                {
                    if (item.Test.OfType<TestICMP>().Any())
                    {
                        icmpElement = item.Test.OfType<TestICMP>().First();
                        Console.WriteLine(item.Id + " " + item.Description + " " + item.Enabled + " " +
                                          icmpElement.Address + " ");
                    }

                    if (item.Test.OfType<TestTCP>().Any())
                    {
                        tcpElement = item.Test.OfType<TestTCP>().First();
                        Console.WriteLine(item.Id + " " + item.Description + " " + item.Enabled + " " +
                                          tcpElement.Address + " " + tcpElement.Port);
                    }
                    
                }

                
            }


        }
    }
}
