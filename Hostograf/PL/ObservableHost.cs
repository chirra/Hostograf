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
    class ObservableHost: Host, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        

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

        //TrulyObservableCollection<TestFactory> observableTestCollection = new TrulyObservableCollection<TestFactory>();

        public TrulyObservableCollection<TestFactory> ObservableTestCollection
        {
            get
            {
                var resultCollection = new TrulyObservableCollection<TestFactory>();
                foreach (var element in TestCollection)
                    resultCollection.Add(element);
                return resultCollection;
            }
            set
            {
                var newTestCollection = new List<TestFactory>();
                foreach (var element in value)
                    newTestCollection.Add(element);
                TestCollection = newTestCollection;
            }
        }

        public ObservableHost(Host host) : base(host)
        {
            
        }

      
    }
}
