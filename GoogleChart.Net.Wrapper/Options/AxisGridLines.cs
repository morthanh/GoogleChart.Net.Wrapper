using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class AxisGridLines
    {
        [JsonProperty("color")]
        public ChartColor? Color { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }

        //TODO: hAxis.gridlines.interval

        [JsonProperty("minSpacing")]
        public int? MinSpacing { get; set; }

        [JsonProperty("multiple")]
        public int? Multiple { get; set; }

        //TODO: hAxis.gridlines.units
    }
}
