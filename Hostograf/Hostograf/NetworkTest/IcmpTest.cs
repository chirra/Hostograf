using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostograf
{
    public class IcmpTest:ITestMethod
    {

        public void test(NetTest netTest)
        {
            Console.WriteLine("{0} ICMP Test", netTest.Hostname);
        }
    }
}
