using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using System;

namespace GoogleChart.Net.Wrapper.JsonConverters
{
    public sealed class ChartBackgroundColorConverter : JsonConverter<ChartBackgroundColor>
    {

        public override ChartBackgroundColor ReadJson(JsonReader reader, Type objectType, ChartBackgroundColor existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


        public override void WriteJson(JsonWriter writer, ChartBackgroundColor chartBackgroundColor, JsonSerializer serializer)
        {
            if (!string.IsNullOrEmpty(chartBackgroundColor.Value))
            {
                writer.WriteValue(chartBackgroundColor.Value);
            }
            else
            {
                serializer.Serialize(writer, chartBackgroundColor);
            }
        }
    }
}
