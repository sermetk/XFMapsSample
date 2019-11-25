using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XFMapsSample
{
    public class SelectLocationPageViewModel : BindingBase
    {
        public ICommand NextPageCommand { get; set; }

        public SelectLocationPageViewModel()
        {
            NextPageCommand = new Command(() =>
            {
                
            });
        }
    }
}
