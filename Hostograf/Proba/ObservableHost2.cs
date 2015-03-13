using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tester;

namespace PL
{
    class ObservableHost2: Host
    {
        ObservableCollection<TestFactory> TestCollection { get; set; }

        public ObservableHost2(Host host)// : base(host.Description, host.TestCollection, host.Enabled)
        {
            TestCollection = new ObservableCollection<TestFactory>(host.TestCollection);
        }

        public Host ToHost()
        {
            IList<TestFactory> testCollection = new List<TestFactory>();
            foreach (TestFactory test in TestCollection)
                testCollection.Add(test);
            return new Host(Description, testCollection, Enabled);
        }
    }
}
