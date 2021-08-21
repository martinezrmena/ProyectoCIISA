using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.External.GpsThings
{
    public interface IGps
    {
        bool Opened();

        void Open();

        void Close();

        int GetSatellitesInView();

        int GetSatellitesInSolution();
    }
}
