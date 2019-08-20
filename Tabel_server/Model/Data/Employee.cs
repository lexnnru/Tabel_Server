using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    public class Employee
    {
        
        public override string ToString()
        {
            return family + " " + name + " " + parentName;
        }
        public void GetEmployee(Model.DataBase_manager db, List<string> znachenie)
        {
            List<string> param=new List<string>() { family, name, parentName};
            List<string> list=db.GetRowFromTable(db.NameOfTablenamberUserTable, param, znachenie);
            tabelNumber=list[0];
            family = list[1];
            name = list[2];
            parentName = list[3];
            mail = list[4];
        }
        public List<Employee> GetAllEmployees(Model.DataBase_manager db)  
        {
            List<Employee> employees=new List<Employee>();
            List<List<string>> Lists = db.GetAllRowFromTable(db.NameOfTablenamberUserTable);
            Lists.ForEach(i => { employees.Add(new Employee() {tabelNumber=i[0], family = i[1], name = i[2],
                 parentName = i[3], mail = i[4]}); });
            return employees;
        }
        public List<Employee> GetAllEmployees(Model.DataBase_manager db, DateTime date)
        {
            List<Employee> employees = new List<Employee>();
            List<List<string>> Lists = db.GetAllRowFromTable(db.NameOfTablenamberUserTable);
            Lists.ForEach(i => {
                employees.Add(new Employee()
                {
                    tabelNumber = i[0],
                    family = i[1],
                    name = i[2],
                    parentName = i[3],
                    dataOfEmployment = Convert.ToInt64(i[4]),
                    dateOfDismiss= Convert.ToInt64(i[5]),
                    salary = Convert.ToInt32(i[6]),
                    post=i[7],


                });
            });
            return employees;
        }
        public string tabelNumber {get; set;}
        public string family { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public string mail { get; set; }
        public Int64 dataOfEmployment { get; set; }
        public Int64 dateOfDismiss { get; set; }
        public int salary { get; set; }
        public string post { get; set; }
    }
}
