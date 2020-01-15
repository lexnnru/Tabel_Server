using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabel_server.Model.Data.Table;
using Tabel_server.Model.Data.Table.EmployeeDay;
using Tabel_server.Model.Data.Table.PlanDay;

namespace Tabel_server.Model.Data.Table
{
    public class DayEmployee
    {
        public Employee Employee { get; set; }
        public DayOnFact DayOnFact {get; set;}
        public DayOnPlan DayOnPlan { get; set; }
        
        public TimeSpan Time1X { get; set; }
        public TimeSpan Time15X { get; set; }
        public TimeSpan Time20X { get; set; }
        public TimeSpan TimeHoli { get; set; }
        /// <summary>
        ////Время недоработанное работником
        /// </summary>
        public TimeSpan Time0 { get; set; }
        public String City { get; set; }
        public String Achiv { get; set; }
        public DayEmployee()
        {
            this.DayOnPlan = new DayOnPlan();

            this.DayOnFact = new DayOnFact();
        }

    }
}
