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
using System.Windows.Shapes;
using Tabel_server.Model.Data;

namespace Tabel_server.PreveiwWindow
{
    /// <summary>
    /// Логика взаимодействия для PreveiwTableForUpdate.xaml
    /// </summary>
    public partial class PreveiwTableForUpdate : Window
    {
        public ObservableCollection<IncomingDataTable> incomingDatas { get; set; }
        public ObservableCollection<IncomingDataTable> rowinDB { get; set; }
        public PreveiwTableForUpdate()
        {
            InitializeComponent();
            this.DataContext = this;
            incomingDatas = new ObservableCollection<IncomingDataTable>() ;
            rowinDB = new ObservableCollection<IncomingDataTable>();

        }
        public void showTable(List<IncomingDataTable> idd, List<IncomingDataTable> rowinDB)
        {          
            idd.ForEach(x=> {
                
                incomingDatas.Add(x); });
            rowinDB.ForEach(x => { this.rowinDB.Add(x); });
           // this.Close();
        }

        private void BtAddChanges_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
