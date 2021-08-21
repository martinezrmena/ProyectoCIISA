namespace CIISA.RetailOnLine.Framework.Common.SystemInfo
{
    //[Serializable]
    public class SystemCIISA
    {
        public string _codCompany { get; set; }
        public string _codAgent { get; set; }
        public string _codRute { get; set; }
        public string _typeAgent { get; set; }

        public string _name { get; set; }
        public string _initials { get; set; }
        public string _version { get; set; }

        public SystemCIISA()
        {
            _codCompany = string.Empty;
            _codAgent = string.Empty;
            _codRute = string.Empty;
            _typeAgent = string.Empty;

            _name = string.Empty;
            _initials = string.Empty;
            _version = string.Empty;
        }

        public SystemCIISA(string pcodCompany,string pcodAgent,string pcodRute,string ptypeAgent,string pname,string pinitials,string pversion)
        {
            _codCompany = pcodCompany;
            _codAgent = pcodAgent;
            _codRute = pcodRute;
            _typeAgent = ptypeAgent;

            _name = pname;
            _initials = pinitials;
            _version = pversion;
        }

    }
}
