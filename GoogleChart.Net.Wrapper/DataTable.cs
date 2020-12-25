using GoogleChart.Net.Wrapper.JsonConverters;
using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper
{
    /// <summary>
    /// Holds all columns, row values, and options used by Google Chart Javascript Api. 
    /// </summary>
    [JsonConverter(typeof(ChartDataTableConverter))]
    public class DataTable
    {
        private readonly bool useLinq;
        private readonly List<Row> rows;

        public DataTable()
        {
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

        [JsonProperty("cols")]
        internal IList<Column> Columns { get; } = new List<Column>();

        [JsonIgnore]
        internal IEnumerable<object> ValuesSource { get; }

        [JsonIgnore]
        public ChartOptions? Options { get; set; }

        [JsonIgnore]
        internal bool UseLinq => useLinq;

        internal List<string> ColumnLabels { get; set; }


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

        public void AddColumnLabels(IEnumerable<string> labels)
        {
            ColumnLabels = labels.ToList();
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



        public string ToJson(bool formatted = false)
        {
            return SerializerHelper.Serialize(this, formatted);
        }


    }
}
