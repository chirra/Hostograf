using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public static class ErrorHandler
    {
        public static void ErrorHandlerToMessageBox(object sender, object error)
        {
                MessageBox.Show(error.ToString(), sender.ToString());
        }


        public static void ErrorHandlerToFile(object sender, object error)
        {
            FileInfo f = new FileInfo("hostograf.log");
            FileStream fs;
            if (f.Exists)
                fs = new FileStream(f.FullName, FileMode.Append);
            else fs = new FileStream(f.FullName, FileMode.Create); 

            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1251));
            sw.WriteLine(DateTime.Now);
            sw.WriteLine(sender);
            sw.WriteLine(error);
            sw.WriteLine();
            sw.Close();
        }
    }
}
