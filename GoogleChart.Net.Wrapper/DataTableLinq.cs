using GoogleChart.Net.Wrapper.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{

    [JsonConverter(typeof(DataTableLinqConverter))]
    public class DataTableLinq
    {
        public DataTableLinq() { }


        [JsonIgnore]
        public IList<ColumnType> ColumnTypes { get; set; } = new List<ColumnType>();

        [JsonPropertyName("cols")]
        internal IList<Column> Columns { get; } = new List<Column>();

        [JsonIgnore]
        internal IEnumerable<object> ValuesSource { get; set; }


        /// <summary>
        /// Add a column to the <see cref="DataTable"/>
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(Column column)
        {

            if (string.IsNullOrEmpty(column.Id))
            {
                column.Id = "Column" + Columns.Count;
            }

            if (Columns.Any(x => x.Id == column.Id))
            {
                throw new Exception($"Column id '{column.Id}' is already used");
            }

            Columns.Add(column);
            ColumnTypes.Add(column.ColumnType);
        }







        public string ToJson()
        {
            return ToJson(new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        }


        public string ToJson(JsonSerializerOptions options)
        {
            if (options.DefaultIgnoreCondition == JsonIgnoreCondition.Never)
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            return JsonSerializer.Serialize(this, options);
        }
    }
}
