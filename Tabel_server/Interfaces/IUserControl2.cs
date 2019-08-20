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
        List<MonthEmployeeData> Employees { set; }
        void SetSummaryTable(List<MonthEmployeeData> monthEmployeeDatas, DateTime date, List<DateTime> HolidateTimes);
    }
}
