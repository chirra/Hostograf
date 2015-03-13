using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class MyClass
    {
        protected IList<string> m_StringList = new List<string>();

        public void AddString(string s)
        {
            m_StringList.Add(s);
        }
    }



    internal class ObservableClass : MyClass
    {

        public ObservableCollection<string> StringList
        {
            get { return (ObservableCollection<string>) m_StringList; }
            set { m_StringList = value; }
        }


        public ObservableClass()
        {
            m_StringList = new ObservableCollection<string>();
        }

      
    }



    class Program
    {
        static void Main(string[] args)
        {
            MyClass myStr = new MyClass();
            myStr.AddString("Hello");
            
            ObservableClass testobject = new ObservableClass();
            testobject = (ObservableClass) myStr;

            //testobject.StringList.Add("new string");
            Console.WriteLine(testobject.StringList[0]);
        }
    }


}
