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

        [JsonProperty("interpolateNulls")]
        public bool? InterpolateNulls { get; set; }

        [JsonProperty("lineDashStyle")]
        public int[]? LineDashStyle { get; set; }

        [JsonProperty("lineWidth")]
        public int? LineWidth { get; set; }

        [JsonProperty("hAxis")]
        public HorizontalAxis? HorizontalAxis { get; set; }

        [JsonProperty("vAxis")]
        public VerticalAxis? VerticalAxis { get; set; }

        [JsonProperty("orientation")]
        public AxisChartOrientation? Orientation { get; set; }

        //TODO: pointShape

        [JsonProperty("pointSize")]
        public int? PointSize { get; set; }

        [JsonProperty("pointsVisible")]
        public bool? PointsVisible { get; set; }

        [JsonProperty("reverseCategories")]
        public bool? ReverseCategories { get; set; }

        //TODO: selectionMode

        //TODO: series

        //TODO: tooltip

        //TODO: colors

        //TODO: trendlines
    }



    public class PieChartOptions : ChartOptions
    {
        [JsonProperty("backgroundColor")]
        [JsonConverter(typeof(ChartBackgroundColorConverter))]
        public ChartBackgroundColor? BackgroundColor { get; set; }

        [JsonProperty("chartArea")]
        public ChartArea? ChartArea { get; set; }


        [JsonProperty("fontSize")]
        public int? FontSize { get; set; }

        [JsonProperty("fontName")]
        public string? FontName { get; set; }

        [JsonProperty("is3D")]
        public bool? Is3D { get; set; }

        [JsonProperty("legend")]
        public LegendOptions? Legend { get; set; }

        [JsonProperty("pieHole")]
        public double? PieHole { get; set; }

        [JsonProperty("pieSliceBorderColor")]
        public ChartColor? PieSliceBorderColor { get; set; }

        //TODO: pieSliceTextStyle

        [JsonProperty("pieStartAngle")]
        public double? PieStartAngle { get; set; }

        [JsonProperty("reverseCategories")]
        public bool? ReverseCategories { get; set; }

        [JsonProperty("pieResidueSliceColor")]
        public ChartColor? PieResidueSliceColor { get; set; }

        [JsonProperty("pieResidueSliceLabel")]
        public string? PieResidueSliceLabel { get; set; }

        //TODO: slices

        [JsonProperty("sliceVisibilityThreshold")]
        public double? SliceVisibilityThreshold { get; set; }


        [JsonProperty("title")]
        public string? Title { get; set; }

        //TODO: titleTextStyle



    }

    public enum PieSliceText
    {
        Percentage,
        Value,
        Label,
        None
    }
}
