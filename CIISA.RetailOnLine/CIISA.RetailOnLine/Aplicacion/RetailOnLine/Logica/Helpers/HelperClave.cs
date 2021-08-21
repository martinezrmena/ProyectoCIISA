using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Text;
using Xamarin.Forms;


namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperClave
    {
        internal string obtenerCodigoPrincipal()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableClave._PRINCIPAL + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._clave);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerCodigoTomaFisica()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableClave._TOMA_FISICA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._clave);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerCodigoFaltantes()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableClave._FALTANTES + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._clave);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerCodigoConsecutivos()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableClave._CONSECUTIVO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._clave);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string mostrarClaves()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("Principal: ");
            _sb.Append(obtenerCodigoPrincipal());
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);

            _sb.Append("Consecutivos: ");
            _sb.Append(obtenerCodigoConsecutivos());
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);

            _sb.Append("Toma Física: ");
            _sb.Append(obtenerCodigoTomaFisica());
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);

            _sb.Append("Faltantes: ");
            _sb.Append(obtenerCodigoFaltantes());

            return _sb.ToString();
        }
    }
}
