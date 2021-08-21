namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class Direccion
    {
        public string v_pais { get; set; }
        public string v_provincia { get; set; }
        public string v_provinciaNombre { get; set; }
        public string v_canton { get; set; }
        public string v_cantonNombre { get; set; }

        public string v_provinciaApo { get; set; }
        public string v_provinciaApoNombre { get; set; }
        public string v_cantonApo { get; set; }
        public string v_cantonApoNombre { get; set; }

        public string v_distrito { get; set; }
        public string v_distritoNombre { get; set; }

        public string v_distritoApo { get; set; }
        public string v_distritoApoNombre { get; set; }

        public string v_direccion { get; set; }

        public string v_direccionApo { get; set; }

        public Direccion()
        {
            v_pais = string.Empty;
            v_provincia = string.Empty;
            v_provinciaNombre = string.Empty;
            v_canton = string.Empty;
            v_cantonNombre = string.Empty;
            v_distrito = string.Empty;
            v_distritoNombre = string.Empty;
            v_provinciaApo = string.Empty;
            v_provinciaApoNombre = string.Empty;
            v_cantonApo = string.Empty;
            v_cantonApoNombre = string.Empty;
            v_distritoApo = string.Empty;
            v_distritoApoNombre = string.Empty;
            v_direccion = string.Empty;
            v_direccionApo = string.Empty;
        }

    }
}
