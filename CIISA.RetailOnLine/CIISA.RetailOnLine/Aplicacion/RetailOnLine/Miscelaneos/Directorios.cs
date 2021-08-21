using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos
{
    public static class Directorios
    {
        public static string reporteFlujoDineroArchivo = "flujoDinero";
        public static string reportePedidosRutaArchivo = "pedidosRuta";
        public static string reportePedidosSinAplicarArchivo = "pedidosSinAplicar";
        public static string reporteCrediticioDelClienteArchivo = "crediticioDelCliente";
        public static string reporteCrediticioDeLaRuta = "crediticioDeLaRuta";
        public static string reporteDocumentosRealizados = "documentosRealizados";
        public static string reporteRecibosDinero = "recibosDinero";
        public static string reporteAnulaciones = "anulaciones";
        public static string reporteVentasPorProducto = "ventasPorProducto";
        public static string reporteTramites = "tramites";
        public static string reporteConsecutivoDocumentos = "consecutivoDocumentos";
        public static string reporteOVTransmitidas = "OVTransmitidas";
        public static string reporteIndicadoresFacturacion = "indicadoresFacturacion";
        public static string reporteDirectorio = "reportesImpresos";
        public static string reporteMaquina = "reporteMaquina";
        public static string documentosImpresos = "documentosImpresos";
        public static string documentosReimpresos = "documentosReimpresos";
        public static string documentosReimpresosAntiguedad = "documentosReimpresosAntiguedad";
        public static string cargaDirectorio = "bitacoraCarga";
        public static string cargaTablasArchivo = "cargaTablas";
        public static string cargaCrearTablasArchivo = "crearTablas";
        public static string cargaCrearBaseDatosArchivo = "crearBaseDatos";
        public static string cargaDiariaArchivo = "recargaDiaria";
        public static string cargaDiariaDirectorio = "bitacoraRecargaDiaria";
        public static string recargaManualDirectorio = "bitacoraRecargaManual";
        public static string descargaDirectorio = "bitacoraDescarga";

        public static string recargaManualArchivo(string ptable)
        {
            return "recargaManual" + ptable;
        }

        public static string recargaManualIndividualArchivo(string ptable)
        {
            return recargaManualArchivo(ptable) + "(CargaIndividual)";
        }        
    }
}
