using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperAgenteVendedor
    {
        internal StringBuilder insertTablaAgenteVendedor(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._agenteVendedor);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._NOM_AGENTE));

            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._NO_RUTA));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._USUARIO));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONTRASENNA));

            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_DOC));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_REC));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_CLI));

            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_OV));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_P));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_DV));

            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_RG));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_RC));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_AN));

            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_TR));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_PD));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._TIPO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CLIENTE_NUEVO));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._TIPO_IMPRESORA));

            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CODIGO_SECTOR));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._NO_EMPLE));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CODIGO_CLIENTE));
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_VT);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("( ");
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.NO_AGENTE]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.NOM_AGENTE]));

            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.NO_RUTA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.USUARIO]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONTRASENNA]));

            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_DOC]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_REC]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_CLI]));

            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_OV]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_CO]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_DV]));

            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_RG]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_RC]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_AN]));

            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_TR]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CONSECUTIVO_PD]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.TIPO_AGENTE]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CLIENTE_NUEVO]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.TIPO_IMPRESORA]));

            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CODIGO_SECTOR]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.NO_EMPLE]));
            _sb.Append(string.Format("'{0}', ", pfila[TableVendedorNAF.CODIGO_CLIENTE]));
            _sb.Append(string.Format("'{0}'", pfila[TableVendedorNAF.CONSECUTIVO_VT]));
            _sb.Append(")");

            return _sb;
        }

        public bool ConsultarIndRutaGeo()
        {
            bool value = false;
            string result = string.Empty;

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            try
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("SELECT ");
                _sb.Append(TableAgenteVendedor._TIPO_AGENTE);
                _sb.Append(" FROM ");
                _sb.Append(TablesROL._agenteVendedor);

                result = MultiGeneric.readStringText(_sb);

                if (result.Equals(SQL._Si))
                {
                    value = true;
                }
            }
            catch (Exception ex)
            {
                
            }

            return value;
        }
    }
}
