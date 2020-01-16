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
        public TimeSpan WorkedTime { get; set; }
        public DayTypeOnFact DayTypeOnEmployee { get; set; }
        public TimeSpan Dinner { get; set; }

        public DayOnFact()
        { }
        public DayOnFact (DateTime StartWork, DateTime EndWOrk, TimeSpan Dinner, DayTypeOnFact DayTypeOnEmployee)
        {
            this.StartWork = StartWork;
            this.EndWOrk = EndWOrk;
            this.Dinner = Dinner;
            this.DayTypeOnEmployee = DayTypeOnEmployee;
            WorkedTime = EndWOrk - StartWork - Dinner;
        }
    }
}
