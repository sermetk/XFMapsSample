using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XFMapsSample.Models;

namespace XFMapsSample
{
    public partial class SelectLocationPage : ContentPage
    {
        public SelectLocationPage()
        {
            InitializeComponent();            SubscribeSearchedPin();
        }
        private void SubscribeSearchedPin()        {            MessagingCenter.Unsubscribe<SelectLocationPageViewModel, Location>(this, "ResultFromGMaps");            MessagingCenter.Subscribe<SelectLocationPageViewModel, Location>(this, "ResultFromGMaps", (sender, args) =>            {                InsertPin(args.lat, args.lng);            });        }
        private void InsertPin(double latitude, double longtitude)        {            var position = new Position(latitude, longtitude);            var pin = new CustomPin            {                Label = "Selected Address",                Position = position,                Type = PinType.SavedPin,
                ImageUrl = "location_person.png"
            };            if(Map.RoutePins.Count == 1)
            {
                Map.RoutePins.Add(pin);
                Map.Pins.Add(pin);            }            else
            {
                Map.RoutePins[1] = pin;
                Map.Pins[1] = pin;
            }            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Map.Pins[1].Position.Latitude, Map.Pins[1].Position.Longitude), Distance.FromKilometers(2)));        }
    }
}
