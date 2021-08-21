using CIISA.RetailOnLine.Framework.Common.Misc;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class FormaPago
    {
        public string _formaPago { get; set; }
        public decimal _monto { get; set; }
        public string _banco { get; set; }
        public string _numTransaccion { get; set; }
        public string _serie { get; set; }

        public FormaPago()
        {
            _formaPago = string.Empty;
            _monto = Numeric._zeroDecimalInitialize;
            _banco = string.Empty;
            _numTransaccion = string.Empty;
            _serie = string.Empty;
        }

    }
}
