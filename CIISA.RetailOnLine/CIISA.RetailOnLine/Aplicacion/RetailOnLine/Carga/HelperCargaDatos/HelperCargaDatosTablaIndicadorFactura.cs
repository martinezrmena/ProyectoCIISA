using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.HelperCargaDatos
{
    internal class HelperCargaDatosTablaIndicadorFactura
    {
        private async Task<int> cargaTablaIndicadorFactura(
            //BacklightSymbol pbacklight,
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            int _inserciones = Numeric._zeroInteger;

            foreach (DataRow _fila in pdt.Rows)
            {
                HelperIndicadorFactura _helper = new HelperIndicadorFactura();

                StringBuilder _sb = _helper.insertTablaIndicadorFactura(_fila);

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                RenderLabel _renderLabel = new RenderLabel();

                await _renderLabel.paintAdvanceProgress(plabel, _inserciones, pdt);
            }

            return _inserciones;
        }

        internal async Task cargaTablaIndicadorFacturaFacturacionFaltantes(
            //BacklightSymbol pbacklight,
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            bool _existe = false;

            foreach (DataRow _fila in pdt.Rows)
            {
                string _codCliente = _fila["NO_CLIENTE"].ToString();

                string _codEstableciento = _fila["NO_ESTABLECIMIENTO"].ToString();

                Logica_ManagerIndicador _manager = new Logica_ManagerIndicador();

                _existe = _manager.ExisteIndicadoresCliente(_codCliente, _codEstableciento);
            }

            int _inserciones = Numeric._zeroInteger;

            if (!_existe)
            {
                _inserciones = await cargaTablaIndicadorFactura(
                    //pbacklight, 
                    ptable, 
                    pdt, 
                    ptextBox, 
                    plabel, 
                    plog);
            }
            else
            {
                _inserciones = 1;
            }

            plog.registrationNumberInserts(
                ptextBox,
                _inserciones,
                pdt.Rows.Count,
                ptable + SubjectTagEmail._facturacionFaltantes
                );

        }

        internal async Task cargaTablaIndicadorFacturaTodos(
            //BacklightSymbol pbacklight,
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            int _inserciones = await cargaTablaIndicadorFactura(
                //pbacklight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);

            plog.registrationNumberInserts(
                ptextBox,
                _inserciones,
                pdt.Rows.Count,
                ptable
                );
        }
    }
}
