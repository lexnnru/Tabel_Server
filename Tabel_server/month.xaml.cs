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
    /// Логика взаимодействия для month.xaml
    /// </summary>
    public partial class month : UserControl
    {
        public event Action<int> Click;
        public string nameMonth { set { NameMonth.Text = value; } } 
        public int numberMonth { get; set; }
        
       
        public bool SelectTriger { set { if (value) { Border.Background = Brushes.Red; } else { Border.Background = Brushes.White; } } }
        public month()
        {
            InitializeComponent();
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(numberMonth);
            SelectTriger = true;
        }
    }
}
