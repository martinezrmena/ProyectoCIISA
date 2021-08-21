using CIISA.RetailOnLine.Droid.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Droid.Framework.Handheld.DataBase;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(EstablishDatabase))]
namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Connection
{
    public class EstablishDatabase : IEstablishDatabase
    {
        public void thereDataBase()
        {
            try
            {
                ConnectionStringDB _objStringConnection = SingletonConnectionSQLCE.getObjConnectionString();

                Variable._thereDataBase = File.Exists(_objStringConnection.getDataBasePath());
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (System.Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                throw;
            }            
        }

        public void createDataBase(Editor ptextBox,Log plog)
        {
            if (!Variable._thereDataBase)
            {
                ConnectionStringDB _objStringConnection = SingletonConnectionSQLCE.getObjConnectionString();
                //SqlCeEngine _engine = new SqlCeEngine(_objStringConnection.getStringConnection());
                //_engine.CreateDatabase();
                //_engine.Dispose();

                File.Create(_objStringConnection.getDataBasePath());

                plog.addSuccessLine(
                    ptextBox,
                    "creó la base de datos " + _objStringConnection.getDataBase(),
                    1
                    );

            }
        }
    }
}