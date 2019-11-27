using System;
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
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.None)
            {
                throw new Exception("Internet not found");
            }
            var parameters = ToQueryString(param);
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://maps.googleapis.com" + endpoint + "?" + parameters);
            var resultObj = JsonConvert.DeserializeObject<T>(result);
            if (resultObj == null)
            {
                throw new Exception("Error occurred when converting result");
            }
            return resultObj;
        }
        public string ToQueryString(ApiParameter[] param)
        {
            return string.Join("&", param.Select(x => x.ToString()));
        }
    }
}
