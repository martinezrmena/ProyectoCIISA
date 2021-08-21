using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class SplitString
    {
        public void Split_String(List<string> Value, string cadena) {

            if (cadena.Length > 47)
            {
                Value.Add(cadena.Substring(0, 48) + Environment.NewLine);

                if (!string.IsNullOrEmpty(cadena.Substring(48)))
                {
                    Value.Add(cadena.Substring(48) + Environment.NewLine);
                }
            }
            else {
                Value.Add(cadena + Environment.NewLine);
            }
        }

        public void Split_Message(List<string> Value, string cadena) {

            do {

                if (cadena.Length > 47)
                {
                    Value.Add(cadena.Substring(0, 48) + Environment.NewLine);

                    if (!string.IsNullOrEmpty(cadena.Substring(48)))
                    {
                        cadena = cadena.Substring(48);
                    }
                }
                else
                {
                    Value.Add(cadena + Environment.NewLine);
                    cadena = string.Empty;
                }

            } while (cadena.Length > 0);

        }

        public void Align_Message_Split(List<string> Value, string cadena)
        {
            Line line = new Line();
            string contracted = string.Empty;

            do
            {
                if (cadena.Length > 27)
                {
                    contracted = cadena.Substring(0, 28);

                    line.alignCenterMessage(
                        Value, 
                        contracted);

                    if (!string.IsNullOrEmpty(cadena.Substring(28)))
                    {
                        cadena = cadena.Substring(28);
                    }
                }
                else
                {
                    line.alignCenterMessage(Value, cadena);
                    cadena = string.Empty;
                }

            } while (cadena.Length > 0);

        }

    }
}
