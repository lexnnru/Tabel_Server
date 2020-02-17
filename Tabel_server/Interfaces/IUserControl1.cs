using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tabel_server.Model.Data;
using Tabel_server.Model.Data.Table;

namespace Tabel_server.Interfaces
{
    public interface IUserControl1
    {
        void SetSource(MonthEmployee emp);
        UserControl uc1 { get; }
    }
}
