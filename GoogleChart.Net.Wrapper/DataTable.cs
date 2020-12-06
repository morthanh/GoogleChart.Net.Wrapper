using GoogleChart.Net.Wrapper.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{

    [JsonConverter(typeof(DataTableConverter))]
    public class DataTable
    {
        private readonly bool useLinq;
        private readonly List<Row> rows;

        public DataTable() {
            rows = new List<Row>();
            ValuesSource = rows.AsEnumerable();
            useLinq = false;
        }

        public DataTable(IEnumerable<object> valueSource)
        {
            ValuesSource = valueSource;
            useLinq = true;
        }

        [JsonIgnore]
        public IList<ColumnType> ColumnTypes { get; set; } = new List<ColumnType>();

        [JsonPropertyName("cols")]
        internal IList<Column> Columns { get; } = new List<Column>();

        [JsonIgnore]
        internal IEnumerable<object> ValuesSource { get; }

        [JsonIgnore]
        internal ChartOptions? Options { get; set; }

        [JsonIgnore]
        internal bool UseLinq => useLinq;


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
        public void AddColumn(ColumnType columnType)
        {
            AddColumn(new Column(columnType));
        }
        public void AddColumn(ColumnType columnType, string label)
        {
            AddColumn(new Column(columnType, label));
        }


        public void AddRow(IEnumerable<Cell> cells)
        {
            if (cells.Count() != Columns.Count)
            {
                throw new Exception("Number of cells does not match number of columns");
            }

            rows.Add(new Row(this, cells));
        }

        internal void AddRowsSource(IEnumerable<Row> rows)
        {
            this.rows.AddRange(rows);
        }


        public void Deconstruct(out string dataJson)
        {
            dataJson = ToJson();
        }

        public void Deconstruct(out string dataJson, out string optionsJson)
        {
            dataJson = ToJson();
            optionsJson = Options?.ToJson() ?? "{ }";
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
