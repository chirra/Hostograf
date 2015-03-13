using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class DBWorker
    {
        private HostografContainer db;

        public DBWorker()
        {
            db = new HostografContainer();
            
        }

        public void AddHostToDatabase(string Description, string Address, string Method = "ICMP", string Port = "",
            bool Enabled = true)
        {
            //Host host = new Host(){Description = Description, Address=Address, Method = Method, Port = Port, Enabled = Enabled};
            //db.Hosts.Add(host);
            //db.SaveChanges();
        }

        public List<Host> GetHostsFromDatabase()
        {
            //ArrayList methods = new ArrayList();
            //List<Method> methods = new List<Method>();

            //var query;
            /*
            ArrayList hosts = new ArrayList();

            using (var db = new HostografContainer())
            {
                var query = from c in db.Hosts select c;
                hosts.Add(query);
                
            }

            foreach (var item in hosts)
            {
                Console.WriteLine(item);
            }*/

            List<Host> newResult = db.Host.AsNoTracking().ToList();



            /*using (var db2 = new HostografContainer())
            {
                newResult = db2.Database.SqlQuery<Host>("SELECT * FROM Hosts").ToList();
            }*/

            return newResult;

            
            
            /*
            ArrayList hosts = new ArrayList();

            foreach (var item in db.Hosts)
            {
                hosts.Add(item);
            }*/

            //var hosts2 = from x in db.Hosts select x;
            //return hosts2 as ArrayList;
            //return hosts;

            /*Host host = new Host() { Description = "BBN", ToMethod = methods[0], Address = "bbn.siam.ru", Enabled = true };
            Method method = new Method() { Name = "Agent" };

            db.Hosts.Add(host);
            db.Methods.Add(method);
            db.SaveChanges();

            Console.WriteLine(methods[0].Name);*/

        }
    }
}
