using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostograf
{
    public class NetTest:Host
    {
        private ITestMethod _testMethod;
        
        public string TestMethod
        {
            get { return TestMethod; }
            set
            {
                if (value.ToLower() == "tcp") _testMethod = new TcpTest();
                else _testMethod = new IcmpTest();
            } 
        }

        //public Host Host { get; set; }

        public string Port { get; set; }

        public NetTest(string TestMethod, string Hostname, string Description, string Port = "")
            : base(Hostname, Description)
        {
            this.TestMethod = TestMethod;
            this.Port = Port;
        }

        public NetTest(string TestMethod, Host host, string Port=""):base(host.Hostname, host.Description)
        {
            this.TestMethod = TestMethod;
            //this.Host = Host;
            this.Port = Port;
        }

        public void SetTestMethod(string TestMethod)
        {
            this.TestMethod = TestMethod;
        }

        public void TestExecute()
        {
            _testMethod.test(this);
        }
    }
}
