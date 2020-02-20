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
        public DayOnFact DayOnFact { get; set; }
        public DayOnPlan DayOnPlan { get; set; }
        public TimeSpan Time1X { get; set; }
        public TimeSpan Time15X { get; set; }
        public TimeSpan Time20X { get; set; }
        public TimeSpan TimeHoli { get; set; }
       
        /// <summary>
        ////Время недоработанное работником
        /// </summary>
        public TimeSpan Time0 { get; set; } // Недоработанное до плана время
        public DayEmployee(DayOnPlan dayOnPlan, DayOnFact dayOnFact, Employee employee)
        {
            DayOnFact = dayOnFact;
            DayOnPlan = dayOnPlan;
            Employee = employee;
            if (dayOnPlan.DayTypeOnPlan == DayTypeOnPlan.Holiday || dayOnFact.DayTypeOnEmployee == DayTypeOnFact.WorkedBusinessTrip)
            { dayOnFact.WorkedTime = dayOnFact.EndWOrk - dayOnFact.StartWork; }
            else
            {
                if (dayOnFact.EndWOrk - dayOnFact.StartWork>=new TimeSpan(4,48,0))
                { dayOnFact.WorkedTime = dayOnFact.EndWOrk - dayOnFact.StartWork - dayOnFact.Dinner; }
                else { dayOnFact.WorkedTime = dayOnFact.EndWOrk - dayOnFact.StartWork; }
            }
            if (dayOnPlan.DayTypeOnPlan == DayTypeOnPlan.Worked || dayOnPlan.DayTypeOnPlan == DayTypeOnPlan.WorkedShort)
            { if (dayOnFact.WorkedTime <= dayOnPlan.WorkedTime)
                { Time1X = dayOnFact.WorkedTime;
                    Time0 = dayOnPlan.WorkedTime - dayOnFact.WorkedTime;
                }
                else if (dayOnFact.WorkedTime > dayOnPlan.WorkedTime && dayOnFact.WorkedTime <= dayOnPlan.WorkedTime + new TimeSpan(2, 0, 0))
                { Time1X = DayOnPlan.WorkedTime;
                    Time15X = dayOnFact.WorkedTime - dayOnPlan.WorkedTime;
                }
                else if (dayOnFact.WorkedTime >dayOnPlan.WorkedTime + new TimeSpan(2, 0, 0))
                {
                    Time1X = DayOnPlan.WorkedTime;
                    Time15X = new TimeSpan(2, 0, 0);
                    Time20X = dayOnFact.WorkedTime - Time1X - Time15X;
                }
            }
            else if (dayOnPlan.DayTypeOnPlan == DayTypeOnPlan.Holiday)
            {    if (dayOnFact.WorkedTime<= new TimeSpan(8, 0, 0))
                { TimeHoli = dayOnFact.WorkedTime; }
                else if (dayOnFact.WorkedTime > new TimeSpan(8, 0, 0))
                { TimeHoli = new TimeSpan(8, 0, 0);
                    Time20X = dayOnFact.WorkedTime - new TimeSpan(8, 0, 0);
                }
            }
         //   TotalTime = Time0 + Time1X + Time15X + Time20X + TimeHoli;
        }

    }
}
