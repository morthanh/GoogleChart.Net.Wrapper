using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleChart.Net.Wrapper.Options
{
    public class HorizontalAxis
    {
        [JsonProperty("baseline")]
        public object? Baseline { get; set; }

        [JsonProperty("baselineColor")]
        public ChartColor? BaselineColor { get; set; }

        [JsonProperty("direction")]
        public int? Direction { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("gridlines")]
        public AxisGridLines? Gridlines { get; set; }

        [JsonProperty("minorGridlines")]
        public AxisGridLines? MinorGridlines { get; set; }

        [JsonProperty("logScale")]
        public bool? LogScale { get; set; }

        //TODO: hAxis.scaleType

        [JsonProperty("textPosition")]
        public AxisTextPosition TextPosition { get; set; }

        [JsonProperty("textStyle")]
        public AxisTextStyle? TextStyle { get; set; }

        //TODO: hAxis.ticks

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("titleTextStyle")]
        public AxisTextStyle? TitleTextStyle { get; set; }

        //hAxis.allowContainerBoundaryTextCutoff

        [JsonProperty("slantedText")]
        public bool? SlantedText { get; set; }

        [JsonProperty("slantedTextAngle")]
        public int? SlantedTextAngle { get; set; }

        [JsonProperty("maxAlternation")]
        public int? MaxAlternation { get; set; }

        [JsonProperty("maxTextLines")]
        public int? MaxTextLines { get; set; }

        [JsonProperty("minTextSpacing")]
        public int? MinTextSpacing { get; set; }

        [JsonProperty("showTextEvery")]
        public int? ShowTextEvery { get; set; }

        [JsonProperty("maxValue")]
        public int? MaxValue { get; set; }

        [JsonProperty("minValue")]
        public int? MinValue { get; set; }

        [JsonProperty("viewWindowMode")]
        public ViewWindowMode? ViewWindowMode { get; set; }

        [JsonProperty("viewWindow")]
        public ViewWindow? ViewWindow { get; set; }

    }
}
