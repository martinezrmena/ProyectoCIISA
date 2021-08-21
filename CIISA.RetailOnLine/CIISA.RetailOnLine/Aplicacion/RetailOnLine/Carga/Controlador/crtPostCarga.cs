using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador
{
    public class crtPostCarga
    {
        public void BorradoTablasPost_aperturaNoctura()
        {
            OperationSQL.deleteTableFeedbackTextBox(TablesROL._inventario, new Editor(), new Log());

            OperationSQL.deleteTableFeedbackTextBox(TablesROL._encabezadoPedido, new Editor(), new Log());

            OperationSQL.deleteTableFeedbackTextBox(TablesROL._detallePedido, new Editor(), new Log());
        }

        public void BorradoTablasPost_cargaDeCero()
        {
            OperationSQL.deleteTableFeedbackTextBox(TablesROL._inventario, new Editor(), new Log());

            OperationSQL.deleteTableFeedbackTextBox(TablesROL._encabezadoPedido, new Editor(), new Log());

            OperationSQL.deleteTableFeedbackTextBox(TablesROL._detallePedido, new Editor(), new Log());
        }

        public bool EncabezadoPedido_Vacio()
        {
            Logica_ManagerEncabezadoPedido _managerEP = new Logica_ManagerEncabezadoPedido();

            if (_managerEP.EncabezadoPedido_Vacio())
            {
                Logica_ManagerDetallePedido _managerDP = new Logica_ManagerDetallePedido();

                if (_managerDP.DetallePedido_Vacio())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
