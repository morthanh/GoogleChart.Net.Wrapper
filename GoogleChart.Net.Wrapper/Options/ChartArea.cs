using GoogleChart.Net.Wrapper.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public sealed class ChartArea
    {
        [JsonProperty("backgroundColor")]
        [JsonConverter(typeof(ChartBackgroundColorConverter))]
        public ChartBackgroundColor? BackgroundColor { get; set; }

        [JsonProperty("left")]
        public UnitSize? Left { get; set; }

        [JsonProperty("top")]
        public UnitSize? Top { get; set; }

        [JsonProperty("width")]
        public UnitSize? Width { get; set; }

        [JsonProperty("height")]
        public UnitSize? Height { get; set; }
    }
}
