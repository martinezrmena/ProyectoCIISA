using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Framework.Handheld.Connection
{
    public interface IMultiGeneric
    {
        string readStringText(StringBuilder psentence);
        DataTable uploadDataTable(StringBuilder psentence);
        int updateRecord(StringBuilder psentence);
        int deleteTable(StringBuilder psentence);
        void CloseSession();
        void insertRecord(StringBuilder psentence);
        void BeginTransaction();
        decimal readDecimal(StringBuilder psentence);
        void Commit();
        void Rollback();
        int uploadGenericTable(StringBuilder psentence);
        void InsertRecordBackUp(StringBuilder psentence);
        int establishTable(StringBuilder psentence);
    }
}
