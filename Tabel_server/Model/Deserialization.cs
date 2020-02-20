using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Tabel_server.Model.Data;

namespace Tabel_server.Model
{
    /// <summary>
    ///Считывает
    /// </summary>
    class Deserialization
        
    {

        public List<IncomingDataTable> GetOneTabelData (string PathToFile, out string tablenumber)
          
            
        {
            IncomingDataTable odd = new IncomingDataTable();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = System.IO.File.ReadAllText(PathToFile);
                List<IncomingDataTable> Rows = serializer.Deserialize<List<IncomingDataTable>>(json);
                Rows.ForEach((j) => { j.daynumber = j.daynumber.ToLocalTime(); j.startday = j.startday.ToLocalTime(); j.endday = j.endday.ToLocalTime(); });
            tablenumber = Rows[0].tabelNumber;
            return Rows;
        }
    }
}

