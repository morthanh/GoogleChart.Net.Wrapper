using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoogleChart.Net.Wrapper.JsonConverters
{

    public sealed class ChartDataTableConverter : JsonConverter<DataTable>
    {
        public override DataTable ReadJson(JsonReader reader, Type objectType, DataTable? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


        public override void WriteJson(JsonWriter writer, DataTable? dt, JsonSerializer serializer)
        {
            if (dt == null)
            { 
                return;
            }

            if (dt.ColumnLabels != null && dt.Columns.Count != dt.ColumnLabels.Count)
            {
                throw new Exception("Number of column labels does not equal number of columns");
            }


            var columnsMeta = dt.Columns;

            writer.WriteStartObject();

            //write cols
            writer.WritePropertyName("cols");
            WriteCols(writer, columnsMeta, serializer);


            //write rows
            writer.WritePropertyName("rows");
            WriteRows(writer, dt.Values, columnsMeta, dt.ColumnLabels);


            writer.WriteEndObject();
        }



        private void WriteCols(JsonWriter writer, IReadOnlyList<ColumnMeta> columns, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var column in columns)
            {
                serializer.Serialize(writer, column);
            }
            writer.WriteEndArray();
        }



        private void WriteRows(JsonWriter writer, IEnumerable<object> valuesSource, IReadOnlyList<ColumnMeta> columns, IList<string> columnLabels)
        {
            int i = 0;
            int numColumns = columns.Count;


            writer.WriteStartArray();

            if (columnLabels?.Count > 0)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("c");
                writer.WriteStartArray();

                foreach (var label in columnLabels)
                {
                    writer.WriteStartObject();
                    WriteLabelValue(label, writer);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            foreach (var val in valuesSource)
            {

                if (i % numColumns == 0)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("c");
                    writer.WriteStartArray();
                }

                writer.WriteStartObject();
                WriteValue(val, columns[i % numColumns], writer);
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


        internal void WriteLabelValue(object v, JsonWriter writer)
        {
            writer.WritePropertyName("v");
            writer.WriteValue(v.ToString());
        }

        internal void WriteValue(object v, ColumnMeta columnMeta, JsonWriter writer)
        {
            if (columnMeta is null)
            {
                throw new ArgumentNullException(nameof(columnMeta));
            }

            writer.WritePropertyName("v");
            if (v is null)
            {
                writer.WriteNull();
                return;
            }

            object val = v;
            string? formattedVal;

            if (v is Cell cell)
            {
                val = cell.Value;
                formattedVal = cell.Formatted;
            }


            if (columnMeta.WriterAction != null)
            {
                columnMeta.WriterAction(writer);
            }
            else
            {
                switch (columnMeta.ColumnType)
                {
                    case ColumnType.Number:

                        var nullValType = Nullable.GetUnderlyingType(columnMeta.ValueType);
                        if (nullValType != null && val is null)
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            if (val is decimal dec) writer.WriteValue(dec);
                            else if (val is double dou) writer.WriteValue(dou);
                            else if (val is float flo) writer.WriteValue(flo);
                            else if (val is int i) writer.WriteValue(i);
                            else if (val is long l) writer.WriteValue(l);
                            else throw new NotSupportedException("Unsupported type " + v.GetType().FullName);
                        }

                        break;
                    case ColumnType.String:
                        writer.WriteValue(val.ToString());
                        break;
                    case ColumnType.Boolean:
                        writer.WriteValue((bool)val);
                        break;
                    case ColumnType.Date:
                        var d = (DateTime)val;
                        writer.WriteValue(string.Format("Date({0}, {1}, {2})", d.Year, d.Month - 1, d.Day));
                        break;
                    case ColumnType.Datetime:
                        var dt = (DateTime)val;
                        writer.WriteValue(string.Format("Date({0}, {1}, {2}, {3}, {4}, {5})", dt.Year, dt.Month - 1, dt.Day,
                                                    dt.Hour, dt.Minute, dt.Second));
                        break;
                    case ColumnType.Timeofday:
                        var tod = val is DateTime time ? time.TimeOfDay : (TimeSpan)val;

                        writer.WriteStartArray();
                        writer.WriteValue(tod.Hours);
                        writer.WriteValue(tod.Minutes);
                        writer.WriteValue(tod.Seconds);
                        writer.WriteEndArray();


                        //writer.WriteRaw( string.Format("[{0}, {1}, {2}]", tod.Hours, tod.Minutes, tod.Seconds));
                        break;
                    default:
                        throw new Exception($"Columntype '{columnMeta.ColumnType}' not supported");
                }
            }



        }


    }
}
