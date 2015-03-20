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

        public bool Enabled { get; set; }

        /// <summary>
        /// Test execution
        /// </summary>
        /// <returns></returns>
       public abstract bool Execute();

    }

}

