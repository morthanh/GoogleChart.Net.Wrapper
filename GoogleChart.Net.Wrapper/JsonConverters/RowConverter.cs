using Newtonsoft.Json;
using System;

namespace GoogleChart.Net.Wrapper.JsonConverters
{
    public sealed class RowConverter : JsonConverter<Row>
    {

        public override Row ReadJson(JsonReader reader, Type objectType, Row existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }



        public override void WriteJson(JsonWriter writer,  Row row, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("c");
            writer.WriteStartArray();

            int i = 0;
            foreach (var cell in row.Cells)
            {
                var columnType = row.DataTable.Columns[i];
                writer.WriteStartObject();

                //cell.WriteValue(row.DataTable.Columns[i], writer, row.IsLabels);

                writer.WriteEndObject();
                i++;
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
