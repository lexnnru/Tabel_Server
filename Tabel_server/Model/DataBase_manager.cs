using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tabel_server.Model.Data;


namespace Tabel_server.Model
{
    public class DataBase_manager
    {
        public event Action<string, string> AddingNewTabelNumber;
        List<string> paramForUsersTable = new List<string>() { "day", "month", "year", "startdayH", "startdayEnd", "enddayH", "enddayMin", "city", "specCheck", "achiv" };
        List<string> typeOfDataForTabelUsersTable= new List<string>() { "integer", "integer", "integer", "integer", "integer", "integer", "integer", "text", "text", "text" };
        public string NameOfTablenamberUserTable = "TabelNambersAndUsers";
        public List<string> paramForTabelNamberUserTable = new List<string>() { "TabelNamber", "Family", "Name",  "ParentName",
            "dataOfEmployment", "dateOfDismiss", "salary", "post" };
        List<string> typedataForTabelNamberUserTable = new List<string>() { "text", "text", "text", "text", "INTEGER", "INTEGER", "INTEGER", "text" };
        private String dbFileName;
        SQLiteConnection db_connection;
        public void CreateTabelUserTable()
        {
            if(CheckExistingTable(NameOfTablenamberUserTable)==false)
            {
                Create_DataBase_Table(NameOfTablenamberUserTable, paramForTabelNamberUserTable, typedataForTabelNamberUserTable);
            }
        }

