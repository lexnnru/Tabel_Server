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

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для UCCalculateZP.xaml
    /// </summary>
    public partial class UCCalculateZP : UserControl
    {
        public UCCalculateZP(ObservableCollection<Model.Data.MonthEmployeesDatasOld> monthEmployeeDatas)
        {
            InitializeComponent();
            List<MonthZP> monthZP = new List<MonthZP>();
            for (int i=0; i<= monthEmployeeDatas.Count; i++)
            {
                monthZP.Add(new MonthZP() { }); ;


                    }
            this.DataContext = this;

        }
    }
    public class MonthZP
    {
        public Employee Employee { get; set; }
        public int MonthBonus       { get;   set;}
        public int BiznessTripBonus { get; set; }
        public int OverWorkingBonus { get; set; }
        public int FreeBonus { get; set; }
        public int ZP { get; set; }
        public int ZPwithout13 { get; set; }
    }
}
