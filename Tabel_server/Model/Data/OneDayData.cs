using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    public class OneDayData
    {
        public DateTime daynumber { get; set; }
        public DateTime startday { get; set; }
        public DateTime endday { get; set; }
        public string city { get; set; }
        public string specCheck { get; set; }
        public string achiv { get; set; }
        public TimeSpan DayDuration { get; set; }
        public TimeSpan Work_time { get; set; }
        public TimeSpan Sick_time { get; set; }
        public TimeSpan Vocation_time { get; set; }
        public TimeSpan Compensatory_time { get; set; }
        public TimeSpan LunchTime { get; set; }
        public bool isHoliday { get; set; }
        public bool isHaveData { get; set; }
        public bool error { get; set; }
        public bool nextDayisholi { get; set; }
    }
}
