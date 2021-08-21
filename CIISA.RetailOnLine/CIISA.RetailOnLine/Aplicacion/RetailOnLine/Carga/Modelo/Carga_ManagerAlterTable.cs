using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo
{
    public class Carga_ManagerAlterTable
    {

        public Carga_ManagerAlterTable(string Table)
        {
            if (Table.Equals(TablesROL._factura))
            {
                AlterTablaFactura();
            }
        }

        public void AlterTablaFactura()
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.AlterTable(
                "altertable_factura.sql",
                TablesROL._factura
                );
        }

    }
}
