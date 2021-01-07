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
        private readonly List<Row>? rows;
        private readonly List<Column> columns = new List<Column>();

        public DataTable()
        {
            rows = new List<Row>();
            Values = CreateRowsEnumeratorToValuesEnumerator(rows);
        }

        public DataTable(IEnumerable<object> valueSource)
        {
            Values = valueSource;
        }


        private IEnumerable<object> CreateRowsEnumeratorToValuesEnumerator(List<Row> rows)
        {
            foreach(var row in rows)
            {
                foreach(var cell in row.Cells)
                {
                    yield return cell;
                }
            }
        }

        [JsonIgnore]
        public IList<ColumnType> ColumnTypes { get; set; } = new List<ColumnType>();

        [JsonProperty("cols")]
        public IReadOnlyList<Column> Columns => columns.AsReadOnly();
        [JsonIgnore]
        public IEnumerable<object> Values { get; }

        [JsonIgnore]
        public ChartOptions? Options { get; set; }

        internal List<string>? ColumnLabels { get; set; }


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

            columns.Add(column);
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
