using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoDatos
    {
        SplitString s = new SplitString();

        internal void datosAgente(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, "Ruta: " + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent, 1);
            s.Split_String(pprintingLinesList,"Agente: " + Agent.getNombreAgente());
        }

        internal void datosClienteTransaccion(List<string> pprintingLinesList,Cliente pobjCliente,string pcodTipoTransaccion)
        {
            Line _line = new Line();

            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoAgente.Equals(Agent._ruteroSigla))
            {
                s.Split_String(pprintingLinesList, "Cliente: " + pobjCliente.v_nombre_largo);

                _line.printingLinesList(pprintingLinesList, "Código: " + pobjCliente.v_no_cliente, 1);
            }

            if (_tipoAgente.Equals(Agent._supermercadoSigla) || _tipoAgente.Equals(Agent._carniceroSigla) || _tipoAgente.Equals(Agent._cobradorSigla))
            {
                s.Split_String(pprintingLinesList, "Cliente: " + pobjCliente.v_objEstablecimiento.v_descEstablecimiento);

                _line.printingLinesList(pprintingLinesList, "Código: " + pobjCliente.v_no_cliente
                    + " / Establecimiento: " + pobjCliente.v_objEstablecimiento.v_codEstablecimiento, 1);

                Logica_ManagerCliente _managerCliente = new Logica_ManagerCliente();

                string _rutaCobro = _managerCliente.buscarRutaCobro_Cliente(pobjCliente.v_no_cliente);

                if (_rutaCobro.Equals(string.Empty))
                {
                    _rutaCobro = "Sin especificar";
                }

                _line.printingLinesList(pprintingLinesList, "Ruta cobro: " + _rutaCobro, 1);
            }

            if (!pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
            {
                _line.printingLinesList(pprintingLinesList, "Cédula: " + pobjCliente.v_cedula, 1);
                _line.printingLinesList(pprintingLinesList, "Local: " + pobjCliente.v_objEstablecimiento.v_descEstablecimiento, 1);

                if (_tipoAgente.Equals(Agent._ruteroSigla))
                {
                    s.Split_String(pprintingLinesList, "Dirección: " + pobjCliente.v_objDireccion.v_direccion);
                }

                if (_tipoAgente.Equals(Agent._supermercadoSigla))
                {
                    s.Split_String(pprintingLinesList, "Dirección: " + pobjCliente.v_objEstablecimiento.v_direccionExacta);
                }

                if (_tipoAgente.Equals(Agent._carniceroSigla))
                {
                    s.Split_String(pprintingLinesList, "Dirección: " + pobjCliente.v_objEstablecimiento.v_direccionExacta);
                }

                if (_tipoAgente.Equals(Agent._cobradorSigla))
                {
                    s.Split_String(pprintingLinesList, "Dirección: " + pobjCliente.v_objEstablecimiento.v_direccionExacta);
                }

                _line.printingLinesList(pprintingLinesList, "Teléfono: " + pobjCliente.v_telefono, 1);
            }
        }

        internal void datosCliente(List<string> pprintingLinesList,Cliente pobjCliente)
        {
            Line _line = new Line();

            s.Split_String(pprintingLinesList, "Cliente: " + pobjCliente.v_nombre_largo);

            _line.printingLinesList(pprintingLinesList, "Código: " + pobjCliente.v_no_cliente, 1);

            _line.printingLinesList(pprintingLinesList, "Cédula: " + pobjCliente.v_cedula, 1);
            _line.printingLinesList(pprintingLinesList, "Local: " + pobjCliente.v_objEstablecimiento.v_descEstablecimiento, 1);

            s.Split_String(pprintingLinesList, "Dirección: " + pobjCliente.v_objEstablecimiento.v_direccionExacta);

            _line.printingLinesList(pprintingLinesList, "Teléfono: " + pobjCliente.v_telefono, 1);
        }

        internal void datosAgenteTransaccion(List<string> pprintingLinesList,string pcodTipoTransaccion)
        {
            if (!pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
            {
                Line _line = new Line();

                _line.printingLinesList(pprintingLinesList, "Ruta: " + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent, 1);
                s.Split_String(pprintingLinesList, "Agente: " + Agent.getNombreAgente());

                _line.simpleHypenLine(pprintingLinesList);
            }
        }
    }
}
