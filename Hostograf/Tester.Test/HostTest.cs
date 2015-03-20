using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester.Test
{
    [TestClass]
    public class HostTest
    {
/*
        [TestMethod]
        public void AddTestElement_CorrectAddTestElement_TestElement()
        {
            //arrange
            Host host = new Host() {Id = new Guid(), Description = "TestHost" };
            TestFactory test = new TestFactory_ICMP(new Guid(), "some.test.address");

            //act
            host.AddTestElement(test);

            //assert
            Assert.AreEqual(host.GetTestCollection()[0], test);
        }
*/


        /*[TestMethod]
        public void AddRangeTestElement_CorrectAddRangeTestElement_TestCollection()
        {
            //arrange
            Host host = new Host(new Guid(), "TestHost");
            TestFactory test1 = new TestFactory_ICMP(new Guid(), "some.test.address");
            TestFactory test2 = new TestFactory_ICMP(new Guid(), "some.test.address");
            IList<TestFactory> testRange = new List<TestFactory>(){test1, test2};

            //act
            host.AddTestRange(testRange);

            //assert
            Assert.AreEqual(host.GetTestCollection().Count, 2);
        }*/


        [TestMethod]
        public void RemoveElement_CorrectRemoveTestElement_NullTestCollection()
        {
            //arrange
            TestFactory test = new TestFactory_ICMP(new Guid(), "some.test.address");
            Host host = new Host(new Guid(), "TestHost", test);
            
            //act
            host.RemoveTestElement(test);

            //assert
            Assert.AreEqual(host.GetTestCollection().Count, 0);
        }


        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetTestCollection_AddTestInReadOnlyCollection_NotSupportedException()
        {
            //arrange
            Host host = new Host(new Guid(), "TestHost");
            TestFactory test = new TestFactory_ICMP(new Guid(), "some.test.address");

            //act
            host.GetTestCollection().Add(test);

            //assert is handled by ExpectedException
        }


        private bool testCollectionIsChanged;
        void OnChangeCollection()
        {
            testCollectionIsChanged = true;
        }

        [TestMethod]
        public void AddTestElement_OnChangeCollectionEvent_True()
        {
            //arrange

            Host host = new Host(new Guid(), "TestHost");
            TestFactory test = new TestFactory_ICMP(new Guid(), "some.test.address");
            host.OnChangeTestCollection += OnChangeCollection;
            
            //act
            testCollectionIsChanged = false;
            host.AddTestElement(test);
            
            //assert
            Assert.AreEqual(testCollectionIsChanged, true);
        }


        [TestMethod]
        public void AddTestRange_OnChangeCollectionEvent_True()
        {
            //arrange
            TestFactory test1 = new TestFactory_ICMP(new Guid(), "some.test.address");
            TestFactory test2 = new TestFactory_ICMP(new Guid(), "some.test.address");
            IList<TestFactory> testRange = new List<TestFactory>() { test1, test2 };
            Host host = new Host(new Guid(), "TestHost", testRange);
            host.OnChangeTestCollection += OnChangeCollection;

            //act
            testCollectionIsChanged = false;
            host.AddTestRange(testRange);

            //assert
            Assert.AreEqual(testCollectionIsChanged, true);
        }


        [TestMethod]
        public void RemoveTestElement_OnChangeCollectionEvent_Invoke()
        {
            //arrange
            Host host = new Host(new Guid(), "TestHost");
            TestFactory test = new TestFactory_ICMP(new Guid(), "some.test.address");
            host.OnChangeTestCollection += OnChangeCollection;

            //act
            host.AddTestElement(test);
            testCollectionIsChanged = false;
            host.RemoveTestElement(test);

            //assert
            Assert.AreEqual(testCollectionIsChanged, true);
        }

       
    }
}
