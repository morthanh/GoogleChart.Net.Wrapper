using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{

    /// <summary>
    /// 
    /// </summary>
    public class DataTable
    {

        private IEnumerable<Row> rows = new List<Row>();
        private readonly IList<Column> columns = new List<Column>();
        private IList<ColumnType> columnTypes = new List<ColumnType>();


        public DataTable() { }

        [JsonPropertyName("cols")]
        public IList<Column> Columns => columns;

        [JsonPropertyName("rows")]
        public IEnumerable<Row> Rows => rows;

        [JsonIgnore]
        internal IList<ColumnType> ColumnTypes { get => columnTypes; set => columnTypes = value; }



        public void AddColumn(Column column)
        {
            if (rows.Any())
            {
                throw new Exception("Cannot add column if any rows have been added");
            }

            if (string.IsNullOrEmpty(column.Id))
            {
                column.Id = "Column" + columns.Count;
            }

            if (columns.Any(x => x.Id == column.Id))
            {
                throw new Exception($"Column id '{column.Id}' is already used");
            }

            columns.Add(column);
            ColumnTypes.Add(column.ColumnType);
        }


        public void AddRow(IEnumerable<Cell> cells)
        {
            if (cells.Count() != columns.Count)
            {
                throw new Exception("Number of cells does not match number of columns");
            }

            ((IList<Row>)rows).Add(new Row(this, cells));
        }

        internal void AddRowsSource(IEnumerable<Row> rows)
        {
            this.rows = rows;
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
