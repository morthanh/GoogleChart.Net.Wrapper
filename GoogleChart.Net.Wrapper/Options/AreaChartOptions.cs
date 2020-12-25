using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace GoogleChart.Net.Wrapper.Options
{
    public class AreaChartOptions : AxisChartOptions
    {
        private double? areaOpacity;

        [JsonProperty("isStacked")]
        public StackedOption IsStacked { get; set; }

        [JsonProperty("areaOpacity")]
        public double? AreaOpacity { get => areaOpacity; set => areaOpacity = 0 <= value && value <= 1 ? value : throw new ArgumentOutOfRangeException(nameof(AreaOpacity)); }



    }
}
