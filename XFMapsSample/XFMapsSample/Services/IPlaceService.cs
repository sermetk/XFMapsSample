using System;
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
        Task<PlacesAutoCompleteDto> GetAutoCompleteGoogleAddresses(string input);
        Task<PlacesAutoCompleteDto> GetAutoCompleteGooglePlaces(string input);
        Task<PlacesDetailDto> GetPlaceDetail(string place);
        Task<ReverseGeocodeDto> ReverseGeocode(string latlng);
        Task<RouteDirectionDto> GetRoute(string keyword);
    }

    public class PlaceService : IPlaceService
    {
        private const string GMapsApiKey = "YOUR_API_KEY";
        private const string PLACE_DETAIL_END_POINT = "/maps/api/place/details/json";
        private const string REVERSE_GEOCODE_END_POINT = "/maps/api/geocode/json";
        private const string ROUTE_DIRECTION_END_POINT = "/maps/api/directions/json";
        private const string AUTOCOMPLETE_END_POINT = "/maps/api/place/autocomplete/json";
        private const string Store_LatLng = "41.0112841745965,28.972308850524";
        public Task<PlacesAutoCompleteDto> GetAutoCompleteGoogleAddresses(string searchText)
        {
            return Task.Run(() =>
            {
                return new ApiService<PlacesAutoCompleteDto>().GetAsync(AUTOCOMPLETE_END_POINT,
                    new ApiParameter("input", searchText),
                    new ApiParameter("types", "address"),
                    new ApiParameter("language", "tr"),
                    new ApiParameter("origin", Store_LatLng),
                    new ApiParameter("location", Store_LatLng),
                    new ApiParameter("radius", "1250"),
                    new ApiParameter("strictbounds", ""),
                    new ApiParameter("key", GMapsApiKey));

            });
        }
        public Task<PlacesAutoCompleteDto> GetAutoCompleteGooglePlaces(string searchText)
        {
            return Task.Run(() =>
            {
                return new ApiService<PlacesAutoCompleteDto>().GetAsync(AUTOCOMPLETE_END_POINT,
                    new ApiParameter("input", searchText),
                    new ApiParameter("types", "establishment"),
                    new ApiParameter("language", "tr"),
                    new ApiParameter("origin", Store_LatLng),
                    new ApiParameter("location", Store_LatLng),
                    new ApiParameter("radius", "1250"),
                    new ApiParameter("strictbounds", ""),
                    new ApiParameter("key", GMapsApiKey));

            });
        }
        public Task<PlacesDetailDto> GetPlaceDetail(string place)
        {
            return Task.Run(() =>
            {
                return new ApiService<PlacesDetailDto>().GetAsync(PLACE_DETAIL_END_POINT,
                    new ApiParameter("placeid", place),
                    new ApiParameter("key", GMapsApiKey));
            });
        }
        public Task<ReverseGeocodeDto> ReverseGeocode(string latlng)
        {
            return Task.Run(() =>
            {
                return new ApiService<ReverseGeocodeDto>().GetAsync(REVERSE_GEOCODE_END_POINT,
                    new ApiParameter("latlng", latlng),
                    new ApiParameter("key", GMapsApiKey));
            });
        }
        public Task<RouteDirectionDto> GetRoute(string destination)
        {
            return Task.Run(() =>
            {
                return new ApiService<RouteDirectionDto>().GetAsync(ROUTE_DIRECTION_END_POINT,
                    new ApiParameter("origin", Store_LatLng),
                    new ApiParameter("destination", destination),
                    new ApiParameter("key", GMapsApiKey));
            });
        }
        /* OK indicates the response contains a valid result.
            INVALID_REQUEST indicates that the provided request was invalid.
            MAX_ELEMENTS_EXCEEDED indicates that the product of origins and destinations exceeds the per-query limit.
            OVER_DAILY_LIMIT indicates any of the following: The API key is missing or invalid.
            Billing has not been enabled on your account.
            A self-imposed usage cap has been exceeded.
            The provided method of payment is no longer valid (for example, a credit card has expired).
            See the Maps FAQ to learn how to fix this.
            OVER_QUERY_LIMIT indicates the service has received too many requests from your application within the allowed time period.
            REQUEST_DENIED indicates that the servicenew denied use of the Distance Matrix service by your application.
            UNKNOWN_ERROR indicates a Distance Matrix request could not be processed due to a server error. The request may succeed if you try again.*/
    }
}