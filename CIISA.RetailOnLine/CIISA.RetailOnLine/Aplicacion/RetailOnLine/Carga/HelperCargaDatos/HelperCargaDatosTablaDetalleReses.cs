﻿using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
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
    public class HelperCargaDatosTablaDetalleReses
    {
        internal async Task cargaTablaDetalleReses(
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
                HelperDetalleReses _helper = new HelperDetalleReses();

                StringBuilder _sb = _helper.insertTablaDetalleReses(_fila);

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                RenderLabel _renderLabel = new RenderLabel();

                await _renderLabel.paintAdvanceProgress(plabel, _inserciones, pdt);
            }

            plog.registrationNumberInserts(ptextBox, _inserciones, pdt.Rows.Count, ptable);
        }


    }
}
