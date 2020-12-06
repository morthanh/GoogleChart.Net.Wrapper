using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;
using System.Drawing;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class LineChart3Model : ExamplePageModelBase
    {

        public void OnGet()
        {
            int i = 0;
            var data = Data.TestData.Data2DimRandom.ToDataTable(conf =>
            {
                conf.AddColumn("Series 1", x => i++);
                conf.AddColumn("Series 2", x => x.Item1);
                conf.AddColumn(x => x.Item2);
                conf.WithOptions<LineChartOptions>(options =>
                {
                    options.Title = "My linechart with options";
                    options.CurveType = CurveType.Function;
                    options.BackgroundColor = ChartBackgroundColor.Create("gray", 1, "aliceblue");
                    options.ChartArea = new ChartArea
                    {
                        BackgroundColor = Color.White,
                        Top = 20
                    };
                    options.Crosshair = new Crosshair
                    {
                        Color = "red",
                    };
                });
            });


            (DataJson, OptionsJson) = data;
        }
    }
}
