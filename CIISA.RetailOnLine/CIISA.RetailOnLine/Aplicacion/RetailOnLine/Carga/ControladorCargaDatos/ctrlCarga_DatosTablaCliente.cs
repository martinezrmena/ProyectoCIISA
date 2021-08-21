using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Server.WS;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.ControladorCargaDatos
{
    public class ctrlCarga_DatosTablaCliente
    {
        private Carga_ManagerDatosTabla v_managerDatosTabla { get; set; }

        public ctrlCarga_DatosTablaCliente(Carga_ManagerDatosTabla pmanagerDatosTabla)
        {
            v_managerDatosTabla = pmanagerDatosTabla;
        }

        public async Task cargaInformacionClienteFacturacionFaltantes(
            string ptable,
            Editor ptextBox,
            Label plabel,
            string pcodCliente,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            //string _memoryStream = servicio.Get_cargaClienteFacturacionFaltantes(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);
            //validacion de 50mts
            string _memoryStream = servicio.cargaClienteFacturacionFaltantesGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionClienteFacturacionFaltantes", TypeTransaction._upload, _memoryStream, ptable);

                if (DataTableValidate.validateDataTable(_dt, ptable + SubjectTagEmail._facturacionFaltantes, ptextBox, plog))
                {
                    OperationSQL.deleteSpecificTable(
                         ptable,
                         TableCliente._NO_CLIENTE,
                         pcodCliente,
                         ptextBox,
                         plog
                         );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaClienteFacturacionFaltantes(ptable, _dt, ptextBox, plabel, plog);
                }
            }
        }

        internal async Task cargaInformacionCliente(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            //string _memoryStream = servicio.Get_cargaCliente(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
            //Validacion 50mts
            string _memoryStream = servicio.cargaClienteGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionCliente", TypeTransaction._upload, _memoryStream, TablesROL._cliente);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaCliente(ptable, _dt, ptextBox, plabel, plog);
                }
            }
        }
    }
}
