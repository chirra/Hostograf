using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tester;

namespace PL
{
    public class ObservableTestFactory: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        //public override event TestExecuteError OnExecuteError;


        TestFactory testFactory;

        public TestFactory TestFactory { get { return testFactory; } }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="testFactory"></param>
        public ObservableTestFactory(TestFactory testFactory)
        {
            this.testFactory = testFactory;
        }


        public string Name
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return testFactory.ToString();
        }

        /// <summary>
        /// Run test
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            return testFactory.Execute();
        }

        /// <summary>
        /// Observe changes Enabled properties of test
        /// </summary>
        public bool ObservableEnabled
        {
            get { return testFactory.Enabled; }
            set
            {
                if (testFactory.Enabled != value && PropertyChanged != null)
                {
                    testFactory.Enabled = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Enabled"));
                }

            }
        }

        private bool _observablePass = true;

        /// <summary>
        /// Observe changes Pass properties of test
        /// </summary>
        public bool ObservablePass
        {
            get { return _observablePass; }
            set
            {
                if (_observablePass != value && PropertyChanged != null)
                {
                    _observablePass = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ObservablePass"));
                }
            }
        }

        private bool _observableCheckedNow = false;

        /// <summary>
        /// Observe changes CheckedNow properties of test
        /// </summary>
        public bool ObservableCheckedNow
        {
            get { return _observableCheckedNow; }
            set
            {
                if (_observableCheckedNow != value && PropertyChanged != null)
                {
                    _observableCheckedNow = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ObservableCheckedNow"));
                }

            }
        }

    }
}
