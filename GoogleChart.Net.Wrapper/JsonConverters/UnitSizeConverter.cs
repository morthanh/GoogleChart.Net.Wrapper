using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using System;

namespace GoogleChart.Net.Wrapper.JsonConverters
{
    public sealed class UnitSizeConverter : JsonConverter<UnitSize>
    {

        public override UnitSize ReadJson(JsonReader reader, Type objectType, UnitSize existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, UnitSize unitSize, JsonSerializer serializer)
        {
            if (unitSize.Parent is LineChartOptions)
            {
                if (!int.TryParse(unitSize.Value, out var pixels))
                {
                    throw new Exception("For a line chart the width and height needs to be integer values");
                }
                writer.WriteValue(pixels);

            }
            else if (unitSize.Parent is TableChartOptions)
            {
                writer.WriteValue(unitSize.Value);
            }
            else
            {
                writer.WriteValue(unitSize.Value);
            }
        }
    }
}
