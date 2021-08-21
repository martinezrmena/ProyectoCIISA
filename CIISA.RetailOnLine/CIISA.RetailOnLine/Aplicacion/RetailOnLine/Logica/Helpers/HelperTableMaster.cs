using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    public class HelperTableMaster
    {
        public bool ValidateTable(string table_name)
        {
            HelperMasterTable _helper = new HelperMasterTable();

            return _helper.ExistsTable(table_name);
        }

        public bool ValidateCampo(string table_name, string campoTabla) {

            HelperMasterTable _helper = new HelperMasterTable();

            return _helper.ExistsCampo(table_name, campoTabla);
        }

    }
}
