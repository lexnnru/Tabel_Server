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
    /// Логика взаимодействия для UCSettings.xaml
    /// </summary>
    public partial class UCSettings : UserControl
    {
        public UCSettings()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tabel_server.Settings.Colors.UCColors uCColors = new Settings.Colors.UCColors();
            GridForUC.Children.Clear();
            GridForUC.Children.Add(uCColors);
        }
    }
}
