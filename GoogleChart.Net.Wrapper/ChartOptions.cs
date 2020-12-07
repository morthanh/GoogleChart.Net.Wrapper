using GoogleChart.Net.Wrapper.JsonConverters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{
    public class ChartOptions
    {
        private UnitSize? height;
        private UnitSize? width;

        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize? Height { get => height; set { height = value; if (height != null) height.Parent = this; } }

        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize? Width { get => width; set { width = value; if (width != null) width.Parent = this; } }

        public string ToJson()
        {
            return JsonSerializer.Serialize<object>(this, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }

    public class GaugeOptions : ChartOptions
    {
        public string? GreenColor { get; set; }

        public double? GreenFrom { get; set; }

        public double? GreenTo { get; set; }

        public double? Max { get; set; }

        public double? Min { get; set; }

        public IEnumerable<string>? MajorTicks { get; set; }

        public int MinorTicks { get; set; }

        public string? RedColor { get; set; }

        public double? RedFrom { get; set; }

        public double? RedTo { get; set; }

        public string? YellowColor { get; set; }

        public double? YellowFrom { get; set; }

        public double? YellowTo { get; set; }

    }

    public class GaugeAnimation
    {
        public double? Duration { get; set; }

        public GaugeAnimationFunction? Easing { get; set; }
    }

    public enum GaugeAnimationFunction
    {
        Linear,
        In,
        Out,
        InAndOut
    }

    public class AxisChartOptions : ChartOptions
    {

        [JsonStringEnumCamelCaseConverter]
        public TitlePosition? TitlePosition { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public AxisTitlesPosition? AxisTitlesPosition { get; set; }

        public LegendOptions Legend { get; set; }

        public string Title { get; set; }

        [JsonConverter(typeof(CharBackgroundColorConverter))]
        public ChartBackgroundColor BackgroundColor { get; set; }

        public ChartArea ChartArea { get; set; }

        public Crosshair Crosshair { get; set; }

    }

    public sealed class ChartArea
    {
        [JsonConverter(typeof(CharBackgroundColorConverter))]
        public ChartBackgroundColor BackgroundColor { get; set; }
        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize Left { get; set; }
        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize Top { get; set; }
        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize Width { get; set; }
        [JsonConverter(typeof(UnitSizeConverter))]
        public UnitSize Height { get; set; }
    }

    public sealed class ChartBackgroundColor
    {
        [JsonIgnore]
        public string? Value { get; }

        public string? Stroke { get; }

        public string? Fill { get; }

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


    public sealed class Crosshair
    {
        private double? opacity;

        public string? Color { get; set; }
        public CrosshairFocused? Focused { get; set; }
        public CrosshairSelected? Selected { get; set; }
        public double? Opacity { get => opacity; set => opacity = 0 >= value && value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof(Opacity)); }

        [JsonStringEnumCamelCaseConverter]
        public CrosshairOrientation? Orientation { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public CrosshairTrigger Trigger { get; set; }
    }

    public enum CrosshairOrientation
    {
        Vertical,
        Horizontal,
        Both
    }

    public enum CrosshairTrigger
    {
        Focus,
        Selection,
        Both
    }

    public sealed class CrosshairFocused
    {
        private double? opacity;
        public string? Color { get; set; }
        public double? Opacity { get => opacity; set => opacity = 0 >= value && value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof(Opacity)); }

    }

    public sealed class CrosshairSelected
    {
        private double? opacity;
        public string? Color { get; set; }
        public double? Opacity { get => opacity; set => opacity = 0 >= value && value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof(Opacity)); }

    }

    public sealed class UnitSize
    {
        public string? Value { get; }

        internal object? Parent { get; set; }

        private UnitSize(string value)
        {
            Value = value;
        }

        public static UnitSize Pixel(int pixels) => new UnitSize(pixels.ToString());
        public static UnitSize Percent(double percent) => new UnitSize(percent.ToString("0.##\\%"));
        public static UnitSize Em(double em) => new UnitSize(em.ToString("0.##em"));

        public static implicit operator UnitSize(int i) => Pixel(i);
    }



    public class LegendOptions
    {
        [JsonStringEnumCamelCaseConverter]
        public LegendAlignment? Alignment { get; set; }

        public int? MaxLines { get; set; }

        public int? PageIndex { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public LegendPosition? Position { get; set; }
    }

    public enum LegendPosition
    {
        Bottom,
        Left,
        In,
        None,
        Right,
        Top
    }

    public enum LegendAlignment
    {
        Start,
        Center,
        End
    }

    public class LineChartOptions : AxisChartOptions
    {

        public int? LineWidth { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public CurveType CurveType { get; set; }


    }

    public enum CurveType
    {
        None,
        Function
    }

    public class AreaChartOptions : AxisChartOptions
    {
        private double? areaOpacity;

        public StackedOption IsStacked { get; set; }

        public double? AreaOpacity { get => areaOpacity; set => areaOpacity = 0 >= value && value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof(AreaOpacity)); }
    }


    public class TableChartOptions : ChartOptions
    {
        public bool? AlternatingRowStyle { get; set; }

        public TableCssClassNames CssClassNames { get; set; }

        public int? FirstRowNumber { get; set; }

        public int? FrozenColumns { get; set; }

        public bool? ShowRowNumber { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public ColumnSortMode? Sort { get; set; }

        public bool? SortAscending { get; set; }

        public int? SortColumn { get; set; }
    }

    public class TableCssClassNames
    {
        public string HeaderRow { get; set; }
        public string TableRow { get; set; }
        public string OddTableRow { get; set; }
        public string SelectedTableRow { get; set; }
        public string HoverTableRow { get; set; }
        public string HeaderCell { get; set; }
        public string TableCell { get; set; }
        public string RowNumberCell { get; set; }
    }

    public enum StackedOption
    {
        False,
        True,
        Percent,
        Relative,
        Absolute
    }

    public enum TitlePosition
    {
        In,
        Out,
        None
    }

    public enum AxisTitlesPosition
    {
        In,
        Out,
        None
    }

    public enum ColumnSortMode
    {
        Enable,
        Event,
        Disable
    }
}
