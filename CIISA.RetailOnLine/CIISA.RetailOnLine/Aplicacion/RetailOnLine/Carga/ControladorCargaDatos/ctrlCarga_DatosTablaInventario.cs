using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
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
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.ControladorCargaDatos
{
    public class ctrlCarga_DatosTablaInventario
    {
        private Carga_ManagerDatosTabla v_managerDatosTabla { get; set; }

        public ctrlCarga_DatosTablaInventario(Carga_ManagerDatosTabla pmanagerDatosTabla)
        {
            v_managerDatosTabla = pmanagerDatosTabla;
        }

        internal async Task cargaInformacionInventario(string ptable,Editor ptextBox,Label plabel,bool pcondicionCarga,Log plog)
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaInventario_inicial(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionInventario", TypeTransaction._upload, _memoryStream, TablesROL._inventario);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    if (pcondicionCarga)
                    {
                        OperationSQL.deleteTableFeedbackTextBox(
                            ptable,
                            ptextBox,
                            plog
                            );

                        await v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaInventario(ptable, _dt, ptextBox, plabel, plog);
                    }
                    else
                    {
                        HelperRecargaDatosTabla _helper = new HelperRecargaDatosTabla();

                        _helper.recargaTablaInventario(ptable, _dt, ptextBox, plog);
                    }
                }
            }

        }

        internal void cargaInformacionInventario(
            string ptable,
            Editor ptextBox,
            Label plabel,
            int ptipoCarga,
            Log plog
            )
        {
            if (ptipoCarga == VariableCarga._inicial)
            {
                cargaInformacionInventario_inicial(ptable, ptextBox, plabel, plog);
            }

            if (ptipoCarga == VariableCarga._seleccionTablas)
            {
                cargaInformacionInventario_seleccionTablas(ptable, ptextBox, plabel, plog);
            }

            if (ptipoCarga == VariableCarga._recargaDiaria)
            {
                cargaInformacionInventario_recargaDiaria(ptable, ptextBox, plabel, plog);
            }

            if (ptipoCarga == VariableCarga._sincronizacion)
            {
                cargaInformacionInventario_sincronizacion(ptable, ptextBox, plabel, plog);
            }
        }

        internal void cargaInformacionInventario_inicial(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaInventario_inicial(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionInventario_inicial", TypeTransaction._upload, _memoryStream, TablesROL._inventario);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                    v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaInventario(ptable, _dt, ptextBox, plabel, plog);
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                }
            }

        }

        internal void cargaInformacionInventario_seleccionTablas(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaInventario_seleccionTablas(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionInventario_seleccionTablas", TypeTransaction._upload, _memoryStream, TablesROL._inventario);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                    v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaInventario(ptable, _dt, ptextBox, plabel, plog);
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                }
            }
        }

        internal void cargaInformacionInventario_recargaDiaria(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaInventario_recarga_diaria(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionInventario_recargaDiaria", TypeTransaction._upload, _memoryStream, TablesROL._inventario);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    HelperRecargaDatosTabla _helper = new HelperRecargaDatosTabla();

                    _helper.recargaTablaInventario(ptable, _dt, ptextBox, plog);
                }
            }
        }

        internal void cargaInformacionInventario_sincronizacion(
            string ptable,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            var servicio = DependencyService.Get<IService_WebServiceUpload>();

            string _memoryStream = servicio.Get_cargaInventario_sincronizacion(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (!_memoryStream.Equals(TypeEvents._errorWS))
            {
                plog.addLineSuccessWSDownload(ptextBox, ptable);

                var _memoryCompress = DependencyService.Get<IMC_HH>();

                DataTable _dt = _memoryCompress.Unzip_HH_DataTable("cargaInformacionInventario_sincronizacion", TypeTransaction._upload, _memoryStream, TablesROL._inventario);

                if (DataTableValidate.validateDataTable(_dt, ptable, ptextBox, plog))
                {
                    OperationSQL.deleteTableFeedbackTextBox(
                        ptable,
                        ptextBox,
                        plog
                        );

#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                    v_managerDatosTabla.v_helperCargaDatosTabla.cargaTablaInventario(ptable, _dt, ptextBox, plabel, plog);
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                }
            }
        }
    }
}
