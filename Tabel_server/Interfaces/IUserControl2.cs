using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tabel_server.Model.Data;

namespace Tabel_server.Interfaces
{
    
    public interface IUserControl2
    {
        UserControl uc2 { get;  }
        List<MonthEmployeesDatasOld> Employees { set; }
        void SetSummaryTable(List<MonthEmployeesDatasOld> monthEmployeeDatas, DateTime date, List<DateTime> HolidateTimes);
    }
}
