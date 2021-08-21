using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol
{
    public interface IService_WebServiceRute
    {
        string Get_eliminarInventario(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_generarRutaEInventario(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
    }
}
