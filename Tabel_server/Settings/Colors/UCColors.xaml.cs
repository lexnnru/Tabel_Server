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

namespace Tabel_server.Settings.Colors
{
    /// <summary>
    /// Логика взаимодействия для UCColors.xaml
    /// </summary>
    public partial class UCColors : UserControl
    {
        public UCColors()
        {
            InitializeComponent();
            try
            {
                string[] ColorBisznesTripMass = Properties.Settings.Default.ColorBiznesTrip.Split(':');
                CPBiznesTrip.SelectedColor = Color.FromRgb(Convert.ToByte(ColorBisznesTripMass[0]), Convert.ToByte(ColorBisznesTripMass[1]), Convert.ToByte(ColorBisznesTripMass[2]));
                string[] ColorVocationMass = Properties.Settings.Default.ColorVocation.Split(':');
                CPVocation.SelectedColor = Color.FromRgb(Convert.ToByte(ColorVocationMass[0]), Convert.ToByte(ColorVocationMass[1]), Convert.ToByte(ColorVocationMass[2]));
                string[] ColorSickMass = Properties.Settings.Default.ColorSick.Split(':');
                CPSick.SelectedColor = Color.FromRgb(Convert.ToByte(ColorSickMass[0]), Convert.ToByte(ColorSickMass[1]), Convert.ToByte(ColorSickMass[2]));
                string[] ColorAdministrativ = Properties.Settings.Default.ColorAdministrativ.Split(':');
                CPAdministrativ.SelectedColor = Color.FromRgb(Convert.ToByte(ColorAdministrativ[0]), Convert.ToByte(ColorAdministrativ[1]), Convert.ToByte(ColorAdministrativ[2]));
                string[] ColorHoli = Properties.Settings.Default.ColorHoliDay.Split(':');
                CPHoliDay.SelectedColor = Color.FromRgb(Convert.ToByte(ColorHoli[0]), Convert.ToByte(ColorHoli[1]), Convert.ToByte(ColorHoli[2]));
                string[] ColorShortWorked = Properties.Settings.Default.ColorWorkedShortDay.Split(':');
                CPWorkedShortDay.SelectedColor = Color.FromRgb(Convert.ToByte(ColorShortWorked[0]), Convert.ToByte(ColorShortWorked[1]), Convert.ToByte(ColorShortWorked[2]));

            }
            catch { }
        }

        private void CPBiznesTrip_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Properties.Settings.Default.ColorBiznesTrip =
                   String.Join(":", new string[] { CPBiznesTrip.SelectedColor.Value.R.ToString(), CPBiznesTrip.SelectedColor.Value.G.ToString(),
            CPBiznesTrip.SelectedColor.Value.B.ToString()});
            Properties.Settings.Default.Save();
        }

        private void CPVocation_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Properties.Settings.Default.ColorVocation =
       String.Join(":", new string[] { CPVocation.SelectedColor.Value.R.ToString(), CPVocation.SelectedColor.Value.G.ToString(),
            CPVocation.SelectedColor.Value.B.ToString()});
            Properties.Settings.Default.Save();
        }

        private void CPSick_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Properties.Settings.Default.ColorSick =
      String.Join(":", new string[] { CPSick.SelectedColor.Value.R.ToString(), CPSick.SelectedColor.Value.G.ToString(),
            CPSick.SelectedColor.Value.B.ToString()});
            Properties.Settings.Default.Save();
        }

        private void CPAdministrativ_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Properties.Settings.Default.ColorAdministrativ =
                  String.Join(":", new string[] { CPAdministrativ.SelectedColor.Value.R.ToString(), CPAdministrativ.SelectedColor.Value.G.ToString(),
            CPSick.SelectedColor.Value.B.ToString()});
            Properties.Settings.Default.Save();
        }

        private void CPHoliDay_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Properties.Settings.Default.ColorHoliDay =
                String.Join(":", new string[] { CPHoliDay.SelectedColor.Value.R.ToString(), CPHoliDay.SelectedColor.Value.G.ToString(),
            CPHoliDay.SelectedColor.Value.B.ToString()});
            Properties.Settings.Default.Save();
        }

        private void CPWorkedShortDay_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Properties.Settings.Default.ColorWorkedShortDay =
               String.Join(":", new string[] { CPWorkedShortDay.SelectedColor.Value.R.ToString(), CPWorkedShortDay.SelectedColor.Value.G.ToString(),
            CPWorkedShortDay.SelectedColor.Value.B.ToString()});
            Properties.Settings.Default.Save();
        }
    }
}
