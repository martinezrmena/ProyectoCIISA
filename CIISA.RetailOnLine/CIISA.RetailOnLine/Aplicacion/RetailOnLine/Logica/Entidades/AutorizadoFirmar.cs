namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class AutorizadoFirmar
    {

        public string v_noCia { get; set; }

        public string v_grupo { get; set; }

        public string v_codCliente { get; set; }

        public string v_cedula { get; set; }

        public string v_nombre { get; set; }


        public AutorizadoFirmar()
        {
            v_noCia = string.Empty;

            v_grupo = string.Empty;

            v_codCliente = string.Empty;

            v_cedula = string.Empty;

            v_nombre = string.Empty;
        }
    }
}
