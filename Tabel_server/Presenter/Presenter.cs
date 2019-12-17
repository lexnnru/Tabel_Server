using Calendar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Threading;
using Tabel_server.Model.Data;

namespace Tabel_server.Presenter
{
    public class Presenter
    {
        ImainWindow imain;
        Model.DataBase_manager DBmanager;
        Employee employee = new Employee();
        ObservableCollection<MonthEmployeeData> monthemployees = new ObservableCollection<MonthEmployeeData>();
        ObservableCollection<Employee> FullEmployees = new ObservableCollection<Employee>();
        public Presenter(ImainWindow imain)
        {
            this.imain = imain;
            DBmanager = new Model.DataBase_manager("TabelDB");
            DBmanager.CreateTabelUserTable();
            DBmanager.CreateHoliTabel();
            imain.LoadHoli += Imain_LoadHoli;
            imain.GetMonthEmployeeData += Imain_GetMonthEmployeeData;
            imain.DateChanged += Imain_DateChanged;
            imain.LoadDataTableToDB += Imain_LoadDataTableToDB;
            monthemployees = new ObservableCollection<MonthEmployeeData>(Imain_GetMonthEmployeeData(imain.dtMain));
            imain.SetlbUsers(monthemployees);
            imain.mu.Loaded += Mu_Loaded;
            imain.mu.AddNewEmpl += Mu_AddNewEmpl;
            imain.mu.Setsource += Mu_Setsource;
            imain.mu.ChangeEmpl += Mu_ChangeEmpl;
            imain.ShowCalendar += Imain_ShowCalendar;
            imain.SpecialDays = DBmanager.Get_DayTypeInYear(imain.dtMain.Date.Year);
        }

        private void Imain_ShowCalendar()
        {
            
            imain.calendar.SetDayType += Year_Set_DayType;
            imain.calendar.GetSpecialDays += Calendar_GetSpecialDays;
        }

        private void Calendar_GetSpecialDays(int obj)
        {
            imain.calendar.SpecialDays= DBmanager.Get_DayTypeInYear(obj);
        }

        private void Year_Set_DayType(DateTime arg1, DayType arg2, TimeSpan arg3)
        {
            DBmanager.SetDayType(arg1, arg2, arg3);
            imain.calendar.SpecialDays = DBmanager.Get_DayTypeInYear(DateTime.Now.Year);
        }

