using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class Table3Model : ExamplePageModelBase
    {
        public void OnGet()
        {
            var data = Data.TestData.Data2DimWithLabels.ToDataTable(conf =>
            {
                conf.AddColumn("Name", x => x.Item1);
                conf.AddColumn("Value 1", x => x.Item2);
                conf.AddColumn("Value 2", x => x.Item3);
                conf.WithOptions<TableChartOptions>(options =>
                {
                    options.Width = UnitSize.Percent(100);
                    options.Height = 300;
                    options.CssClassNames = new TableCssClassNames
                    {
                        OddTableRow = "text-danger",
                        HeaderRow = "bg-dark"
                    };
                });
            });

            DataJson = data.ToJson();

            (DataJson, OptionsJson) = data;
        }
    }
}
