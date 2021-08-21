using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    public class HelperMasterTable
    {
        /// <summary>
        ///  Función que identifica si una tabla existe
        /// </summary>
        /// <param name="table_name">Nombre de la tabla a buscará</param>
        /// <returns>Un bool que permitira identificar si existe o no</returns>
        internal bool ExistsTable(string table_name)
        {
            table_name = table_name.ToUpper();

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT * FROM sqlite_master WHERE type ");
            _sb.Append("= 'table' AND tbl_name =  ");
            _sb.Append("'" + table_name + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            try
            {
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        ///  Función que identifica si un campo existe en una tabla
        /// </summary>
        /// <param name="table_name">Nombre de la tabla donde se buscará el campo</param>
        /// <param name="campo">Nombre del campo a buscar</param>
        /// <returns>Un bool que permitira identificar si existe o no</returns>
        internal bool ExistsCampo(string table_name, string campo)
        {
            table_name = table_name.ToUpper();
            campo = campo.ToUpper();

            StringBuilder _sb = new StringBuilder();

            _sb.Append("PRAGMA table_info('"+table_name+"')");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            try
            {
                if (_dt.Rows.Count > 0)
                {

                    var Consulta = DependencyService.Get<IFillDataSet_Table>();

                    return Consulta.ExistsCampo(_dt, campo, "name");

                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
