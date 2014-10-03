using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostograf
{
    class Host
    {
        private ITestMethod _testMethod;

        public Host(ITestMethod testMethod)
        {
            _testMethod = testMethod;
        }

        public void SetTestMethod(ITestMethod testMethod)
        {
            _testMethod = testMethod;
        }

        public void TestExecute()
        {
            _testMethod.test();
        }
    }
}
