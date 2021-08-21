using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol
{
    public interface IService_WebServiceDownload
    {
        string descargaCierreMaquina(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pestado, DateTime pfecha);
    }
}
