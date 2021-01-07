using GoogleChart.Net.Wrapper.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class LineChartOptions : AxisChartOptions
    {

        [JsonProperty("lineWidth")]
        public int? LineWidth { get; set; }

        [JsonProperty("curveType")]
        public CurveType CurveType { get; set; }


    }


    public class CandlestickChartOptions: AxisChartOptions
    {
        [JsonProperty("bar")]
        public ColumnChartBar? Bar { get; set; }

        [JsonProperty("candlestick")]
        public CandlestickOptions? Candlestick { get; set; }

    }

    public class CandlestickOptions
    {
        [JsonProperty("hollowIsRising")]
        public bool? HollowIsRising { get; set; }

        [JsonProperty("fallingColor")]
        [JsonConverter(typeof(ChartBackgroundColorConverter))]
        public ChartBackgroundColor? FallingColor { get; set; }

        [JsonProperty("risingColor")]
        [JsonConverter(typeof(ChartBackgroundColorConverter))]
        public ChartBackgroundColor? RisingColor { get; set; }
    }
}
