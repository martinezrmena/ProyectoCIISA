using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario
{
    internal class LogicaIdentificarUsuario
    {
        private ctrlIdentificarUsuario controlador = null;

        internal LogicaIdentificarUsuario(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        internal string obtenerCodigoCompannia(int pselectedIndex)
        {
            string _codCompannia = string.Empty;

            switch (pselectedIndex)
            {
                case 0:
                    _codCompannia = "01";
                    break;
                case 1:
                    _codCompannia = "02";
                    break;
                case 2:
                    _codCompannia = "03";
                    break;
                case 3:
                    _codCompannia = "04";
                    break;
            }

            return _codCompannia;
        }

        internal void establecerVariableDeEntorno_Agente(DataTable pdt)
        {
            foreach (DataRow _fila in pdt.Rows)
            {
                GlobalVariables.AgentVariables(
                    _fila[TableAgenteVendedor._NO_CIA].ToString(),
                    _fila[TableAgenteVendedor._NO_RUTA].ToString(),
                    _fila[TableAgenteVendedor._NO_AGENTE].ToString(),
                    _fila[TableAgenteVendedor._NO_EMPLE].ToString(),
                    _fila[TableAgenteVendedor._CODIGO_SECTOR].ToString(),
                    _fila[TableAgenteVendedor._NOM_AGENTE].ToString(),
                    _fila[TableAgenteVendedor._CLIENTE_NUEVO].ToString(),
                    _fila[TableAgenteVendedor._TIPO_AGENTE].ToString()
                    );
            }
        }
    }
}
