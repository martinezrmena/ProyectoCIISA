using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperInventario
    {        
        internal void buscarLineasVentasPorProducto(List<string> pprintingLinesList)
        {
            #region REPORTE: VENTAS POR PRODUCTO
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._VENTAS + ", ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                decimal _ventas = Numeric._zeroDecimalInitialize;

                _lineaUno += _position.tabular(_lineaUno.Length, RepVentas.codigo);
                _lineaUno += _fila[TableInventario._CODPRODUCTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepVentas.descripcion);

                if (_fila[TableProducto._DESCPRODUCTO].ToString().Length > 31)
                {
                    _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString().Substring(0, 31);

                }
                else
                {
                    _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                }

                _lineaUno += _position.tabular(_lineaUno.Length, RepVentas.ventas);
                _ventas = FormatUtil.convertStringToDecimal(_fila["VENTAS"].ToString());

                _lineaUno += FormatUtil.applyCurrencyFormat(_ventas);

                pprintingLinesList.Add(_lineaUno + Environment.NewLine);
                pprintingLinesList.Add(Environment.NewLine);
            }

            pprintingLinesList.Add(
                "No. Líneas: " +
                _dt.Rows.Count +
                Environment.NewLine);
            #endregion
        }

        internal void buscarLineasInventarioTeorico(List<string> pprintingLinesList)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._CODCIA + ", ");
            _sb.Append("I." + TableInventario._CODAGENTE + ", ");
            _sb.Append("I." + TableInventario._FECHATOMA + ", ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("I." + TableInventario._CANTIDAD + ", ");
            _sb.Append("I." + TableInventario._VENTAS + ", ");
            _sb.Append("I." + TableInventario._DEVOLUCIONESBUENAS + ", ");
            _sb.Append("I." + TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append("I." + TableInventario._REGALIAS + ", ");
            _sb.Append("I." + TableInventario._ANULACIONES + ", ");
            _sb.Append("I." + TableInventario._ANULACIONESBUENAS + ", ");
            _sb.Append("I." + TableInventario._ANULACIONESMALAS + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _cantLineas = 0;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                string _lineaDos = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.codigo);
                _lineaUno += _fila[TableInventario._CODPRODUCTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.descripcion);
                _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                _lineaUno += Environment.NewLine;

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.cantidad);
                decimal _cantidad = FormatUtil.convertStringToDecimal(_fila[TableInventario._CANTIDAD].ToString());

                _lineaDos += decimal.Parse(_fila[TableInventario._CANTIDAD].ToString()).ToString("N2");

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.ventas);
                decimal _ventas = FormatUtil.convertStringToDecimal(_fila[TableInventario._VENTAS].ToString());

                decimal _an = FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONES].ToString());

                decimal _ab = FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONESBUENAS].ToString());

                _ventas = _ventas - _ab;

                if (_ventas <= 0)
                {
                    _ventas = 0;
                }

                _lineaDos += FormatUtil.applyCurrencyFormat(_ventas);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.devoluciones);
                decimal _db = FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESBUENAS].ToString());

                decimal _dm = FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESMALAS].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_db + _dm);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.regalias);
                _lineaDos += _fila[TableInventario._REGALIAS].ToString();

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.anulaciones);
                decimal _am = FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONESMALAS].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_an + _ab + _am);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.disponible);
                _lineaDos += decimal.Parse(_fila[TableInventario._DISPONIBLE].ToString()).ToString("N2");

                _lineaDos += Environment.NewLine;

                _cantLineas++;

                pprintingLinesList.Add(_lineaUno);
                pprintingLinesList.Add(_lineaDos);
                pprintingLinesList.Add(Environment.NewLine);
            }

            pprintingLinesList.Add(Environment.NewLine +
                "No. Líneas: " +
                _cantLineas +
                Environment.NewLine);
        }

        internal void buscarLineasInventarioAuditoria(List<string> pprintingLinesList)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("I." + TableInventario._CANTIDAD + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + ", ");
            _sb.Append("I." + TableInventario._AUDITORIA + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _cantLineas = 0;

            foreach (DataRow _fila in _dt.Rows)
            {
                string _lineaUno = string.Empty;
                string _lineaDos = string.Empty;

                Position _position = new Position();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioAuditoria.codigo);
                _lineaUno += _fila[TableInventario._CODPRODUCTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioAuditoria.descripcion);
                _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                _lineaUno += Environment.NewLine;

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.cantidad);
                decimal _cantidad = FormatUtil.convertStringToDecimal(_fila[TableInventario._CANTIDAD].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_cantidad);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.disponible);

                decimal _disponible = FormatUtil.convertStringToDecimal(_fila[TableInventario._DISPONIBLE].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_disponible);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.auditado);

                decimal _auditoria = FormatUtil.convertStringToDecimal(_fila[TableInventario._AUDITORIA].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_auditoria);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.diferencia);

                _lineaDos += FormatUtil.applyCurrencyFormat(_auditoria - _disponible);

                _lineaDos += Environment.NewLine;

                _cantLineas++;

                pprintingLinesList.Add(_lineaUno);
                pprintingLinesList.Add(_lineaDos);
                pprintingLinesList.Add(Environment.NewLine);
            }

            pprintingLinesList.Add(Environment.NewLine +
                "No. Líneas: " +
                _cantLineas +
                Environment.NewLine);
        }

        internal void buscarLineasInventarioTomaFisica(List<string> pprintingLinesList)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("I." + TableInventario._CANTIDAD + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + ", ");
            _sb.Append("I." + TableInventario._TOMA_FISICA + ", ");
            _sb.Append("I." + TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _cantLineas = 0;

            bool _hayDevolucionesMalEstado = false;

            foreach (DataRow _fila in _dt.Rows)
            {
                string _lineaUno = string.Empty;
                string _lineaDos = string.Empty;

                Position _position = new Position();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioAuditoria.codigo);
                _lineaUno += _fila[TableInventario._CODPRODUCTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioAuditoria.descripcion);
                _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                _lineaUno += Environment.NewLine;

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.cantidad);
                decimal _cantidad = FormatUtil.convertStringToDecimal(_fila[TableInventario._CANTIDAD].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_cantidad);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.disponible);

                decimal _disponible = FormatUtil.convertStringToDecimal(_fila[TableInventario._DISPONIBLE].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_disponible);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.auditado);

                decimal _tomaFisica = FormatUtil.convertStringToDecimal(_fila[TableInventario._TOMA_FISICA].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_tomaFisica);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.diferencia);

                decimal _diferencia = _tomaFisica - _disponible;

                _lineaDos += FormatUtil.applyCurrencyFormat(_diferencia);

                _lineaDos += Environment.NewLine;

                if (_diferencia != Numeric._zeroDecimalInitialize)
                {
                    _cantLineas++;

                    pprintingLinesList.Add(_lineaUno);
                    pprintingLinesList.Add(_lineaDos);
                    pprintingLinesList.Add(Environment.NewLine);
                }

                decimal _devolucionesMalEstado = FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESMALAS].ToString());

                if (_devolucionesMalEstado > 0)
                {
                    _hayDevolucionesMalEstado = true;
                }
            }

            pprintingLinesList.Add(
                "No. Líneas: " +
                _cantLineas +
                Environment.NewLine
                );

            Line _line = new Line();
            _line.simpleHypenLine(pprintingLinesList);

            if (_hayDevolucionesMalEstado)
            {
                pintarLineasInventarioConExistencias_devolucionesMalas(pprintingLinesList, _dt);
            }
        }

        internal void buscarLineasInventarioConExistencias(List<string> pprintingLinesList)
        {
            DataTable _dt = sentenciaInventarioConExistencias();

            pintarLineasInventarioConExistencias(pprintingLinesList, _dt);
        }

        private DataTable sentenciaInventarioConExistencias()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._CODCIA + ", ");
            _sb.Append("I." + TableInventario._CODAGENTE + ", ");
            _sb.Append("I." + TableInventario._FECHATOMA + ", ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("I." + TableInventario._CANTIDAD + ", ");
            _sb.Append("I." + TableInventario._VENTAS + ", ");
            _sb.Append("I." + TableInventario._DEVOLUCIONESBUENAS + ", ");
            _sb.Append("I." + TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append("I." + TableInventario._REGALIAS + ", ");
            _sb.Append("I." + TableInventario._ANULACIONES + ", ");
            _sb.Append("I." + TableInventario._ANULACIONESBUENAS + ", ");
            _sb.Append("I." + TableInventario._ANULACIONESMALAS + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._DISPONIBLE + " > 0 ");
            _sb.Append("AND ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        private void pintarLineasInventarioConExistencias(List<string> pprintingLinesList, DataTable pdt)
        {
            int _cantLineas = 0;

            bool _hayDevolucionesMalEstado = false;

            foreach (DataRow _fila in pdt.Rows)
            {
                string _lineaUno = string.Empty;
                string _lineaDos = string.Empty;

                Position _position = new Position();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.codigo);
                _lineaUno += _fila[TableInventario._CODPRODUCTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.descripcion);
                _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                _lineaUno += Environment.NewLine;

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.cantidad);
                _lineaDos += decimal.Parse(_fila[TableInventario._CANTIDAD].ToString()).ToString("N2");

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.ventas);
                decimal _ventas = FormatUtil.convertStringToDecimal(_fila[TableInventario._VENTAS].ToString());

                decimal _an = FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONES].ToString());

                decimal _ab = FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONESBUENAS].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_ventas - _ab);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.devoluciones);
                decimal _db = FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESBUENAS].ToString());

                decimal _dm = FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESMALAS].ToString());

                if (_dm > 0)
                {
                    _hayDevolucionesMalEstado = true;
                }

                _lineaDos += FormatUtil.applyCurrencyFormat(_db);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.regalias);
                _lineaDos += _fila[TableInventario._REGALIAS].ToString();

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.anulaciones);
                decimal _am = FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONESMALAS].ToString());

                _lineaDos += FormatUtil.applyCurrencyFormat(_an - _ab);

                _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.disponible);
                _lineaDos += decimal.Parse(_fila[TableInventario._DISPONIBLE].ToString()).ToString("N2");

                _lineaDos += Environment.NewLine;

                _cantLineas++;

                pprintingLinesList.Add(_lineaUno);
                pprintingLinesList.Add(_lineaDos);
                pprintingLinesList.Add(Environment.NewLine);
            }

            pprintingLinesList.Add(
                Environment.NewLine
                + "No. Líneas: "
                + _cantLineas
                + Environment.NewLine
                );

            Line _line = new Line();
            _line.simpleHypenLine(pprintingLinesList);

            if (_hayDevolucionesMalEstado)
            {
                pintarLineasInventarioConExistencias_devolucionesMalas(pprintingLinesList, pdt);
            }
        }

        private void pintarLineasInventarioConExistencias_devolucionesMalas(List<string> pprintingLinesList, DataTable pdt)
        {
            int _cantLineas = 0;

            pprintingLinesList.Add(Environment.NewLine);
            pprintingLinesList.Add("*** Devoluciones en mal estado. ***");
            pprintingLinesList.Add(Environment.NewLine);
            pprintingLinesList.Add(Environment.NewLine);

            foreach (DataRow _fila in pdt.Rows)
            {
                decimal _dm = FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESMALAS].ToString());

                if (_dm > 0)
                {
                    string _lineaUno = string.Empty;
                    string _lineaDos = string.Empty;

                    Position _position = new Position();

                    _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.codigo);
                    _lineaUno += _fila[TableInventario._CODPRODUCTO].ToString();

                    _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.descripcion);
                    _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                    _lineaUno += Environment.NewLine;

                    _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.devoluciones);
                    _lineaDos += FormatUtil.applyCurrencyFormat(_dm);

                    _lineaDos += Environment.NewLine;

                    _cantLineas++;

                    pprintingLinesList.Add(_lineaUno);
                    pprintingLinesList.Add(_lineaDos);
                    pprintingLinesList.Add(Environment.NewLine);
                }
            }

            pprintingLinesList.Add(
                Environment.NewLine
                + "No. Líneas Devoluciones Malas: "
                + _cantLineas
                + Environment.NewLine
                );
        }
    }
}
