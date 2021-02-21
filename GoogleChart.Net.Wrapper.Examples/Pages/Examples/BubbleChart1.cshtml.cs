using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Options;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class BubbleChart1Model : ExamplePageModelBase
    {
        public void OnGet()
        {
            var data = Data.TestData.Data4DimRandom.ToDataTable(conf =>
            {
                int i = 1;
                conf.AddColumn(x => $"Bubble {i++}");
                conf.AddColumn(ColumnType.Number, "X axis", x => x.Item1);
                conf.AddColumn(ColumnType.Number, "Y axis", x => x.Item2);
                conf.AddColumn(ColumnType.String, "Odd or even", x => x.Item4 % 2 == 0 ? "Even" : "Odd");
                conf.AddColumn(ColumnType.Number, "Size", x => x.Item4);
                conf.WithOptions<ChartOptions>(opt =>
                {
                    opt.Height = 300;
                    opt.Width = UnitSize.Percent(100);
                    //opt.Title = "My bubble chart";

                });
            });

            DataJson = data.ToJson();


            OptionsJson = data.Options.ToJson();

        }
    }
}
