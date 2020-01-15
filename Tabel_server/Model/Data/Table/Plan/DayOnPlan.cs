using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.PlanDay
{
    public class DayOnPlan
    {
        public DayTypeOnPlan DayTypeOnPlan { get; set; }
        public TimeSpan WorkedTime { get; set; }
        public DayOnPlan()
        {
        }
        public DayOnPlan(DayTypeOnPlan DayTypeOnPlan, TimeSpan WorkedTime)
        {
            this.DayTypeOnPlan = DayTypeOnPlan;
            this.WorkedTime = WorkedTime;
        }


    }

}
