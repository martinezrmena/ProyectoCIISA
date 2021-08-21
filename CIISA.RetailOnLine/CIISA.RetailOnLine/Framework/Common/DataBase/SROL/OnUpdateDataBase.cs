using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.DataBase.SROL
{
    public class OnUpdateDataBase
    {

        protected AplicationData aplication = new AplicationData();

        /// <summary>
        ///La principal idea de esta clase es suplantar a los metodos sobre escritos
        ///onupdatedatabase de Android, Deberán actualizarse e ir creciendo de acuerdo
        ///a las futuras necesidades del aplicativo
        /// </summary>
        public void UpdateDatabase()
        {
            string v = DependencyService.Get<IAppVersion>().GetVersion();
            int b = DependencyService.Get<IAppVersion>().GetBuild();
            HelperTableMaster helper = new HelperTableMaster();

            if (v.Equals(aplication.LastUpdatedVersion) && b.Equals(aplication.LastUpdatedBuild))
            {
                //Validamos si es necesario agregar una tabla
                if (!helper.ValidateTable(TablesROL._indAnulacion))
                {
                    Carga_ManagerEstablecerTabla _manager = new Carga_ManagerEstablecerTabla(TablesROL._indAnulacion);
                }

                //Validamos si es necesario agregar una columna a alguna tabla
                if (!helper.ValidateCampo(TablesROL._factura, TableFactura._FECHA_INSERT))
                {
                    Carga_ManagerAlterTable _manager = new Carga_ManagerAlterTable(TablesROL._factura);
                }

            }
        }


    }
}
