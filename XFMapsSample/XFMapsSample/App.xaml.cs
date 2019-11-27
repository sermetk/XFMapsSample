using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
