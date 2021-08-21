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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.ControladorCargaDatos
{
    public class ctrlCarga_DatosTablaIndicadorFactura
    {
        private Carga_ManagerDatosTabla v_managerDatosTabla { get; set; }

        public ctrlCarga_DatosTablaIndicadorFactura(Carga_ManagerDatosTabla pmanagerDatosTabla)
        {
            v_managerDatosTabla = pmanagerDatosTabla;
        }

        public async Task cargaInformacionIndicadorFacturaFacturacionFaltantes(
            string pcodCliente,
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            //string _memoryStream = servicio.Get_cargaIndicadorFacturaFacturacionFaltantes(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);
            //validacion 50mts
            string _memoryStream = servicio.cargaIndicadorFacturaFacturacionFaltantesGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionIndicadorFacturaFacturacionFaltantes", TypeTransaction._upload, _memoryStream, ptable);

                if (DataTableValidate.validateDataTable(_dt, ptable + SubjectTagEmail._facturacionFaltantes, ptextBox, plog))
                {
                    OperationSQL.deleteSpecificTable(
                         ptable,
                         TableIndicadorFactura._NO_CLIENTE,
                         pcodCliente,
                         ptextBox,
                         plog
                         );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaIndicadorFacturaFacturacionFaltantes(ptable, _dt, ptextBox, plabel, plog);
                }
            }

        }

        public async Task cargaInformacionIndicadorFactura(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            //string _memoryStream = servicio.Get_cargaIndicadorFactura(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
            //Validacion 50mts
            string _memoryStream = servicio.cargaIndicadorFacturaGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionIndicadorFactura", TypeTransaction._upload, _memoryStream, TablesROL._indicadorFactura);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaIndicadorFactura(ptable, _dt, ptextBox, plabel, plog);
                }
            }

        }
    }
}
