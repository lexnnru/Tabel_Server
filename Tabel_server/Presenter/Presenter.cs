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
        Model.DataBase_manager dataBase_Manager;
        Employee employee = new Employee();
        //Model.Monitoring monitor = new Model.Monitoring();
        ObservableCollection<MonthEmployeeData> monthemployees = new ObservableCollection<MonthEmployeeData>();
        ObservableCollection<Employee> FullEmployees = new ObservableCollection<Employee>();
        public Presenter(ImainWindow imain)
        {
            this.imain = imain;
            dataBase_Manager = new Model.DataBase_manager("TabelDB");
            //imain.Lb_users_SelectionChange += Lb_users_SelectionChange;
            imain.LoadHoli += Imain_LoadHoli;
            imain.GetMonthEmployeeData += Imain_GetMonthEmployeeData;
            imain.DateChanged += Imain_DateChanged;
            imain.LoadDataTableToDB += Imain_LoadDataTableToDB;
            //monitor.filechange += Monitor_filechange;
            dataBase_Manager.CreateTabelUserTable();
            //Thread tr = new Thread(monitor.Chek);
            //tr.IsBackground = true;
            //tr.Start();
            monthemployees = new ObservableCollection<MonthEmployeeData>(Imain_GetMonthEmployeeData(imain.dtMain));
            imain.SetlbUsers(monthemployees);
            imain.mu.Loaded += Mu_Loaded;
            imain.mu.AddNewEmpl += Mu_AddNewEmpl;
            imain.mu.Setsource += Mu_Setsource;
            imain.mu.ChangeEmpl += Mu_ChangeEmpl;
            
        }
        private void Imain_LoadDataTableToDB(List<string> obj)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                try
                {
                    bool show = false;
                    List< IncomingDataTable> rowForUpdate =dataBase_Manager.CompareFileTabelWithDatabase(obj[i]);
                    List<IncomingDataTable> rowINDB = dataBase_Manager.Get_Month_IDD(rowForUpdate[0].tabelNumber, rowForUpdate[0].daynumber.Year, rowForUpdate[0].daynumber.Month);
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
                         
                        window.showTable(rowForUpdate, rowINDB, dataBase_Manager.GetEmployee(rowForUpdate[0].tabelNumber), obj[i]);
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
            dataBase_Manager.AddTabelToDB(obj);
            Model.Loger.GetLog(obj + " файл табеля добавлен");
        }

        private void Mu_ChangeEmpl(Employee arg1, Employee arg2)
        {
            dataBase_Manager.UpdateEmployee(arg1, arg2);
        }
        private void Mu_Setsource()
        {
            imain.mu.employees = new ObservableCollection<Employee>(employee.GetAllEmployees(dataBase_Manager, imain.dtMain));

            imain.mu.lbUsers.ItemsSource = imain.mu.employees;
        }
        private void Mu_AddNewEmpl(Employee obj)
        {
            string message=dataBase_Manager.AddNewEmplpyee(obj);
            imain.ShowMess(message);
            imain.SetlbUsers(new ObservableCollection<MonthEmployeeData>(Imain_GetMonthEmployeeData(imain.dtMain)));
        }
        private void Mu_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            imain.mu.employees = new ObservableCollection<Employee>(employee.GetAllEmployees(dataBase_Manager, imain.dtMain));
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
            List<Employee> empls = emp.GetAllEmployees(dataBase_Manager, imain.dtMain);
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>(employee.GetAllEmployees(dataBase_Manager, imain.dtMain));
            List<string> tabelnumbers = new List<string>();
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
                List<IncomingDataTable> idd = dataBase_Manager.Get_Month_IDD(empls[i].tabelNumber, date.Year, date.Month);
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
                    odd.isHoliday =
                       dataBase_Manager.HoliIsThisDay(idd[k].daynumber.Year, idd[k].daynumber.Month, idd[k].daynumber.Day);
                    DateTime nextDay = odd.daynumber.AddDays(1);
                    odd.LunchTime = new TimeSpan(0, 48, 0);
                    odd.nextDayisholi = dataBase_Manager.HoliIsThisDay(nextDay.Year, nextDay.Month, nextDay.Day);
                    if (odd.isHoliday == true || odd.specCheck == "ком.")
                    {
                        odd.Work_time = odd.endday - odd.startday;
                    }
                    else if (odd.specCheck == "больн." || odd.specCheck == "отг." || odd.specCheck == "отп." || odd.specCheck == "отп.б.с.")
                    {

                        if (odd.specCheck == "больн." || odd.specCheck == "отп.б.с.")
                        {
                            if (dataBase_Manager.HoliIsThisDay(nextDay.Year, nextDay.Month, nextDay.Day))
                            { odd.Sick_time = new TimeSpan(7, 12, 0); }
                            else
                            { odd.Sick_time = new TimeSpan(8, 12, 0); }
                        }
                        else if (odd.specCheck == "отг.")
                        {
                            if (dataBase_Manager.HoliIsThisDay(nextDay.Year, nextDay.Month, nextDay.Day))
                            { odd.Compensatory_time = new TimeSpan(7, 12, 0); }
                            else
                            { odd.Compensatory_time = new TimeSpan(8, 12, 0); }
                        }
                        else if (odd.specCheck == "отп.")
                        {
                            if (dataBase_Manager.HoliIsThisDay(nextDay.Year, nextDay.Month, nextDay.Day))
                            { odd.Vocation_time = new TimeSpan(7, 12, 0); }
                            else
                            { odd.Vocation_time = new TimeSpan(8, 12, 0); }
                        }
                    }
                    else
                    {
                        if ((odd.endday - odd.startday) > new TimeSpan(4, 48, 0))
                            odd.Work_time = odd.endday - odd.startday - odd.LunchTime;
                        else odd.Work_time = odd.endday - odd.startday;
                        if (odd.endday - odd.startday == new TimeSpan(0, 0, 0) || odd.daynumber == new DateTime(0001, 1, 1))
                        { odd.error = true; }
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
                    monthEmployeeData.oneDayDatas.Add(odd);
                }
                if (date<new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
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
                    if ( Properties.Settings.Default.dayX.Day<20)
                    {
                        for (int q = 1; q <= 15; q++)
                        {
                            if (monthEmployeeData.oneDayDatas[q - 1].error == true)
                            {
                                monthEmployeeData.error = true;
                            }
                        }
                    }
                    else

                    {
                        for (int q = 1; q <=DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); q++)
                        {
                            if (monthEmployeeData.oneDayDatas[q - 1].error == true)
                            {
                                monthEmployeeData.error = true;
                            }
                        }
                    }
                }
                
                monthEmployeeData.persentFill = (100 / DateTime.DaysInMonth(date.Year, date.Month)) * fillCount;
                monthEmployeeData.post = dataBase_Manager.GetEmployee(monthEmployeeData.tabelNumber).post;

                foreach (string tabelnumber in tabelnumbers)
                {
                    if (tabelnumber == monthEmployeeData.tabelNumber)
                    { monthEmployeeDatas.Add(monthEmployeeData); }
                    else { }
                };
            }
            imain.HoliDateTimes = dataBase_Manager.GetHoliDateTimes(imain.dtMain);
            return monthEmployeeDatas;
        }
        private void Imain_LoadHoli(string obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = System.IO.File.ReadAllText(obj);
            HoliYear hy = serializer.Deserialize<HoliYear>(json);
            for (int i = 0; i <= hy.HoliYea.Count - 1; i++)
            {
                dataBase_Manager.SetMonthHoliday(hy.HoliYea[i].ToLocalTime().Year, hy.HoliYea[i].ToLocalTime().Month, hy.HoliYea[i].ToLocalTime().Day);
            }
            for (int i = 0; i <= hy.HoliWorkYea.Count - 1; i++)
            {
                dataBase_Manager.SetMonthWorkDayOnHoloday(hy.HoliWorkYea[i].Year, hy.HoliWorkYea[i].Month, hy.HoliWorkYea[i].Day);
            }
        }
        //private void Lb_users_SelectionChange(string tabelnamber)
        //{
        //    imain.HoliDateTimes = dataBase_Manager.GetHoliDateTimes(imain.dtMain);
        //    imain.ShowTable(dataBase_Manager.Get_Month_IDD(tabelnamber, imain.dtMain.Year, imain.dtMain.Month)); 
        //}
        public void GetDayX()
        {
            List<int> holiMonth = dataBase_Manager.GetRealHoliMonth(DateTime.Now.Year, DateTime.Now.Month);
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
            //if (DateTime.Now.Day > dayX)
            //{
            //    for (int i = 0; i <= 3; i++)
            //    {
            //        Invoke(new Action(() => {
            //            showNotify(true);
            //        }));
            //        Thread.Sleep(1500);
            //        Invoke(new Action(() => {
            //            showNotify(false);
            //        }));
            //        Thread.Sleep(1500);
            //    }
            //}
            //else
            //{
            //    Invoke(new Action(() =>
            //    {
            //        showNotify(false);
            //    }));
            //}



        }
        //private void Monitor_filechange()
        //{
        //    imain.Get.Dispatcher.Invoke(() =>
        //    {
        //        List<string> files = monitor.Get_files();
        //        for (int i = 0; i < files.Count; i++)
        //            imain.ShowLog(DateTime.Now.ToString() + " Обнаружен новый файл: " + files[i]);
        //    });
        //    List<string> files1 = monitor.Get_files();
        //    for (int i = 0; i < files1.Count; i++)
        //    {
        //        try
        //        {
        //            dataBase_Manager.LoadFileTabelToDatabase(files1[i]);
        //            imain.Get.Dispatcher.Invoke(() =>
        //            {
        //                imain.ShowLog(DateTime.Now.ToString() + " Файл успешно добавлен: " + files1[i]);
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            string extext = ex.Message;
        //            imain.Get.Dispatcher.Invoke(() =>
        //            {
        //                imain.ShowLog(DateTime.Now.ToString() + extext);
        //            });
        //        }
        //        System.IO.File.Delete(files1[i]);
        //        imain.Get.Dispatcher.Invoke(() =>
        //        {
        //            imain.ShowLog(DateTime.Now.ToString() + " Файл удален: " + files1[i]);
        //        });
        //    }
        //    imain.Get.Dispatcher.Invoke(() =>
        //    {
        //        imain.SetlbUsers(new ObservableCollection<Employee>(employee.GetAllEmployees(dataBase_Manager)));

        //    });
        //}
        ///Управление пользователями

    }
}
