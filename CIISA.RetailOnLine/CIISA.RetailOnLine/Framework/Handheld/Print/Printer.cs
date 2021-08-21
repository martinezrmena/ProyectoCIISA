using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Print
{
    public class Printer
    {
        string puerto = string.Empty;

        IServicio_Print v_printer = null;

        public Printer(string pcomPort)
        {
            puerto = pcomPort;
            v_printer = DependencyService.Get<IServicio_Print>();
            //v_printer = new PrinterZebra(pcomPort);
        }

        public bool FuncionaImpresora() {
            v_printer = DependencyService.Get<IServicio_Print>();
            return v_printer.ImpresoraFunciona();
        }

        public async Task connect()
        {
            //v_printer.connect();
            await v_printer.connect(puerto);
        }

        public async Task print(string ptexto)
        {
            try
            {
                //await v_printer.print(ptexto,lineas);
                await v_printer.print(ptexto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task printBarCode(string code)
        {
            try
            {
                await v_printer.printBarCode(code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void disconnect()
        {
            v_printer.disconnect();
        }

        public async Task<bool> ValidarImpresorasConectadas()
        {
            bool hayImpresoras = false;

            hayImpresoras = await v_printer.HayImpresorasConectadas();

            return hayImpresoras;
        }
    }
}
