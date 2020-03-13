using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table
{
   public class MonthEmployee
    {
        public DayEmployee[] Days { get; set; }
        public TimeSpan Time1X { get; set; }
        public TimeSpan Time15X { get; set; }
        public TimeSpan Time20X { get; set; }
        public TimeSpan TimeHoli { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int DaysWorked { get; set; }
        public int DaysWorkedBusinessTrip { get; set; }
        public int DaysNotWorkedSick { get; set; }
        public int DaysNotWorkedMatherhoodVacation { get; set; }
        public int DaysNotWorkedVacation { get; set; }
        public int DaysNotWorkedAdministrative { get; set; }
        public int WorkedDayInMonthPlaned { get; set; }
        public TimeSpan WorkedHoursInMonthPlaned { get; set; }
        bool saveStart;
        public bool SaveStart { 
            get{ return saveStart; }
            set { saveStart = value;
            if (saveStart==true)
                { SaveMonthZP?.Invoke(MonthZP); }
            }
        }

        MonthZP monthZP;
        public MonthZP MonthZP { get            { return monthZP; }
            set { 
                monthZP = value;
            }
        }
        /// <summary>
        ////Время недоработанное работником
        /// </summary>
        public TimeSpan Time0 { get; set; }
        public Employee Employee { get; set; }
        public bool Error { get; set; }
        public event Action<MonthZP> SaveMonthZP;
        public MonthEmployee(DayEmployee[] Days)
        {
            
            this.Days = Days;
            Time1X = new TimeSpan(0);
            Time15X = new TimeSpan(0);
            Time20X = new TimeSpan(0);
            TimeHoli = new TimeSpan(0);


            foreach (DayEmployee day in Days)
            {
                
                if (day.Error == true)
                { Error = true; }
                Time1X += day.Time1X;
                Time15X += day.Time15X;
                Time20X += day.Time20X;
                Time0 += day.Time0;
                TimeHoli += day.TimeHoli;
                TotalTime += day.DayOnFact.WorkedTime;
                switch (day.DayOnFact.DayTypeOnEmployee)
                {
                    case EmployeeDay.DayTypeOnFact.Worked:
                         DaysWorked+= 1;
                        break;
                    case EmployeeDay.DayTypeOnFact.WorkedBusinessTrip:
                        DaysWorkedBusinessTrip += 1;
                        break;
                    case EmployeeDay.DayTypeOnFact.NotWorkedVacation:
                        DaysNotWorkedVacation += 1;
                        break;
                    case EmployeeDay.DayTypeOnFact.NotWorkedSick:
                        DaysNotWorkedSick += 1;
                        break;
                    case EmployeeDay.DayTypeOnFact.NotWorkedMatherhoodVacation:
                        DaysNotWorkedMatherhoodVacation += 1;
                        break;
                    case EmployeeDay.DayTypeOnFact.NotWorkedAdministrative:
                        DaysNotWorkedAdministrative += 1;
                        break;

                }
                switch (day.DayOnPlan.DayTypeOnPlan)
                {
                    case DayTypeOnPlan.Worked:
                        WorkedDayInMonthPlaned += 1;
                        WorkedHoursInMonthPlaned += day.DayOnPlan.WorkedTime;
                        break;
                    case DayTypeOnPlan.WorkedShort:
                        WorkedDayInMonthPlaned += 1;
                        WorkedHoursInMonthPlaned += day.DayOnPlan.WorkedTime;
                        break;

                }

                }
        }
    }
}
