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
        public MainWindow()
        {
            InitializeComponent();
            this.SnapsToDevicePixels = true;
            UseLayoutRounding = true;
            this.Main.Children.Add(new Year(DateTime.Now, GetSpecialDays(DateTime.Now.Year))
            { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center });
        }

        List<(DateTime, DayType)> GetSpecialDays(int year)
        {
            List<(DateTime, DayType)> SpecialDays = new List<(DateTime, DayType)>();
            switch (year)
            {
                case 2019:
                    SpecialDays.Add((new DateTime(2019, 1, 1), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 1, 2), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 1, 3), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 1, 4), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 1, 7), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 1, 8), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 2, 22), DayType.VeryShortDay));
                    SpecialDays.Add((new DateTime(2019, 3, 7), DayType.ShortDay));
                    SpecialDays.Add((new DateTime(2019, 3, 8), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 4, 30), DayType.ShortDay));
                    SpecialDays.Add((new DateTime(2019, 5, 1), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 5, 2), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 5, 3), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 5, 8), DayType.ShortDay));
                    SpecialDays.Add((new DateTime(2019, 5, 9), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 5, 10), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 6, 11), DayType.ShortDay));
                    SpecialDays.Add((new DateTime(2019, 6, 12), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 11, 4), DayType.FreeDay));
                    SpecialDays.Add((new DateTime(2019, 12, 31), DayType.VeryShortDay));
                    break;
            }
            return SpecialDays;
        }
    }
}
