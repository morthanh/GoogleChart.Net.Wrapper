using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class ViewWindow
    {
        [JsonProperty("max")]
        public object? Max { get; set; }
        
        [JsonProperty("min")]
        public object? Min { get; set; }
    }
}
