using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tabel_server.Model.Data;
using Tabel_server.Model.Data.Table;

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, Interfaces.IUserControl1
    {
       public ObservableCollection<OneDayData> odd { get; set; }
        public UserControl1()
        {
            InitializeComponent();
            odd = new ObservableCollection<OneDayData>();

            this.DataContext = this;
        }

        public UserControl uc1 => this;

        public void SetSource(MonthEmployeesDatasOld employees)
        {
            odd.Clear();
            employees.oneDayDatas.ForEach(x => {
                if (x.Work_time_According_plan==new TimeSpan(0,0,0))
                { x.isHoliday = true; }

                odd.Add(x); });
        }
        public void SetSourceNew(MonthEmployee employees)
        {
            odd.Clear();
            for (int i=0; i<employees.Days.Length; i++)
            { }
          
                
            //    ForEach(x => {
            //    if (x.Work_time_According_plan == new TimeSpan(0, 0, 0))
            //    { x.isHoliday = true; }

            //    odd.Add(x);
            //});
        }
    }
}
