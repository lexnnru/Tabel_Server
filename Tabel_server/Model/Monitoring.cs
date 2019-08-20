using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tabel_server.Model
{
    class Monitoring
    {

        public event Action filechange;
        public void Chek()
        {
            for (; ; )
            {
                List<string> files = Directory.GetFiles(Environment.CurrentDirectory, "*.json", SearchOption.AllDirectories).ToList<string>();
                if (files.Count != 0)
                {
                    filechange?.Invoke();
                    Thread.Sleep(5000);
                }  
                else
                { Thread.Sleep(5000); }
            }
        }
        public List<string> Get_files ()
       {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.AllDirectories);
            List<string> list1 = files.ToList<string>();
            return list1;
        }
    }
}
