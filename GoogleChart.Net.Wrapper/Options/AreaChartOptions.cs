using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Drawing;

namespace GoogleChart.Net.Wrapper.Options
{
    public class AreaChartOptions : AxisChartOptions
    {
        private double? areaOpacity;

        [JsonProperty("isStacked")]
        public StackedOption IsStacked { get; set; }

        [JsonProperty("areaOpacity")]
        public double? AreaOpacity { get => areaOpacity; set => areaOpacity = 0 <= value && value <= 1 ? value : throw new ArgumentOutOfRangeException(nameof(AreaOpacity)); }

        [JsonProperty("hAxis")]
        public HorizontalAxis? HorizontalAxis { get; set; }



    }

    public enum FocusTarget
    {
        Datum,
        Category
    }


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


    }

    public class AxisTextStyle
    {
        [JsonProperty("color")]
        public ChartColor? Color { get; set; }

        [JsonProperty("fontName")]
        public string? FontName { get; set; }

        [JsonProperty("fontSize")]
        public int? FontSize { get; set; }

        [JsonProperty("bold")]
        public bool? Bold { get; set; }

        [JsonProperty("italic")]
        public bool? Italic { get; set; }
    }



    public enum AxisTextPosition
    {
        None,
        Out,
        In
    }

    public class AxisGridLines
    {
        [JsonProperty("color")]
        public ChartColor? Color { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }

        //TODO: hAxis.gridlines.interval

        [JsonProperty("minSpacing")]
        public int? MinSpacing { get; set; }

        [JsonProperty("multiple")]
        public int? Multiple { get; set; }

        //TODO: hAxis.gridlines.units
    }

    public class ChartColor
    {
        private ChartColor(string htmlValue)
        {
            HtmlValue = htmlValue;
        }
        public string HtmlValue { get; }

        public static implicit operator ChartColor(string value) => new ChartColor(value);
        public static implicit operator ChartColor(Color color) => new ChartColor("#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
    }
}
