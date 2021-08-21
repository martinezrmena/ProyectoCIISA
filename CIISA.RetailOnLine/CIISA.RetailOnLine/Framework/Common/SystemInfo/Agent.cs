namespace CIISA.RetailOnLine.Framework.Common.SystemInfo
{
    public class Agent
    {
        public const string _supermercadoSigla = "S";
        public const string _ruteroSigla = "R";
        public const string _cobradorSigla = "O";
        public const string _carniceroSigla = "C";
        public const string _supermercadoNombre = "Supermercado";
        public const string _ruteroNombre = "Rutero";
        public const string _cobradorNombre = "Cobrador";
        public const string _carniceroNombre = "Carnicero";
        public const int _codigoEstablecimientoRuteros = 0;

        private static string _codCompannia = string.Empty;
        public static string getCodCompannia()
        {
            return _codCompannia;
        }
        public static void setCodCompannia(string pcodCompannia)
        {
            _codCompannia = pcodCompannia;
        }

        private static string _codRute = string.Empty;
        public static string getCodRute()
        {
            return _codRute;
        }    
        public static void setCodRute(string pcodRute)
        {
            _codRute = pcodRute;
        }

        private static string _codAgente = string.Empty;
        public static string getCodAgente()
        {
            return _codAgente;
        }
        public static void setCodAgente(string pcodAgente)
        {
            _codAgente = pcodAgente;
        }

        private static string _codEmpleado = string.Empty;
        public static string getCodEmpleado()
        {
            return _codEmpleado;
        }
        public static void setCodEmpleado(string pcodEmpleado)
        {
            _codEmpleado = pcodEmpleado;
        }

        private static string _codSector = string.Empty;
        public static string getCodSector()
        {
            return _codSector;
        }
        public static void setCodSector(string pcodSector)
        {
            _codSector = pcodSector;
        }

        private static string _nombreAgente = string.Empty;
        public static string getNombreAgente()
        {
            return _nombreAgente;
        }
        public static void setNombreAgente(string pnombreAgente)
        {
            _nombreAgente = pnombreAgente;
        }

        private static string _codClienteGenerico = string.Empty;
        public static string getCodClienteGenerico()
        {
            return _codClienteGenerico;
        }
        public static void setCodClienteGenerico(string pcodClienteGenerico)
        {
            _codClienteGenerico = pcodClienteGenerico;
        }

        private static string _tipoAgente = string.Empty;
        public static string getTipoAgente()
        {
            return _tipoAgente;
        }
        public static void setTipoAgente(string ptipoAgente)
        {
            _tipoAgente = ptipoAgente;
        }
    }
}
