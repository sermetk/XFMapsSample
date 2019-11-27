using System;
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
    {        public enum AddressSelectionMode        {            Autocomplete,            Click        }

        private string _searchBarText;        public string SearchBarText        {            get { return _searchBarText; }            set            {                _searchBarText = value;                if (string.IsNullOrEmpty(_searchBarText))                {                    PlaceSearchAutoComplete = new ObservableCollection<Prediction>();                }                OnPropertyChanged();            }        }

        private ObservableCollection<Prediction> _placeSearchAutoComplete;        public ObservableCollection<Prediction> PlaceSearchAutoComplete        {            get { return _placeSearchAutoComplete; }            set { _placeSearchAutoComplete = value; OnPropertyChanged(); }        }

        private bool _isBusy;        public bool IsBusy        {            get { return _isBusy; }            set { _isBusy = value; OnPropertyChanged(); }        }

        public AddressSelectionMode SelectionMode;        public Location SelectedLocation;

        private string FormattedAddress;

        private readonly IPlaceService Service;
        
        public ICommand NextCommand { get; set; }        public ICommand AddressSearchCommand { get; set; }        public ICommand ReverseGeocodeCommand { get; set; }        public ICommand PlacePredictionSelectCommand { get; set; }

        public SelectLocationPageViewModel()
        {
            Service = DependencyService.Resolve<IPlaceService>();
            PlaceSearchAutoComplete = new ObservableCollection<Prediction>();            Xamarin.Forms.BindingBase.EnableCollectionSynchronization(PlaceSearchAutoComplete, null, ObservableCollectionCallback);
            InitCommands();
        }
        private void InitCommands()        {            AddressSearchCommand = new Command(async () =>            {                IsBusy = true;                await Task.WhenAll(GetGoogleAddressSuggestions(), GetGooglePlaceSuggestions());                IsBusy = false;            });            PlacePredictionSelectCommand = new Command(async (e) =>            {                if (e is Prediction prediction)                {                    SearchBarText = string.Empty;                    await GetPlaceDetail(prediction.place_id);                }            });            NextCommand = new Command(async () =>            {                switch (SelectionMode)                {                    case AddressSelectionMode.Autocomplete:                        await NavigeNextPage();                        break;                    case AddressSelectionMode.Click:
                        IsBusy = true;                        await ReverseGeocoding();
                        IsBusy = false;                        break;                }            });        }

        private void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)        {            lock (collection)            {                accessMethod?.Invoke();            }        }

        private async Task NavigeNextPage()        {            if (string.IsNullOrEmpty(FormattedAddress))                await Application.Current.MainPage.DisplayAlert("Warning", "Please click location or search place", "Okay");            await Application.Current.MainPage.DisplayAlert("Result", string.Format("Address: {0}\n Lat: {1}\n Long: {2}", FormattedAddress, SelectedLocation.lat, SelectedLocation.lng) ,"Okay");        }

        private async Task GetGoogleAddressSuggestions()        {             PlaceSearchAutoComplete = new ObservableCollection<Prediction>();            var result = await Service.GetAutoCompleteGoogleAddresses(SearchBarText);            if (result.status == "OK")            {                foreach (var prediction in result.predictions)                {                    prediction.SuggestionTypeColor = Color.Red;                    PlaceSearchAutoComplete.Add(prediction);                }            }            else if (result.status == "OVER_QUERY_LIMIT" || result.status == "OVER_DAILY_LIMIT" || result.status == "MAX_ELEMENTS_EXCEEDED")            {                await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");            }            else            {                if (PlaceSearchAutoComplete != null)                    PlaceSearchAutoComplete = new ObservableCollection<Prediction>();            }        }        private async Task GetGooglePlaceSuggestions()        {            var result = await Service.GetAutoCompleteGooglePlaces(SearchBarText);            if (result.status == "OK")            {                foreach (var prediction in result.predictions)                {                    prediction.SuggestionTypeColor = Color.Black;                    PlaceSearchAutoComplete.Add(prediction);                }            }            else if (result.status == "OVER_QUERY_LIMIT" || result.status == "OVER_DAILY_LIMIT" || result.status == "MAX_ELEMENTS_EXCEEDED")            {                await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");            }            else            {                if (PlaceSearchAutoComplete != null)                    PlaceSearchAutoComplete = new ObservableCollection<Prediction>();            }        }        private async Task GetPlaceDetail(string place)        {            var result = await Service.GetPlaceDetail(place);
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
        }        private async Task ReverseGeocoding()        {            var latLon = string.Format("{0},{1}", SelectedLocation.lat.ToString().Replace(",", "."), SelectedLocation.lng.ToString().Replace(",", "."));            var result = await Service.ReverseGeocode(latLon);            if (result.status == "OK")            {                var googleResult = result.results.FirstOrDefault();                FormattedAddress = googleResult.formatted_address;                await NavigeNextPage();            }            else if (result.status == "OVER_QUERY_LIMIT" || result.status == "OVER_DAILY_LIMIT" || result.status == "MAX_ELEMENTS_EXCEEDED")            {
                await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");            }        }
    }
}