        private void Imain_LoadDataTableToDB(List<string> obj)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                try
                {
                    bool show = false;
                    List<IncomingDataTable> rowForUpdate = DBmanager.CompareFileTabelWithDatabase(obj[i]);
                    List<IncomingDataTable> rowINDB = DBmanager.Get_Month_IDD(rowForUpdate[0].tabelNumber, rowForUpdate[0].daynumber.Year, rowForUpdate[0].daynumber.Month);
                    for (int j = 0; j < rowForUpdate.Count; j++)
                    {

                        if (rowForUpdate[j].daynumber == rowINDB[j].daynumber && rowForUpdate[j].startday == rowINDB[j].startday && rowForUpdate[j].endday == rowINDB[j].endday &&
                            rowForUpdate[j].city == rowINDB[j].city && rowForUpdate[j].achiv == rowINDB[j].achiv && rowForUpdate[j].specCheck == rowINDB[j].specCheck)
                        {
                            rowForUpdate[j].overlap = true;
                        }
                        else
                        {
                            show = true;
                            rowForUpdate[j].overlap = false;
                            if (rowForUpdate[j].startday != rowINDB[j].startday)
                            { rowForUpdate[j].startdayMarking = true; }
                            if (rowForUpdate[j].endday != rowINDB[j].endday)
                            { rowForUpdate[j].enddayMarking = true; }
                            if (rowForUpdate[j].city != rowINDB[j].city)
                            { rowForUpdate[j].cityMarking = true; }
                            if (rowForUpdate[j].achiv != rowINDB[j].achiv)
                            { rowForUpdate[j].achivMarking = true; }
                            if (rowForUpdate[j].specCheck != rowINDB[j].specCheck)
                            { rowForUpdate[j].specCheckMarking = true; }
                        }
                    }
                    if (show)
                    {
                        PreveiwWindow.PreveiwTableForUpdate window = new PreveiwWindow.PreveiwTableForUpdate();

                        window.showTable(rowForUpdate, rowINDB, DBmanager.GetEmployee(rowForUpdate[0].tabelNumber), obj[i]);
                        window.AddTabelToDB += Window_AddTabelToDB;
                        window.Show();
                    }

                }
                catch (Exception ex)
                {
                    Model.Loger.GetLog(ex.Message);
                }

            }
        }
        private void Window_AddTabelToDB(string obj)
        {
            DBmanager.AddTabelToDB(obj);
            Model.Loger.GetLog(obj + " файл табеля добавлен");
        }

        private void Mu_ChangeEmpl(Employee arg1, Employee arg2)
        {
            DBmanager.UpdateEmployee(arg1, arg2);
        }
        private void Mu_Setsource()
        {
            imain.mu.employees = new ObservableCollection<Employee>(employee.GetAllEmployees(DBmanager, imain.dtMain));

            imain.mu.lbUsers.ItemsSource = imain.mu.employees;
        }
        private void Mu_AddNewEmpl(Employee obj)
        {
            string message = DBmanager.AddNewEmplpyee(obj);
            imain.ShowMess(message);
            imain.SetlbUsers(new ObservableCollection<MonthEmployeeData>(Imain_GetMonthEmployeeData(imain.dtMain)));
        }
        private void Mu_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            imain.mu.employees = new ObservableCollection<Employee>(employee.GetAllEmployees(DBmanager, imain.dtMain));
            imain.mu.lbUsers.ItemsSource = imain.mu.employees;
        }
        private void Imain_DateChanged()
        {

            monthemployees = new ObservableCollection<MonthEmployeeData>(Imain_GetMonthEmployeeData(imain.dtMain));
            imain.SetlbUsers(monthemployees);
        }
        private List<MonthEmployeeData> Imain_GetMonthEmployeeData(DateTime date)
        {
            GetDayX();
            List<MonthEmployeeData> monthEmployeeDatas = new List<MonthEmployeeData>();
            Employee emp = new Employee();
            List<Employee> empls = emp.GetAllEmployees(DBmanager, imain.dtMain);
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>(employee.GetAllEmployees(DBmanager, imain.dtMain));
            List<string> tabelnumbers = new List<string>();
            List<(DateTime, DayType, TimeSpan)> SpecialDays = DBmanager.Get_DayTypeInYear(date.Year);
            foreach (Employee employee in employees)
            {
                if (new DateTime(new DateTime(employee.dataOfEmployment).ToLocalTime().Year, new DateTime(employee.dataOfEmployment).ToLocalTime().Month, 1) <= new DateTime(imain.dtMain.Year, imain.dtMain.Month, DateTime.DaysInMonth(imain.dtMain.Year, imain.dtMain.Month)) &&
         new DateTime(employee.dateOfDismiss).ToLocalTime() >= new DateTime(imain.dtMain.Year, imain.dtMain.Month, 1))
                {
                    tabelnumbers.Add(employee.tabelNumber);
                }
            }
            for (int i = 0; i < empls.Count; i++)
            {
                MonthEmployeeData monthEmployeeData = new MonthEmployeeData();
                List<IncomingDataTable> idd = DBmanager.Get_Month_IDD(empls[i].tabelNumber, date.Year, date.Month);
                monthEmployeeData.family = empls[i].family;
                monthEmployeeData.name = empls[i].name;
                monthEmployeeData.parentName = empls[i].parentName;
                monthEmployeeData.mail = empls[i].mail;
                monthEmployeeData.tabelNumber = empls[i].tabelNumber;
                monthEmployeeData.fio = String.Join(" ", new string[] { empls[i].family, empls[i].name, empls[i].parentName });
                int fillCount = 0;
                for (int k = 0; k < idd.Count; k++)
                {
                    OneDayData odd = new OneDayData();
                    odd.startday = idd[k].startday;
                    odd.endday = idd[k].endday;
                    odd.daynumber = idd[k].daynumber;
                    odd.specCheck = idd[k].specCheck;
                    odd.city = idd[k].city;
                    odd.achiv = idd[k].achiv;
                    odd.isHaveData = idd[k].isHaveData;
                    if (k + 1 <= DateTime.DaysInMonth(date.Year, date.Month))
                    { odd.Work_time_According_plan = Work_time_According_plan(new DateTime(date.Year, date.Month, k + 1), SpecialDays); }

                    odd.LunchTime = new TimeSpan(0, 48, 0);
                    if (odd.isHoliday == true || odd.specCheck == "ком.")
                    {
                        odd.Work_time = odd.endday - odd.startday;
                    }
                    else if (odd.specCheck == "больн." || odd.specCheck == "отг." || odd.specCheck == "отп." || odd.specCheck == "отп.б.с.")
                    {

                        if (odd.specCheck == "больн." || odd.specCheck == "отп.б.с.")
                        {
                            {
                                odd.Sick_time = Work_time_According_plan(odd.daynumber, SpecialDays);
                            }
                        }
                        else if (odd.specCheck == "отг.")
                        {
                            {
                                odd.Compensatory_time = Work_time_According_plan(odd.daynumber, SpecialDays);
                            }

                        }
                        else if (odd.specCheck == "отп.")
                        {
                            {
                                odd.Vocation_time = Work_time_According_plan(odd.daynumber, SpecialDays);
                            }
                        }
                    }
                    else
                    {
                        if ((odd.endday - odd.startday) > new TimeSpan(4, 48, 0))
                        { odd.Work_time = odd.endday - odd.startday - odd.LunchTime; }
                        else { odd.Work_time = odd.endday - odd.startday; }

                    }
                    if (odd.daynumber == new DateTime(0001, 1, 1))
                    {
                        odd.isHaveData = false;
                    }
                    else
                    {
                        odd.isHaveData = true;
                        fillCount += 1;
                    }

                    if (odd.endday - odd.startday == new TimeSpan(0, 0, 0) && odd.Work_time_According_plan != new TimeSpan(0, 0, 0)
                        || odd.daynumber == new DateTime(0001, 1, 1))
                    { odd.error = true; }
                    monthEmployeeData.oneDayDatas.Add(odd);
                }
                if (date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                {
                    for (int q = 1; q <= DateTime.DaysInMonth(date.Year, date.Month); q++)
                    {
                        if (monthEmployeeData.oneDayDatas[q - 1].error == true)
                        {
                            monthEmployeeData.error = true;
                        }
                    }
                }
                else
                {
                    if (Properties.Settings.Default.dayX.Day < 20)
                    {
                        for (int q = 1; q <= Properties.Settings.Default.dayX.Day; q++)
                        {
                            if (monthEmployeeData.oneDayDatas[q - 1].error == true)
                            {
                                monthEmployeeData.error = true;
                            }
                        }
                    }
                    else
                    {
                        for (int q = 1; q <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); q++)
                        {
                            if (monthEmployeeData.oneDayDatas[q - 1].error == true)
                            {
                                monthEmployeeData.error = true;
                            }
                        }
                    }
                }

                monthEmployeeData.persentFill = (100 / DateTime.DaysInMonth(date.Year, date.Month)) * fillCount;
                monthEmployeeData.post = DBmanager.GetEmployee(monthEmployeeData.tabelNumber).post;

                foreach (string tabelnumber in tabelnumbers)
                {
                    if (tabelnumber == monthEmployeeData.tabelNumber)
                    { monthEmployeeDatas.Add(monthEmployeeData); }
                    else { }
                };
            }
            imain.HoliDateTimes = GetHoliDateTimes(imain.dtMain, SpecialDays);
            return monthEmployeeDatas;
        }
        public List<DateTime> GetHoliDateTimes(DateTime dateTime, List<(DateTime, DayType, TimeSpan)> SpecialDays)
        {
            List<DateTime> dt = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i++)
            {
                SpecialDays.ForEach(x =>
                {
                    if (new DateTime(dateTime.Year, dateTime.Month, i) == x.Item1)
                    {
                        dt.Add(new DateTime(dateTime.Year, dateTime.Month, i));
                    }

                });
                if (dt.Contains(new DateTime(dateTime.Year, dateTime.Month, i)))
                {
                }
                else
                {
                    if (new DateTime(dateTime.Year, dateTime.Month, i).DayOfWeek == DayOfWeek.Saturday ||
                    new DateTime(dateTime.Year, dateTime.Month, i).DayOfWeek == DayOfWeek.Sunday)
                    { dt.Add(new DateTime(dateTime.Year, dateTime.Month, i)); }
                }
            }
            return dt;
        }
        private TimeSpan Work_time_According_plan(DateTime day, List<(DateTime, DayType, TimeSpan)> SpecialDays)
        {
            TimeSpan ts = new TimeSpan(8, 12, 0);
            bool this_day_is_special = false;
            bool next_day_is_special = false;
            SpecialDays.ForEach(x =>
            {
                if (day == x.Item1)
                {
                    this_day_is_special = true;
                }

            });
            if (this_day_is_special == false)
            {
                SpecialDays.ForEach(x =>
                {
                    if (day + TimeSpan.FromDays(1) == x.Item1)
                    {
                        if (x.Item2 == DayType.FreeDay)
                        { ts = new TimeSpan(7, 12, 0); }
                        if (x.Item2 == DayType.FullDay)
                        { ts = new TimeSpan(8, 12, 0); }
                        next_day_is_special = true;
                    }
                });
            }
            if (this_day_is_special == false && next_day_is_special == false)
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                { ts = new TimeSpan(7, 12, 0); }
                else if (day.DayOfWeek == DayOfWeek.Saturday)
                { ts = new TimeSpan(0, 0, 0); }
                else if (day.DayOfWeek == DayOfWeek.Sunday)
                { ts = new TimeSpan(0, 0, 0); }
            }
            return ts;
        }
        private void Imain_LoadHoli(string obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = System.IO.File.ReadAllText(obj);
            HoliYear hy = serializer.Deserialize<HoliYear>(json);
            for (int i = 0; i <= hy.HoliYea.Count - 1; i++)
            {
                DBmanager.SetMonthHoliday(hy.HoliYea[i].ToLocalTime().Year, hy.HoliYea[i].ToLocalTime().Month, hy.HoliYea[i].ToLocalTime().Day);
            }
            for (int i = 0; i <= hy.HoliWorkYea.Count - 1; i++)
            {
                DBmanager.SetMonthWorkDayOnHoloday(hy.HoliWorkYea[i].Year, hy.HoliWorkYea[i].Month, hy.HoliWorkYea[i].Day);
            }
        }

        public void GetDayX()
        {
            List<int> holiMonth = new List<int>();
            List<(DateTime, DayType, TimeSpan)> SpecialDays = DBmanager.Get_DayTypeInYear(imain.dtMain.Year);
            List<DateTime> ldt = GetHoliDateTimes(imain.dtMain, SpecialDays);
            ldt.ForEach(x =>
            {
                holiMonth.Add(x.Day);
            });

            int dayX = 15;
            bool isitHoly = false;
            if (DateTime.Now.Day <= 15)
            {
                dayX = 15;
                for (int i = 15; i > 0; i--)
                {
                    isitHoly = false;
                    for (int j = 0; j < holiMonth.Count; j++)
                    {
                        if (holiMonth[j] == i)
                        {
                            isitHoly = true;
                        }
                    }
                    if (isitHoly == true)
                    { dayX--; }
                    else if (isitHoly == false)
                    { goto end1; }
                }
            }
            if (DateTime.Now.Day > 15)
            {
                dayX = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                for (int i = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i > 15; i--)
                {
                    isitHoly = false;
                    for (int j = 0; j < holiMonth.Count; j++)
                    {
                        if (holiMonth[j] == i)
                        {
                            isitHoly = true;
                        }
                    }
                    if (isitHoly == true)
                    { dayX--; }
                    else if (isitHoly == false)
                    { goto end1; }
                }
            }
        end1:;
            if (Properties.Settings.Default.dayX != new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayX))
            {
                Properties.Settings.Default.dayXOld = Properties.Settings.Default.dayX;
            }
            Properties.Settings.Default.dayX = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayX);
            Properties.Settings.Default.Save();
        }
    }
}
