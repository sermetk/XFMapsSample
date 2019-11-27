using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XFMapsSample.Models
{
    public class PlacesAutoCompleteDto
    {
        public List<Prediction> predictions { get; set; }
        public string status { get; set; }
    }

    public class Prediction
    {
        public string description { get; set; }
        public int distance_meters { get; set; }
        public string id { get; set; }
        public List<Matched_Substrings> matched_substrings { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public Structured_Formatting structured_formatting { get; set; }
        public List<Term> terms { get; set; }
        public List<string> types { get; set; }
        [JsonIgnore]
        public Color SuggestionTypeColor { get; set; }
    }

    public class Structured_Formatting
    {
        public string main_text { get; set; }
        public List<Main_Text_Matched_Substrings> main_text_matched_substrings { get; set; }
        public string secondary_text { get; set; }
    }

    public class Main_Text_Matched_Substrings
    {
        public int length { get; set; }
        public int offset { get; set; }
    }

    public class Matched_Substrings
    {
        public int length { get; set; }
        public int offset { get; set; }
    }

    public class Term
    {
        public int offset { get; set; }
        public string value { get; set; }
    }
}
