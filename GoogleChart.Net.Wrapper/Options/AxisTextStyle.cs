using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class AxisTextStyle
    {
        [JsonProperty("color")]
        public ChartColor? Color { get; set; }

        [JsonProperty("fontName")]
        public string? FontName { get; set; }

        [JsonProperty("fontSize")]
        public int? FontSize { get; set; }

        [JsonProperty("bold")]
        public bool? Bold { get; set; }

        [JsonProperty("italic")]
        public bool? Italic { get; set; }
    }
}
