using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GoogleChart.Net.Wrapper.Tests
{
    public static class JsonHelper
    {

        public static string SerializeFormatted(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }

        public static JsonElement Deserialize(string json)
        {
            return (JsonElement)JsonSerializer.Deserialize<object>(json);
        }

    }
}
