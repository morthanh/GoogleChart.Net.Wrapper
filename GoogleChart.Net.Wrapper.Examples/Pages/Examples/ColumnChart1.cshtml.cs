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
    public class ColumnChart1Model : ExamplePageModelBase
    {
        public void OnGet()
        {
            var data = Data.TestData.Data4DimRandom.Take(5).ToDataTable(conf =>
            {
                int i = 1;
                conf.AddColumn(x => $"Group {i++}");
                conf.AddColumn(new Column(ColumnType.Number, "Cars"), x => x.Item1);
                conf.AddColumn(new Column(ColumnType.Number, "Houses"), x => x.Item2);
                conf.AddColumn(new Column(ColumnType.Number, "Dogs"), x => x.Item3);
                conf.AddColumn(new Column(ColumnType.Number, "Ants"), x => x.Item4);
                conf.AddColumn(new Column(ColumnType.Number, ColumnRole.Interval), x => x.Item4*0.8);
                conf.AddColumn(new Column(ColumnType.Number, ColumnRole.Interval), x => x.Item4*1.2);
                conf.WithOptions<ColumnChartOptions>(opt =>
                {
                    opt.Height = 200;
                    opt.Width = 500;
                    opt.Title = "My columnchart";
                    
                });
            });

            DataJson = data.ToJson();


            OptionsJson = data.Options.ToJson();

        }
    }
}
