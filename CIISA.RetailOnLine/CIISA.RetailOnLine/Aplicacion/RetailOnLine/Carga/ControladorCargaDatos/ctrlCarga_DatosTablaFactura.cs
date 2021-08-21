using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
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

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.HelperCargaDatos
{
    public class ctrlCarga_DatosTablaFactura
    {
        private Carga_ManagerDatosTabla v_managerDatosTabla { get; set; }

        public ctrlCarga_DatosTablaFactura(Carga_ManagerDatosTabla pmanagerDatosTabla)
        {
            v_managerDatosTabla = pmanagerDatosTabla;
        }

        private async Task cargaInformacionFactura_inicial(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaFactura_inicial(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionFactura_inicial", TypeTransaction._upload, _memoryStream, TablesROL._factura);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaFactura(ptable, _dt, ptextBox, plabel, plog);
                }
            }
        }

        private async Task cargaInformacionFactura_seleccionTablas(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaFactura_seleccionTablas(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionFactura_seleccionTablas", TypeTransaction._upload, _memoryStream, TablesROL._factura);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaFactura(ptable, _dt, ptextBox, plabel, plog);
                }
            }
        }

        private async Task cargaInformacionFactura_recargaDiaria(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaFactura_recarga_diaria(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionFactura_recargaDiaria", TypeTransaction._upload, _memoryStream, TablesROL._factura);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    /*Edwin Vargas V. 16/11/2017
                      Se cambia el procedimiento para borrar el contenido de la tabla factura
                      La forma anterior no limpiaba por completo la tabla dejando factuas que aparecian vencidas cuando en NAF ya no lo estaban 
                      
                      OperationSQL.deleteSpecificTable(
                        TablesROL._factura,
                        TableFactura._CREADA,
                        SQL._NAF,
                        ptextBox,
                        plog
                        );
                     */

                    OperationSQL.deleteTableFeedbackTextBox(
                        TablesROL._factura,
                        ptextBox,
                        plog
                        );

                    await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaFactura(ptable, _dt, ptextBox, plabel, plog);
                }
            }
        }

        internal async Task cargaInformacionFactura(
            string ptable,
            Editor ptextBox,
            Label plabel,
            int ptipoCarga,
            Log plog
            )
        {
            if (ptipoCarga == VariableCarga._inicial)
            {
                await cargaInformacionFactura_inicial(ptable, ptextBox, plabel, plog);
            }

            if (ptipoCarga == VariableCarga._seleccionTablas)
            {
                await cargaInformacionFactura_seleccionTablas(ptable, ptextBox, plabel, plog);
            }

            if (ptipoCarga == VariableCarga._recargaDiaria)
            {
                await cargaInformacionFactura_recargaDiaria(ptable, ptextBox, plabel, plog);
            }
        }
    }
}
