using CIISA.RetailOnLine.Droid.Framework.Handheld.DataBase;
using System.Runtime.CompilerServices;

namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Connection
{
    public class SingletonConnectionSQLCE
    {
        private static ConnectionStringDB v_objConnectionStringDB = null;
        static readonly object v_padlock = new object();

        public static ConnectionStringDB getObjConnectionString()
        {
            if (v_objConnectionStringDB == null)
            {
                createConnectionString();
            }

            return v_objConnectionStringDB;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void createConnectionString()
        {
            try
            {
                lock (v_padlock)
                {
                    if (v_objConnectionStringDB == null)
                    {
                        v_objConnectionStringDB = new ConnectionStringDB();
                    }
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (System.Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                throw;
            }
            
        }
    }
}