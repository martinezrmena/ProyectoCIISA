using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class Factura
    {

        public string v_noFisico { get; set; }
        public decimal v_monto { get; set; }
        public decimal v_montoAbonos { get; set; }
        public decimal v_saldo { get; set; }
        public DateTime v_fechaDocumento { get; set; }
        public DateTime v_fechaVence { get; set; }
        public Int16 v_diasVencida { get; set; }

        public Factura()
        {
            v_noFisico = string.Empty;

            v_monto = Numeric._zeroDecimalInitialize;

            v_montoAbonos = Numeric._zeroDecimalInitialize;

            v_saldo = Numeric._zeroDecimalInitialize;

            v_fechaDocumento = VarTime.getNow();

            v_fechaVence = VarTime.getNow();

            v_diasVencida = 0;
        }

    }
}
