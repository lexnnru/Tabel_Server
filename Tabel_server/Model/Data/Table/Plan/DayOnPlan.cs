using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.PlanDay
{
    class DayOnPlan
    {
        DayTypeOnPlan DayTypeOnPlan { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWOrk { get; set; }
        public TimeSpan WorkedTimeOnPlan { get; set; }
        public TimeSpan Dinner { get; set; }
        public DayOnPlan(DateTime StartWork, DateTime EndWOrk, TimeSpan Dinner, DayTypeOnPlan DayTypeOnPlan)
        {
            this.StartWork = StartWork;
            this.EndWOrk = EndWOrk;
            this.Dinner = Dinner;
            this.DayTypeOnPlan = DayTypeOnPlan;
            WorkedTimeOnPlan = EndWOrk - StartWork - Dinner;
        }
    }
}
