using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tabel_server.Model.Data;

namespace Tabel_server.Interfaces
{
         public interface IUserControl3
    {
        void SetSource(List<MonthEmployeesDatasOld> oneDayDatas, List<DateTime> HolidateTimes, DateTime dateTime);
        UserControl uc3 { get; }
    }
}
