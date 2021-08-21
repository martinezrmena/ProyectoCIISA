using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.Print
{
    public interface IServicio_Print
    {
        Task connect(string address);
        void disconnect();        
        Task print(string texto);
        Task printBarCode(string code);
        Task<bool> HayImpresorasConectadas();
        bool ImpresoraFunciona();
    }
}
