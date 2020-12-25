using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class TableCssClassNames
    {
        [JsonProperty("headerRow")]
        public string HeaderRow { get; set; }

        [JsonProperty("tableRow")]
        public string TableRow { get; set; }

        [JsonProperty("oddTableRow")]
        public string OddTableRow { get; set; }

        [JsonProperty("selectedTableRow")]
        public string SelectedTableRow { get; set; }

        [JsonProperty("hoverTableRow")]
        public string HoverTableRow { get; set; }

        [JsonProperty("headerCell")]
        public string HeaderCell { get; set; }

        [JsonProperty("tableCell")]
        public string TableCell { get; set; }

        [JsonProperty("rowNumberCell")]
        public string RowNumberCell { get; set; }
    }
}
