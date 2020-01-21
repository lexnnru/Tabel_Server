using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.PlanDay
{
    public class DayOnPlan
    {
        public DateTime Day { get; set; }
        public DayTypeOnPlan DayTypeOnPlan { get; set; }
        public TimeSpan WorkedTime { get; set; }
    }
}
