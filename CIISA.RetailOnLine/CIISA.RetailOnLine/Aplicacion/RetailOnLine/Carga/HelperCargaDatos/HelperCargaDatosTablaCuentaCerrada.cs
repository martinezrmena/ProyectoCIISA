﻿using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.HelperCargaDatos
{
    internal class HelperCargaDatosTablaCuentaCerrada
    {
        internal async Task cargaTablaCuentaCerrada(
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
                HelperCuentaCerrada _helper = new HelperCuentaCerrada();

                StringBuilder _sb = _helper.insertTablaCuentaCerrada(_fila);

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                RenderLabel _renderLabel = new RenderLabel();

                await _renderLabel.paintAdvanceProgress(plabel, _inserciones, pdt);
            }

            plog.registrationNumberInserts(ptextBox, _inserciones, pdt.Rows.Count, ptable);
        }
    }
}
