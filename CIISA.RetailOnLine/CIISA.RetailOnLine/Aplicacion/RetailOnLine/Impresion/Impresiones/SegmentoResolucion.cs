using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoResolucion
    {

        public void Resolucion(List<string> plistaLineasImpresion) {

            Line _line = new Line();

            string _mensajeUno =
                "Resolución No DGT-R-13-2017 Articulo 15."
                + Environment.NewLine
                + "Este comprobante provisional no puede ser"
                + Environment.NewLine
                + "utilizado para el respaldo de créditos"
                + Environment.NewLine
                + "fiscales ni como gastos deducibles.";

            _line.printingLinesList(
                        plistaLineasImpresion,
                        _mensajeUno,
                        1);

        }

    }
}
