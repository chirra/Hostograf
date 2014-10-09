using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostograf
{
    public class Host
    {
        public string Hostname { get; set; }
        public string Description { get; set; }

        public Host(string Hostname, string Description)
        {
            this.Hostname = Hostname;
            this.Description = Description;
        }

    }//class

 
}//namespace
