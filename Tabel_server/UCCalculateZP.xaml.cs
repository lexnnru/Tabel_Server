using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для UCCalculateZP.xaml
    /// </summary>
    public partial class UCCalculateZP : UserControl
    {
        //public ObservableCollection<MonthZP> MonthZP { get; set; }
        public ObservableCollection<MonthEmployee> MonthEmployees { get; set; }
        public UCCalculateZP(ObservableCollection<MonthEmployee> monthEmployees)
        {
            InitializeComponent();
            MonthEmployees = monthEmployees;
            //MonthZP = new ObservableCollection<MonthZP>();
            for (int i=0; i< monthEmployees.Count; i++)
            {
                if (monthEmployees[i].MonthZP==null)
                {
                    monthEmployees[i].MonthZP = new MonthZP(monthEmployees[i]);
                    
                   // MonthZP.Add(new MonthZP(monthEmployees[i])); ;
                }
                monthEmployees[i].MonthZP.CalculateZP();
            }
            DataContext = this;
            if (MonthEmployees.Count == 0)
            { btSave.IsEnabled = false; }
            else
            {
                if (MonthEmployees[0].Days[0].DayOnPlan.Day < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)-TimeSpan.FromDays(62) ||
                      DateTime.Now< MonthEmployees[0].Days[0].DayOnPlan.Day)
                { btSave.IsEnabled = false; }
            }
          
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (MonthEmployee monthEmployee in MonthEmployees)
            { monthEmployee.SaveStart = true;
                monthEmployee.MonthZP.SavedDate = DateTime.Now.ToString();
            }

           
        }
    }
}

