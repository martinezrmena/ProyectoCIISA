using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController
{
    public interface IServicioDistancia
    {
        Task<bool> ValidacionGeolocalizacion(double LongitudCliente, double LatitudeCliente);
    }
}
