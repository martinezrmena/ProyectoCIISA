using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerSms
    {

        public bool enviarCoordenadas(string ptipoDocumento)
        {
            HelperSms _multiSms = new HelperSms();

            return _multiSms.enviarCoordenadas(ptipoDocumento);
        }

        public bool enviarSms(string ptipoDocumento)
        {
            HelperSms _multiSms = new HelperSms();

            return _multiSms.enviarSms(ptipoDocumento);
        }

        public string buscarTelefono_1(string ptipoDocumento)
        {
            HelperSms _multiSms = new HelperSms();

            return _multiSms.buscarTelefono_1(ptipoDocumento);
        }

        public string buscarTelefono_2(string ptipoDocumento)
        {
            HelperSms _multiSms = new HelperSms();

            return _multiSms.buscarTelefono_2(ptipoDocumento);
        }

        public string buscarTelefono_3(string ptipoDocumento)
        {
            HelperSms _multiSms = new HelperSms();

            return _multiSms.buscarTelefono_3(ptipoDocumento);
        }

        public DataTable buscarSms_DT(string ptipoDocumento)
        {
            HelperSms _multiSms = new HelperSms();

            return _multiSms.buscarSms_DT(ptipoDocumento);
        }

    }
}
