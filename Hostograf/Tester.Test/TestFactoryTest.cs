using System;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using Moq;
//using NUnit.Framework;

namespace Tester.Test
{
    [TestClass]
    public class TestFactoryTest
    {
        [TestMethod]
        public void TestFactoryIcmp_LocalhostExecute_True()
        {
            //arrange
            TestFactory test = new TestFactory_ICMP(new Guid(), true, "localhost");

            //act
            bool result = test.Execute();

            //assert
            Assert.IsTrue(result);
        }

/*
        [TestMethod]
        public void TestFactoryIcmp_NonExistHostExecute_False()
        {
            //arrange
            TestFactory test = new TestFactory_ICMP(new Guid(), "non.exist.host");

            //act
            bool result = test.Execute();

            //assert
            Assert.IsFalse(result);
        }
*/
        

        private bool testExecuteError;

        void onExecuteError(object sender, object e)
        {
            testExecuteError = true;
        }


        [TestMethod]
        public void TestFactoryIcmp_NonExistHostExecute_OnExecuteErrorEvent()
        {
            //arrange
            TestFactory test = new TestFactory_ICMP(new Guid(), true, "non.exist.host");
            test.OnExecuteError += onExecuteError;

            //act
            testExecuteError = false;
            bool result = test.Execute();

            //assert
            Assert.IsTrue(testExecuteError);
        }


        [TestMethod]
        public void TestFactoryTCP_NonExistHostExecute_OnExecuteErrorEvent()
        {
            //arrange
            TestFactory test = new TestFactory_TCP(new Guid(), true, "non.exist.host", "99999");
            test.OnExecuteError += onExecuteError;

            //act
            testExecuteError = false;
            bool result = test.Execute();
            

            //assert
            Assert.IsTrue(testExecuteError);
        }


    }
}
