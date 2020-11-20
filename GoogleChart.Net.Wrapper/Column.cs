using GoogleChart.Net.Wrapper.JsonConverters;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        [JsonPropertyName("label")]
        public string Label { get; }

        [JsonPropertyName("type")]
        [JsonStringEnumCamelCaseConverter]
        public ColumnType ColumnType { get; }

        [JsonPropertyName("role")]
        public ColumnRole? Role { get; }

        public Column() : this(ColumnType.String, null, null, null) { }

        public Column(ColumnType columnType) : this(columnType, null, null, null) { }

        public Column(ColumnType columnType, string id) : this(columnType, id, null, null) { }

        public Column(ColumnType columnType, string id, string label) : this(columnType, id, label, null) { }

        public Column(ColumnType columnType, string id, string label, ColumnRole? role)
        {
            ColumnType = columnType;
            Id = id;
            Label = label;
            Role = role;
        }






    }
}
