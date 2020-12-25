using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace GoogleChart.Net.Wrapper.Tests
{
    public static class JsonHelper
    {

        public static string SerializeFormatted(object obj)
        {
            return JsonConvert.SerializeObject( obj, Formatting.Indented);
        }

        public static JObject Deserialize(string json)
        {
            return JObject.Parse(json);
        }

    }
}
