using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Detail;
using CIISA.RetailOnLine.Framework.Common.Feedback.DownTextList;
using CIISA.RetailOnLine.Framework.Common.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.LogFiles
{
    internal class LogTextBox
    {
        private void addLineTextBox(
            ref Editor ptextBox,
            ref string pdetailLine,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            int pspace,
            ref DateTime pstartTime
            )
        {
            LogDownTextList _logDownTextList = new LogDownTextList();

            _logDownTextList.addLineDownTextList(
                ref pdownTextList,
                ref pdetailLine
                );

            RenderTextBox _renderTextbox = new RenderTextBox();

            _renderTextbox.writeReverseText(pdownTextList, ptextBox);

            LogDetail _logDetail = new LogDetail();

            _logDetail.addLine(
                ref pbodyLog,
                ref pdetailLine,
                pspace,
                ref pstartTime
                );
        }

        internal void addSuccessLine(
            ref Editor ptextBox,
            ref string pdetailLine,
            ref int pspace,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            string _detailLine = "     ● Éxito: " + pdetailLine;

            addLineTextBox(
                ref ptextBox,
                ref _detailLine,
                ref pdownTextList,
                ref pbodyLog,
                pspace,
                ref pstartTime
                );
        }

        internal void addLine(
            ref Editor ptextBox,
            ref string pdetailLine,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            addLineTextBox(
                ref ptextBox,
                ref pdetailLine,
                ref pdownTextList,
                ref pbodyLog,
                2,
                ref pstartTime
                );
        }

        internal void addLineSuccessWSDownload(
            ref Editor ptextBox,
            ref string ptable,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            string _detailLine = "     ● Éxito: descargó datos de la tabla ";

            _detailLine += ptable;
            _detailLine += Simbol._point;

            addLineTextBox(
                ref ptextBox,
                ref _detailLine,
                ref pdownTextList,
                ref pbodyLog,
                0,
                ref pstartTime
                );
        }

        internal void addErrorLineDataTableNull(
            ref Editor ptextBox,
            ref string ptable,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            string _detailLine = "  ■ Error: DataTable ";

            _detailLine += ptable;
            _detailLine += " nulo";
            _detailLine += Simbol._point;

            addLineTextBox(
                ref ptextBox,
                ref _detailLine,
                ref pdownTextList,
                ref pbodyLog,
                0,
                ref pstartTime
                );
        }

        internal void addAlertLineDataTable(
            ref Editor ptextBox,
            ref string ptable,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            string _newLine = "  ◊ Alerta: la consulta a la tabla ";

            _newLine += ptable;
            _newLine += " no devolvió resultados.";

            addLineTextBox(
                ref ptextBox,
                ref _newLine,
                ref pdownTextList,
                ref pbodyLog,
                0,
                ref pstartTime
                );
        }

        internal void addAlertLine(
            ref Editor ptextBox,
            ref string pdetailLine,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            string _detailLine = "  ◊ Alerta: ";

            _detailLine += pdetailLine;

            addLineTextBox(
                ref ptextBox,
                ref _detailLine,
                ref pdownTextList,
                ref pbodyLog,
                0,
                ref pstartTime
                );
        }

        internal void addErrorLine(
            ref Editor ptextBox,
            ref string pdetailLine,
            ref int pspace,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            string _detailLine = "  ■ Error: " + pdetailLine;

            addLineTextBox(
                ref ptextBox,
                ref _detailLine,
                ref pdownTextList,
                ref pbodyLog,
                pspace,
                ref pstartTime
                );
        }

        internal void registrationNumberInserts(
            ref Editor ptextBox,
            ref int pinserts,
            ref int precordAmount,
            ref string ptable,
            ref List<string> pdownTextList,
            ref StringBuilder pbodyLog,
            ref DateTime pstartTime
            )
        {
            int _space = 0;
            string _newLine = "realizó ";

            _newLine += pinserts;
            _newLine += Simbol._slash;
            _newLine += precordAmount;
            _newLine += " inserciones en la tabla ";
            _newLine += ptable;
            _newLine += Simbol._point;

            if ((pinserts - precordAmount) == 0)
            {
                addSuccessLine(
                    ref ptextBox,
                    ref _newLine,
                    ref _space,
                    ref pdownTextList,
                    ref pbodyLog,
                    ref pstartTime
                    );
            }
            else
            {
                addErrorLine(
                    ref ptextBox,
                    ref _newLine,
                    ref _space,
                    ref pdownTextList,
                    ref pbodyLog,
                    ref pstartTime
                    );
            }

        }
    }
}
