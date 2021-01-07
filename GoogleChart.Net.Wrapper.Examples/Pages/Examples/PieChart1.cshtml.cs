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
    public class PieChart1Model : ExamplePageModelBase
    {
        public void OnGet()
        {
            var data = Data.TestData.Data1DimWithLabels.ToDataTable(conf =>
            {
                conf.AddColumn(x => x.Item1);
                conf.AddColumn(x => x.Item2);
            });

            DataJson = data.ToJson();


            var options = new PieChartOptions
            {
                Height = 400,
                Width = 500,
                SliceVisibilityThreshold = 0.05,
                ChartArea = new ChartArea { BackgroundColor = Color.AliceBlue, Left = UnitSize.Percent(5), Width = UnitSize.Percent(90)},
                PieHole = 0.5,
                Legend = new LegendOptions { Position = LegendPosition.None}
            };

            OptionsJson = options.ToJson();

        }
    }
}
