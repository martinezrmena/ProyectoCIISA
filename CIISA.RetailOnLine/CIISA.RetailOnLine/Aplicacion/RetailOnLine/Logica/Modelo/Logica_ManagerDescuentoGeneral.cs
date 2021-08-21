using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDescuentoGeneral
    {
        public decimal obtenerDescuentoGeneral(string pcodCliente, string pcodProducto, decimal pcantidad)
        {
            HelperDescuentoGeneral _helper = new HelperDescuentoGeneral();

            return _helper.obtenerDescuentoGeneral(pcodCliente, pcodProducto, pcantidad);
        }

        public string obtenerTipoDescuentoGeneral(string pcodCliente, string pcodProducto)
        {
            HelperDescuentoGeneral _helper = new HelperDescuentoGeneral();

            string value = _helper.obtenerTipoDescuentoGeneral(pcodCliente, pcodProducto);

            if (string.IsNullOrEmpty(value))
            {
                value = TipoDescuento._NotExists;
            }

            return value;
        }
    }
}
