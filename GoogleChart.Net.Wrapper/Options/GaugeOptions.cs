using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace GoogleChart.Net.Wrapper.Options
{
    public class GaugeOptions : ChartOptions
    {
        [JsonProperty("animation")]
        public GaugeAnimation Animation { get; set; }

        [JsonProperty("greenColor")]
        public string? GreenColor { get; set; }

        [JsonProperty("greenFrom")]
        public double? GreenFrom { get; set; }

        [JsonProperty("greenTo")]
        public double? GreenTo { get; set; }

        [JsonProperty("max")]
        public double? Max { get; set; }

        [JsonProperty("min")]
        public double? Min { get; set; }

        [JsonProperty("majorTicks")]
        public IEnumerable<string>? MajorTicks { get; set; }

        [JsonProperty("minorTicks")]
        public int MinorTicks { get; set; }

        [JsonProperty("redColor")]
        public string? RedColor { get; set; }

        [JsonProperty("redFrom")]
        public double? RedFrom { get; set; }

        [JsonProperty("redTo")]
        public double? RedTo { get; set; }

        [JsonProperty("yellowColor")]
        public string? YellowColor { get; set; }

        [JsonProperty("yellowFrom")]
        public double? YellowFrom { get; set; }

        [JsonProperty("yellowTo")]
        public double? YellowTo { get; set; }

    }
}
