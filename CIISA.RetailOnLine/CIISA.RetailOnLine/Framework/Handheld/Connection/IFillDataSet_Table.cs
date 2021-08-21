using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.Connection
{
    public interface IFillDataSet_Table
    {
        void FillTable(DataSet pds, DataTable tabla);
        DataTable DataTable_Format_EncDoc(DataTable tabla);
        DataTable DataTable_Format(DataTable tabla);
        DataTable DataTable_FormatBitacora(pnlBitacoraModel datos);
        bool ExistsCampo(DataTable tabla, string campo, string column);
    }
}
