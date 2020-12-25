using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using System;

namespace GoogleChart.Net.Wrapper.JsonConverters
{
    public sealed class ChartColorConverter : JsonConverter<ChartColor>
    {

        public override ChartColor ReadJson(JsonReader reader, Type objectType, ChartColor existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


        public override void WriteJson(JsonWriter writer, ChartColor chartColor, JsonSerializer serializer)
        {
            writer.WriteValue(chartColor.HtmlValue);
        }
    }
}
