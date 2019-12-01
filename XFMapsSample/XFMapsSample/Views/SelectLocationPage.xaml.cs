using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XFMapsSample
{
    public partial class SelectLocationPage : ContentPage
    {
        public SelectLocationPage()
        {
            InitializeComponent();
            SubscribeSearchedPin();
        }
        private void SubscribeSearchedPin()
        {
            MessagingCenter.Unsubscribe<SelectLocationPageViewModel, Models.Location>(this, "ResultFromGMaps");
            MessagingCenter.Subscribe<SelectLocationPageViewModel, Models.Location>(this, "ResultFromGMaps", (sender, args) =>
            {
                InsertPin(args.lat, args.lng);
            });
            MessagingCenter.Unsubscribe<SelectLocationPageViewModel, List<Models.Location>>(this, "RouteDetail");
            MessagingCenter.Subscribe<SelectLocationPageViewModel, List<Models.Location>>(this, "RouteDetail", (sender, args) =>
            {
                DrawRoute(args);
            });
        }
        private void InsertPin(double latitude, double longtitude)
        {
            if (Map.RoutePins.Count > 1)
            {
                for (int i = 1; i < Map.RoutePins.Count; i++)
                {
                    Map.RoutePins.RemoveAt(i);
                }
                Map.Pins.RemoveAt(1);
            }
            var position = new Position(latitude, longtitude);
            var pin = new CustomPin
            {
                Label = "Selected Address",
                Position = position,
                Type = PinType.SavedPin,
                ImageUrl = "location_person.png"
            };

            Map.RoutePins.Add(pin);
            Map.Pins.Add(pin);

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Map.Pins[1].Position.Latitude, Map.Pins[1].Position.Longitude), Distance.FromKilometers(2)));
        }

        private void DrawRoute(List<Models.Location> steps)
        {
            Map.RoutePins.Clear();
            Map.Pins.Clear();
            foreach (var coordinates in steps)
            {
                var position = new Position(coordinates.lat, coordinates.lng);
                var pin = new CustomPin
                {
                    Position = position,
                    Label = "Selected Address",
                    Type = PinType.SavedPin
                };
                Map.RoutePins.Add(pin);
            }
            var firstPin = Map.RoutePins.First();
            var lastPin = Map.RoutePins.Last();
            firstPin.Label = "Store Address";
            lastPin.Label = "Selected Address";
            firstPin.ImageUrl = "location_store_mall.png";
            lastPin.ImageUrl = "location_person.png";
            Map.Pins.Add(firstPin);
            Map.Pins.Add(lastPin);
        }
    }
}
