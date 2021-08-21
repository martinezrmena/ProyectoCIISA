namespace CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol
{
    public interface IService_WebServiceTotalDownload
    {
        string Get_TotalSend_Automatic(string pdatosSROL, string ptipoRutero, bool ptomaFisica, Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pwriteDataTables);

        string Get_TotalSend(string pdatosSROL, string ptipoRutero, bool ptomaFisica, CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pwriteDataTables);
    }
}
