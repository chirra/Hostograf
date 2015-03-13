using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {

        static void Main(string[] args)
        {
            DBController dbController = new DBController();

            //Add Host
          /*  TestFactory test = new TestFactory_ICMP("bbn.siam.ru");
            TestFactory test2 = new TestFactory_TCP("10.1.0.295", "3389");
            TestFactory test22 = new TestFactory_TCP("10.1.0.296", "3389");
            TestFactory test3 = new TestFactory_TCP("10.1.0.24", "3384");

            Host host = new Host(Guid.NewGuid(), "BBN");
            host.TestCollection.Add(test2);
            host.TestCollection.Add(test22);
            host.TestCollection.Add(test3);
            dbController.AddHost(host);*/


            //Console.WriteLine(host.TestExecute());
            /*foreach (var item in host.TestCollection)
            {
                Console.WriteLine(item.Execute());
            }*/


           // dbController.AddOrUpdateHost(host);


            var hosts = dbController.GetHosts();
           // dbController.RemoveHost(hosts[1]);
            hosts[1].Enabled = !hosts[1].Enabled;
            ((TestFactory_TCP)hosts[1].TestCollection[0]).Address = "xyxyxy";
            dbController.AddOrUpdateHost(hosts[1]);

            hosts = dbController.GetHosts();
            foreach (var item in hosts)
            {
                Console.WriteLine(item);   
            }

            Console.ReadKey();

            //TestFactory test2 = new TestFactory_TCP(host);
            //test2.execute();
        }

       
    }
}
