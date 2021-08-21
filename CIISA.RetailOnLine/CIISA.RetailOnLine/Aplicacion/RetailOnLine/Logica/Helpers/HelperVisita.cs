using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperVisita
    {
        private StringBuilder sentenciaBuscarSegmentoDeRutaHoy(string pfiltro, string ptipoBusquedaCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("V." + TableVisitas._CODCIA + ", ");
            _sb.Append("V." + TableVisitas._CODRUTA + ", ");
            _sb.Append("V." + TableVisitas._DIA + ", ");
            _sb.Append("V." + TableVisitas._CODESTABLECIMIENTO + ", ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + ", ");
            _sb.Append("V." + TableVisitas._ORDEN + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + ", ");
            _sb.Append("INDF." + TableIndicadorFactura._IND_ESTADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._visitas + " V, ");
            _sb.Append(TablesROL._cliente + " C, ");
            _sb.Append(TablesROL._indicadorFactura + " INDF ");
            _sb.Append("WHERE ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._DIA + " = ");
            _sb.Append("'" + VarTime.getDayOfWeek() + "' ");
            _sb.Append("AND ");

            if (ptipoBusquedaCliente.Equals(VarComboBox._cbxCode))
            {
                _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            }
            else
            {
                _sb.Append("C." + TableCliente._NOMBRE + " ");
            }

            _sb.Append("LIKE ");
            _sb.Append("'%" + pfiltro + "%' ");

            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " = ");
            _sb.Append("INDF." + TableIndicadorFactura._NO_CLIENTE + " ");
            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODESTABLECIMIENTO + " = ");
            _sb.Append("INDF." + TableIndicadorFactura._NO_ESTABLECIMIENTO + " ");

            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " ");
            _sb.Append("NOT IN (");
            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento);
            _sb.Append(") ");
            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " ");
            _sb.Append("NOT IN (");
            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoRazonesNV._NO_CLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRazonesNV);
            _sb.Append(") ");
            _sb.Append("ORDER BY ");
            _sb.Append("V." + TableVisitas._ORDEN + " ASC");

            return _sb;
        }

        private StringBuilder sentenciaBuscarSegmentoDeRutaTodos(string pfiltro, string ptipoBusquedaCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("V." + TableVisitas._CODCIA + ", ");
            _sb.Append("V." + TableVisitas._CODRUTA + ", ");
            _sb.Append("V." + TableVisitas._DIA + ", ");
            _sb.Append("V." + TableVisitas._CODESTABLECIMIENTO + ", ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + ", ");
            _sb.Append("V." + TableVisitas._ORDEN + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + ", ");
            _sb.Append("INDF." + TableIndicadorFactura._IND_ESTADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._visitas + " V, ");
            _sb.Append(TablesROL._cliente + " C, ");
            _sb.Append(TablesROL._indicadorFactura + " INDF ");
            _sb.Append("WHERE ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("AND ");

            if (ptipoBusquedaCliente.Equals(VarComboBox._cbxCode))
            {
                _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            }
            else
            {
                _sb.Append("C." + TableCliente._NOMBRE + " ");
            }

            _sb.Append("LIKE ");
            _sb.Append("'%" + pfiltro + "%' ");

            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " = ");
            _sb.Append("INDF." + TableIndicadorFactura._NO_CLIENTE + " ");
            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODESTABLECIMIENTO + " = ");
            _sb.Append("INDF." + TableIndicadorFactura._NO_ESTABLECIMIENTO + " ");

            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " ");
            _sb.Append("NOT IN (");
            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento);
            _sb.Append(") ");
            _sb.Append("AND ");
            _sb.Append("V." + TableVisitas._CODCLIENTE + " ");
            _sb.Append("NOT IN (");
            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoRazonesNV._NO_CLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRazonesNV);
            _sb.Append(") ");
            _sb.Append("ORDER BY ");
            _sb.Append("V." + TableVisitas._DIA + ", ");
            _sb.Append("V." + TableVisitas._ORDEN + " ");
            _sb.Append("ASC");

            return _sb;
        }

        internal void buscarSegmentoRuta(ListView pltvClientes, string ptipoBusquedaSegmento, string ptipoBusquedaCliente, string pfiltro)
        {
            StringBuilder _sb = new StringBuilder();
            DateTime _fechaHora = VarTime.getNow();

            if (ptipoBusquedaSegmento.Equals(VarComboBox._cbxToday))
            {
                _sb = sentenciaBuscarSegmentoDeRutaHoy(pfiltro, ptipoBusquedaCliente);
            }

            if (ptipoBusquedaSegmento.Equals(VarComboBox._cbxAll))
            {
                _sb = sentenciaBuscarSegmentoDeRutaTodos(pfiltro, ptipoBusquedaCliente);
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvClientes.ItemsSource as ObservableCollection<pnlClientes_ltvClientes>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlClientes_ltvClientes _lvi = new pnlClientes_ltvClientes();

                _lvi.ORDEN = _fila[TableVisitas._ORDEN].ToString();
                _lvi.CODCLIENTE = _fila[TableVisitas._CODCLIENTE].ToString();
                _lvi.CODESTABLECIMIENTO = _fila[TableVisitas._CODESTABLECIMIENTO].ToString();

                _lvi.NOMBRE = _fila[TableCliente._NOMBRE].ToString();

                if (_fila[TableIndicadorFactura._IND_ESTADO].ToString().Equals(SQL._No))
                {
                    _lvi.ItemTextColor = (Color)App.Current.Resources["RedColor"];
                }
                else
                {
                    _lvi.ItemTextColor = Color.Default;
                }

                Source.Add(_lvi);
            }

            pltvClientes.ItemsSource = Source;

            if (Source.Count == 1)
            {
                for (int i = 0; i < Source.Count; i++)
                {
                    pltvClientes.SelectedItem = Source[i];
                }
            }
        }

        internal int calcularTotalClientesSegmento()
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            int _numeroDiaSemanaTomaFisica = _manager.buscarNumeroDiaDeLaSemanaFechaTomaFisica();

            int _numeroDiaSemanaHoy = VarTime.getDayOfWeek(VarTime.getNow());

            List<int> _listaDiasSemana = new List<int>();

            for (int i = _numeroDiaSemanaTomaFisica; i <= _numeroDiaSemanaHoy; i++)
            {
                _listaDiasSemana.Add(i);
            }

            Util _util = new Util();

            string _diasSemana = _util.recordListInt(_listaDiasSemana);

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DISTINCT ");
            _sb.Append(TableVisitas._CODCLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._visitas + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableVisitas._DIA + " ");
            _sb.Append("IN (");
            _sb.Append("" + _diasSemana);
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            return _dt.Rows.Count;
        }

        internal void nuevaVisita(Cliente pobjCliente)
        {
            pobjCliente.v_objEstablecimiento.v_codEstablecimiento = 1;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._visitas + " ");
            _sb.Append("(");
            _sb.Append(TableVisitas._CODCIA + ", ");
            _sb.Append(TableVisitas._CODRUTA + ", ");
            _sb.Append(TableVisitas._DIA + ", ");
            _sb.Append(TableVisitas._CODESTABLECIMIENTO + ", ");
            _sb.Append(TableVisitas._CODCLIENTE);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
            _sb.Append("'" + Numeric._oneInteger + "', ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "')");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal string buscarOrdenVisita(string pcodCliente, int pday)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableVisitas._ORDEN + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._visitas + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableVisitas._CODCLIENTE + " = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableVisitas._DIA + " = ");
            _sb.Append("'" + pday + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal bool ExisteVisita(string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableVisitas._CODCIA + ", ");
            _sb.Append(TableVisitas._CODRUTA + ", ");
            _sb.Append(TableVisitas._DIA + ", ");
            _sb.Append(TableVisitas._CODESTABLECIMIENTO + ", ");
            _sb.Append(TableVisitas._CODCLIENTE + ", ");
            _sb.Append(TableVisitas._ORDEN);
            _sb.Append(" FROM ");
            _sb.Append(TablesROL._visitas + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableVisitas._CODCLIENTE + " = ");
            _sb.Append("'" + pcodCliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows != null)
            {
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
