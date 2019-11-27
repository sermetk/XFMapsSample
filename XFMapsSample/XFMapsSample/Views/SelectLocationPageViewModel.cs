﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XFMapsSample.Models;
using XFMapsSample.Services;

namespace XFMapsSample
{
    public class SelectLocationPageViewModel : BindingBase
    {

        private string _searchBarText;

        private ObservableCollection<Prediction> _placeSearchAutoComplete;

        private bool _isBusy;

        public AddressSelectionMode SelectionMode;

        private string FormattedAddress;

        private readonly IPlaceService Service;
        
        public ICommand NextCommand { get; set; }

        public SelectLocationPageViewModel()
        {
            Service = DependencyService.Resolve<IPlaceService>();
            PlaceSearchAutoComplete = new ObservableCollection<Prediction>();
            InitCommands();
        }
        private void InitCommands()
                        IsBusy = true;
                        IsBusy = false;

        private void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)

        private async Task NavigeNextPage()

        private async Task GetGoogleAddressSuggestions()
            switch (result.status)
            {
                case "OK":
                    var googleResult = result.result;
                    SelectedLocation = googleResult.geometry.location;
                    FormattedAddress = googleResult.formatted_address;
                    SelectionMode = AddressSelectionMode.Autocomplete;
                    MessagingCenter.Send(this, "ResultFromGMaps", googleResult.geometry.location);
                    break;
                case "OVER_QUERY_LIMIT":
                case "OVER_DAILY_LIMIT":
                case "MAX_ELEMENTS_EXCEEDED":
                    await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");
                    break;
                case "ZERO_RESULTS":
                    PlaceSearchAutoComplete = new ObservableCollection<Prediction>();
                    break;
                case "INVALID_REQUEST":
                    break;
            }
        }
                await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");
    }
}