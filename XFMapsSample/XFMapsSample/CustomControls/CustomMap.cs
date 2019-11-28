using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XFMapsSample.Models;

namespace XFMapsSample
{
    public class CustomMap : Map
    {
        public readonly List<CustomPin> RoutePins;

        public List<Position> AvailableRegions;

        public CustomMap()
        {
            RoutePins = new List<CustomPin>();
            var myStorePosition = new Position(41.0112841745965, 28.972308850524);
            MoveToRegion(MapSpan.FromCenterAndRadius(myStorePosition, Distance.FromKilometers(2)));
            AddRegionBorders();
            var pin = new CustomPin
            {
                Label = "Store Address",
                Position = myStorePosition,
                Type = PinType.SavedPin,
                ImageUrl = "location_store_mall.png"
            };
            RoutePins.Add(pin);
            Pins.Add(pin);
            MapClicked += OnMapClicked;
        }

        private void AddRegionBorders()
        {
            AvailableRegions = new List<Position> {
                new Position(41.017294,28.957591),
                new Position(41.017683,28.965145),
                new Position(41.017488,28.970638),
                new Position(41.017165,28.975187),
                new Position(41.016193,28.978706),
                new Position(41.015869,28.981452),
                new Position(41.013797,28.983684),
                new Position(41.009846,28.984371),
                new Position(41.007515,28.982997),
                new Position(41.006025,28.979478),
                new Position(41.005183,28.975101),
                new Position(41.004859,28.969179),
                new Position(41.005636,28.959308),
                new Position(41.006802,28.956390),
                new Position(41.011465,28.955188),
                new Position(41.014833,28.955446),
                new Position(41.017229,28.957248)
            };
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            if (!AreaWithin(e.Position))
            {
                Application.Current.MainPage.DisplayAlert("Warning", "We cannot service your chosen region", "Okay");
                return;
            }

            if (BindingContext is SelectLocationPageViewModel selectLocationVm)
            {
                selectLocationVm.SelectedLocation = new Location { lat = e.Position.Latitude, lng = e.Position.Longitude };
                selectLocationVm.SelectionMode = SelectLocationPageViewModel.AddressSelectionMode.Click;
            }

            if (RoutePins.Count > 1)
            {
                for (int i = 1; i < RoutePins.Count; i++)
                {
                    RoutePins.RemoveAt(i);
                }
                Pins.RemoveAt(1);
            }

            var pin = new CustomPin
            {
                Label = "Selected Address",
                Position = e.Position,
                Type = PinType.SavedPin,
                ImageUrl = "location_person.png"
            };

            RoutePins.Add(pin);
            Pins.Add(pin);

            MoveToRegion(MapSpan.FromCenterAndRadius(e.Position, Distance.FromKilometers(2)));
        }

        public bool AreaWithin(Position selectedPosition)
        {
            bool c = false;
            int i = 0;
            int j = AvailableRegions.Count - 1;
            foreach (var point in AvailableRegions)
            {
                double lat_i = point.Latitude;
                double lon_i = point.Longitude;

                double lat_j = AvailableRegions[j].Latitude;
                double lon_j = AvailableRegions[j].Longitude;

                if (((lat_i > selectedPosition.Latitude != (lat_j > selectedPosition.Latitude)) &&
                        (selectedPosition.Longitude < (lon_j - lon_i) * (selectedPosition.Latitude - lat_i) / (lat_j - lat_i) + lon_i)))
                    c = !c;
                j = i++;
            }
            return c;
        }
    }
}
