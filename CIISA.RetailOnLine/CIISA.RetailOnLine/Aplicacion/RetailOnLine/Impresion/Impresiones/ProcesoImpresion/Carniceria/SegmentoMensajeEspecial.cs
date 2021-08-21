using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoMensajeEspecial
    {
        SplitString s = new SplitString();

        internal void Mensaje(List<string> pprintingLinesList)
        {
            string _message = string.Empty;

            HelperMensajeFactura helperMensaje = new HelperMensajeFactura();

            _message = helperMensaje.buscarMensajeEspecial();

            if (!string.IsNullOrEmpty(_message))
            {
                foreach (string singleline in Regex.Split(_message, Environment.NewLine))
                {
                    s.Align_Message_Split(pprintingLinesList, singleline);
                }

                Line _line = new Line();

                _line.printLineSpace(pprintingLinesList, 1);
            }

        }
    }
}
