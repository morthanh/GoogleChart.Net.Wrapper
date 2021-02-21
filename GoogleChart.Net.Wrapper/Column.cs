using GoogleChart.Net.Wrapper.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace GoogleChart.Net.Wrapper
{

    /// <summary>
    /// Represents a column for use with <see cref="DataTable"/>. 
    /// </summary>
    public class ColumnMeta
    {


        /// <summary>
        /// Optional id for use when referencing this <see cref="Column"/> from JavaScript
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; internal set; }

        [JsonProperty("label")]
        public string? Label { get; }

        [JsonProperty("type")]
        //[JsonConverter(typeof(StringEnumConverter))]
        public ColumnType ColumnType { get; }

        [JsonProperty("role")]
        //[JsonConverter(typeof(StringEnumConverter))]
        public ColumnRole? Role { get; }

        [JsonIgnore]
        public Type ValueType { get;  }

        [JsonIgnore]
        public Action<JsonWriter>? WriterAction { get; }

        internal ColumnMeta(string? id, string? label, ColumnType columnType, ColumnRole? role, Type? valueType, Action<JsonWriter>? writerAction)
        {
            Id = id;
            Label = label;
            ColumnType = columnType;
            Role = role;
            ValueType = valueType ?? GetDefaultColumnValueType(columnType);
            WriterAction = writerAction;
        }
        private Type GetDefaultColumnValueType(ColumnType columnType)
        {
            return columnType switch
            {
                ColumnType.String => typeof(string),
                ColumnType.Number => typeof(double),
                ColumnType.Boolean => typeof(bool),
                ColumnType.Date => typeof(DateTime),
                ColumnType.Datetime => typeof(DateTime),
                ColumnType.Timeofday => typeof(TimeSpan),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
