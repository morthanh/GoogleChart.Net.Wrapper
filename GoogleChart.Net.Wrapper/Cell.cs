using Newtonsoft.Json;
using System;
using System.Globalization;

namespace GoogleChart.Net.Wrapper
{
    public class Cell
    {
        private object v;

        [JsonProperty("v")]
        public object Value { get => v; set => this.v = value; }
        public string Formatted { get; }
        [JsonProperty("f")]
        public string FormattedValue { get; set; }


        public Cell(object value) : this(value, null) { }

        public Cell(object value, string formatted)
        {
            Value = value;
            Formatted = formatted;
        }

        internal void WriteValue(ColumnType columnType, JsonWriter writer, bool isLabels)
        {
            if (isLabels)
            {
                writer.WritePropertyName("v");
                writer.WriteValue(v.ToString());
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
                                writer.WriteValue( (decimal)v);
                                break;
                            case TypeCode.Double:
                                writer.WriteValue((double)v);
                                break;
                            case TypeCode.Single:
                                writer.WriteValue((float)v);
                                break;
                            case TypeCode.Int32:
                                writer.WriteValue( (int)v);
                                break;
                            default:
                                throw new NotSupportedException("Unsupported type " + v.GetType().FullName);
                        }
                        break;
                    case ColumnType.String:
                        writer.WriteValue( v.ToString());
                        break;
                    case ColumnType.Boolean:
                        writer.WriteValue( (bool)v);
                        break;
                    case ColumnType.Date:
                        var d = (DateTime)v;
                        writer.WriteValue( string.Format("Date({0}, {1}, {2})", d.Year, d.Month - 1, d.Day));
                        break;
                    case ColumnType.Datetime:
                        var dt = (DateTime)v;
                        writer.WriteValue( string.Format("Date({0}, {1}, {2}, {3}, {4}, {5})", dt.Year, dt.Month - 1, dt.Day,
                                                    dt.Hour, dt.Minute, dt.Second));
                        break;
                    case ColumnType.Timeofday:
                        var tod = v is DateTime time ? time.TimeOfDay : (TimeSpan)v;
                        writer.WriteValue( string.Format("[\"{0}, {1}, {2}\"]", tod.Hours, tod.Minutes, tod.Seconds));
                        break;
                    default:
                        throw new Exception($"Columntype '{columnType}' not supported");
                }

            }


        }


    }
}
