namespace CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController
{
    public static class GPS_Info
    {
        public static GPS_Device v_gpsDevice = null;

        public async static void initializeGPS()
        {
            v_gpsDevice = new GPS_Device();

            await v_gpsDevice.StartGps();
        }
    }
}
