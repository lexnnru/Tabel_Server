using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabel_server.Model.Data.Table.PlanDay;

namespace Tabel_server.Model.Data.Table.Plan
{
    class MonthOnPlan
    {
        TimeSpan MonthWorkerdTime { get; set; }
        public MonthOnPlan(DayOnPlan[] Days)
        {
            foreach (DayOnPlan day in Days)
            {
                MonthWorkerdTime += day.WorkedTimeOnPlan;
            }
        }
    }
}
