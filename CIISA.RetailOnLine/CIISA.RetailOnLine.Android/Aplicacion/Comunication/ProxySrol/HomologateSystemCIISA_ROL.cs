using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol
{
    public class HomologateSystemCIISA_ROL
    {
        public static webServiceSROL_upload.SystemCIISA Upload(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            webServiceSROL_upload.SystemCIISA _systemCIISA = new webServiceSROL_upload.SystemCIISA();

            _systemCIISA._codCompany = psystemCIISA._codCompany;
            _systemCIISA._codAgent = psystemCIISA._codAgent;
            _systemCIISA._codRute = psystemCIISA._codRute;
            _systemCIISA._name = psystemCIISA._name;
            _systemCIISA._initials = psystemCIISA._initials;
            _systemCIISA._version = psystemCIISA._version;

            return _systemCIISA;
        }

        public static webServiceSROL_consultation.SystemCIISA Consultation(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            webServiceSROL_consultation.SystemCIISA _systemCIISA = new webServiceSROL_consultation.SystemCIISA();

            _systemCIISA._codCompany = psystemCIISA._codCompany;
            _systemCIISA._codAgent = psystemCIISA._codAgent;
            _systemCIISA._codRute = psystemCIISA._codRute;
            _systemCIISA._name = psystemCIISA._name;
            _systemCIISA._initials = psystemCIISA._initials;
            _systemCIISA._version = psystemCIISA._version;

            return _systemCIISA;
        }

        public static webServiceSROL_totalDownload.SystemCIISA TotalDownload(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            webServiceSROL_totalDownload.SystemCIISA _systemCIISA = new webServiceSROL_totalDownload.SystemCIISA();

            _systemCIISA._codCompany = psystemCIISA._codCompany;
            _systemCIISA._codAgent = psystemCIISA._codAgent;
            _systemCIISA._codRute = psystemCIISA._codRute;
            _systemCIISA._name = psystemCIISA._name;
            _systemCIISA._initials = psystemCIISA._initials;
            _systemCIISA._version = psystemCIISA._version;

            return _systemCIISA;
        }

        public static webServiceSROL_passedOn.SystemCIISA PassedOn(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            webServiceSROL_passedOn.SystemCIISA _systemCIISA = new webServiceSROL_passedOn.SystemCIISA();

            _systemCIISA._codCompany = psystemCIISA._codCompany;
            _systemCIISA._codAgent = psystemCIISA._codAgent;
            _systemCIISA._codRute = psystemCIISA._codRute;
            _systemCIISA._name = psystemCIISA._name;
            _systemCIISA._initials = psystemCIISA._initials;
            _systemCIISA._version = psystemCIISA._version;

            return _systemCIISA;
        }

        public static webServiceSROL_download.SystemCIISA Download(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            webServiceSROL_download.SystemCIISA _systemCIISA = new webServiceSROL_download.SystemCIISA();

            _systemCIISA._codCompany = psystemCIISA._codCompany;
            _systemCIISA._codAgent = psystemCIISA._codAgent;
            _systemCIISA._codRute = psystemCIISA._codRute;
            _systemCIISA._name = psystemCIISA._name;
            _systemCIISA._initials = psystemCIISA._initials;
            _systemCIISA._version = psystemCIISA._version;

            return _systemCIISA;
        }

        public static webServiceSROL_rute.SystemCIISA Rute(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            webServiceSROL_rute.SystemCIISA _systemCIISA = new webServiceSROL_rute.SystemCIISA();

            _systemCIISA._codCompany = psystemCIISA._codCompany;
            _systemCIISA._codAgent = psystemCIISA._codAgent;
            _systemCIISA._codRute = psystemCIISA._codRute;
            _systemCIISA._name = psystemCIISA._name;
            _systemCIISA._initials = psystemCIISA._initials;
            _systemCIISA._version = psystemCIISA._version;

            return _systemCIISA;
        }
    }
}