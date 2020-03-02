using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    public class Employee : INotifyPropertyChanged 
    {
        public string TabelNumber { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }
        public Int64 DataOfEmployment { get; set; }
        public Int64 DateOfDismiss { get; set; }
        public int Salary { get; set; }
        public string Post { get; set; }
        public override string ToString()
        {
            return string.Join(" ", Surname, Name, Patronymic);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //public List<OneDayData> GetOneDayDatas(Model.DataBase_manager db, DateTime date)
        //{ }
        public List<Employee> GetAllEmployees(Model.DataBase_manager db, DateTime date)
        {
            List<Employee> employees = new List<Employee>();
            List<List<string>> Lists = db.GetAllRowFromTable(db.NameOfTablenamberUserTable);
            Lists.ForEach(i => {
                employees.Add(new Employee()
                {
                    TabelNumber = i[0],
                    Surname = i[1],
                    Name = i[2],
                    Patronymic = i[3],
                    DataOfEmployment = Convert.ToInt64(i[4]),
                    DateOfDismiss= Convert.ToInt64(i[5]),
                    Salary = Convert.ToInt32(i[6]),
                    Post=i[7]
                });
            });
            return employees;
        }

    }
}
