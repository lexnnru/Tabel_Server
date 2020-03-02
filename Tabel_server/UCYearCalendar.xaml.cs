using System;
using System.Collections.Generic;
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

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для UserControl4.xaml
    /// </summary>
    public partial class UCYearCalendar : UserControl
    {
        DateTime data;
        public DateTime Data { 
            get { return data; }
            set { data = value; tbYear.Text = Data.Year.ToString(); SelectedDateChanged?.Invoke(value); } }
        List<month> userControl4s;
        public event Action<DateTime> SelectedDateChanged;
        public UCYearCalendar( )
        {
            InitializeComponent();
            Data = DateTime.Now; 
            userControl4s = new List<month>() { month1, month2, month3, month4, month5, month6, month7, month8, month9, month10, month11, month12 };
            userControl4s[Data.Month-1].SelectTriger = true;
        }
        private void Month_Click(int obj)
        {
            for (int i=0; i< userControl4s.Count; i++)
            { userControl4s[i].SelectTriger = false; }
            Data = new DateTime(Convert.ToInt32(tbYear.Text), obj, 1);
            SelectedDateChanged?.Invoke(Data);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbYear.Text=(Convert.ToInt32(tbYear.Text)-1).ToString();
            for (int i = 0; i < userControl4s.Count; i++)
            { userControl4s[i].SelectTriger = false; }
            if (tbYear.Text==Data.Year.ToString())
            { userControl4s[Data.Month - 1].SelectTriger = true; }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tbYear.Text = (Convert.ToInt32(tbYear.Text) + 1).ToString();
            for (int i = 0; i < userControl4s.Count; i++)
            { userControl4s[i].SelectTriger = false; }
            if (tbYear.Text == Data.Year.ToString())
            { userControl4s[Data.Month - 1].SelectTriger = true; }
        }
    }
}
