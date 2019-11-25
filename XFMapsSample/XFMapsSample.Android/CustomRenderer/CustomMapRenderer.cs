using System;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using XFMapsSample;
using XFMapsSample.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace XFMapsSample.Droid.CustomRenderer
{
    public class CustomMapRenderer : MapRenderer    {        private CustomMap FormsMap;        public CustomMapRenderer(Context context) : base(context)        {        }        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)        {            base.OnElementChanged(e);            if (e.NewElement != null)            {                FormsMap = (CustomMap)e.NewElement;            }        }

        protected override MarkerOptions CreateMarker(Pin pin)        {            var marker = new MarkerOptions();            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));            if (pin.Label == "Store Address")            {                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.location_store_mall));            }            else            {                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.location_person));            }            return marker;        }        protected override void OnMapReady(GoogleMap map)        {            base.OnMapReady(map);            map.UiSettings.MapToolbarEnabled = false;            if (FormsMap.RoutePins != null && FormsMap.RoutePins.Count > 2)            {                var polylineOptions = new PolylineOptions();                polylineOptions.InvokeColor(Color.Red.ToAndroid());                foreach (var pins in FormsMap.RoutePins)                {                    polylineOptions.Add(new LatLng(pins.Position.Latitude, pins.Position.Longitude));                }                NativeMap.AddPolyline(polylineOptions);            }            else if (FormsMap.AvailableRegions != null)            {                var polygonOptions = new PolygonOptions();                polygonOptions.InvokeFillColor(Android.Graphics.Color.ParseColor("#2271cce7"));                polygonOptions.InvokeStrokeColor(Android.Graphics.Color.ParseColor("#2271cce7"));                polygonOptions.InvokeStrokeWidth(15.0f);                foreach (var position in FormsMap.AvailableRegions)                {                    polygonOptions.Add(new LatLng(position.Latitude, position.Longitude));                }                NativeMap.AddPolygon(polygonOptions);            }        }    }}