using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XFMapsSample.Models;

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
            var position = new Position(latitude, longtitude);
            var pin = new CustomPin
            {
                Label = "Selected Address",
                Position = position,
                Type = PinType.SavedPin,
                ImageUrl = "location_person.png"
            };

            if (Map.RoutePins.Count == 1)
            {
                Map.RoutePins.Add(pin);
                Map.Pins.Add(pin);
            }
            else
            {
                Map.RoutePins[1] = pin;
                Map.Pins[1] = pin;
            }

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Map.Pins[1].Position.Latitude, Map.Pins[1].Position.Longitude), Distance.FromKilometers(2)));
        }

        private void DrawRoute(List<Models.Location> steps)
        {
            for (int i = 1; i < steps.Count -1; i++)
            {
                var position = new Position(steps[i].lat, steps[i].lng);
                var pin = new CustomPin
                {
                    Position = position,
                    Label = "Selected Address",
                    Type = PinType.SavedPin
                };
                Map.RoutePins.Insert(i,pin);
            }           
        }
    }
}
