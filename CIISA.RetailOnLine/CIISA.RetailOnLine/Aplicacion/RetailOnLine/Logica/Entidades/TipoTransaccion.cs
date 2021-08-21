using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class TipoTransaccion
    {

        private string v_nombre = string.Empty;

        private string v_sigla = string.Empty;

        public TipoTransaccion()
        {

        }

        public TipoTransaccion(TipoTransaccion tipo)
        {
            this.v_nombre = tipo.v_nombre;
            this.v_sigla = tipo.v_sigla;
        }

        public void SetNombre(string pnombre)
        {
            v_nombre = pnombre;

            Logica_ManagerTipoTransaccion _managerTipoTransaccion = new Logica_ManagerTipoTransaccion();

            v_sigla = _managerTipoTransaccion.obtenerCodigoTipoTransaccion(pnombre);
        }

        public void SetSigla(string psigla)
        {
            v_sigla = psigla;

            Logica_ManagerTipoTransaccion _managerTipoTransaccion = new Logica_ManagerTipoTransaccion();

            v_nombre = _managerTipoTransaccion.obtenerDescripcionTipoTransaccion(psigla);
        }

        public string GetNombre()
        {
            return v_nombre;
        }

        public string GetSigla()
        {
            return v_sigla;
        }

    }
}
