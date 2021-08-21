namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador
{
    internal static class LanzadorMenu
    {
        internal static bool _mostrarMenuPrincipal = false;

        internal static bool _cargaDesdeCero = false;

        internal static bool _aperturaNocturna = false;

        internal static bool _aperturaDiaria = false;

        internal static void inicializarVariables()
        {
            _mostrarMenuPrincipal = false;
            _cargaDesdeCero = false;
            _aperturaNocturna = false;
            _aperturaDiaria = false;
        }

    }
}
