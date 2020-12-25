using GoogleChart.Net.Wrapper.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoogleChart.Net.Wrapper
{

    /// <summary>
    /// Represents a column for use with <see cref="DataTable"/>. 
    /// </summary>
    public class Column
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

        public Column() : this(ColumnType.String, null, null, null) { }

        public Column(ColumnType columnType) : this(columnType, null, null, null) { }

        public Column(ColumnType columnType, string? label) : this(columnType, label, null, null) { }

        public Column(ColumnType columnType, string? label, string? id) : this(columnType, label, id, null) { }

        public Column(ColumnType columnType, string? label, string? id, ColumnRole? role)
        {
            ColumnType = columnType;
            Id = id;
            Label = label;
            Role = role;
        }






    }
}
