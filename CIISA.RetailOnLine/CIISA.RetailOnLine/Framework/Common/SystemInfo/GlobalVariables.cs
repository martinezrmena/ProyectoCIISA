namespace CIISA.RetailOnLine.Framework.Common.SystemInfo
{
    public static class GlobalVariables
    {
        public static Agent v_agent = new Agent();

        public static void AgentVariables_Temporal(string pcodCompannia,string pcodAgent)
        {
            Agent.setCodCompannia(pcodCompannia);

            Agent.setCodAgente(pcodAgent);
        }

        public static void SystemVariables(string pname, string pinitials, string pversion)
        {
            Sistema._name = pname;
            Sistema._initials = pinitials;
            Sistema._version = pversion;
        }

        public static void AgentVariables(string pcodCompannia,string pcodRute,string pcodAgent,string pcodEmployee,string pcodSector,string pagentName,string pcodGenericClient,string ptypeAgent)
        {
            Agent.setCodCompannia(pcodCompannia);

            Agent.setCodRute(pcodRute);

            Agent.setCodAgente(pcodAgent);

            Agent.setCodEmpleado(pcodEmployee);

            Agent.setCodSector(pcodSector);

            Agent.setNombreAgente(pagentName);

            Agent.setCodClienteGenerico(pcodGenericClient);

            Agent.setTipoAgente(ptypeAgent);
        }
    }
}
