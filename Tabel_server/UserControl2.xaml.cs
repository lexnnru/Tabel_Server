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
using Tabel_server.Model.Data.Table.EmployeeDay;

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
        public DayTypeOnFact day1c { get; set; }
        public DayTypeOnFact day2c { get; set; }
        public DayTypeOnFact day3c { get; set; }
        public DayTypeOnFact day4c { get; set; }
        public DayTypeOnFact day5c { get; set; }
        public DayTypeOnFact day6c { get; set; }
        public DayTypeOnFact day7c { get; set; }
        public DayTypeOnFact day8c { get; set; }
        public DayTypeOnFact day9c { get; set; }
        public DayTypeOnFact day10c { get; set; }
        public DayTypeOnFact day11c { get; set; }
        public DayTypeOnFact day12c { get; set; }
        public DayTypeOnFact day13c { get; set; }
        public DayTypeOnFact day14c { get; set; }
        public DayTypeOnFact day15c { get; set; }
        public DayTypeOnFact day16c { get; set; }
        public DayTypeOnFact day17c { get; set; }
        public DayTypeOnFact day18c { get; set; }
        public DayTypeOnFact day19c { get; set; }
        public DayTypeOnFact day20c { get; set; }
        public DayTypeOnFact day21c { get; set; }
        public DayTypeOnFact day22c { get; set; }
        public DayTypeOnFact day23c { get; set; }
        public DayTypeOnFact day24c { get; set; }
        public DayTypeOnFact day25c { get; set; }
        public DayTypeOnFact day26c { get; set; }
        public DayTypeOnFact day27c { get; set; }
        public DayTypeOnFact day28c { get; set; }
        public DayTypeOnFact day29c { get; set; }
        public DayTypeOnFact day30c { get; set; }
        public DayTypeOnFact day31c { get; set; }
        public double summa { get; set; }
        public string Details { get; set; }
        public string info { get; set; }
        

    }
    public partial class UserControl2 : UserControl
    {
        public ObservableCollection<Table> tables { get; set; }
        public ObservableCollection<OneDayData> otables { get; set; }
        public UserControl uc2 => this;
        public List<MonthEmployee> Employees { set; get; }
        public UserControl2()
        {
            InitializeComponent();
            this.DataContext = this;
            tables = new ObservableCollection<Table>();
        }
        public void SetSummaryTable(ObservableCollection<MonthEmployee> Employees, DateTime date, List<DateTime> HolidateTimes)
        {     
            
            tables.Clear();
            ObservableCollection<DataGridColumn> dt = datagridSummary.Columns;

            for (int i = 0; i < dt.Count; i++)
            {
                dt[i].CellStyle = new Style(typeof(DataGridCell));
                dt[i].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.White)));
            }
            for (int i = 1; i <= DateTime.DaysInMonth(Employees[0].Days[0].DayOnFact.StartWork.Year, Employees[0].Days[0].DayOnFact.StartWork.Month); i++)
            {
                if (Employees[0].Days[i-1].DayOnPlan.DayTypeOnPlan==DayTypeOnPlan.Holiday)
                {
                    //dt[i].CellStyle = new Style(typeof(DataGridCell));
                    dt[i].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.Pink)));
                }
                if (Employees[0].Days[i-1].DayOnPlan.DayTypeOnPlan == DayTypeOnPlan.WorkedShort)
                {
                   // dt[i].CellStyle = new Style(typeof(DataGridCell));
                    dt[i].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.AliceBlue)));
                }
                //for (int j = 0; j < Employees[0].Days.Length; j++)
                //{
                //    if (new DateTime(date.Year, date.Month, i) == new DateTime(HolidateTimes[j].Year, HolidateTimes[j].Month, HolidateTimes[j].Day))
                //    {
                //        dt[i].CellStyle = new Style(typeof(DataGridCell));
                //        dt[i].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.Pink)));
                //    }
                //}
            }
            for (int i = 0; i < Employees.Count; i++)
            {

                tables.Add(new Table());

                tables[i].info = "табельный номер: " + Employees[i].Employee.TabelNumber + ", должность: " + Employees[i].Employee.Post;
                tables[i].name = Employees[i].Employee.ToString();
                tables[i].day1 = Employees[i].Days[0].DayOnFact.WorkedTime;
                tables[i].day2 = Employees[i].Days[1].DayOnFact.WorkedTime;
                tables[i].day3 = Employees[i].Days[2].DayOnFact.WorkedTime;
                tables[i].day4 = Employees[i].Days[3].DayOnFact.WorkedTime;
                tables[i].day5 = Employees[i].Days[4].DayOnFact.WorkedTime;
                tables[i].day6 = Employees[i].Days[5].DayOnFact.WorkedTime;
                tables[i].day7 = Employees[i].Days[6].DayOnFact.WorkedTime;
                tables[i].day8 = Employees[i].Days[7].DayOnFact.WorkedTime;
                tables[i].day9 = Employees[i].Days[8].DayOnFact.WorkedTime;
                tables[i].day10 = Employees[i].Days[9].DayOnFact.WorkedTime;
                tables[i].day11 = Employees[i].Days[10].DayOnFact.WorkedTime;
                tables[i].day12 = Employees[i].Days[11].DayOnFact.WorkedTime;
                tables[i].day13 = Employees[i].Days[12].DayOnFact.WorkedTime;
                tables[i].day14 = Employees[i].Days[13].DayOnFact.WorkedTime;
                   tables[i].day15 = Employees[i].Days[14].DayOnFact.WorkedTime;
                    tables[i].day16 = Employees[i].Days[15].DayOnFact.WorkedTime;
                   tables[i].day17 = Employees[i].Days[16].DayOnFact.WorkedTime;
                    tables[i].day18 = Employees[i].Days[17].DayOnFact.WorkedTime;
                    tables[i].day19 = Employees[i].Days[18].DayOnFact.WorkedTime;
                tables[i].day20 = Employees[i].Days[19].DayOnFact.WorkedTime;
                    tables[i].day21 = Employees[i].Days[20].DayOnFact.WorkedTime;
                    tables[i].day22 = Employees[i].Days[21].DayOnFact.WorkedTime;
                   tables[i].day23 = Employees[i].Days[22].DayOnFact.WorkedTime;
                   tables[i].day24 = Employees[i].Days[23].DayOnFact.WorkedTime;
                    tables[i].day25 = Employees[i].Days[24].DayOnFact.WorkedTime;
                    tables[i].day26 = Employees[i].Days[25].DayOnFact.WorkedTime;
                   tables[i].day27 = Employees[i].Days[26].DayOnFact.WorkedTime;
                    tables[i].day28 = Employees[i].Days[27].DayOnFact.WorkedTime;
                if (Employees[i].Days.Length > 28)
                    tables[i].day29 = Employees[i].Days[28].DayOnFact.WorkedTime;
                if (Employees[i].Days.Length > 29)
                    tables[i].day30 = Employees[i].Days[29].DayOnFact.WorkedTime;
                if (Employees[i].Days.Length > 30)
                    tables[i].day31 = Employees[i].Days[30].DayOnFact.WorkedTime;
                    tables[i].day1c = Employees[i].Days[0].DayOnFact.DayTypeOnEmployee;
                    tables[i].day2c = Employees[i].Days[1].DayOnFact.DayTypeOnEmployee;
                    tables[i].day3c = Employees[i].Days[2].DayOnFact.DayTypeOnEmployee;
                    tables[i].day4c = Employees[i].Days[3].DayOnFact.DayTypeOnEmployee;
                    tables[i].day5c = Employees[i].Days[4].DayOnFact.DayTypeOnEmployee;
                   tables[i].day6c = Employees[i].Days[5].DayOnFact.DayTypeOnEmployee;
                   tables[i].day7c = Employees[i].Days[6].DayOnFact.DayTypeOnEmployee;
                   tables[i].day8c = Employees[i].Days[7].DayOnFact.DayTypeOnEmployee;
                   tables[i].day9c = Employees[i].Days[8].DayOnFact.DayTypeOnEmployee;
                   tables[i].day10c = Employees[i].Days[9].DayOnFact.DayTypeOnEmployee;
                   tables[i].day11c = Employees[i].Days[10].DayOnFact.DayTypeOnEmployee;
                    tables[i].day12c = Employees[i].Days[11].DayOnFact.DayTypeOnEmployee;
                   tables[i].day13c = Employees[i].Days[12].DayOnFact.DayTypeOnEmployee;
                   tables[i].day14c = Employees[i].Days[13].DayOnFact.DayTypeOnEmployee;
                   tables[i].day15c = Employees[i].Days[14].DayOnFact.DayTypeOnEmployee;
                   tables[i].day16c = Employees[i].Days[15].DayOnFact.DayTypeOnEmployee;
                   tables[i].day17c = Employees[i].Days[16].DayOnFact.DayTypeOnEmployee;
                   tables[i].day18c = Employees[i].Days[17].DayOnFact.DayTypeOnEmployee;
                   tables[i].day19c = Employees[i].Days[18].DayOnFact.DayTypeOnEmployee;
                   tables[i].day20c = Employees[i].Days[19].DayOnFact.DayTypeOnEmployee;
                    tables[i].day21c = Employees[i].Days[20].DayOnFact.DayTypeOnEmployee;
                   tables[i].day22c = Employees[i].Days[21].DayOnFact.DayTypeOnEmployee;
                    tables[i].day23c = Employees[i].Days[22].DayOnFact.DayTypeOnEmployee;
                    tables[i].day24c = Employees[i].Days[23].DayOnFact.DayTypeOnEmployee;
                   tables[i].day25c = Employees[i].Days[24].DayOnFact.DayTypeOnEmployee;
                    tables[i].day26c = Employees[i].Days[25].DayOnFact.DayTypeOnEmployee;
                   tables[i].day27c = Employees[i].Days[26].DayOnFact.DayTypeOnEmployee;
                   tables[i].day28c = Employees[i].Days[27].DayOnFact.DayTypeOnEmployee;
                if (Employees[i].Days.Length>28)
                   tables[i].day29c = Employees[i].Days[28].DayOnFact.DayTypeOnEmployee;
                if (Employees[i].Days.Length > 29)
                    tables[i].day30c = Employees[i].Days[29].DayOnFact.DayTypeOnEmployee;
                if (Employees[i].Days.Length > 30)
                    tables[i].day31c = Employees[i].Days[30].DayOnFact.DayTypeOnEmployee;
                    //Details = string.Format("Должность: {0}  Табельный номер: {1}.";
                    //Employees[i].post, Employees[i].tabelNumber),
                    tables[i].summa = Employees[i].TotalTime.TotalHours;
                    //Employees[i].oneDayDatas.Sum(n => n.Work_time.Hours)
                 
            }
            int dim = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 31; i > 27; i--)
            {
                dt[i].Width = dt[25].Width;
            }
            for (int i = 31; i > dim; i--)
            {
                dt[i].MinWidth = 0;
                dt[i].Width = 0;
            }
        }
    }
}
