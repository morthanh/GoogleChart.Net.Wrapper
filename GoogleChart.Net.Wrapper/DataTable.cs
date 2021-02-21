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
        private readonly List<ColumnMeta> columns = new List<ColumnMeta>();

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

        public IReadOnlyList<ColumnMeta> Columns => columns.AsReadOnly();

        [JsonIgnore]
        public IEnumerable<object> Values { get; }

        [JsonIgnore]
        public ChartOptions? Options { get; set; }

        internal List<string>? ColumnLabels { get; set; }


        public void AddColumn()
        {
            AddColumn(ColumnType.String, null, null, null, null, null);
        }

        public void AddColumn(ColumnType columnType)
        {
            AddColumn(columnType, null, null, null, null, null);
        }

        public void AddColumn(ColumnType columnType, string label)
        {
            AddColumn(columnType, label, null, null, null, null);
        }

        public void AddColumn(ColumnType columnType, string label, string id)
        {
            AddColumn(columnType, label, id, null, null, null);
        }

        public void AddColumn(ColumnType columnType, string label, string id, Action<JsonWriter> valueWriter)
        {
            AddColumn(columnType, label, id, null, null, valueWriter);
        }

        public void AddColumn(ColumnType columnType, ColumnRole role)
        {
            AddColumn(columnType, null, null, role, null, null);
        }

        public void AddColumn(ColumnType columnType, ColumnRole role, string id)
        {
            AddColumn(columnType, null, id, role, null, null);
        }




        internal void AddColumn(ColumnType columnType, string? label, string? id, ColumnRole? role, Type? valueType, Action<JsonWriter>? valueWriter)
        {
            AddColumn(new ColumnMeta(id, label, columnType, role, valueType, valueWriter));
        }

        internal void AddColumn(ColumnMeta columnMeta)
        {
            if (string.IsNullOrEmpty(columnMeta.Id))
            {
                columnMeta.Id = "Column" + Columns.Count;
            }

            if (Columns.Any(x => x.Id == columnMeta.Id))
            {
                throw new Exception($"Column id '{columnMeta.Id}' is already used");
            }

            columns.Add(columnMeta);
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
