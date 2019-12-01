using Xamarin.Forms;

namespace XFMapsSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SelectLocationPage()) { BarBackgroundColor = Color.Teal, BarTextColor = Color.White };
        }
    }
}
