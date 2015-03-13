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
        //public delegate void TestExecuteError(object error);
        
        public override event TestExecuteError OnExecuteError;

	    public string Address { get; set; }
	    public string Port { get; set; }
	    
        public TestFactory_TCP(Guid id, string address, string port)
        {
            Id = id;
            Address = address;
            Port = port;
        }

	    public override bool Execute()
        {

            bool result = false;
	        
            TcpClient tcpClient = new TcpClient();
	       // CheckedNow = true;
            try
            {
                var ipAddress = Dns.GetHostEntry(Address).AddressList[0].ToString();
                tcpClient.Connect(ipAddress, Int32.Parse(Port));
                tcpClient.Close();
                result = true;

            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception.Message);
                //Console.WriteLine("Print from {0} : {1}", this.GetType(), exception);
                if (OnExecuteError != null) OnExecuteError.Invoke(this, exception);
                //OnExecuteError = new TestExecuteError(exception);
                    
            }
	       // CheckedNow = false;
	        return result;
        }

	    public override string ToString()
	    {
            return "TCP: " + Address + ":" + Port;
	    }
	}
}

