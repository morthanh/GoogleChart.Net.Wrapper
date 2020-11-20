using GoogleChart.Net.Wrapper.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{
    public class BaseChartOptions
    {
        //[JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public TitlePosition? TitlePosition { get; set; }

        [JsonStringEnumCamelCaseConverter]
        public AxisTitlesPosition? AxisTitlesPosition { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public LegendOptions Legend { get; set; }



        public string ToJson()
        {
            return JsonSerializer.Serialize<object>(this, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
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

    public class LineChartOptions : BaseChartOptions
    {

        public int? LineWidth { get; set; }


    }

    public class AreaChartOptions : BaseChartOptions
    {
        public StackedOption IsStacked { get; set; }
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
}
