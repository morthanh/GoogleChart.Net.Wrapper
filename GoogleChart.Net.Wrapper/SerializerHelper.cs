using GoogleChart.Net.Wrapper.JsonConverters;
using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoogleChart.Net.Wrapper
{
    public static class SerializerHelper
    {

        public static JsonSerializer CreateSerializer(bool formatted = false)
        {
            return new JsonSerializer()
            {
                Formatting = formatted ? Formatting.Indented : Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                Converters =
                {
                    new ChartColorConverter(),
                    new UnitSizeConverter(),
                    new JavaScriptDateTimeConverter(),
                    new StringEnumConverter
                    {
                        NamingStrategy = new CamelCaseNamingStrategy ()
                    }
                }
            };
        }

        public static string Serialize(object obj, bool formatted = false)
        {
            var serializer = CreateSerializer(formatted);

            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                //writer.QuoteName = false;
                serializer.Serialize(writer, obj);
            }
            return stringWriter.ToString();

        }

    }
}
