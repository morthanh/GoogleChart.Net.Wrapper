using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper.JsonConverters
{
    public sealed class DataTableLinqConverter : JsonConverter<DataTableLinq>
    {

        JsonStringEnumConverter enumCamelCaseConverterFactory = new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase, false);



        public override DataTableLinq Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DataTableLinq dt, JsonSerializerOptions options)
        {

            writer.WriteStartObject();

            //write cols
            writer.WritePropertyName("cols");
            JsonSerializer.Serialize(writer, dt.Columns, typeof(IList<Column>), options);


            //write rows
            WriteRows(writer, dt.ValuesSource, dt.ColumnTypes);


            writer.WriteEndObject();
        }



        private void WriteRows(Utf8JsonWriter writer, IEnumerable<object> valuesSource, IList<ColumnType> columnTypes)
        {
            int i = 0;
            int numColumns = columnTypes.Count;


            writer.WriteStartArray("rows");


            foreach(var val in valuesSource)
            {
                if (i % numColumns == 0)
                {
                    writer.WriteStartObject();
                    writer.WriteStartArray("c");
                }

                writer.WriteStartObject();
                WriteValue(val, columnTypes[i % numColumns], writer, false);
                writer.WriteEndObject();

                if (i % numColumns == numColumns - 1)
                {
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }

                i++;
            }

            writer.WriteEndArray();

        }

        internal void WriteValue(object v, ColumnType columnType, Utf8JsonWriter writer, bool isLabels)
        {
            if (isLabels)
            {
                writer.WriteString("v", v.ToString());
            }
            else
            {

                switch (columnType)
                {
                    case ColumnType.Number:
                        switch (Type.GetTypeCode(v.GetType()))
                        {
                            case TypeCode.Decimal:
                                writer.WriteNumber("v", (decimal)v);
                                break;
                            case TypeCode.Double:
                                writer.WriteNumber("v", (double)v);
                                break;
                            case TypeCode.Single:
                                writer.WriteNumber("v", (float)v);
                                break;
                            case TypeCode.Int32:
                                writer.WriteNumber("v", (int)v);
                                break;
                            default:
                                throw new NotSupportedException("Unsupported type " + v.GetType().FullName);
                        }
                        break;
                    case ColumnType.String:
                        writer.WriteString("v", v.ToString());
                        break;
                    case ColumnType.Boolean:
                        writer.WriteBoolean("v", (bool)v);
                        break;
                    case ColumnType.Date:
                        var d = (DateTime)v;
                        writer.WriteString("v", string.Format("new Date({0}, {1}, {2})", d.Year, d.Month - 1, d.Day));
                        break;
                    case ColumnType.Datetime:
                        var dt = (DateTime)v;
                        writer.WriteString("v", string.Format("new Date({0}, {1}, {2}, {3}, {4}, {5})", dt.Year, dt.Month - 1, dt.Day,
                                                    dt.Hour, dt.Minute, dt.Second));
                        break;
                    case ColumnType.Timeofday:
                        var tod = v is DateTime time ? time.TimeOfDay : (TimeSpan)v;
                        writer.WriteString("v", string.Format("[\"{0}, {1}, {2}\"]", tod.Hours, tod.Minutes, tod.Seconds));
                        break;
                    default:
                        throw new Exception($"Columntype '{columnType}' not supported");
                }
            }


        }

    }
}
