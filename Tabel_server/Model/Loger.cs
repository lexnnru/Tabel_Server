using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model
{
   public static class Loger
    {
        public static event Action<string> LogChange;
        public static void GetLog(string log) => LogChange?.Invoke(log);
    }
}
