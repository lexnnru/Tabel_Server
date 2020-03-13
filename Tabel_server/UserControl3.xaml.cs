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
using Tabel_server.Interfaces;
using Tabel_server.Model.Data;
using Tabel_server.Model.Data.Table;

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для UserControl3.xaml
    /// </summary>
    //public class table
    //{
    //    public string fio { get; set; }
    //    public TimeSpan hour1 { get; set; }
    //    public TimeSpan hour15 { get; set; }
    //    public TimeSpan hour20 { get; set; }
    //    public TimeSpan hourHoli20 { get; set; }
    //    public TimeSpan sickHours { get; set; }
    //    public int sickDays { get; set; }
    //    public TimeSpan vacationHours { get; set; }
    //    public int vacationDays { get; set; }
    //    public TimeSpan compensatory_hours { get; set; }
    //    public int compensatory_days { get; set; }
    //    public int daycomm { get; set; }
    //    public TimeSpan totalWorkHours { get; set; }
    //    public string Details { get; set; }
    //    public TimeSpan totalWorkHours_According_Plan { get; set; }



    //}
    public partial class UserControl3 : UserControl
    {
        public ObservableCollection<MonthEmployee> employees { get; set; }
       // public ObservableCollection<table> tables { get; }
        
        public UserControl3()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public UserControl uc3 =>this;

        public void SetSource(ObservableCollection<MonthEmployee> monthemployees, List<DateTime> HolidateTimes, DateTime dateTime)
        {
            this.employees=monthemployees;
            dgSummary.ItemsSource = employees;
           

        }
    }
}
