﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace XFMapsSample.Services
{
    public class ApiService<T>
    {
        public async Task<T> GetAsync(string endpoint, params ApiParameter[] param)
        public string ToQueryString(ApiParameter[] param)
    }
}