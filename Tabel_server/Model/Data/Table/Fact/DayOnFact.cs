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
        public String City { get; set; }
        public String Achiv { get; set; }
        public DayOnFact()
        { WorkedTime = new TimeSpan(0);
        }
    }
}
