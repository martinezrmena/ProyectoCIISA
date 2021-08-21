using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.AuditOnLine.Modelo
{
    internal class Auditoria_ManagerInventario
    {
        internal void buscarInventarioAuditoria(ListView pltvProductos)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarInventarioAuditoria(pltvProductos);
        }

        internal void actualizarInventarioAuditoria(ListView pltvProductos)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarInventarioAuditoria(pltvProductos);
        }
    }
}
