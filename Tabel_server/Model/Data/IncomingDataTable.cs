using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    public class IncomingDataTable
    {

        public IncomingDataTable(DataRow drow)
        {
            daynumber = new DateTime(Convert.ToInt16(drow[2]), Convert.ToInt16(drow[1]), Convert.ToInt32(drow[0]));
            startday = new DateTime(Convert.ToInt16(drow[2]), Convert.ToInt16(drow[1]), Convert.ToInt16(drow[0]), Convert.ToInt16(drow[3]), Convert.ToInt16(drow[4]), 0);
            endday = new DateTime(Convert.ToInt16(drow[2]), Convert.ToInt16(drow[1]), Convert.ToInt16(drow[0]), Convert.ToInt16(drow[5]), Convert.ToInt16(drow[6]), 0);
            DayDuration = (endday - startday);
            city = Convert.ToString(drow[8]);
            specCheck = Convert.ToString(drow[9]);
            achiv = Convert.ToString(drow[10]);
        }
        public IncomingDataTable()
        {
        }
        public DateTime daynumber { get; set; }
        public DateTime startday { get; set; }
        public DateTime endday { get; set; }
        public string city { get; set; }
        public string specCheck { get; set; }
        public string achiv { get; set; }
        public TimeSpan DayDuration { get; set; }
        public TimeSpan Work_time { get; set; }
        public TimeSpan LunchTime { get; set; }
        public string family { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public string mail { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string tabelNumber { get; set; }
        public bool isHoliday { get; set; }
        public bool isHaveData { get; set; }




    }
}
