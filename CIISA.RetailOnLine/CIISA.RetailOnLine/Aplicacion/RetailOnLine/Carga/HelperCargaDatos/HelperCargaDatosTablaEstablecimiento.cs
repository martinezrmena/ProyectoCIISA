using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
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
    internal class HelperCargaDatosTablaEstablecimiento
    {
        private async Task<int> cargaTablaEstablecimiento(
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
                HelperEstablecimiento _helper = new HelperEstablecimiento();

                StringBuilder _sb = _helper.insertTablaEstablecimiento(_fila);

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                RenderLabel _renderLabel = new RenderLabel();

                await _renderLabel.paintAdvanceProgress(plabel, _inserciones, pdt);
            }

            return _inserciones;
        }

        internal async Task cargaTablaEstablecimientoTodos(
            //BacklightSymbol pbacklight,
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            int _inserciones = await cargaTablaEstablecimiento(
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

        internal async Task cargaTablaEstablecimientoFacturacionFaltantes(
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
                string _codEstablecimiento = _fila["NO_ESTABLECIMIENTO"].ToString();

                Logica_ManagerEstablecimiento _manager = new Logica_ManagerEstablecimiento();

                _existe = _manager.ExisteEstablecimiento(_codEstablecimiento);

                //MessageBox.Show("existe establecimiento = " + _existe);

                LogMessageAttention lma = new LogMessageAttention();

                await lma.generalAttention("existe establecimiento = " + _existe);
            }

            int _inserciones = Numeric._zeroInteger;

            if (!_existe)
            {
                _inserciones = await cargaTablaEstablecimiento(
                    //pbacklight, 
                    ptable, pdt, ptextBox, plabel, plog);
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
    }
}
