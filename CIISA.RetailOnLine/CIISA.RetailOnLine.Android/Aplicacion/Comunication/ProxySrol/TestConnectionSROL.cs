using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(TestConnectionSROL))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol
{
    public class TestConnectionSROL: ITestConnectionSROL
    {
        private ProxySROL _proxySROL = null;

        private void initializeProxySROL()
        {
            if (_proxySROL == null)
            {
                _proxySROL = new ProxySROL();
            }
        }

        public ProxySROL getProxySROL()
        {
            initializeProxySROL();

            return _proxySROL;
        }

        public async Task<bool> testConnectionBoolean(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pshowMessage)
        {
            bool _connect = false;

            initializeProxySROL();

            try
            {
                _connect = _proxySROL.webServiceUpload(psystemCIISA).testConnection(HomologateSystemCIISA_ROL.Upload(psystemCIISA));

                _proxySROL.webServiceUpload(psystemCIISA).Dispose();
            }
            catch (Exception ex)
            {
                if (pshowMessage)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            return _connect;
        }

        public async Task testConnectionString(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pshowMessage)
        {
            try
            {
                initializeProxySROL();

                await LogMessageSuccess._generalSuccess(_proxySROL.webServiceUpload(psystemCIISA).test(
                    HomologateSystemCIISA_ROL.Upload(psystemCIISA)
                    ));

                _proxySROL.webServiceUpload(psystemCIISA).Dispose();
            }
            catch (Exception ex)
            {
                if (pshowMessage)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

    }
}