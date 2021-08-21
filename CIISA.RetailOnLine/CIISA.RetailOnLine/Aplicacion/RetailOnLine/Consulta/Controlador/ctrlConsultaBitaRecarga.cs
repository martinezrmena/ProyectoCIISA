using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Server.WS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador
{
    public class ctrlConsultaBitaRecarga
    {
        public async Task<DataTable> ConsultaBitaRecarga()
        {
            DataTable _dt = null;

            var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                var ServicioUpload = DependencyService.Get<IService_WebServiceConsultation>();

                string _memoryStream = ServicioUpload.Get_consultaBitaRecarga(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    _dt = _memoryCompress.Unzip_HH_DataTable("ConsultaBitaRearga", TypeTransaction._select, _memoryStream, "ARDIBITA_RECARGA");
                }
                else
                {
                    throw new Exception();
                }
            }

            return _dt;
        }


    }
}
