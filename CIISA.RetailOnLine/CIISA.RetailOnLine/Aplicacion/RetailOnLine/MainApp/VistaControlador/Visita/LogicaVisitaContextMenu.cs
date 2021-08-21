using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaContextMenu
    {
        private vistaVisita view = null;

        internal LogicaVisitaContextMenu(vistaVisita pview)
        {
            view = pview;
        }

        internal async Task indicadores()
        {
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(
                Simbol._bulletPoint
                + "Pedido:              "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_pedido)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Factura Contado: "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_facturaContado)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Factura Crédito:   "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_facturaCredito)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Respeta Límite:    "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Cheques:            "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_condicionCheque)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Vencimiento:       "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_vencimiento)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Cuenta Activa:     "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_estado)
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Cliente de la Ruta:     "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_no_agente
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Cobrador:     "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_cobrador
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Es cobrador:        "
                + MiscUtils.getVariableStringSQLState(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador)
                );
        }

        internal async Task infoGeneral()
        {
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(
                Simbol._bulletPoint
                + "Cliente: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_objEstablecimiento.v_descEstablecimiento
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Cédula: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_cedula
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Nombre Comercial: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_nombre
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Razón Social: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_nombre_largo
                );
        }

        internal async Task infoFinanciera()
        {
            MiscUtils _miscUtils = new MiscUtils();

            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(
                Simbol._bulletPoint
                + "Límite crédito (LC): "
                + Environment.NewLine
                + Simbol._tab
                + FormatUtil.applyCurrencyFormat(
                    view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito
                    )
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Respeta LC: "
                + _miscUtils.getWordStringSQLEstado(view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito)
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Crédito disponible: "
                + Environment.NewLine
                + Simbol._tab
                + FormatUtil.applyCurrencyFormat(view.controlador.v_objCliente.creditoDisponible())
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Plazo: "
                + view.controlador.v_objCliente.v_plazo
                + " días"
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Exento Impuesto: "
                + _miscUtils.getWordStringSQLEstado(view.controlador.v_objCliente.v_exento_imp)
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Pendiente Pago: "
                + Environment.NewLine
                + Simbol._tab
                + FormatUtil.applyCurrencyFormat(
                    view.controlador.v_objCliente.montoPendienteDePago()
                    )
                );
        }

        internal async Task autorizadoFirmar()
        {
            StringBuilder _sb = new StringBuilder();

            if (view.controlador.v_objCliente.v_listaAutorizadosFirmar.Count > 0)
            {
                foreach (AutorizadoFirmar _objAutorizadoFirmar in view.controlador.v_objCliente.v_listaAutorizadosFirmar)
                {
                    _sb.Append(_objAutorizadoFirmar.v_nombre);
                    _sb.Append(Environment.NewLine);
                    _sb.Append(Simbol._tab);
                    _sb.Append(_objAutorizadoFirmar.v_cedula);
                    _sb.Append(Environment.NewLine);
                    _sb.Append(Environment.NewLine);
                }
            }
            else
            {
                _sb.Append(LogMessageAttention._withoutAuthorizedToSignText);
            }


            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(_sb.ToString());
        }

        internal async Task ubicacion()
        {
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(
                Simbol._bulletPoint
                + "Provincia: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.nombreProvicia()
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Cantón: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.nombreCanton()
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Distrito: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.nombreDistrito()
                + Environment.NewLine
                + Environment.NewLine
                + Simbol._bulletPoint
                + "Dirección: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_objEstablecimiento.v_direccionExacta
                );
        }

        internal async Task contacto()
        {
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(
                Simbol._bulletPoint
                + "Teléfono: "
                + Environment.NewLine
                + Simbol._tab
                + view.controlador.v_objCliente.v_telefono
                + Environment.NewLine
                + Environment.NewLine);
        }
    }
}
