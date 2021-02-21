using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class LineChart4Model : ExamplePageModelBase
    {
        public void OnGet()
        {
            var data = Data.TestData.Data2Dim.ToDataTable(conf =>
            {
                conf.AddColumn(ColumnType.Number, x => x.Item1);
                conf.AddColumn(ColumnType.Number, x => x.Item2);
                conf.AddColumn(ColumnType.Boolean, ColumnRole.Emphasis, x => x.Item2 > 20);
                conf.AddColumn(ColumnType.Boolean, ColumnRole.Certainty, x => x.Item2 < 30);
                conf.AddColumn(ColumnType.Number, ColumnRole.Interval, x => x.Item2 * 0.8);
                conf.AddColumn(ColumnType.Number, ColumnRole.Interval, x => x.Item2 * 1.2);
        });

            DataJson = data.ToJson();


            var options = new LineChartOptions
            {
                Height = 200,
                Width = 500,
                Title = "Linechart with role columns"
            };

            OptionsJson = options.ToJson();

        }
    }
}
