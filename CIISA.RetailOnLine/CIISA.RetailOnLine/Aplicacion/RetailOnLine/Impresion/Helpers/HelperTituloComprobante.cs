using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperTituloComprobante
    { 

        internal void buscarTituloFacturaPorTipoDocumento(string pcodTipoDocumento, List<string> pprintingLinesList)
        {
            StringBuilder _sb = new StringBuilder();
            Line line = new Line();

            _sb.Append("SELECT ");
            _sb.Append(TableTituloComprobante._NO_CIA + ", ");
            _sb.Append(TableTituloComprobante._TIPO_DOC + ", ");
            _sb.Append(TableTituloComprobante._TITULO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tituloComprobante + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableTituloComprobante._TIPO_DOC + " = ");
            _sb.Append("'" + pcodTipoDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int i = 1;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineasImpresion = string.Empty;
                _lineasImpresion += _position.center(_fila[TableTituloComprobante._TITULO].ToString().Length);
                _lineasImpresion += _fila[TableTituloComprobante._TITULO].ToString();
                //_lineasImpresion += "\\&";
                _lineasImpresion += Environment.NewLine;
                
                if ((pcodTipoDocumento.Equals(ROLTransactions._facturaCreditoSigla) ||
                    pcodTipoDocumento.Equals(ROLTransactions._facturaContadoSigla) ||
                    pcodTipoDocumento.Equals(ROLTransactions._devolucionSigla)) &&
                    _dt.Rows.Count == i)
                {
                    line.printLineSpace(pprintingLinesList, 1);

                    pprintingLinesList.Add(_lineasImpresion);
                }
                else {

                    pprintingLinesList.Add(_lineasImpresion);
                }

                i++;
            }
        }
    }
}
