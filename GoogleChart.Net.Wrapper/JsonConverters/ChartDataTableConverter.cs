using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoogleChart.Net.Wrapper.JsonConverters
{

    public sealed class ChartDataTableConverter : JsonConverter<DataTable>
    {
        public override DataTable ReadJson(JsonReader reader, Type objectType, DataTable existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


        public override void WriteJson(JsonWriter writer, DataTable dt, JsonSerializer serializer)
        {
            if (dt.ColumnLabels != null && dt.Columns.Count != dt.ColumnLabels.Count)
            {
                throw new Exception("Number of column labels does not equal number of columns");
            }


            writer.WriteStartObject();

            //write cols
            writer.WritePropertyName("cols");
            serializer.Serialize(writer, dt.Columns, typeof(IList<Column>));


            //write rows
            WriteRows(writer, dt.Values, dt.ColumnTypes, dt.ColumnLabels);


            writer.WriteEndObject();
        }




        private void WriteRows(JsonWriter writer, IEnumerable<object> valuesSource, IList<ColumnType> columnTypes, IList<string> columnLabels)
        {
            int i = 0;
            int numColumns = columnTypes.Count;

            writer.WritePropertyName("rows");

            writer.WriteStartArray();

            if (columnLabels?.Count > 0)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("c");
                writer.WriteStartArray();

                foreach (var label in columnLabels)
                {
                    writer.WriteStartObject();
                    WriteValue(label, columnTypes[i % numColumns], writer, true);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            foreach (var val in valuesSource)
            {
                //if (val is Row row)
                //{
                //    foreach (var cell in row.Cells)
                //    {
                //        if (i % numColumns == 0)
                //        {
                //            writer.WriteStartObject();
                //            writer.WritePropertyName("c");
                //            writer.WriteStartArray();
                //        }

                //        writer.WriteStartObject();
                //        WriteValue(cell, columnTypes[i % numColumns], writer, false);
                //        writer.WriteEndObject();

                //        if (i % numColumns == numColumns - 1)
                //        {
                //            writer.WriteEndArray();
                //            writer.WriteEndObject();
                //        }

                //        i++;
                //    }
                //}
                //else
                //{
                    if (i % numColumns == 0)
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("c");
                        writer.WriteStartArray();
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
                //}
            }

            writer.WriteEndArray();

        }

        internal void WriteValue(object v, ColumnType columnType, JsonWriter writer, bool isLabel)
        {
            if (isLabel)
            {
                writer.WritePropertyName("v");
                writer.WriteValue(v.ToString());
            }
            else if (v is Cell cell)
            {
                cell.WriteValue(columnType, writer, isLabel);
            }
            else
            {
                writer.WritePropertyName("v");

                switch (columnType)
                {
                    case ColumnType.Number:
                        switch (Type.GetTypeCode(v.GetType()))
                        {
                            case TypeCode.Decimal:
                                writer.WriteValue((decimal)v);
                                break;
                            case TypeCode.Double:
                                writer.WriteValue((double)v);
                                break;
                            case TypeCode.Single:
                                writer.WriteValue((float)v);
                                break;
                            case TypeCode.Int32:
                                writer.WriteValue((int)v);
                                break;
                            default:
                                throw new NotSupportedException("Unsupported type " + v.GetType().FullName);
                        }
                        break;
                    case ColumnType.String:
                        writer.WriteValue( v.ToString());
                        break;
                    case ColumnType.Boolean:
                        writer.WriteValue((bool)v);
                        break;
                    case ColumnType.Date:
                        var d = (DateTime)v;
                        writer.WriteValue( string.Format("Date({0}, {1}, {2})", d.Year, d.Month - 1, d.Day));
                        break;
                    case ColumnType.Datetime:
                        var dt = (DateTime)v;
                        writer.WriteValue(string.Format("Date({0}, {1}, {2}, {3}, {4}, {5})", dt.Year, dt.Month - 1, dt.Day,
                                                    dt.Hour, dt.Minute, dt.Second));
                        break;
                    case ColumnType.Timeofday:
                        var tod = v is DateTime time ? time.TimeOfDay : (TimeSpan)v;

                        writer.WriteStartArray();
                        writer.WriteValue(tod.Hours);
                        writer.WriteValue(tod.Minutes);
                        writer.WriteValue(tod.Seconds);
                        writer.WriteEndArray();


                        //writer.WriteRaw( string.Format("[{0}, {1}, {2}]", tod.Hours, tod.Minutes, tod.Seconds));
                        break;
                    default:
                        throw new Exception($"Columntype '{columnType}' not supported");
                }

            }


        }


    }
}
