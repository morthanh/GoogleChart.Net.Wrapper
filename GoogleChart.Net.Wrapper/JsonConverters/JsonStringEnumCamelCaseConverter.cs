using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper.JsonConverters
{


    public sealed class JsonStringEnumCamelCaseConverter : JsonConverterAttribute
    {
        public override JsonConverter CreateConverter(Type typeToConvert)
        {
            return new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase, false);
        }
    }


}
