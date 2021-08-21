using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerMensajeFactura
    {
        public StringBuilder insertTablaMensajeFactura(DataRow pdr)
        {
            HelperMensajeFactura _helper = new HelperMensajeFactura();

            return _helper.insertTablaMensajeFactura(pdr);
        }
    }
}
