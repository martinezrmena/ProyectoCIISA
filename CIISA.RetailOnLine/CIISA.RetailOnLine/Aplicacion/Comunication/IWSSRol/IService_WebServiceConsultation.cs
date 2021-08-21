using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol
{
    public interface IService_WebServiceConsultation
    {
        bool Get_existeCliente(Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcedulaCliente);

        string Get_consultaClientePadronNacional(Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcedulaCliente);

        string Get_consultaInventarioDocumento(Framework.Common.SystemInfo.SystemCIISA psystemCIISA);

        string Get_consultaBitaRecarga(Framework.Common.SystemInfo.SystemCIISA psystemCIISA);

        bool estadoSistemaCerradoPorFecha(DataTable pFechaToma, CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
    }
}
