using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol
{
    public interface ITestConnectionSROL
    {
        Task<bool> testConnectionBoolean(Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pshowMessage);

        Task testConnectionString(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pshowMessage);
    }
}
