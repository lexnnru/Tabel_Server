using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.EmployeeDay
{
    public class DayOnFact
    {
        public DateTime StartWork { get; set; }
        public DateTime EndWOrk { get; set; }
        TimeSpan workertime;
        public TimeSpan WorkedTime { get { return workertime; }
            set { workertime = value;
                if (workertime == new TimeSpan(0, 0, 0) && DayTypeOnEmployee==DayTypeOnFact.Worked)
                { Error = true; }
            } }
        DayTypeOnFact dayTypeOnEmployee;
        public DayTypeOnFact DayTypeOnEmployee { get { return dayTypeOnEmployee; } 
            set { dayTypeOnEmployee = value;
                if (workertime == new TimeSpan(0, 0, 0) && DayTypeOnEmployee == DayTypeOnFact.Worked)
                { Error = true; }
            } }
        public TimeSpan Dinner { get; set; }
        public String City { get; set; }
        public String Achiv { get; set; }
        public bool Error { get; set; }
        public DayOnFact()
        { WorkedTime = new TimeSpan(0);
        }
    }
}
