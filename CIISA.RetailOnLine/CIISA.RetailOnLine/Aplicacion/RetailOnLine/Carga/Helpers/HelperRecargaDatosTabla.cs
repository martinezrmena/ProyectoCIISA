using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperRecargaDatosTabla
    {

        public void recargaTablaInventario(string ptable, DataTable pdt, Editor ptextBox, Log plog)
        {
            int _actualizaciones = Numeric._zeroInteger;
            int _inserciones = Numeric._zeroInteger;

            foreach (DataRow _fila in pdt.Rows)
            {
                Logica_ManagerInventario _managerInventario = new Logica_ManagerInventario();

                bool _existeProducto = _managerInventario.ExisteProducto(_fila["NO_ARTI"].ToString());

                HelperInventario _helper = new HelperInventario();

                StringBuilder _sb = new StringBuilder();

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                if (_existeProducto)
                {
                    _sb = _helper.updateTablaInventario(_fila);                    

                    _actualizaciones += MultiGeneric.uploadGenericTable(_sb);
                }
                else
                {
                    _sb = _helper.insertTablaInventario(_fila);

                    _inserciones += MultiGeneric.uploadGenericTable(_sb);
                }

                _managerInventario.calcularCantidadDisponible(_fila["NO_ARTI"].ToString());

            }

            int _total = _actualizaciones + _inserciones;

            plog.registrationNumberInserts(ptextBox, _total, pdt.Rows.Count, ptable);

        }

    }
}
