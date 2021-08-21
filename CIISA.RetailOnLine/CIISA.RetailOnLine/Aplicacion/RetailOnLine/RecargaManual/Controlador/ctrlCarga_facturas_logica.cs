using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador
{
    public class ctrlCarga_facturas_logica
    {
        private void buscarFacturasExistentesHandheld(
            DataTable pdtFacturasHH,
            DataRow pfilaDescargada,
            List<DataRow> prowsToDelete,
            string pnoFisicoDescargado,
            string pclienteDescargado
            )
        {
            foreach (DataRow _fila in pdtFacturasHH.Rows)
            {
                string _noFisicoHH = _fila[TableFactura._NO_FISICO].ToString();
                string _clienteHH = _fila[TableFactura._NO_CLIENTE].ToString();

                if (pnoFisicoDescargado.Equals(_noFisicoHH))
                {
                    if (pclienteDescargado.Equals(_clienteHH))
                    {
                        prowsToDelete.Add(pfilaDescargada);
                    }
                }
            }
        }

        private void removerFilasDuplicadas(DataTable pdtDescargado,List<DataRow> pfilasBorrar)
        {
            foreach (DataRow row in pfilasBorrar)
            {
                pdtDescargado.Rows.Remove(row);
            }
        }

        public void eliminarFacturasYaTransmitidas(DataTable pdtDescargado)
        {
            Carga_ManagerFactura _manager = new Carga_ManagerFactura();

            DataTable _dtFacturasHH = _manager.buscarFacturas();

            List<DataRow> _filasBorrar = new List<DataRow>();

            foreach (DataRow _filaDescargada in pdtDescargado.Rows)
            {
                string _noFisicoDescargado = _filaDescargada[TableFacturaNAF.NO_FISICO].ToString();
                string _clienteDescargado = _filaDescargada[TableFacturaNAF.NO_CLIENTE].ToString();

                buscarFacturasExistentesHandheld(
                    _dtFacturasHH,
                    _filaDescargada,
                    _filasBorrar,
                    _noFisicoDescargado,
                    _clienteDescargado
                    );
            }

            removerFilasDuplicadas(pdtDescargado, _filasBorrar);
        }
    }
}
