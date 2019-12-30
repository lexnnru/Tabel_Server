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

namespace Calendar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Year Year;
        public int year { get; set; }
        TimeSpan daylength;
        public DayType DayTypeSelectedDay { get; set; }
        public event Action<DateTime, DayType, TimeSpan> SetDayType;
        public event Action<int> GetSpecialDays;
        public List<(DateTime, DayType, TimeSpan)>  SpecialDays {get; set;}
        public MainWindow()
        {
            year = DateTime.Now.Year;
            this.DataContext = this;
            InitializeComponent();            
            this.SnapsToDevicePixels = true;
            UseLayoutRounding = true;
            this.Loaded += MainWindow_Loaded;
            GetSpecialDays?.Invoke(year);
            Year = new Year(DateTime.Now, SpecialDays) { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left };
            this.Main.Children.Add(Year);
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public MainWindow(List<(DateTime, DayType, TimeSpan)> specisal_Days)
        {
            InitializeComponent();
            btSetDayLength.IsEnabled = false;
            UpDownSelLengthDay.Visibility = Visibility.Hidden;
            tbInfo1.Visibility = Visibility.Hidden;
            year = DateTime.Now.Year;
            this.DataContext = this;
            this.SnapsToDevicePixels = true;
            UseLayoutRounding = true;
            Year = new Year(DateTime.Now, specisal_Days) { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left };
            this.Main.Children.Add(Year);
        }
        private void Bt_SetDayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btSetDayLength.IsEnabled = true;
            if (cbDayType.SelectedIndex==2)
            {
                UpDownSelLengthDay.Visibility = Visibility.Visible;
                tbInfo1.Visibility = Visibility.Visible;
            }
            else
            {
                UpDownSelLengthDay.Visibility = Visibility.Hidden;
                tbInfo1.Visibility = Visibility.Hidden;
            }
        }

        private void BtSetDayLength_Click(object sender, RoutedEventArgs e)
        {
            if (cbDayType.SelectedIndex == 0)
            { DayTypeSelectedDay = DayType.FreeDay;
                daylength = new TimeSpan(0, 0, 0);
            }
            else if (cbDayType.SelectedIndex == 1)
            { DayTypeSelectedDay = DayType.FullDay;
                daylength = new TimeSpan(8, 12, 0);
            }
            else if (cbDayType.SelectedIndex == 2)
            { DayTypeSelectedDay = DayType.ShortDay;
                daylength = new TimeSpan(0, Convert.ToInt32(UpDownSelLengthDay.Value * 60), 0) ;
            }
            SetDayType?.Invoke(Year.SelectedDay, DayTypeSelectedDay, daylength);
            this.Main.Children.Clear();
            Year = new Year(DateTime.Now, SpecialDays) { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left };
            this.Main.Children.Add(Year);
        }
    }
}
