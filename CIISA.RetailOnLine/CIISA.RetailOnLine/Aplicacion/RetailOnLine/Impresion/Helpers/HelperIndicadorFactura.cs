using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperIndicadorFactura
    {
        internal void buscarLineasIndicadorFactura(List<string> pprintingLinesList)
        {
            #region REPORTES: Indicadores facturación
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableIndicadorFactura._NO_CLIENTE + ", ");
            _sb.Append(TableIndicadorFactura._IND_PED + ", ");
            _sb.Append(TableIndicadorFactura._IND_FACCONT + ", ");
            _sb.Append(TableIndicadorFactura._IND_FACCRED + ", ");
            _sb.Append(TableIndicadorFactura._IND_RESPETA_LIMITE + ", ");
            _sb.Append(TableIndicadorFactura._IND_CHEQUE + ", ");
            _sb.Append(TableIndicadorFactura._MONTO_LIMITE + ", ");
            _sb.Append(TableIndicadorFactura._IND_VENCIMIENTO + ", ");
            _sb.Append(TableIndicadorFactura._IND_ESTADO + ", ");
            _sb.Append(TableIndicadorFactura._NO_AGENTE + ", ");
            _sb.Append(TableIndicadorFactura._COBRADOR + ", ");
            _sb.Append(TableIndicadorFactura._IND_COBRO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._indicadorFactura);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.noCliente);
                _lineaUno += _fila[TableIndicadorFactura._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.pedido);
                _lineaUno += _fila[TableIndicadorFactura._IND_PED].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.facturaContado);
                _lineaUno += _fila[TableIndicadorFactura._IND_FACCONT].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.facturaCredito);
                _lineaUno += _fila[TableIndicadorFactura._IND_FACCRED].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.respetaLimite);
                _lineaUno += _fila[TableIndicadorFactura._IND_RESPETA_LIMITE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.cheque);
                _lineaUno += _fila[TableIndicadorFactura._IND_CHEQUE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.montoLimite);
                _lineaUno += _fila[TableIndicadorFactura._MONTO_LIMITE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.vencimiento);
                _lineaUno += _fila[TableIndicadorFactura._IND_VENCIMIENTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.estado);
                _lineaUno += _fila[TableIndicadorFactura._IND_ESTADO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.cobrador);
                _lineaUno += _fila[TableIndicadorFactura._COBRADOR].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.cobro);
                _lineaUno += _fila[TableIndicadorFactura._IND_COBRO].ToString();

                pprintingLinesList.Add(_lineaUno + Environment.NewLine);
                pprintingLinesList.Add(Environment.NewLine);
            }

            pprintingLinesList.Add(
                "No. Líneas: " +
                _dt.Rows.Count +
                Environment.NewLine
                );
            #endregion
        }
    }
}
