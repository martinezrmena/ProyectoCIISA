using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDescuento
    {
        public decimal obtenerDescuento(string pcodCliente, string pcodProducto, decimal pcantidad)
        {
            HelperDescuento _helper = new HelperDescuento();

            return _helper.obtenerDescuento(pcodCliente, pcodProducto, pcantidad);
        }

        public string obtenerTipoDescuento(string pcodCliente, string pcodProducto)
        {
            HelperDescuento _helper = new HelperDescuento();

            string value = _helper.obtenerTipoDescuento(pcodCliente, pcodProducto);

            if (string.IsNullOrEmpty(value))
            {
                value = TipoDescuento._NotExists;
            }

            return value;
        }

        public string obtenerFechaInicio(string pcodCliente, string pcodProducto)
        {
            HelperDescuento _helper = new HelperDescuento();

            return _helper.obtenerFechaInicio(pcodCliente, pcodProducto);
        }

        public string getDateExpires(string pcodCliente, string pcodProducto)
        {
            HelperDescuento _helper = new HelperDescuento();

            return _helper.getDateExpires(pcodCliente, pcodProducto);
        }
    }
}
