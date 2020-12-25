using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class GaugeAnimation
    {
        [JsonProperty("duration")]
        public double? Duration { get; set; }

        [JsonProperty("easing")]
        public GaugeAnimationFunction? Easing { get; set; }
    }
}
