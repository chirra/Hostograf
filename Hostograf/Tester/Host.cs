using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Reflection;
using DAL;
using PL;

namespace Tester
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

    public class Host// : INotifyPropertyChanged
	{
        public delegate void ChangeEnabled();

	    public event ChangeEnabled OnChangeEnabled;

        private Guid id;
        public Guid Id { get { return id; } set { id = value; } }

        private string description;
		public string Description { get { return description; } set { description = value; } }
	    
        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
            /*{
                if (enabled != value && PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Enabled"));
                enabled = value;
            }*/
        }

        public delegate void ChangeTestCollection();
        public event ChangeTestCollection OnChangeTestCollection;

        List<TestFactory> testCollection = new List<TestFactory>(); 
        //public TrulyObservableCollection<TestFactory> TestCollection { get; set; }
        public IList<TestFactory> TestCollection
        {
            get
            {
                return GetTestCollection();
            }
            set
            {
                testCollection.Clear();
                AddTestRange(value);
            }
        }

        /// <summary>
        /// Get collection of tests as read only. For remove or add element use another methods.
        /// </summary>
        /// <returns></returns>
        public IList<TestFactory> GetTestCollection()
        {
            return testCollection.AsReadOnly();
            //return testCollection;
        }

        public void AddTestRange(IList<TestFactory> collection)
        {
            foreach (var element in collection)
            {
                testCollection.Add(element);
            }
            if (OnChangeTestCollection != null) OnChangeTestCollection.Invoke();
        }

        public void AddTestElement(TestFactory element)
        {
            testCollection.Add(element);
            if (OnChangeTestCollection != null) OnChangeTestCollection.Invoke();
        }

        public void RemoveTestElement(TestFactory element)
        {
            testCollection.Remove(element);
            if (OnChangeTestCollection != null) OnChangeTestCollection.Invoke();
        }
 

	    public Host()
	    {
            Id = Guid.NewGuid();
	        Description = "";
	        Enabled = true;
            //TestCollection = new TrulyObservableCollection<TestFactory>();
	    }


        public Host(Guid id, string description)
        {
            Id = id;
	        this.description = description;
            this.enabled = true;
            //TestCollection = new TrulyObservableCollection<TestFactory>();
	    }

	    public Host(Guid id, string description, TestFactory test, bool enabled=true)
	    {
	        Id = id;
	        this.Description = description;
	        //Test = this.Test;
            //TestCollection = new TrulyObservableCollection<TestFactory>();
            AddTestElement(test);
	        Enabled = enabled;
	    }

        public Host(Guid id, string description, IList<TestFactory> testCollection, bool enabled = true)
        {
            Id = id;
	        this.description = description;
	        this.enabled = enabled;
            //TestCollection = new TrulyObservableCollection<TestFactory>();

            /*foreach (var icmpTest in testCollection.OfType<TestFactory_ICMP>())
                //TestCollection.Add(new TestFactory_ICMP(icmpTest.Id, icmpTest.Address));
                this.AddTestElement(new TestFactory_ICMP(icmpTest.Id, icmpTest.Address));

            foreach (var tcpTest in testCollection.OfType<TestFactory_TCP>())
                //TestCollection.Add(new TestFactory_TCP(tcpTest.Id, tcpTest.Address, tcpTest.Port));
                this.AddTestElement(new TestFactory_TCP(tcpTest.Id, tcpTest.Address, tcpTest.Port));*/
            AddTestRange(testCollection);

	    }

        public Host(Host host)
        {
            Id = host.Id;
            description = host.Description;
            enabled = host.Enabled;
            //TestCollection = new TrulyObservableCollection<TestFactory>();

            /*foreach (var icmpTest in host.TestCollection.OfType<TestFactory_ICMP>())
                //TestCollection.Add(new TestFactory_ICMP(icmpTest.Id, icmpTest.Address));
                this.AddTestElement(new TestFactory_ICMP(icmpTest.Id, icmpTest.Address));

            foreach (var tcpTest in host.TestCollection.OfType<TestFactory_TCP>())
                //TestCollection.Add(new TestFactory_TCP(tcpTest.Id, tcpTest.Address, tcpTest.Port));
                this.AddTestElement(new TestFactory_TCP(tcpTest.Id, tcpTest.Address, tcpTest.Port));*/
            AddTestRange(host.TestCollection);
        }

        public override string ToString()
        {
            return Description + ":" + Enabled;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

    }
}

