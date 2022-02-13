using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Options;
using System.Drawing;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class AreaChart1Model : ExamplePageModelBase
    {

        public void OnGet()
        {
            int i = 1;
            DataJson = Data.TestData.Data2DimWithLabels.ToDataTable(conf =>
            {
                conf.AddColumn(x => new DateTime(2020, 10, i++), (r, v) => v.ToString("yyyy-MM-dd"));
                conf.AddColumn("Apples", x => x.Item2);
                conf.AddColumn("Oranges", x => x.Item3);
            }).ToJson();


            var options = new AreaChartOptions
            {
                Height = 400,
                Width = 800,
                Title = "Car sales",
                IsStacked = StackedOption.True,
                Legend = new LegendOptions { Position = LegendPosition.In },
                HorizontalAxis = new HorizontalAxis
                {
                    Baseline = new DateTime(2020, 10, 3),
                    BaselineColor = Color.Red,
                    Direction = -1,
                    Gridlines = new AxisGridLines
                    {
                        Color = Color.Blue,
                        Multiple = 2
                    }
                }
            };

            OptionsJson = options.ToJson();

        }
    }
}
