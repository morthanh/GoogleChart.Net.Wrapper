using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class AreaChartModel : ExamplePageModelBase
    {

        public void OnGet()
        {
            DataJson = Data.TestData.Data2DimWithLabels.ToDataTable(conf =>
            {
                conf.AddColumn(x => x.Item1);
                conf.AddColumn("Series 1", x => x.Item2);
                conf.AddColumn("Series 2", x => x.Item3);
            }).ToJson();


            var options = new AreaChartOptions
            {
                Height = 400,
                Width = 800,
                Title = "My areachart",
                IsStacked = StackedOption.True,
                Legend = new LegendOptions { Position = LegendPosition.In }
            };

            OptionsJson = options.ToJson();

        }
    }
}
