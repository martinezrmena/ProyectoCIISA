using CIISA.RetailOnLine.Droid.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;
using System.Data;

[assembly: Dependency(typeof(Puente_MultiGeneric))]
namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Connection
{
    public class Puente_MultiGeneric : IMultiGeneric
    {
        public string readStringText(StringBuilder psentence)
        {
            return MultiGeneric.readStringText(psentence);
        }

        public DataTable uploadDataTable(StringBuilder psentence)
        {
            return MultiGeneric.uploadDataTable(psentence);
        }

        public int updateRecord(StringBuilder psentence)
        {
            return MultiGeneric.updateRecord(psentence);
        }

        public int deleteTable(StringBuilder psentence)
        {
            return MultiGeneric.deleteTable(psentence);
        }

        public void CloseSession()
        {
            MultiGeneric.CloseSession();
        }

        public void insertRecord(StringBuilder psentence)
        {
            MultiGeneric.insertRecord(psentence);
        }

        public void BeginTransaction()
        {
            MultiGeneric.BeginTransaction();
        }

        public decimal readDecimal(StringBuilder psentence)
        {
            return MultiGeneric.readDecimal(psentence);
        }

        public void Commit()
        {
            MultiGeneric.Commit();
        }

        public void Rollback()
        {
            MultiGeneric.Rollback();
        }

        public int uploadGenericTable(StringBuilder psentence)
        {
            return MultiGeneric.uploadGenericTable(psentence);
        }

        public void InsertRecordBackUp(StringBuilder psentence)
        {
            MultiGeneric.InsertRecordBackUp(psentence);
        }

        public int establishTable(StringBuilder psentence)
        {
            return MultiGeneric.establishTable(psentence);
        }
    }
}