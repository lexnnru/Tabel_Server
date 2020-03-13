using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tabel_server.Model
{
   public static class Loger
    {
        public static event Action<string> LogChange;
        public static void SetLog(string log)
        { LogChange?.Invoke(log);
           string  writePath =System.IO.Directory.GetCurrentDirectory()+ @"\Log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now +"| " +log);
                }
            }
            catch
            { }
        }
        public static void SetLog(string log, bool show)
        {
            LogChange?.Invoke(log);
            string writePath = System.IO.Directory.GetCurrentDirectory() + @"\Log.txt";
            if (show==true)
            { MessageBox.Show(log); }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now + "| " + log);
                }
            }
            catch
            { }
        }
    }
}
