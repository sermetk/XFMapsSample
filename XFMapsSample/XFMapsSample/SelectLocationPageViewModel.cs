using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XFMapsSample
{
    public class SelectLocationPageViewModel : BindingBase
    {
        private bool _isRouteMode;
        public bool IsRouteMode
        {
            get { return _isRouteMode; }
            set { _isRouteMode = value; OnPropertyChanged(); }
        }

        public ICommand SelectModeCommand { get; set; }
        public ICommand RouteModeCommand { get; set; }

        public SelectLocationPageViewModel()
        {
            SelectModeCommand = new Command(() =>
            {
                IsRouteMode = false;
            });

            RouteModeCommand = new Command(() =>
            {
                IsRouteMode = true;
            });

        }
    }
}
