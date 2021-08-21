namespace CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol
{
    public interface IService_WebServicePassedOn
    {
        string consultaOrdenesVentaTransmitidas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, decimal pdia);
        void Dispose();
        string consultaDocumentosTransmitidos(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, decimal pdia);
        string consultaRecibosRecaudacionesTransmitidas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, decimal pdia);
    }
}
