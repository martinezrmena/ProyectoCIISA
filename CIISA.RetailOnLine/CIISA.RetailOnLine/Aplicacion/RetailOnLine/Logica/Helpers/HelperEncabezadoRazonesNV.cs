using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEncabezadoRazonesNV
    {
        internal void guardarEncabezadoRazonesNV(string pcodCliente, string pcodRazonNV)
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            DateTime _fechaTomaFisica = _manager.buscarFechaTomaFisica();

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._encabezadoRazonesNV + " ");
            _sb.Append("(");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("CODIGO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("FECHA_RUTA, ");
            _sb.Append("FECHA_CREA_MAQUINA, ");
            _sb.Append("ENVIADO");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pcodCliente + "', ");
            _sb.Append("'" + pcodRazonNV + "', ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
            _sb.Append("'" + VarTime.dateSQLCE(_fechaTomaFisica) + "', ");
            _sb.Append("DATE('NOW'), ");
            _sb.Append("'" + SQL._No + "'");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }
    }
}
