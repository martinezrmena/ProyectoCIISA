using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Compress
{
    public interface IMC_HH
    {
        DataTable Unzip_HH_DataTable(string pdirectoryName, string ptypeTransaction, string pmemoryStream, string ptableName);

        string Zip_HH_DataSet(string pdirectoryName, string ptypeTransaction, DataSet pds);
    }
}
