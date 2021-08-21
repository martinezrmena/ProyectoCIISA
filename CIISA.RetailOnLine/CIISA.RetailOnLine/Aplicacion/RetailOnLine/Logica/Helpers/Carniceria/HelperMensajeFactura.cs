using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria
{
    internal class HelperMensajeFactura
    {
        internal string buscarMensajeEspecial()
        {
            StringBuilder _sb = new StringBuilder();

            string mensaje = string.Empty;

            _sb.Append("SELECT ");
            _sb.Append(TableMensajeFactura._COMENTARIO + ", ");
            _sb.Append(TableMensajeFactura._COMENTARIO1 + ", ");
            _sb.Append(TableMensajeFactura._COMENTARIO2 + ", ");
            _sb.Append(TableMensajeFactura._COMENTARIO3 + ", ");
            _sb.Append(TableMensajeFactura._COMENTARIO4 + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._MensajeFactura + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DatosMensaje = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in DatosMensaje.Rows)
            {
                string variable = string.Empty;

                variable = _fila["COMENTARIO"].ToString();

                if (!string.IsNullOrEmpty(variable))
                {
                    mensaje = variable;
                    mensaje += Environment.NewLine;
                }

                variable = _fila["COMENTARIO1"].ToString();

                if (!string.IsNullOrEmpty(variable))
                {
                    mensaje += variable;
                    mensaje += Environment.NewLine;
                }

                variable = _fila["COMENTARIO2"].ToString();

                if (!string.IsNullOrEmpty(variable))
                {
                    mensaje += variable;
                    mensaje += Environment.NewLine;
                }

                variable = _fila["COMENTARIO3"].ToString();

                if (!string.IsNullOrEmpty(variable))
                {
                    mensaje += variable;
                    mensaje += Environment.NewLine;
                }

                variable = _fila["COMENTARIO4"].ToString();

                if (!string.IsNullOrEmpty(variable))
                {
                    mensaje += variable;
                    mensaje += Environment.NewLine;
                }

            }

            return mensaje;
        }
    }
}
