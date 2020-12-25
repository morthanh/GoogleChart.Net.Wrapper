using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class TableChartOptions : ChartOptions
    {
        [JsonProperty("alternatingRowStyle")]
        public bool? AlternatingRowStyle { get; set; }

        [JsonProperty("cssClassNames")]
        public TableCssClassNames? CssClassNames { get; set; }

        [JsonProperty("firstRowNumber")]
        public int? FirstRowNumber { get; set; }

        [JsonProperty("frozenColumns")]
        public int? FrozenColumns { get; set; }

        [JsonProperty("showRowNumbers")]
        public bool? ShowRowNumber { get; set; }

        [JsonProperty("sort")]
        public ColumnSortMode? Sort { get; set; }

        [JsonProperty("sortAscending")]
        public bool? SortAscending { get; set; }

        [JsonProperty("sortColumn")]
        public int? SortColumn { get; set; }
    }
}
