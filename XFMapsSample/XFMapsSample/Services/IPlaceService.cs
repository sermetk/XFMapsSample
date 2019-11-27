﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFMapsSample.Models;
using XFMapsSample.Services;

[assembly: Dependency(typeof(PlaceService))]
namespace XFMapsSample.Services
{
    public interface IPlaceService
    {
        private const string GMapsApiKey = "YOUR_API_KEY";
        private const string PLACE_DETAIL_END_POINT = "/maps/api/place/details/json";
        /* OK indicates the response contains a valid result.
    }
}