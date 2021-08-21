using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public class RenderLabel
    {

        public async Task paintAdvanceProgress(Label plabel, int pinserciones, DataTable pdt)
        {
            await Task.Run(() => 

                Device.BeginInvokeOnMainThread(()=>

                    plabel.Text = "Avance [" + pdt.TableName + "]: " + pinserciones + "\\" + pdt.Rows.Count + "."
                )
            ).ConfigureAwait(true);
        }

    }
}
