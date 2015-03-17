using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tester;

namespace PL
{
    public class ObservableHost: Host, INotifyPropertyChanged
    {
        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Observe changes Enabled properties of test
        /// </summary>
        public bool ObservableEnabled
        {
            get { return Enabled; }
            set 
            {
                if (Enabled != value && PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ObservableEnabled"));
                Enabled = value;
            }
        }

        TrulyObservableCollection<ObservableTestFactory> observableTestCollection = new TrulyObservableCollection<ObservableTestFactory>();

       public TrulyObservableCollection<ObservableTestFactory> ObservableTestCollection
        {
            get { return observableTestCollection; }
            set { observableTestCollection = value; }
        }

        public ObservableHost()
        {
            
        }
        
        public ObservableHost(Host host) : base(host)
        {
            this.OnChangeTestCollection += GetParentTestCollection;
            GetParentTestCollection(); 
        }

        /// <summary>
        /// Get collection of tests from base class
        /// </summary>
        private void GetParentTestCollection()
        {
            ObservableTestCollection.Clear();
            foreach (TestFactory test in TestCollection)
            {
                ObservableTestCollection.Add(new ObservableTestFactory(test));
            }
        }
    }
}
