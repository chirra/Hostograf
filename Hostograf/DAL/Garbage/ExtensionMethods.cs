using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class ExtensionMethods
    {
        public static IQueryable<Host> HostWithTestICMP(this HostografContainer context)
        {
            return context.Host.Include("ToTestICMP.Address");
        }

        public static IQueryable<Host> HostWithTestTCP(this HostografContainer context)
        {
            return context.Host.Include("ToTestTCP.Address").Include("ToTestTCP.Port");
        }

        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
