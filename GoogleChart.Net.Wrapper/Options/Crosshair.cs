using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace GoogleChart.Net.Wrapper.Options
{
    public sealed class Crosshair
    {
        private double? opacity;

        [JsonProperty("color")]
        public string? Color { get; set; }

        [JsonProperty("focused")]
        public CrosshairFocused? Focused { get; set; }

        [JsonProperty("selected")]
        public CrosshairSelected? Selected { get; set; }

        [JsonProperty("opacity")]
        public double? Opacity { get => opacity; set => opacity = 0 >= value && value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof(Opacity)); }

        [JsonProperty("orientation")]
        public CrosshairOrientation? Orientation { get; set; }

        [JsonProperty("trigger")]
        public CrosshairTrigger Trigger { get; set; }
    }
}
