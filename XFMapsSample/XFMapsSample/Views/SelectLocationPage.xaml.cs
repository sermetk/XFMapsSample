﻿using System;
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
            InitializeComponent();
        }
        private void SubscribeSearchedPin()
        private void InsertPin(double latitude, double longtitude)
                ImageUrl = "location_person.png"
            };
            {
                Map.RoutePins.Add(pin);
                Map.Pins.Add(pin);
            {
                Map.RoutePins[1] = pin;
                Map.Pins[1] = pin;
            }
    }
}