using System.Net;
using System.Net.Sockets;
using System.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.NetworkInformation;


namespace Tester
{
    /// <summary>
    /// ICMP Test send ping
    /// </summary>
	public class TestFactory_ICMP : TestFactory
	{

        //public delegate void TestExecuteError(object error);

        public override event TestExecuteError OnExecuteError;

        /// <summary>
        /// Can to work with IP address or hostname
        /// </summary>
        public string Address { get; set; }
 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address"></param>
        public TestFactory_ICMP(Guid id, string address)
        {
            Id = id;
            Address = address;
	    }


        /// <summary>
        /// Run test
        /// </summary>
        /// <returns></returns>
	    public override bool Execute()
        {
           // CheckedNow = true;
            var result = false;
            try
            {
                var ipAddress = Dns.GetHostEntry(Address).AddressList[0].ToString();
                var ping = new Ping();
                var pingReply = ping.Send(ipAddress);
                if (pingReply.Status == IPStatus.Success)
                    result = true;
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception.Message);
                if (OnExecuteError != null) OnExecuteError.Invoke(this, exception);
            }

            //CheckedNow = false;
            return result;

        }


	    public override string ToString()
	    {
	        return "ICMP: " + Address;
	    }
	}
}

