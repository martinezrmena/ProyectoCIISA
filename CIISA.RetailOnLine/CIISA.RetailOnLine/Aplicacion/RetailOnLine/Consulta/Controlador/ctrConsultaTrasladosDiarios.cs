using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Server.WS;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador
{
    public class ctrConsultaTrasladosDiarios
    {
        public async Task<DataTable> ConsultaTrasladosDiarios()
        {
            DataTable _dt = new DataTable();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                var _servicio = DependencyService.Get<IService_WebServiceConsultation>();

                string _memoryStream = _servicio.Get_consultaInventarioDocumento(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    _dt = _memoryCompress.Unzip_HH_DataTable("ConsultaTrasladosDiarios", TypeTransaction._select, _memoryStream, "EncabezadoInvDoc");
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
