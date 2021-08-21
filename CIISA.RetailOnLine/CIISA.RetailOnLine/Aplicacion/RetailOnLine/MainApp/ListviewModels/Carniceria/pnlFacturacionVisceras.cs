namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria
{
    public class pnlFacturacionVisceras
    {
        public string TIPOCANAL { get; set; }
        public string TIPOVICERAS { get; set; }
        public bool EXTRAVICERAS { get; set; }
        public pnlTransacciones_ltvDetalleReses DETALLERES { get; set; }

        public pnlFacturacionVisceras()
        {
            TIPOCANAL = string.Empty;
            TIPOVICERAS = string.Empty;
            EXTRAVICERAS = false;
            DETALLERES = new pnlTransacciones_ltvDetalleReses();
        }
    }
}
