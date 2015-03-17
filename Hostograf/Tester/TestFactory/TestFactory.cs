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
        public abstract event TestExecuteError OnExecuteError;

        public Guid Id { get; set; }

        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }


        /// <summary>
        /// Test execution
        /// </summary>
        /// <returns></returns>
       public abstract bool Execute();

    }

}

