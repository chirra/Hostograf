using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostograf
{
    class Program
    {
        static void Main(string[] args)
        {
            Host bbn = new Host("bbn.siamoil.ru", "BBN");
            NetTest test = new NetTest("icmp", bbn);
            test.TestExecute();
        }
    }
}
