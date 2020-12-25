using GoogleChart.Net.Wrapper.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;

namespace GoogleChart.Net.Wrapper.Options
{
    public class ChartOptions
    {
        private UnitSize? height;
        private UnitSize? width;

        [JsonProperty("height")]
        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize? Height { get => height; set { height = value; if (height != null) height.Parent = this; } }

        [JsonProperty("width")]
        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize? Width { get => width; set { width = value; if (width != null) width.Parent = this; } }

        public string ToJson(bool formatted = false)
        {
            return SerializerHelper.Serialize(this, formatted);
        }
    }
}
