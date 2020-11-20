using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{
    public class Cell
    {
        private object v;

        [JsonPropertyName("v")]
        public object Value { get => v; set => this.v = value; }
        public string Formatted { get; }
        [JsonPropertyName("f")]
        public string FormattedValue { get; set; }


        public Cell(object value) : this(value, null) { }

        public Cell(object value, string formatted)
        {
            Value = value;
            Formatted = formatted;
        }

        internal void WriteValue(ColumnType columnType, Utf8JsonWriter writer, bool isLabels)
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
