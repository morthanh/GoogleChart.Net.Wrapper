using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Drawing;

namespace GoogleChart.Net.Wrapper.Options
{
    public sealed class ChartBackgroundColor
    {
        [JsonIgnore]
        public string? Value { get; }

        [JsonProperty("stroke")]
        public string? Stroke { get; }

        [JsonProperty("fill")]
        public string? Fill { get; }

        [JsonProperty("strokeWidth")]
        public int? StrokeWidth { get; }

        private ChartBackgroundColor(string? value, string? stroke, int? strokeWidth, string? fill)
        {
            Value = value;
            Stroke = stroke;
            Fill = fill;
            StrokeWidth = strokeWidth;
        }

        public static ChartBackgroundColor Create(string value) => new ChartBackgroundColor(value, null, null, null);
        public static ChartBackgroundColor Create(string stroke, int strokeWidth, string fill) => new ChartBackgroundColor(null, stroke, strokeWidth, fill);

        public static implicit operator ChartBackgroundColor(string value) => Create(value);
        public static implicit operator ChartBackgroundColor(Color color) => Create("#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
    }
}
