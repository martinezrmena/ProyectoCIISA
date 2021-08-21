using CIISA.RetailOnLine.Aplicacion.InventoryOnLine.Helpers;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.InventoryOnLine.Modelo
{
    class Inventario_ManagerInventario
    {
        internal void actualizarInventarioTomaFisica(ListView ppnlTomaFisica_ltvInventario)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarInventarioTomaFisica(ppnlTomaFisica_ltvInventario);
        }

        internal void buscarInventarioTomaFisica(ListView ppnlTomaFisica_ltvInventario)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarInventarioTomaFisica(ppnlTomaFisica_ltvInventario);
        }

        internal void actualizarConsolidado()
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarConsolidado();
        }

    }

}

