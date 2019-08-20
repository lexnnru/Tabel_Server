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
    /// Логика взаимодействия для UserControl2.xaml
    /// </summary>
    /// 
    public class Table
    {
        public string name { get; set; }
        public string color { get; set; }      
        public TimeSpan day1 { get; set; }
        public TimeSpan day2 { get; set; }
        public TimeSpan day3 { get; set; }
        public TimeSpan day4 { get; set; }
        public TimeSpan day5 { get; set; }
        public TimeSpan day6 { get; set; }
        public TimeSpan day7 { get; set; }
        public TimeSpan day8 { get; set; }
        public TimeSpan day9 { get; set; }
        public TimeSpan day10 { get; set; }
        public TimeSpan day11 { get; set; }
        public TimeSpan day12 { get; set; }
        public TimeSpan day13 { get; set; }
        public TimeSpan day14 { get; set; }
        public TimeSpan day15 { get; set; }
        public TimeSpan day16 { get; set; }
        public TimeSpan day17 { get; set; }
        public TimeSpan day18 { get; set; }
        public TimeSpan day19 { get; set; }
        public TimeSpan day20 { get; set; }
        public TimeSpan day21 { get; set; }
        public TimeSpan day22 { get; set; }
        public TimeSpan day23 { get; set; }
        public TimeSpan day24 { get; set; }
        public TimeSpan day25 { get; set; }
        public TimeSpan day26 { get; set; }
        public TimeSpan day27 { get; set; }
        public TimeSpan day28 { get; set; }
        public TimeSpan day29 { get; set; }
        public TimeSpan day30 { get; set; }
        public TimeSpan day31 { get; set; }
        public int summa { get; set; }

    }
    public partial class UserControl2 : UserControl, Interfaces.IUserControl2
    {
        public ObservableCollection<Table> tables { get; set; }
        public UserControl uc2 => this;

        public List<MonthEmployeeData> Employees { set; get; }
       

        public UserControl2()
        {
            InitializeComponent();
            this.DataContext = this;
            tables = new ObservableCollection<Table>();
        }
        public void SetSummaryTable(List<MonthEmployeeData> Employees, DateTime date, List<DateTime> HolidateTimes)
        {


            //datagridSummary.Columns.Clear();
            tables.Clear();
            for (int i = 0; i < Employees.Count; i++)
            {
                tables.Add(new Table() { name = Employees[i].fio,
                    day1 = Employees[i].oneDayDatas[0].Work_time,
                    day2 = Employees[i].oneDayDatas[1].Work_time,
                    day3 = Employees[i].oneDayDatas[1].Work_time,
                    day4 = Employees[i].oneDayDatas[3].Work_time,
                    day5 = Employees[i].oneDayDatas[4].Work_time,
                    day6 = Employees[i].oneDayDatas[5].Work_time,
                    day7 = Employees[i].oneDayDatas[6].Work_time,
                    day8 = Employees[i].oneDayDatas[7].Work_time,
                    day9 = Employees[i].oneDayDatas[8].Work_time,
                    day10 = Employees[i].oneDayDatas[9].Work_time,
                    day11 = Employees[i].oneDayDatas[10].Work_time,
                    day12 = Employees[i].oneDayDatas[11].Work_time,
                    day13 = Employees[i].oneDayDatas[12].Work_time,
                    day14 = Employees[i].oneDayDatas[13].Work_time,
                    day15 = Employees[i].oneDayDatas[14].Work_time,
                    day16 = Employees[i].oneDayDatas[15].Work_time,
                    day17 = Employees[i].oneDayDatas[16].Work_time,
                    day18 = Employees[i].oneDayDatas[17].Work_time,
                    day19 = Employees[i].oneDayDatas[18].Work_time,
                    day20 = Employees[i].oneDayDatas[19].Work_time,
                    day21 = Employees[i].oneDayDatas[20].Work_time,
                    day22 = Employees[i].oneDayDatas[21].Work_time,
                    day23 = Employees[i].oneDayDatas[22].Work_time,
                    day24 = Employees[i].oneDayDatas[23].Work_time,
                    day25 = Employees[i].oneDayDatas[24].Work_time,
                    day26 = Employees[i].oneDayDatas[25].Work_time,
                    day27 = Employees[i].oneDayDatas[26].Work_time,
                    day28 = Employees[i].oneDayDatas[27].Work_time,
                    day29 = Employees[i].oneDayDatas[28].Work_time,
                    day30 = Employees[i].oneDayDatas[29].Work_time,
                    day31 = Employees[i].oneDayDatas[30].Work_time,
                    summa= Employees[i].oneDayDatas.Sum(n=>n.Work_time.Hours)


            });;
            }

            this.Employees = Employees;
            ObservableCollection<DataGridColumn> dt = datagridSummary.Columns;
            int dim = DateTime.DaysInMonth(date.Year, date.Month);

            for (int i = 31; i > dim; i--)
            {
                dt[i].MinWidth = 0;
                dt[i].Width = 0;
            }

            for (int i = 1; i <=DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                for (int j = 0; j < HolidateTimes.Count; j++)
                {
                    if (new DateTime(date.Year, date.Month, i)== new DateTime(HolidateTimes[j].Year, HolidateTimes[j].Month, HolidateTimes[j].Day))
                    {
                        dt[i].CellStyle = new Style(typeof(DataGridCell));
                        dt[i].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.LightBlue)));
                    }
                }
            }
        }
        
    }
}
