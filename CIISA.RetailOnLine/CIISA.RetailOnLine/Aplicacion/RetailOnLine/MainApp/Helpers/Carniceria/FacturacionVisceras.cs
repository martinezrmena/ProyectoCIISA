using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.Carniceria
{
    public class FacturacionVisceras
    {
        internal pnlFacturacionVisceras viscera = new pnlFacturacionVisceras();
        internal Cliente _objCliente = null;
        public List<pnlTransacciones_ltvDetalleReses> detallesReses = new List<pnlTransacciones_ltvDetalleReses>();
        public List<string> CodProductosVisita = new List<string>();
        public List<string> CodDetalleReses = new List<string>();
        internal Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();
        internal vistaVisita vistaVisita = null;

        public FacturacionVisceras(vistaVisita pview, Cliente _cliente)
        {
            vistaVisita = pview;
            _objCliente = _cliente;
            BuscarDetallesResesListViewVisita();
        }

        /// <summary>
        /// Metodo encargado de corregir indicadores asignados desde visita en caso
        /// de cierres inesperados en la aplicación
        /// </summary>
        private void BuscarDetallesResesListViewVisita()
        {
            var Source = vistaVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;
            
            foreach (var item in Source)
            {
                if (logica.EsDetalleRes(item._pt_codigo))
                {
                    CodProductosVisita.Add(item._pt_codigo);
                }

                if (!string.IsNullOrEmpty(item.consecutivo_DReses))
                {
                    CodDetalleReses.Add(item.consecutivo_DReses);
                }
            }
        }

        /// <summary>
        /// Corrige indicadores visceras en caso de que hayan cierres inesperados de la aplicacion
        /// </summary>
        /// <param name="codProductoDetalleRes"></param>
        public void CorregirIndicadores(string codProductoDetalleRes)
        {
            detallesReses = logica.BuscarDetallesResesIndicadoresVisceras(codProductoDetalleRes, _objCliente.v_no_cliente, true);

            foreach (var item in detallesReses)
            {
                bool exists = CodDetalleReses.Any(s => s.Contains(item._vc_consecutivo));

                //Si ya existe en el listview entonces no cambiar asignacion
                if (!exists)
                {
                    logica.CambiarAsignacion(item._vc_consecutivo, false);
                }
            }

            CodProductosVisita.Remove(codProductoDetalleRes);
        }

        /// <summary>
        /// Busca los indicadores disponibles para asignar
        /// </summary>
        /// <param name="codProductoDetalleRes"></param>
        /// <returns>Una lista de detalles reses disponibles para asignar sus indicadores tipo porcion</returns>
        public List<pnlTransacciones_ltvDetalleReses> BuscarIndicadores(string codProductoDetalleRes)
        {
            detallesReses = logica.BuscarDetallesResesIndicadoresVisceras(codProductoDetalleRes, _objCliente.v_no_cliente, false);

            if (detallesReses.Count == 0)
            {
                CodProductosVisita.Remove(codProductoDetalleRes);
            }

            return detallesReses;
        }

        /// <summary>
        /// Metodo principal encargado de gestionar los indicadores que se asignarán
        /// a los productos que correspondan a visceras
        /// </summary>
        /// <param name="codProducto"></param>
        public async Task<pnlFacturacionVisceras> RecorrerAsignacionesDR(string codProducto) {

            viscera = new pnlFacturacionVisceras();

            BuscarIndicadores(codProducto);

            bool validar = false;

            if (detallesReses.Count > 0)
            {
                foreach (var item in detallesReses)
                {
                    item._vcVasignado = SQL._Si.ToString();

                    viscera.DETALLERES = item;

                    viscera.TIPOVICERAS = item._vc_tipoporcion;

                    //Es necesario validar si es M
                    //en cuyo caso se ha pedido que se le verifique al cliente si quiere adquirirlo
                    if (!item._vc_tipoporcion.Equals(TypeSlice.PorcionCompletaSigla))
                    {
                        if (await LogMessages._dialogResultYes("El producto generará costo para el cliente, ¿Desea continuar?", "Alerta"))
                        {
                            validar = true;
                        }
                        else
                        {
                            //Reiniciamos detallereses ya que al usuario no le interesa el actual detalle res
                            detallesReses = new List<pnlTransacciones_ltvDetalleReses>();
                        }
                    }
                    else
                    {
                        validar = true;
                    }

                    //Es necesario modificar el asignado
                    logica.CambiarAsignacion(item._vc_consecutivo, true);

                    if (validar)
                    {
                        break;
                    }

                }
            }
            else
            {
                viscera = null;
            }

            return viscera;
        }

    }
}
