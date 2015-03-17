using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Reflection;
using DAL;


namespace Tester
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

    public class Host
	{
        private Guid id;
        public Guid Id { get { return id; } set { id = value; } }

        private string description;
		public string Description { get { return description; } set { description = value; } }
	    
        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public delegate void ChangeTestCollection();
        public event ChangeTestCollection OnChangeTestCollection;

        List<TestFactory> testCollection = new List<TestFactory>(); 

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
        }


        public Host(Guid id, string description)
        {
            Id = id;
	        this.description = description;
            this.enabled = true;
        }

	    public Host(Guid id, string description, TestFactory test, bool enabled=true)
	    {
	        Id = id;
	        this.Description = description;
	        AddTestElement(test);
	        Enabled = enabled;
	    }

        public Host(Guid id, string description, IList<TestFactory> testCollection, bool enabled = true)
        {
            Id = id;
	        this.description = description;
	        this.enabled = enabled;
            AddTestRange(testCollection);

	    }


        public Host(Host host)
        {
            Id = host.Id;
            description = host.Description;
            enabled = host.Enabled;
            AddTestRange(host.TestCollection);
        }


        public override string ToString()
        {
            return Description + ":" + Enabled;
        }

        
    }
}

