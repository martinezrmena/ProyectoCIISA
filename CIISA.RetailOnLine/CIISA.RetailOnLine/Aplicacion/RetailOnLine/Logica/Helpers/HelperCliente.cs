using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperCliente
    {
        internal DataTable buscarFacturaElectronicaPorCodigoDocumentoSentencia(FacturaElectronica facturaElectronica, string cod_transaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NUM_ORDEN, ");
            _sb.Append("FECHA_ORDEN, ");
            _sb.Append("NUM_RECEPCION, ");
            _sb.Append("NUM_RECLAMO, ");
            _sb.Append("FECHA_RECLAMO, ");
            _sb.Append("COD_PROVEEDOR ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODDOCUMENTO = ");
            _sb.Append("'" + cod_transaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal DataTable buscarClientePorCodigoClienteSentencia(string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NOMBRE, ");
            _sb.Append("NOMBRE_LARGO, ");
            _sb.Append("CEDULA, ");
            _sb.Append("EXCENTO_IMP, ");
            _sb.Append("PLAZO, ");
            _sb.Append("F_CIERRE, ");
            _sb.Append("LISTA_PRECIOS, ");
            _sb.Append("PAIS, ");
            _sb.Append("PROVINCIA, ");
            _sb.Append("CANTON, ");
            _sb.Append("DISTRITO, ");
            _sb.Append("DIRECCION, ");
            _sb.Append("TELEFONO, ");
            _sb.Append("NOMBRE_ENC, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("CLIENTE_NUEVO, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("COPIAS_FAC, ");
            //_sb.Append("NUEVO_CLIENTE ");
            //Validacion 50mts
            _sb.Append("NUEVO_CLIENTE, ");
            _sb.Append("LATITUD, ");
            _sb.Append("LONGITUD ");
            _sb.Append("");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal FacturaElectronica buscarFacturaElectronicaPorCodigoTransaccionCarga(DataTable pdt, FacturaElectronica facturaElectronica)
        {
            facturaElectronica = new FacturaElectronica();

            foreach (DataRow _fila in pdt.Rows)
            {

                if (!string.IsNullOrEmpty(_fila["COD_PROVEEDOR"].ToString()))
                {
                    facturaElectronica.v_codProveedor = _fila["COD_PROVEEDOR"].ToString();
                }

                if (!string.IsNullOrEmpty(_fila["FECHA_RECLAMO"].ToString()))
                {
                    facturaElectronica.v_fechaReclamo = DateTime.Parse(_fila["FECHA_RECLAMO"].ToString());
                }

                if (!string.IsNullOrEmpty(_fila["NUM_RECLAMO"].ToString()))
                {
                    facturaElectronica.v_numReclamo = _fila["NUM_RECLAMO"].ToString();
                }

                if (!string.IsNullOrEmpty(_fila["NUM_RECEPCION"].ToString()))
                {
                    facturaElectronica.v_numRecibo = _fila["NUM_RECEPCION"].ToString();
                }

                if (!string.IsNullOrEmpty(_fila["FECHA_ORDEN"].ToString()))
                {
                    facturaElectronica.v_fechaOrden = DateTime.Parse(_fila["FECHA_ORDEN"].ToString());
                }

                if (!string.IsNullOrEmpty(_fila["NUM_ORDEN"].ToString()))
                {
                    facturaElectronica.v_numOrden = _fila["NUM_ORDEN"].ToString();
                }

            }

            return facturaElectronica;
        }

        internal async Task buscarClientePorCodigoClienteCargaCliente(DataTable pdt, Cliente pobjCliente)
        {
            foreach (DataRow _fila in pdt.Rows)
            {
                pobjCliente.v_no_cia = _fila["NO_CIA"].ToString();
                pobjCliente.v_no_cliente = _fila["NO_CLIENTE"].ToString();
                pobjCliente.v_nombre = _fila["NOMBRE"].ToString();
                pobjCliente.v_nombre_largo = _fila["NOMBRE_LARGO"].ToString();
                pobjCliente.v_cedula = _fila["CEDULA"].ToString();

                pobjCliente.v_exento_imp = MiscUtils.getVariableBooleanSQLStateStringEmptyTrue(_fila["EXCENTO_IMP"].ToString());

                pobjCliente.v_plazo = FormatUtil.convertStringToInt(_fila["PLAZO"].ToString());
                pobjCliente.v_f_cierre = _fila["F_CIERRE"].ToString();
                pobjCliente.v_lista_precios = _fila["LISTA_PRECIOS"].ToString();
                pobjCliente.v_objDireccion.v_pais = _fila["PAIS"].ToString();
                pobjCliente.v_objDireccion.v_provincia = _fila["PROVINCIA"].ToString();
                pobjCliente.v_objDireccion.v_canton = _fila["CANTON"].ToString();
                pobjCliente.v_objDireccion.v_distrito = _fila["DISTRITO"].ToString();
                pobjCliente.v_objDireccion.v_direccion = _fila["DIRECCION"].ToString();
                pobjCliente.v_telefono = _fila["TELEFONO"].ToString();
                pobjCliente.v_nombre_enc = _fila["NOMBRE_ENC"].ToString();
                pobjCliente.v_no_agente = _fila["NO_AGENTE"].ToString();
                pobjCliente.v_codClienteGenerico = _fila["CLIENTE_NUEVO"].ToString();

                //Validacion 50mts
                pobjCliente.v_latitud =  FormatUtil.convertStringToDouble(_fila["LATITUD"].ToString());
                pobjCliente.v_longitud = FormatUtil.convertStringToDouble(_fila["LONGITUD"].ToString());

                pobjCliente.v_nuevoCliente = MiscUtils.getVariableBooleanSQLStateStringEmptyTrue(_fila["NUEVO_CLIENTE"].ToString());

                pobjCliente.v_copias_fac = FormatUtil.convertStringToInt(_fila["COPIAS_FAC"].ToString());

                if (!pobjCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador)
                {
                }

                Logica_ManagerAutorizadoFirmar _managerAutorizadoFirmar = new Logica_ManagerAutorizadoFirmar();
                _managerAutorizadoFirmar.buscarListaAutorizadosFirmar(pobjCliente);

                Logica_ManagerEstablecimiento _managerEstablecimiento = new Logica_ManagerEstablecimiento();
                await _managerEstablecimiento.buscarEstablecimientoPorCodigoEstablecimiento(pobjCliente);
            }
        }

        internal FacturaElectronica buscarFacturaElectronicaPorCodigoDocumento(FacturaElectronica facturaElectronica, string cod_transaccion) {

            DataTable _dt = buscarFacturaElectronicaPorCodigoDocumentoSentencia(facturaElectronica, cod_transaccion);

            return buscarFacturaElectronicaPorCodigoTransaccionCarga(_dt, facturaElectronica);
        }

        internal async Task buscarClientePorCodigoCliente(Cliente pobjCliente)
        {
            DataTable _dt = buscarClientePorCodigoClienteSentencia(pobjCliente.v_no_cliente);

            await buscarClientePorCodigoClienteCargaCliente(_dt, pobjCliente);
        }

        internal void nuevoCliente(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._cliente + " ");
            _sb.Append("(");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NOMBRE, ");
            _sb.Append("NOMBRE_LARGO, ");
            _sb.Append("CEDULA, ");
            _sb.Append("EXCENTO_IMP, ");
            _sb.Append("PLAZO, ");
            _sb.Append("F_CIERRE, ");
            _sb.Append("LISTA_PRECIOS, ");
            _sb.Append("PAIS, ");
            _sb.Append("PROVINCIA, ");
            _sb.Append("CANTON, ");
            _sb.Append("DISTRITO, ");
            _sb.Append("DIRECCION, ");
            _sb.Append("TELEFONO, ");
            _sb.Append("NOMBRE_ENC, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("CLIENTE_NUEVO, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("COPIAS_FAC, ");
            _sb.Append("TIPO_ID_TRIBUTARIO, ");
            _sb.Append("NUEVO_CLIENTE, ");
            _sb.Append("CLASIFICACION, ");
            _sb.Append("EMAIL, ");
            _sb.Append("LATITUD, ");
            _sb.Append("LONGITUD, ");
            _sb.Append("NOMBRE_APO, ");
            _sb.Append("CEDULA_APO, ");
            _sb.Append("PROVINCIA_APO, ");
            _sb.Append("CANTON_APO, ");
            _sb.Append("DISTRITO_APO, ");
            _sb.Append("DIRECCION_APO, ");
            _sb.Append("OBSERVACIONES, ");
            _sb.Append("DIAS_ATENCION");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("'" + pobjCliente.v_nombre + "', ");
            _sb.Append("'" + pobjCliente.v_nombre_largo + "', ");
            _sb.Append("'" + pobjCliente.v_cedula + "', ");
            _sb.Append("'" + pobjCliente.exentoImpuesto() + "', ");
            _sb.Append("" + pobjCliente.v_plazo + ", ");
            _sb.Append("'" + pobjCliente.fechaCierre() + "', ");
            _sb.Append("'" + pobjCliente.v_lista_precios + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_pais + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_provincia + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_canton + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_distrito + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_direccion + "', ");
            _sb.Append("'" + pobjCliente.v_telefono + "', ");
            _sb.Append("'" + pobjCliente.v_nombre_enc + "', ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
            _sb.Append("'" + pobjCliente.v_codClienteGenerico + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("" + Variable._invoiceNumberCopies + ", ");
            _sb.Append("'" + pobjCliente.v_tipoCedula + "', ");
            _sb.Append("'" + SQL._Si + "', ");
            _sb.Append("'" + pobjCliente.v_clasificacion + "', ");
            _sb.Append("'" + pobjCliente.v_email + "', ");
            _sb.Append("" + pobjCliente.v_latitud + ", ");
            _sb.Append("" + pobjCliente.v_longitud + ", ");
            _sb.Append("'" + pobjCliente.v_nombre_apo + "', ");
            _sb.Append("'" + pobjCliente.v_cedula_apo + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_provinciaApo + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_cantonApo + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_distritoApo + "', ");
            _sb.Append("'" + pobjCliente.v_objDireccion.v_direccionApo + "', ");
            _sb.Append("'" + pobjCliente.v_observaciones + "', ");
            _sb.Append("'" + pobjCliente.v_dias_atencion + "'");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        private void cargarListaClientes(DataTable pdt, ListView pltvClientes)
        {
            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            var Source = pltvClientes.ItemsSource as ObservableCollection<pnlClientes_ltvClientes>;

            foreach (DataRow _fila in pdt.Rows)
            {
                pnlClientes_ltvClientes _lvi = new pnlClientes_ltvClientes();

                _lvi.NO_CLIENTE = _fila["NO_CLIENTE"].ToString();
                _lvi.NO_ESTABLECIMIENTO = _fila["NO_ESTABLECIMIENTO"].ToString();
                _lvi.NO_CLIENTE_NO_ESTABLECIMIENTO = _fila["NO_CLIENTE"].ToString()
                    + Simbol._point
                    + _fila["NO_ESTABLECIMIENTO"].ToString();
                _lvi.TELEFONO = _fila["TELEFONO"].ToString();

                if (_tipoAgente.Equals(Agent._ruteroSigla))
                {
                    _lvi.DESCRIPCION = _fila["NOMBRE"].ToString();
                }

                if (_tipoAgente.Equals(Agent._supermercadoSigla))
                {
                    _lvi.DESCRIPCION = _fila["DESCESTABLECIMIENTO"].ToString();
                }

                if (_tipoAgente.Equals(Agent._carniceroSigla))
                {
                    _lvi.DESCRIPCION = _fila["DESCESTABLECIMIENTO"].ToString();
                }

                if (_tipoAgente.Equals(Agent._cobradorSigla))
                {
                    _lvi.DESCRIPCION = _fila["DESCESTABLECIMIENTO"].ToString();
                }

                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();
                _lvi.COBRADOR = _fila["COBRADOR"].ToString();
                _lvi.IND_COBRO = _fila["IND_COBRO"].ToString();

                if (_fila["IND_COBRO"].ToString().Equals(SQL._Si))
                {
                    _lvi.ItemTextColor = Color.Blue;
                }

                if (_fila["IND_ESTADO"].ToString().Equals(Indicators._N))
                {
                    _lvi.ItemTextColor = Color.Red;
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

        internal void buscarListaClienteRuteros(ListView ppnlClientes_ltvClientes, string ptipoBusqueda, string pfiltro)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT DISTINCT ");
            _sb.Append("A.NO_CLIENTE, ");
            _sb.Append("A.NOMBRE, ");
            _sb.Append("B.IND_ESTADO, ");
            _sb.Append("B.NO_AGENTE, ");
            _sb.Append("B.COBRADOR, ");
            _sb.Append("B.IND_COBRO, ");
            _sb.Append("B.NO_ESTABLECIMIENTO, ");
            _sb.Append("A.TELEFONO,  ");
            _sb.Append("C.DESCESTABLECIMIENTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " A, ");
            _sb.Append(TablesROL._indicadorFactura + " B, ");
            _sb.Append(TablesROL._establecimiento + " C ");
            _sb.Append("WHERE ");
            _sb.Append("A.NO_CLIENTE = ");
            _sb.Append("B.NO_CLIENTE ");
            _sb.Append("AND ");
            _sb.Append("A.NO_AGENTE = ");
            _sb.Append("B.COBRADOR ");

            _sb.Append("AND ");
            _sb.Append("A.NO_CLIENTE = ");
            _sb.Append("C.CODCLIENTE ");
            _sb.Append("AND ");
            _sb.Append("B.NO_ESTABLECIMIENTO = ");
            _sb.Append("C.CODESTABLECIMIENTO ");

            _sb.Append("AND ");

            if (ptipoBusqueda.Equals(VarComboBox._cbxCode))
            {
                _sb.Append("A.NO_CLIENTE ");
                _sb.Append("LIKE ");
                _sb.Append("'%" + pfiltro + "%' ");
            }

            if (ptipoBusqueda.Equals(VarComboBox._cbxDescription))
            {
                _sb.Append("A.NOMBRE ");
                _sb.Append("LIKE ");
                _sb.Append("'%" + pfiltro + "%' ");
            }

            _sb.Append("GROUP BY A.NO_CLIENTE ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            cargarListaClientes(_dt, ppnlClientes_ltvClientes);
        }

        internal string buscarNombreClientePorCodigoCliente(string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NOMBRE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal bool ExisteCliente(string pcodCliente)
        {
            DataTable _dt = buscarClientePorCodigoClienteSentencia(pcodCliente);

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

        internal string buscarRutaCobro_Cliente(string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("RUTA_COBRO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
