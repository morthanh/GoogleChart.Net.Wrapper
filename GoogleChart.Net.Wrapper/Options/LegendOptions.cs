using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class LegendOptions
    {
        [JsonProperty("alignment")]
        public LegendAlignment? Alignment { get; set; }

        [JsonProperty("maxLines")]
        public int? MaxLines { get; set; }

        [JsonProperty("pageIndex")]
        public int? PageIndex { get; set; }

        [JsonProperty("position")]
        public LegendPosition? Position { get; set; }
    }
}
