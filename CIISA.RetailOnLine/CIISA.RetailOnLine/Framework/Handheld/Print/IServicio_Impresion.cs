using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.Print
{
    public interface IServicio_Impresion
    {
        Task Print(string address, string texto);
    }
}
