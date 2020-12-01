using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper.JsonConverters
{
    public sealed class RowConverter : JsonConverter<Row>
    {
        public override Row Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Row row, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteStartArray("c");

            int i = 0;
            foreach (var cell in row.Cells)
            {
                var columnType = row.DataTable.ColumnTypes[i];
                writer.WriteStartObject();

                cell.WriteValue(row.DataTable.ColumnTypes[i], writer, row.IsLabels);

                writer.WriteEndObject();
                i++;
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
