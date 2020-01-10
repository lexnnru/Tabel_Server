using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    public class MonthEmployeesDatasOld
    {
       
        public string family { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public string fio { get; set; }
        public string mail { get; set; }
        public string tabelNumber { get; set; }
        public string post { get; set; }

        public int persentFill { get; set; }
        public bool error { get; set; }
        public List<OneDayData> oneDayDatas { get; set; } = new List<OneDayData>();
        

    }
}
