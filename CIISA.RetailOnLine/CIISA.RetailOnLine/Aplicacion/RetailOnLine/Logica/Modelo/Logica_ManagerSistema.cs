using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerSistema
    {
        public string buscarEstado()
        {
            HelperSistema _multiSistema = new HelperSistema();
            string _maxFecha = _multiSistema.buscarMaximaFecha();

            string _state = string.Empty;

            if (!_maxFecha.Equals(string.Empty))
            {
                DateTime _fecha = FormatUtil.covertStringToDateTimeWithoutTime(_maxFecha);

                _state = _multiSistema.buscarEstado(_fecha);
            }

            return _state;
        }

        public void actualizarEstado(bool pbloquear)
        {
            HelperSistema _multiSistema = new HelperSistema();

            _multiSistema.actualizarEstado(pbloquear);
        }

        public string buscarMaximaFecha()
        {
            HelperSistema _multiSistema = new HelperSistema();

            return _multiSistema.buscarMaximaFecha();
        }
    }
}
