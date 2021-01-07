using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleChart.Net.Wrapper.Options
{
    public class ColumnChartOptions : AxisChartOptions
    {

        [JsonProperty("bar")]
        public ColumnChartBar? Bar { get; set; }

        [JsonProperty("isStacked")]
        public StackedOption IsStacked { get; set; }


    }

    public class ColumnChartBar
    {
        [JsonProperty("groupWidth")]
        public UnitSize? GroupWidth { get; set; }
    }
}
