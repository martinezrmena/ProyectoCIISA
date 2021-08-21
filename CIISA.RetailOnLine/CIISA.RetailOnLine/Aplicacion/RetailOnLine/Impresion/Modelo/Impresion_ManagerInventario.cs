using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerInventario
    {
        internal void buscarLineasVentasPorProducto(List<string> pprintingLinesList)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarLineasVentasPorProducto(pprintingLinesList);
        }

        internal void buscarLineasInventarioTeorico(List<string> pprintingLinesList)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarLineasInventarioTeorico(pprintingLinesList);
        }

        internal void buscarLineasInventarioAuditoria(List<string> pprintingLinesList)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarLineasInventarioAuditoria(pprintingLinesList);
        }

        internal void buscarLineasInventarioConExistencias(List<string> pprintingLinesList)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarLineasInventarioConExistencias(pprintingLinesList);
        }

        internal void buscarLineasInventarioTomaFisica(List<string> pprintingLinesList)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarLineasInventarioTomaFisica(pprintingLinesList);
        }
    }
}
