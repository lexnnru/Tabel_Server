using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.Fact
{
        class MonthOnFact
        {
            public DayEmployee[] Days { get; set; }
            public TimeSpan Time1X { get; set; }
            public TimeSpan Time15X { get; set; }
            public TimeSpan Time20X { get; set; }
            public TimeSpan TimeHoli { get; set; }
            public int DaysWorked { get; set; }
            public int DaysWorkedBusinessTrip { get; set; }
            public int DaysNotWorkedSick { get; set; }
            public int DaysNotWorkedMatherhoodVacation { get; set; }
            public int DaysNotWorkedVacation { get; set; }
            public int DaysNotWorkedAdministrative { get; set; }
            /// <summary>
            ////Время недоработанное работником
            /// </summary>
            public TimeSpan Time0 { get; set; }
        public String City { get; set; }
        public String Achiv { get; set; }

        public MonthOnFact (DayEmployee[] Days)
            {
                foreach (DayEmployee day in Days)
                {
                    Time1X += day.Time1X;
                    Time15X += day.Time15X;
                    Time20X += day.Time20X;
                    Time0 += day.Time0;
                    switch (day.DayOnFact.DayTypeOnEmployee)
                    {
                        case EmployeeDay.DayTypeOnFact.Worked:
                            DaysWorked += 1;
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
                    }
                }
            }
        }
    
}
