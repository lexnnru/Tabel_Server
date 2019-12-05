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

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для UserControl3.xaml
    /// </summary>
    public class table
    {
        public string fio { get; set; }
        public TimeSpan hour1 { get; set; }
        public TimeSpan hour15 { get; set; }
        public TimeSpan hour20 { get; set; }
        public TimeSpan hourHoli20 { get; set; }
        public TimeSpan sickHours { get; set; }
        public int sickDays { get; set; }
        public TimeSpan vacationHours { get; set; }
        public int vacationDays { get; set; }
        public TimeSpan compensatory_hours { get; set; }
        public int compensatory_days { get; set; }
        public int daycomm { get; set; }
        public TimeSpan totalWorkHours { get; set; }
        public string Details { get; set; }
        public TimeSpan totalWorkHours_According_Plan { get; set; }



    }
    public partial class UserControl3 : UserControl, IUserControl3
    {
        ObservableCollection<MonthEmployeeData> employees;
        public ObservableCollection<table> tables { get; }
        
        public UserControl3()
        {
            InitializeComponent();
            this.DataContext = this;
            tables = new ObservableCollection<table>();
        }

        public UserControl uc3 =>this;


        public void SetSource(List<MonthEmployeeData> monthemployees, List<DateTime> HolidateTimes, DateTime dateTime)
        {

            tables.Clear();
            int DayinMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            this.employees=new ObservableCollection<MonthEmployeeData>(monthemployees);
            for (int i=0; i<monthemployees.Count; i++)
            {
                TimeSpan hour1=new TimeSpan(0, 0, 0);
                TimeSpan hour15 = new TimeSpan(0, 0, 0);
                TimeSpan hour20 = new TimeSpan(0, 0, 0);
                TimeSpan hourHoli20 = new TimeSpan(0, 0, 0);
                TimeSpan sickHours = new TimeSpan(0, 0, 0);
                TimeSpan vacationHours = new TimeSpan(0, 0, 0);
                TimeSpan compensatory_hours = new TimeSpan(0, 0, 0);
                TimeSpan totalWorkHours = new TimeSpan(0, 0, 0);
                TimeSpan totalWorkHours_According_Plan = new TimeSpan(0, 0, 0);

                int daycomm = 0;
                int sickDays = 0;
                int vacationDays = 0;
                int compensatory_days = 0;


                for (int j = 0; j < monthemployees[i].oneDayDatas.Count; j++)
                {
                    if(monthemployees[i].oneDayDatas[j].specCheck=="ком.")
                    { daycomm++;}
                    if (monthemployees[i].oneDayDatas[j].specCheck == "больн." || monthemployees[i].oneDayDatas[j].specCheck == "отп.б.с.")
                    { sickDays++;
                        sickHours += monthemployees[i].oneDayDatas[j].Sick_time;
                    }
                    if (monthemployees[i].oneDayDatas[j].specCheck == "отг.")
                    {
                        compensatory_days++;
                        compensatory_hours += monthemployees[i].oneDayDatas[j].Compensatory_time;
                    }
                    if (monthemployees[i].oneDayDatas[j].specCheck == "отп.")
                    {
                        compensatory_days++;
                        compensatory_hours += monthemployees[i].oneDayDatas[j].Vocation_time;
                    }


                    if (monthemployees[i].oneDayDatas[j].Work_time_According_plan> new TimeSpan(0,0,0))
                    {

                        if (monthemployees[i].oneDayDatas[j].Work_time <= monthemployees[i].oneDayDatas[j].Work_time_According_plan)
                        {
                            hour1 += monthemployees[i].oneDayDatas[j].Work_time;
                        }
                        else if (monthemployees[i].oneDayDatas[j].Work_time > monthemployees[i].oneDayDatas[j].Work_time_According_plan &&
                        monthemployees[i].oneDayDatas[j].Work_time <= monthemployees[i].oneDayDatas[j].Work_time_According_plan + new TimeSpan(2, 0, 0))
                        {
                            hour1 += monthemployees[i].oneDayDatas[j].Work_time_According_plan;
                            hour15 = hour15 + monthemployees[i].oneDayDatas[j].Work_time - monthemployees[i].oneDayDatas[j].Work_time_According_plan;
                        }
                        else if (monthemployees[i].oneDayDatas[j].Work_time > monthemployees[i].oneDayDatas[j].Work_time_According_plan + new TimeSpan(2, 0, 0))
                            {
                                hour1 += monthemployees[i].oneDayDatas[j].Work_time_According_plan;
                                hour15 += new TimeSpan(2, 0, 0);
                                hour20 = hour20 + monthemployees[i].oneDayDatas[j].Work_time - monthemployees[i].oneDayDatas[j].Work_time_According_plan - new TimeSpan(2, 0, 0);
                            }
                        
        

                  
                    }
                    else
                    {
                        if (monthemployees[i].oneDayDatas[j].Work_time <= new TimeSpan(8, 0, 0))
                        {
                            hourHoli20 += monthemployees[i].oneDayDatas[j].Work_time;
                        }
                        else
                        {
                            hour20 = hour20 + monthemployees[i].oneDayDatas[j].Work_time - new TimeSpan(8, 0, 0);
                        }
                    }

                    totalWorkHours = sickHours + vacationHours + compensatory_hours + hour1;
                    totalWorkHours_According_Plan += monthemployees[i].oneDayDatas[j].Work_time_According_plan;
                }

            tables.Add(new table { fio = monthemployees[i].fio, hour1=hour1, hour15=hour15, hour20=hour20, hourHoli20=hourHoli20 ,
                daycomm =daycomm, sickDays=sickDays, sickHours=sickHours, compensatory_days=compensatory_days,
                compensatory_hours =compensatory_hours, vacationDays=vacationDays, vacationHours=vacationHours, totalWorkHours=totalWorkHours,
                Details = string.Format("Должность: {0}  Табельный номер: {1}.", monthemployees[i].post, monthemployees[i].tabelNumber),
                totalWorkHours_According_Plan=totalWorkHours_According_Plan
            });
            }

        }
    }
}
