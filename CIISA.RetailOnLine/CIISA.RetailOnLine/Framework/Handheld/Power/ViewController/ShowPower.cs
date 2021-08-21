using CIISA.RetailOnLine.Framework.Handheld.Power.View;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Power.ViewController
{
    public class ShowPower
    {
        public void showPowerForm()
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewPower());
        }
    }
}
