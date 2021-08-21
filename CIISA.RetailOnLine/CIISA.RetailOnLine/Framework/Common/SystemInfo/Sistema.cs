namespace CIISA.RetailOnLine.Framework.Common.SystemInfo
{
    public static class Sistema
    {
        public static string _name = string.Empty;
        public static string _initials = string.Empty;
        public static string _version = "v.1.0.0.0";
        public static string _typeAgent = string.Empty;

        public static SystemCIISA establecerObjetoSystemCIISA()
        {
            SystemCIISA _systemCIISA = new SystemCIISA(Agent.getCodCompannia(),Agent.getCodAgente(),Agent.getCodRute(),Agent.getTipoAgente(),_name,_initials,_version);
            return _systemCIISA;
        }

        public static SystemCIISA establecerObjetoSystemCIISA(string pversion)
        {
            SystemCIISA _systemCIISA = new SystemCIISA(Agent.getCodCompannia(),Agent.getCodAgente(),Agent.getCodRute(),Agent.getTipoAgente(),_name,_initials,pversion);
            return _systemCIISA;
        }
    }
}
