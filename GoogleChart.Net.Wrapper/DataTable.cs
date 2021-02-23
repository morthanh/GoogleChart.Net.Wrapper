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
    public class DataTableBase
    {
        [JsonIgnore]
        public ChartOptions? Options { get; set; }


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

    /// <summary>
    /// Holds all columns, row values, and options used by Google Chart Javascript Api. 
    /// </summary>
    [JsonConverter(typeof(ChartDataTableConverter<>))]
    public class DataTable<T> : DataTableBase
    {
        private readonly List<ColumnMeta<T>> columns = new List<ColumnMeta<T>>();

        internal DataTable(IEnumerable<ValueSourceItem<T>> valueSource)
        {
            Values = valueSource;
        }


        [JsonIgnore]

        public IReadOnlyList<ColumnMeta<T>> Columns => columns.AsReadOnly();

        [JsonIgnore]
        internal IEnumerable<ValueSourceItem<T>> Values { get; }

        internal List<string>? ColumnLabels { get; set; }


        internal void AddColumn(ColumnMeta<T> columnMeta)
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
    }
}
