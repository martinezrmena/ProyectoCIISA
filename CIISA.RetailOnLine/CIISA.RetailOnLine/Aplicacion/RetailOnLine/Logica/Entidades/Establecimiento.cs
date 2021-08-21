namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class Establecimiento
    {

        public int v_codEstablecimiento { get; set; }
        public string v_descEstablecimiento { get; set; }
        public string v_direccion { get; set; }
        public string v_codLocalizacion { get; set; }
        public string v_direccionExacta { get; set; }
        public Indicador v_objIndicador = new Indicador();

        public Establecimiento()
        {
            v_codEstablecimiento = 0;
            v_descEstablecimiento = string.Empty;
            v_direccion = string.Empty;
            v_codLocalizacion = string.Empty;
            v_direccionExacta = string.Empty;
            v_objIndicador = new Indicador();
        }

    }
}
