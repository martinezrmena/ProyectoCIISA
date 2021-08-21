using CIISA.RetailOnLine.Framework.Common.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Connection
{
    public interface IEstablishDatabase
    {
        void thereDataBase();
        void createDataBase(Editor ptextBox, Log plog);
    }
}
