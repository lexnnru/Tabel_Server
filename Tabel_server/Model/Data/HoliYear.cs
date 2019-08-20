using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    class HoliYear
    {
        public List<DateTime> HoliYea;
        public List<DateTime> HoliWorkYea;
        public int year;
        public HoliYear(int year)
        {
            DataBase_manager db = new DataBase_manager("TabelDB");
            HoliYea = db.GetHoliYear(DateTime.Now.Year);
            HoliWorkYea = db.GetWorkHoliYear(DateTime.Now.Year);
        }
        public HoliYear()
        { }
    }
}
