using GoogleChart.Net.Wrapper.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace GoogleChart.Net.Wrapper.Options
{
    public class AxisChartOptions : ChartOptions
    {
        private double? dataOpacity;

        [JsonProperty("titlePosition")]
        public TitlePosition? TitlePosition { get; set; }

        [JsonProperty("axisTitlesPosition")]
        public AxisTitlesPosition? AxisTitlesPosition { get; set; }

        [JsonProperty("legend")]
        public LegendOptions? Legend { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("backgroundColor")]
        [JsonConverter(typeof(ChartBackgroundColorConverter))]
        public ChartBackgroundColor? BackgroundColor { get; set; }

        [JsonProperty("chartArea")]
        public ChartArea? ChartArea { get; set; }

        [JsonProperty("crosshair")]
        public Crosshair? Crosshair { get; set; }

        [JsonProperty("dataOpacity")]
        public double? DataOpacity { get => dataOpacity; set => dataOpacity = 0 <= value && value <= 1 ? value : throw new ArgumentOutOfRangeException(nameof(DataOpacity)); }

        [JsonProperty("enableInteractivity")]
        public bool? EnableInteractivity { get; set; }

        [JsonProperty("focusTarget")]
        public FocusTarget? FocusTarget { get; set; }

        [JsonProperty("fontSize")]
        public int? FontSize { get; set; }

        [JsonProperty("fontName")]
        public string? FontName { get; set; }

    }
}
