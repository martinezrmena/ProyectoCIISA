using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Scanner
{
    public interface IQScanningService
    {
        Task<string> ScanAsync();
    }
}
