using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Detail;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.LogFiles
{
    public class LogEstruture
    {
        public string generalHeading(DateTime pstartTime)
        {
            string _string = string.Empty;

            _string += Document._firstLine;
            _string += Environment.NewLine;
            _string += "Fecha:         ";
            _string += VarTime.getDateCR();
            _string += Environment.NewLine;
            _string += "Hora:          ";
            _string += VarTime.getTimeCR();
            _string += Space._one;
            _string += VarTime.getMeridian();
            _string += Environment.NewLine;
            _string += "Hora Inicio:   ";
            _string += pstartTime.TimeOfDay.ToString();
            _string += Environment.NewLine;
            _string += "Duración:      ";

            LogDetailUtils _logDetailsUtils = new LogDetailUtils();

            _string += _logDetailsUtils.setDurationTime(pstartTime);
            _string += Environment.NewLine;
            _string += Environment.NewLine;

            return _string;
        }

        public string generalFoot()
        {
            string _string = string.Empty;

            _string += Environment.NewLine;
            _string += Document._lastLine;
            _string += Environment.NewLine;
            _string += "@Autor:    Félix Alonso Carvajal Elizondo";
            _string += Environment.NewLine;
            _string += "@Correo-e: fcarvajal@ciisa.com";
            _string += Environment.NewLine;
            _string += "@Correo-e: nanoce@gmail.com";
            _string += Environment.NewLine;
            _string += "@Año:      2010-2015";
            _string += Environment.NewLine;

            return _string;
        }
    }
}
