using System.Collections.Generic;

namespace XFMapsSample.Models
{
    public class ReverseGeocodeDto
    {
        public Plus_Code plus_code { get; set; }
        public List<ReverseGeocodeResult> results { get; set; }
        public string status { get; set; }
    }

    public class Plus_Code
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class ReverseGeocodeResult
    {
        public List<Address_Components> address_components { get; set; }
        public string formatted_address { get; set; }
        public ReverseGeocodeGeometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class ReverseGeocodeGeometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }


    public class Northeast1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Southwest1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}