        public DataBase_manager(String dbFileName)
        {
            this.dbFileName = dbFileName;
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }
            db_connection = new SQLiteConnection("Data Source=" + dbFileName);
            db_connection.Open();
        }
        public void Connect(string dbFileName)
        {
            db_connection = new SQLiteConnection("Data Source=" + dbFileName);
            db_connection.Open();
        }
        public void Create_DataBase_Table(string nametable, List<string> nameField, List<string> tipeOfData)
        {
            string parametrForCreateTable = "";
            int i = 0;
            while (i < nameField.Count)
            {
                parametrForCreateTable = parametrForCreateTable + nameField[i] + " " + tipeOfData[i];
                if (i < nameField.Count - 1)
                { parametrForCreateTable = parametrForCreateTable + ", "; }
                i++;
            }
            new SQLiteCommand($"CREATE TABLE IF NOT EXISTS '{nametable}' (id INTEGER PRIMARY KEY AUTOINCREMENT, {parametrForCreateTable})", db_connection).ExecuteNonQuery();

        }
        public List<List<string>> GetAllRowFromTable(string nameTable)
        {
            List<List<string>> listEpl = new List<List<string>>();
            List<string> list;
            SQLiteDataReader dr = new SQLiteCommand($"select * from '{nameTable}'", db_connection).ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    list = new List<string>();
                    for (int i = 1; i < dr.FieldCount; i++)
                    {
                        list.Add(Convert.ToString(dr.GetValue(i)));
                    }
                    listEpl.Add(list);
                }
            }
            return listEpl;
        }
            public List<string> GetRowFromTable(string nameTable, List<string> ColumnForSearch, List<string> ZnachenieForSearch)
             {
            List<string> list = new List<string>();
            string partOFcommand="";

                for (int i = 0; i < ColumnForSearch.Count; i++)
                {
                    partOFcommand = partOFcommand + ColumnForSearch[i] + "=" + ZnachenieForSearch[i];
                    if (i < ZnachenieForSearch.Count - 1)
                    {
                        partOFcommand = partOFcommand + " and ";
                    }
                }
                SQLiteDataReader dr = new SQLiteCommand($"select * from '{nameTable}' where  {partOFcommand}", db_connection).ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            list.Add(Convert.ToString(dr.GetValue(i)));
                        }
                        dr.Close();
                    }
                }
                return list;
            }
        public List<string> GetColumnFromDB(string TableName, string ColumnName)
        {
            List<string> list = new List<string>();
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = $"SELECT {ColumnName} FROM {TableName}";
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    list.Add(Convert.ToString(dr.GetValue(0)));
                }
            }
            return list;
        }
        public List<IncomingDataTable> Get_Month_IDD(string nameTable, int year, int month)
        {
            List<IncomingDataTable> list = new List<IncomingDataTable>();
            for (int i = 1; i <= 31; i++)
            {
                list.Add(Get_IncomingDataTable(nameTable, year, month, i));
            }
            return list;
        }
        public IncomingDataTable Get_IncomingDataTable(string nameTable, int year, int month, int day)
        {
            IncomingDataTable idd = new IncomingDataTable();
            SQLiteDataReader dr = new SQLiteCommand($"select * from '{nameTable}' where  {paramForUsersTable[2]}={year} and {paramForUsersTable[1]}={month} and {paramForUsersTable[0]}={day} ", db_connection).ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                { 
                    idd.daynumber = new DateTime(Convert.ToInt32(dr.GetValue(3)), Convert.ToInt32(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(1)));
                    idd.startday= new DateTime(Convert.ToInt32(dr.GetValue(3)), 
                        Convert.ToInt32(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(1)), Convert.ToInt32(dr.GetValue(4)), Convert.ToInt32(dr.GetValue(5)), 0);
                    idd.endday = new DateTime(Convert.ToInt32(dr.GetValue(3)),
    Convert.ToInt32(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(1)), Convert.ToInt32(dr.GetValue(6)), Convert.ToInt32(dr.GetValue(7)), 0);
                    idd.city = Convert.ToString(dr.GetValue(8));
                    idd.specCheck = Convert.ToString(dr.GetValue(9));
                    idd.achiv= Convert.ToString(dr.GetValue(10));
                    if (idd.isHoliday=true || idd.specCheck=="ком.")
                    idd.Work_time = idd.endday - idd.startday;
                    else
                    idd.Work_time = idd.endday - idd.startday-new TimeSpan(0, 48, 0);
                }
            }
            dr.Close();
            return idd;
        }
        public List<(int, IncomingDataTable)> LoadFileTabelToDatabase(string path)
        {
           
                Deserialization ds = new Deserialization();
                List<IncomingDataTable> odd = ds.GetOneTabelData(path, out string tablename);
                List<(int, IncomingDataTable) > oddRowToUpdate=new List<(int, IncomingDataTable)>();
                if (CheckExistingTable(tablename))
                {
                for (int i = 0; i < odd.Count; i++)
                {
                    CheckExistingRowInTable(odd[i].tabelNumber, odd[i].daynumber.Year, odd[i].daynumber.Month, odd[i].daynumber.Day);
                    List<string> znachenies = new List<string>();
                    znachenies.Add(odd[i].daynumber.Day.ToString());
                    znachenies.Add(odd[i].daynumber.Month.ToString());
                    znachenies.Add(odd[i].daynumber.Year.ToString());
                    znachenies.Add(odd[i].startday.Hour.ToString());
                    znachenies.Add(odd[i].startday.Minute.ToString());
                    znachenies.Add(odd[i].endday.Hour.ToString());
                    znachenies.Add(odd[i].endday.Minute.ToString());
                    znachenies.Add(odd[i].city);
                    znachenies.Add(odd[i].specCheck);
                    znachenies.Add(odd[i].achiv);
                    if (CheckExistingRowInTable(odd[i].tabelNumber, odd[i].daynumber.Year, odd[i].daynumber.Month, odd[i].daynumber.Day))
                    {
                        oddRowToUpdate.Add((i, odd[i]));
                       // UpdateRow(odd[i].tabelNumber, paramForUsersTable, znachenies);
                    }
                    else { AddRowToTable(odd[i].tabelNumber, paramForUsersTable, znachenies); }
                      }
                MessageBox.Show("Файл " + path + " был добавлен.");
                return oddRowToUpdate;
            }
            
                else
                {
                    MessageBox.Show("Работника с табельным номером: " + tablename +" не обнаружено!" + " Файл " + path +" не был добавлен.");
                return oddRowToUpdate;
                //Create_DataBase_Table(tablename, paramForUsersTable, typeOfDataForTabelUsersTable);
                //List<string> znachenie = new List<string>() { tablename, odd[0].family, odd[0].name, odd[0].parentName, odd[0].mail};
                //AddRowToTable(NameOfTablenamberUserTable, paramForTabelNamberUserTable, znachenie);
            }
                
            
        }
        public bool CheckExistingTable(string nametable)
        {
            SQLiteDataReader dr = new SQLiteCommand($"SELECT count(*) FROM sqlite_master where type='table' and name='{nametable}'", db_connection).ExecuteReader();
            dr.Read();
            object count = dr["count(*)"];
            bool checktabelnamber;
            if (Convert.ToInt32(count) == 1)
            { checktabelnamber = true; }
            else checktabelnamber = false;
            dr.Close();
            return checktabelnamber;
        }
        public bool CheckExistingRowInTable(string nametable, int year, int month, int day)
        {
            bool checktabelnamber;
            if (CheckExistingTable(nametable))
            {
                SQLiteDataReader dr = new SQLiteCommand($"select count(*) from '{nametable}' where year = {year} and month = {month} and day = {day}", db_connection).ExecuteReader();
                dr.Read();
                object count = dr["count(*)"];
                if (Convert.ToInt32(count) == 1)
                { checktabelnamber = true; }
                else checktabelnamber = false;
                dr.Close();
                return checktabelnamber;
            }
            else {
                checktabelnamber = false;
                return checktabelnamber;
              }
        }
        public void AddRowToTable(string nametable, List<string> parametr, List<string> znachenie)
        {
            string parametrs = "";
            for (int i = 0; i < parametr.Count; i++)
            { parametrs = parametrs + parametr[i];
                if (i < parametr.Count - 1)
                { parametrs = parametrs + ", "; }
            }
            string znachenies = "";
            for (int i = 0; i < znachenie.Count; i++)
            {
                if (znachenie[i] == "")
                    znachenies = znachenies + "null";
                else { znachenies = znachenies + "'" + znachenie[i] + "'"; }
                if (i < znachenie.Count - 1)
                {
                    znachenies = znachenies + ", ";
                }
            }
            new SQLiteCommand($"INSERT INTO '{nametable}' ({parametrs}) VALUES({znachenies})", db_connection).ExecuteNonQuery();
        }
        public void UpdateEmployee(Employee olddata, Employee newData)
        {
            List<string> znachenies = new List<string>();
            znachenies.Add(olddata.tabelNumber);
            znachenies.Add(newData.family);
            znachenies.Add(newData.name);
            znachenies.Add(newData.parentName);
            znachenies.Add(newData.dataOfEmployment.ToString());
            znachenies.Add(newData.dateOfDismiss.ToString());
            znachenies.Add(newData.salary.ToString());
            znachenies.Add(newData.post.ToString());
            string fullCommand = "";
            string endcomand = "";
            for (int i = 0; i < paramForTabelNamberUserTable.Count; i++)
            {
                if (znachenies[i] == "")
                    fullCommand = paramForTabelNamberUserTable[i] + "= null";
                else { fullCommand = paramForTabelNamberUserTable[i] + "=" + "'" + znachenies[i] + "'"; }
                if (i < paramForTabelNamberUserTable.Count - 1)
                {
                    fullCommand = fullCommand + ", ";
                }
                endcomand += fullCommand;
            }
            new SQLiteCommand($"UPDATE '{NameOfTablenamberUserTable}' set  {endcomand} where {paramForTabelNamberUserTable[0]}={znachenies[0]}", db_connection).ExecuteNonQuery();
        }
        public void UpdateRow(string nametable, List<string> parametr, List<string> znachenie)
        {
            string fullCommand = "";
            string endCommand = "";
            for (int i = 0; i < parametr.Count; i++)
            {
                if (znachenie[i] == "")
                    fullCommand = parametr[i] + "= null";
                else { fullCommand = parametr[i] + "=" + "'" + znachenie[i] + "'"; }
                if (i < parametr.Count - 1)
                {
                    fullCommand = fullCommand + ", ";
                }
                endCommand += fullCommand;
            }
            
            new SQLiteCommand($"UPDATE '{nametable}' set  {endCommand} where {parametr[0]}={znachenie[0]} " +
            $"and {parametr[1]}={znachenie[1]} and {parametr[2]}={znachenie[2]}", db_connection).ExecuteNonQuery();
        }

        ///Работа с таблицей выходных
        public List<DateTime> GetHoliYear(int year)
        {
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = "SELECT * FROM Holiday where year =  @year ";
            sqlcmd.Parameters.Add("@year", System.Data.DbType.String).Value = year;
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            List<DateTime> yearHoliday = new List<DateTime>();
            if (dr.HasRows)
            {
                while (dr.Read()) // построчно считываем данные
                {
                    int day = Convert.ToInt16(dr.GetValue(3));
                    int month = Convert.ToInt16(dr.GetValue(2));
                    int yea = Convert.ToInt16(dr.GetValue(1));
                    yearHoliday.Add(new DateTime(yea, month, day).ToUniversalTime());
                }
            }
            return yearHoliday;
        }
        public List<DateTime> GetWorkHoliYear(int year)
        {
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = "SELECT * FROM WorkHoliday where year =  @year ";
            sqlcmd.Parameters.Add("@year", System.Data.DbType.String).Value = year;
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            List<DateTime> yearHoliday = new List<DateTime>();
            if (dr.HasRows)
            {
                while (dr.Read()) // построчно считываем данные
                {
                    int day = Convert.ToInt16(dr.GetValue(3));
                    int month = Convert.ToInt16(dr.GetValue(2));
                    int yea = Convert.ToInt16(dr.GetValue(1));
                    yearHoliday.Add(new DateTime(yea, month, day).ToUniversalTime());
                }
            }
            return yearHoliday;
        }
        public void ClearholiTable()
        {
            new SQLiteCommand("DELETE FROM Holiday ", db_connection).ExecuteNonQuery();
            new SQLiteCommand("DELETE FROM WorkHoliday ", db_connection).ExecuteNonQuery();
            
        }
        public void SetMonthHoliday(int year, int month, int day)
        {
            Create_DataBase_Table("Holiday", new List<string> { "Year", "Month", "Day" }, new List<string> { "text", "text", "text" });
            DataTable sqlTable = new DataTable();
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = "INSERT INTO Holiday (Year, Month, Day) VALUES (@year, @month, @day)";
            sqlcmd.Parameters.Add("@year", System.Data.DbType.String).Value = year;
            sqlcmd.Parameters.Add("@month", System.Data.DbType.String).Value = month;
            sqlcmd.Parameters.Add("@day", System.Data.DbType.String).Value = day;
            sqlcmd.ExecuteNonQuery();
        }
        public void SetMonthWorkDayOnHoloday(int year, int month, int day)
        {
            Create_DataBase_Table("WorkHoliday", new List<string> { "Year", "Month", "Day" }, new List<string> { "text", "text", "text" });
            DataTable sqlTable = new DataTable();
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = "INSERT INTO WorkHoliday (Year, Month, Day) VALUES (@year, @month, @day)";
            sqlcmd.Parameters.Add("@year", System.Data.DbType.String).Value = year;
            sqlcmd.Parameters.Add("@month", System.Data.DbType.String).Value = month;
            sqlcmd.Parameters.Add("@day", System.Data.DbType.String).Value = day;
            sqlcmd.ExecuteNonQuery();
        }
        public List<int> GetMonthHoliday(int year, int month)
        {
            DataTable sqlTable = new DataTable();
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = "SELECT day FROM Holiday where year =  @year and  month = @month  ";
            sqlcmd.Parameters.Add("@year", System.Data.DbType.String).Value = year;
            sqlcmd.Parameters.Add("@month", System.Data.DbType.String).Value = month;
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            List<int> monthHoliday = new List<int>();
            if (dr.HasRows)
            {
                while (dr.Read()) // построчно считываем данные
                {
                    int day = Convert.ToInt16(dr.GetValue(0));
                    monthHoliday.Add(day);
                }
            }
            return monthHoliday;
        }
        public List<int> GetWorkDayOnHoliday(int year, int month)
        {
            DataTable sqlTable = new DataTable();
            SQLiteCommand sqlcmd = db_connection.CreateCommand();
            sqlcmd.CommandText = "SELECT day FROM WorkHoliday where year =  @year and  month = @month  ";
            sqlcmd.Parameters.Add("@year", System.Data.DbType.String).Value = year;
            sqlcmd.Parameters.Add("@month", System.Data.DbType.String).Value = month;
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            List<int> monthHoliday = new List<int>();
            if (dr.HasRows)
            {
                while (dr.Read()) // построчно считываем данные
                {
                    int day = Convert.ToInt16(dr.GetValue(0));
                    monthHoliday.Add(day);
                }
            }
            return monthHoliday;
        }
        public bool HoliIsThisDay(int Year, int Month, int Day)
        {
            bool HoliIsThisDay = false;
            int dayinmonth = DateTime.DaysInMonth(Year, Month);
            List<int> Weekend = new List<int>();
            for (int i = 1; i <= dayinmonth; i++)   // Add Weekend marking
            {
                if (new DateTime(Year, Month, i).DayOfWeek == DayOfWeek.Saturday || new DateTime(Year, Month, i).DayOfWeek == DayOfWeek.Sunday)
                {
                    Weekend.Add(i);
                }
            }
            List<int> monthHoliday = GetMonthHoliday(Year, Month);
            List<int> list1 = Weekend.Union(monthHoliday).ToList();
            list1.Distinct();
            List<int> workMonthHoliday = GetWorkDayOnHoliday(Year, Month);
            List<int> result = new List<int>();
            result = list1.Except(workMonthHoliday).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (Day == result[i])
                { HoliIsThisDay = true; }
            }
            return HoliIsThisDay;
        }
        //// Получить список выходных
        public List<DateTime> GetHoliDateTimes(DateTime dateTime)
        {
            List<DateTime> dt = new List<DateTime>();
            for (int i=1; i<=DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i++)
            { if (HoliIsThisDay(dateTime.Year, dateTime.Month, i))
                { dt.Add(new DateTime(dateTime.Year, dateTime.Month, i));
                }
            }
            return dt;
        }
        public List<int> GetRealHoliMonth(int year, int month)

        {
            List<int> Get = new List<int>();

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
                if (HoliIsThisDay(DateTime.Now.Year, DateTime.Now.Month, i) == true)
                {
                    Get.Add(i);
                }
            return Get;
        }
        public void AddNewEmplpyee(Employee employee)
        {
            if (CheckExistingTable(employee.tabelNumber))
            {
                MessageBox.Show("Работник с таким табельным номером уже существует");
            }
            else
            {
                Create_DataBase_Table(employee.tabelNumber, paramForUsersTable, typeOfDataForTabelUsersTable);
                List<string> znachenie = new List<string>() { employee.tabelNumber, employee.family, employee.name, employee.parentName,
                    employee.dataOfEmployment.ToString(), employee.dateOfDismiss.ToString(), employee.salary.ToString(), employee.post};
                AddRowToTable(NameOfTablenamberUserTable, paramForTabelNamberUserTable, znachenie);
            }
        }
    }
   
}
