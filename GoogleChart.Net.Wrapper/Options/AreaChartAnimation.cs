using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class AreaChartAnimation
    {

        [JsonProperty("duration")]
        public double? Duration { get; set; }

        [JsonProperty("easing")]
        public AreaChartAnimationFunction? Easing { get; set; }

        [JsonProperty("startup")]
        public bool? Startup { get; set; }
    }
}
