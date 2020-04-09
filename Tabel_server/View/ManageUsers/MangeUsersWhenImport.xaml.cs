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
using Tabel_server.Model;
using Tabel_server.Model.Data;

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для MangeUsers.xaml
    /// </summary>
    public partial class MangeUsersWhenImport : UserControl
    {
        public event Action<Employee> AddNewEmpl;
        public event Action<List<string>> ReImportTabel;
        ObservableCollection<Employee> employees;
        public List<bool> EmployeesNotSavedList;
        public List<string> LinksToTabel;
        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { 
                employees = value;
                EmployeesNotSavedList = new List<bool>();
                foreach (Employee employee in Employees)
                {
                    EmployeesNotSavedList.Add(true); }
            }
        }
        public Employee employee { get; set; }
        long DateOfEmployeement
        { get
            {
                if (dtDateOfEmployeement.SelectedDate == null)
                    return 0;
                else { return dtDateOfEmployeement.SelectedDate.Value.Date.ToUniversalTime().Ticks; }
            }
          set
            {if (value==0)
                { dtDateOfEmployeement.SelectedDate = null; } else { dtDateOfEmployeement.SelectedDate = new DateTime(value).ToLocalTime();  }
            }
        }
        long DateOfDismiss
        {
            get
            {
                if (dtDateOfDismiss.SelectedDate == null)
                    return new DateTime(2199, 1, 1).ToUniversalTime().Ticks;
                else { return dtDateOfDismiss.SelectedDate.Value.Date.ToUniversalTime().Ticks; }
            }
            set
            {
                if (value == new DateTime(2199, 1, 1).ToUniversalTime().Ticks)
                { dtDateOfDismiss.SelectedDate = null; }
                else { dtDateOfDismiss.SelectedDate = new DateTime(value).ToLocalTime(); }
            }
        }
        public MangeUsersWhenImport()
        {
            InitializeComponent();
            DataContext = this;
            dtDateOfEmployeement.SelectedDate = DateTime.Now;
            
           // SetSourceLBUsers();
        }

        public void BtNewEmpl_Click(object sender, RoutedEventArgs e)
        {
            tbFamily.Text = "";
            tbFamily.IsEnabled = true;
            tbName.Text = "";
            tbName.IsEnabled = true;
            tbParentName.Text = "";
            tbParentName.IsEnabled = true;
            tbTabelNumber.Text = "";
            tbTabelNumber.IsEnabled = true;
            dtDateOfEmployeement.Text = "";
            dtDateOfEmployeement.IsEnabled = true;
            dtDateOfDismiss.Text = "";
            dtDateOfDismiss.IsEnabled = false;
            tbSalary.Text = "";
            tbSalary.IsEnabled = true;
            tbPost.Text = "";
            tbPost.IsEnabled = true;
            btChangeEmpl.IsEnabled = false;
            btSave.IsEnabled = false;
            btAddNowEmpl.IsEnabled = true;
            
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            //if (CheckData() == false)
            //{ MessageBox.Show("Не все поля заполнены"); }
            //else
            //{
            //    employee = new Employee()
            //    {
            //        Surname = tbFamily.Text,
            //        Name = tbName.Text,
            //        Patronymic = tbParentName.Text,
            //        Salary = Convert.ToInt32(tbSalary.Text),
            //        Post = tbPost.Text,
            //        TabelNumber = tbTabelNumber.Text
            //    };
            //    ChangeEmpl?.Invoke(employee, new Employee()
            //    {
            //        Surname = tbFamily.Text,
            //        Name = tbName.Text,
            //        Patronymic = tbParentName.Text,
            //        Post = tbPost.Text,
            //        Salary = Convert.ToInt32(tbSalary.Text),
            //        DataOfEmployment = DateOfEmployeement,
            //        DateOfDismiss = DateOfDismiss
            //    });
            //    Setsource?.Invoke();
            //    btSave.IsEnabled = false;
            //    tbTabelNumber.IsEnabled = false;
            //    tbFamily.IsEnabled = false;
            //    tbName.IsEnabled = false;
            //    tbParentName.IsEnabled = false;
            //    dtDateOfEmployeement.IsEnabled = false;
            //    dtDateOfDismiss.IsEnabled = false;
            //    tbSalary.IsEnabled = false;
            //    tbPost.IsEnabled = false;
            //    btSave.IsEnabled = false;
            //    btChangeEmpl.IsEnabled = false;
            //}
        }

        private void TbSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }

        private void TbSalary_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void TbFamily_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if (inp < 'А' || inp > 'я')
                e.Handled = true;
        }

        private void LbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (lbUsers.SelectedIndex==-1)
            { }
            else
            {
                employee = Employees[lbUsers.SelectedIndex];
                tbFamily.Text = employee.Surname;
                tbName.Text = employee.Name;
                tbParentName.Text = employee.Patronymic;
                tbSalary.Text = employee.Salary.ToString();
                tbPost.Text = employee.Post;
               tbTabelNumber.Text = employee.TabelNumber.ToString();
                DateOfEmployeement = employee.DataOfEmployment;
                DateOfDismiss = employee.DateOfDismiss;
                btAddNowEmpl.IsEnabled = true;
                EnableDisbaleTB(true);
                //EnableDisbaleTB(EmployeesNotSavedList[lbUsers.SelectedIndex]);
            }
        }
        private void BtChangeEmpl_Click(object sender, RoutedEventArgs e)
        {

            tbFamily.IsEnabled = true;
            tbName.IsEnabled = true;
            tbParentName.IsEnabled = true;
            dtDateOfDismiss.IsEnabled = true;
            tbSalary.IsEnabled = true;
            tbPost.IsEnabled = true;
            btSave.IsEnabled = true;
            btChangeEmpl.IsEnabled = false;
            
        }

        private void BtAddNowEmpl_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData()==false)
            { MessageBox.Show("Не все поля заполнены"); }
            else 
            {
                employee = new Employee()
                {
                    Surname = tbFamily.Text,
                    Name = tbName.Text,
                    Patronymic = tbParentName.Text,
                    Salary = Convert.ToInt32(tbSalary.Text),
                    DataOfEmployment = DateOfEmployeement,
                    Post = tbPost.Text,
                    TabelNumber = tbTabelNumber.Text,
                };
                AddNewEmpl?.Invoke(employee);
                //Loger.SetLog("Сотрудник "+ Employees[lbUsers.SelectedIndex] +" добавлен", true);
                Employees.Remove(Employees[lbUsers.SelectedIndex]);
                tbFamily.Text = "";
                tbName.Text = "";
                tbParentName.Text = "";
                tbSalary.Text = "0";
                dtDateOfEmployeement.SelectedDate = null;
                tbPost.Text = "";
                tbTabelNumber.Text = "";
                btAddNowEmpl.IsEnabled = false;
                EnableDisbaleTB(false);
                
                if (Employees.Count == 0)
                {
                    Loger.SetLog("Все сотрудники успешно добавлены. Запуск повторного импорта табелей", true);
                    ReImportTabel?.Invoke(LinksToTabel);
                }

               
                // Setsource?.Invoke();
                //EmployeesNotSavedList[lbUsers.SelectedIndex] = false;
            }
            
        }
        public bool CheckData()
        {
            if (tbFamily.Text == "" || tbName.Text == "" || tbParentName.Text == "" || DateOfEmployeement == 0 || tbTabelNumber.Text == "" || tbPost.Text == ""
                   || tbSalary.Text == "")
            { return false; }
            else return true;
        }
        public void EnableDisbaleTB(bool enable)
        { if (enable == true)
            { tbFamily.IsEnabled = true;
                tbName.IsEnabled = true;
                tbParentName.IsEnabled = true;
                tbPost.IsEnabled = true;
                tbSalary.IsEnabled = true;
                dtDateOfEmployeement.IsEnabled = true;
                btAddNowEmpl.IsEnabled = true;
            }
        else
            {
                tbFamily.IsEnabled = false;
                tbName.IsEnabled = false;
                tbParentName.IsEnabled = false;
                tbPost.IsEnabled = false;
                tbSalary.IsEnabled = false;
                dtDateOfEmployeement.IsEnabled = false;
                btAddNowEmpl.IsEnabled = false;
            }
        }

        private void LbUsers_GotFocus(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedIndex == -1)
            { }
            else
            {
                employee = Employees[lbUsers.SelectedIndex];
                tbFamily.Text = employee.Surname;
                tbName.Text = employee.Name;
                tbParentName.Text = employee.Patronymic;
                tbSalary.Text = employee.Salary.ToString();
                tbPost.Text = employee.Post;
                tbTabelNumber.Text = employee.TabelNumber.ToString();
                DateOfEmployeement = employee.DataOfEmployment;
                DateOfDismiss = employee.DateOfDismiss;
            }

        }

        private void lbUsers_LostFocus(object sender, RoutedEventArgs e)
        {
            int a = lbUsers.SelectedIndex;
        }
    }
}
