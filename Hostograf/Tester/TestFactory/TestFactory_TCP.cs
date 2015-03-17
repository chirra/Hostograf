using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tester
{

	public class TestFactory_TCP : TestFactory
	{
        public override event TestExecuteError OnExecuteError;

        /// <summary>
        /// Can to work with IP address or hostname
        /// </summary>
	    public string Address { get; set; }

        /// <summary>
        /// TCP Port
        /// </summary>
	    public string Port { get; set; }
	    
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public TestFactory_TCP(Guid id, string address, string port)
        {
            Id = id;
            Address = address;
            Port = port;
        }

        /// <summary>
        /// Run test
        /// </summary>
        /// <returns></returns>
	    public override bool Execute()
        {

            bool result = false;
            TcpClient tcpClient = new TcpClient();

            try
            {
                var ipAddress = Dns.GetHostEntry(Address).AddressList[0].ToString();
                tcpClient.Connect(ipAddress, Int32.Parse(Port));
                tcpClient.Close();
                result = true;

            }
            catch (Exception exception)
            {
                if (OnExecuteError != null) OnExecuteError.Invoke(this, exception);
            }
	        return result;
        }

	    public override string ToString()
	    {
            return "TCP: " + Address + ":" + Port;
	    }
	}
}

