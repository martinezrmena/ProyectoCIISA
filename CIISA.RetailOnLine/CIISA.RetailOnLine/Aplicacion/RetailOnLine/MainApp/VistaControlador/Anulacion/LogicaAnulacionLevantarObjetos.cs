using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Anulacion
{
    public class LogicaAnulacionLevantarObjetos
    {

        private vistaAnulaciones view = null;

        internal LogicaAnulacionLevantarObjetos(vistaAnulaciones pview)
        {
            view = pview;
        }

        internal TransaccionEncabezado levantarObjetoTransaccionEncabezado()
        {
            TransaccionEncabezado _objTransaccion = new TransaccionEncabezado();

            _objTransaccion.v_anulado = SQL._No;
            _objTransaccion.v_codAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent;
            _objTransaccion.v_codCia = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;
            _objTransaccion.v_codCliente = view.controlador.v_objCliente.v_no_cliente;
            _objTransaccion.v_codClienteGenerico = Agent.getCodClienteGenerico();
            _objTransaccion.v_codDocumento = string.Empty;

            _objTransaccion.v_objTipoDocumento.SetNombre(view.controlador.AnulacionSeleccionado.DESCRIPCION);
            _objTransaccion.v_enviado = SQL._No;

            _objTransaccion.v_fechaCreacion = VarTime.getNowSQLite();
            _objTransaccion.v_fechaEntrega = VarTime.getDateExpires(Pedido._diasParaEntrega);

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _objTransaccion.v_fechaTomaFisica = _manager.buscarFechaTomaFisica();

            _objTransaccion.v_indAutomatico = SQL._No;

            _objTransaccion.v_observacion = string.Empty;
            _objTransaccion.v_saldo = Numeric._zeroDecimalInitialize;

            _objTransaccion.v_tramite = SQL._No;

            _objTransaccion.v_total = Decimal.Parse(view.controlador.AnulacionSeleccionado.TOTAL);

            _objTransaccion.v_totalImp = Numeric._zeroDecimalInitialize;

            view.controlador.v_objCliente.v_objTransaccion = _objTransaccion;

            return _objTransaccion;
        }

        internal void guardarNuevoPedido(TransaccionEncabezado pobjTransaccion)
        {
            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();
            _managerAgenteVendedor.BuscarConsecutivo_Pedido(view.controlador.v_objCliente);

            pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

            view.controlador.v_objCliente.v_objTransaccion = pobjTransaccion;

            Logica_ManagerEncabezadoPedido _managerEncabezadoPedido = new Logica_ManagerEncabezadoPedido();
            _managerEncabezadoPedido.guardarEncabezadoPedido(view.controlador.v_objCliente);

            _managerAgenteVendedor.AumentarConsecutivo_Pedido();

        }
    }
}
