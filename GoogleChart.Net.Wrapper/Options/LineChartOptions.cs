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
}
