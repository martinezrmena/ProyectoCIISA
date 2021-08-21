using CIISA.RetailOnLine.Framework.Common.Feedback.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.External;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.SystemHH.ViewController
{
    public class ApplicationExit
    {
        public async Task LogOut()
        {
            if (await LogMessages._dialogResultYes(
                "¿Desea salir del sistema?",
                "Salir del sistema")
                )
            {
                await DirectExit();
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention("No salió del sistema");
            }

        }

        public async Task DirectExit()
        {
            exitProcess();

            LogMessageAttention _logMessageAttention = new LogMessageAttention();

            await _logMessageAttention.generalAttention("Salió del sistema");

            var App_service = DependencyService.Get<IServicio_Aplicacion>();
            App_service.Exit();
        }

        public void exitProcess()
        {

            if (Variable._thereDataBase)
            {
                var MultiGeneric = DependencyService.Get<IMultiGeneric>();
                MultiGeneric.CloseSession();
            }

            var _storage = DependencyService.Get<IStorage>();

            _storage.hideDirectories();

            _storage.hideFiles(_storage.executableFile());
        }
    }
}
