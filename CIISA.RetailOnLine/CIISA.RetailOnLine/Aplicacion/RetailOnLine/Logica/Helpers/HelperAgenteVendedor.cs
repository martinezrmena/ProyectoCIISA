using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperAgenteVendedor
    {

        private LogMessageAttention logMessageAttention = new LogMessageAttention();

        /// <summary>
        /// Metodo que permite validar y modificar los consecutivos en caso de que ya existan para evitar duplicados
        /// </summary>
        /// <param name="pobjCliente"></param>
        internal async Task<bool> verificarConsecutivoRepetido(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();
            bool repetido = false;
            int intentos = 0;
            string Mensaje1 = "El consecutivo asignado: {0} ya existe, por lo cual se procederá a asignar el siguiente mayor." + Environment.NewLine + "Número de intentos: {1}.";
            string Mensaje2 = "El consecutivo ha sido aumentado al código: {0}; se procederá a guardar el documento, debe contactar a backoffice para mover los consecutivos y luego proceder con la recarga de la tabla.";

            //Primero se verificará si existe el consecutivo asignado en la tabla de documentos
            while (VerificarExisteConsecutivo(pobjCliente))
            {
                intentos++;

                await logMessageAttention.generalAttention(string.Format(Mensaje1, pobjCliente.v_objTransaccion.v_codDocumento, (intentos)));

                //Si el codigo del documento ya existe entonces es necesario actualizarlo
                actualizarConsecutivoTransaccion(pobjCliente);

                //Ahora es necesario buscar el nuevo consecutivo del documento
                buscarConsecutivoTransaccion(pobjCliente);

                repetido = true;

            }

            if (repetido)
            {
                await logMessageAttention.generalAttention(string.Format(Mensaje2, pobjCliente.v_objTransaccion.v_codDocumento));
            }

            return repetido;
        }

        /// <summary>
        /// Se verifica si existe el codigo del consecutivo en la tabla de documentos
        /// </summary>
        /// <param name="pobjCliente"></param>
        /// <returns></returns>
        internal bool VerificarExisteConsecutivo(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "'");
            _sb.Append(" AND ");

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._ordenVentaSigla + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                _sb.Append("(");
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "'");
                _sb.Append(" OR "); 
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._facturaContadoSigla + "'");
                _sb.Append(")");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                _sb.Append("(");
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "'");
                _sb.Append(" OR ");
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._facturaContadoSigla + "'");
                _sb.Append(")");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._cotizacionSigla + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._devolucionSigla + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._regaliaSigla + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._recaudacionSigla + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._anulacionSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._anulacionSigla + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                _sb.Append("'" + ROLTransactions._reciboDineroSigla + "'");
            }

            _sb.Append(" AND ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");
            _sb.Append(" LIMIT 1 ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string result = MultiGeneric.readStringText(_sb);

            if (!string.IsNullOrEmpty(result))
            {
                return true;
            }

            return false;

        }

        internal DataTable buscarAgenteVendedor(string pcodAgente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._NO_CIA + ", ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + ", ");
            _sb.Append(TableAgenteVendedor._NO_EMPLE + ", ");
            _sb.Append(TableAgenteVendedor._CODIGO_SECTOR + ", ");
            _sb.Append(TableAgenteVendedor._NOM_AGENTE + ", ");
            _sb.Append(TableAgenteVendedor._NO_RUTA + ", ");
            _sb.Append(TableAgenteVendedor._USUARIO + ", ");
            _sb.Append(TableAgenteVendedor._CONTRASENNA + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + ", ");
            _sb.Append(TableAgenteVendedor._TIPO_AGENTE + ", ");
            _sb.Append(TableAgenteVendedor._CLIENTE_NUEVO + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_PD + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + pcodAgente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoAgente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarConsecutivoCliente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_CLI + " ");
            _sb.Append("+ 1 ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal void aumentarConsecutivoNuevoCliente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("SET ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_CLI + " = ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_CLI + " + 1 ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        private void BuscarConsecutivo(Cliente pobjCliente, string pcampoTabla)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(pcampoTabla + " ");
            _sb.Append("+ 1 ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            pobjCliente.v_objTransaccion.v_codDocumento =
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                + MultiGeneric.readStringText(_sb);
        }

        internal void BuscarConsecutivo_Pedido(Cliente pobjCliente)
        {
            BuscarConsecutivo(pobjCliente, TableAgenteVendedor._CONSECUTIVO_PD);
        }

        internal void AumentarConsecutivo_Pedido()
        {
            AumentarConsecutivo(TableAgenteVendedor._CONSECUTIVO_PD);
        }

        private void AumentarConsecutivo(string pcampoTabla)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("SET ");
            _sb.Append(pcampoTabla + " = ");
            _sb.Append(pcampoTabla + " ");
            _sb.Append("" + VarOperators._sum + " ");
            _sb.Append("+ 1 ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void buscarConsecutivoTransaccion(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_OV + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_P + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DV + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_RG + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_RC + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._anulacionSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_AN + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + " ");
                _sb.Append("+ 1 ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            pobjCliente.v_objTransaccion.v_codDocumento =
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                + MultiGeneric.readStringText(_sb);
        }

        internal void actualizarConsecutivoTransaccion(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_OV + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_OV + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_P + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_P + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DV + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_DV + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_RG + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_RG + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_RC + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_RC + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._anulacionSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_AN + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_AN + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._agenteVendedor + " ");
                _sb.Append("SET ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + " = ");
                _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + " ");
                _sb.Append("" + VarOperators._sum + " ");
                _sb.Append("+ 1 ");
                _sb.Append("WHERE ");
                _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void buscarConsecutivoTramite(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_TR + " ");
            _sb.Append("+ 1 ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            pobjCliente.v_objTransaccion.v_codDocumento =
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                + MultiGeneric.readStringText(_sb);
        }

        internal void actualizarConsecutivoTramite(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("SET ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_TR + " = ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_TR + " ");
            _sb.Append("" + VarOperators._sum + " ");
            _sb.Append("+ 1 ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal string obtenerCodigoAgenteGenerico(string pcodAgente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._CLIENTE_NUEVO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + pcodAgente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerCodigoClienteAgenteVendedor()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._CODIGO_CLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerTipoAgente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._TIPO_AGENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal void actualizarConsecutivoRecaudacion()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("SET ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_RC + " = ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_RC + " + 1 ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void actualizarConsecutivoRecibo()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("SET ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + " = ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + " + 1 ");
            _sb.Append("WHERE ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " = ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }
    }
}
