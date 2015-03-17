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

        TrulyObservableCollection<ObservableTestFactory> observableTestCollection = new TrulyObservableCollection<ObservableTestFactory>();

       public TrulyObservableCollection<ObservableTestFactory> ObservableTestCollection
        {
            get { return observableTestCollection; }
            set { observableTestCollection = value; }
/*
            get
            {
                var resultCollection = new TrulyObservableCollection<ObservableTestFactory>();
                foreach (var element in TestCollection)
                    resultCollection.Add(new ObservableTestFactory(element));
                    return resultCollection;
            }
            set
            {
                var newTestCollection = new List<TestFactory>();
                foreach (var element in value)
                    newTestCollection.Add(element.TestFactory);
                TestCollection = newTestCollection;
            }
*/
        }


        /*public TrulyObservableCollection<ObservableTestFactory> ObservableTestCollection
        {
            get
            {
                var resultCollection = new TrulyObservableCollection<ObservableTestFactory>();
                foreach (var element in TestCollection)
                    //resultCollection.Add(new ObservableTestFactory(element));
                    resultCollection.Add((ObservableTestFactory)element);
                return resultCollection;
            }
            
        }*/

        public ObservableHost()
        {
            
        }
        
        public ObservableHost(Host host) : base(host)
        {
            this.OnChangeTestCollection += HostOnChangeTestCollection;
            HostOnChangeTestCollection();
        }

        private void HostOnChangeTestCollection()
        {
            ObservableTestCollection.Clear();
            foreach (TestFactory test in TestCollection)
            {
                ObservableTestCollection.Add(new ObservableTestFactory(test));
            }
        }
    }
}
