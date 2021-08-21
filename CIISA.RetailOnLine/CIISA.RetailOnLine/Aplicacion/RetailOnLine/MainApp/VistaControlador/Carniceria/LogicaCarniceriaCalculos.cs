using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria
{
    //Elementos para escaneo de cajas
    /// <summary>
    ///Clase encargada de realizar calculos o modificaciones sobre elementos relacionados
    ///con carniceria
    /// </summary>
    internal class LogicaCarniceriaCalculos
    {
        private ctrlVisita controlador = null;

        internal LogicaCarniceriaCalculos(ctrlVisita ctrl)
        {
            controlador = ctrl;
        }

    }
}


