using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.EmployeeDay
{
    class DayOnFact
    {
        public DateTime StartWork { get; set; }
        public DateTime EndWOrk { get; set; }
        public TimeSpan WorkedTime { get; set; }
        public DayTypeOnFact DayTypeOnEmployee { get; set; }
        public TimeSpan Dinner { get; set; }
    }
}
