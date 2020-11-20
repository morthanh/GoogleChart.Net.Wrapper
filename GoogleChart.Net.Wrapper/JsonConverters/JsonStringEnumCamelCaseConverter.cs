using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper.JsonConverters
{


    public sealed class JsonStringEnumCamelCaseConverter : JsonConverterAttribute
    {
        public override JsonConverter CreateConverter(Type typeToConvert)
        {
            return new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase, false);
        }
    }


    public sealed class CellValueConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {

        }
    }

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

            for (int i = 0; i < row.Cells.Count; i++)
            {
                Cell cell = row.Cells[i];
                writer.WriteStartObject();

                cell.WriteValue(row.DataTable.ColumnTypes[i], writer, row.IsLabels);

                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
