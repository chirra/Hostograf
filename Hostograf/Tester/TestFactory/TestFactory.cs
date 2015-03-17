using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tester
{
    
    /// <summary>
    /// Abstract Class of tests
    /// </summary>
    public abstract class TestFactory //: INotifyPropertyChanged
    {
        public delegate void TestExecuteError(object sender, object error);
        public delegate void ChangeEnabled();
        public delegate void ChangePass();

        public abstract event TestExecuteError OnExecuteError;
        public event ChangeEnabled OnChangeEnabled;
        public event ChangePass OnChangePass;

        public Guid Id { get; set; }

        //Name будет в ObservableTestFactory
        //public string Name { get { return this.ToString(); } }
        
        private bool enabled = true;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
            /*set
            {

                if (enabled != value && PropertyChanged != null)
                {
                    enabled = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Enabled"));
                }
                
            }*/
        }

/*
        private bool pass = true;
        
        public bool Pass
        {
            get { return pass; }
            set
            {
                if (pass != value && PropertyChanged != null)
                {
                    pass = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Pass"));
                }
            }
        }

        private bool checkedNow = false;
        public bool CheckedNow { 
            get { return checkedNow; }
            set
            {
                if (checkedNow != value && PropertyChanged != null)
                {
                    checkedNow = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CheckedNow"));
                }
                
            } 
        }
*/
            
        


        /// <summary>
        /// Test execution
        /// </summary>
        /// <returns></returns>
       public abstract bool Execute();


       //public event PropertyChangedEventHandler PropertyChanged;
    }

}

