using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;

namespace GoogleChart.Net.Wrapper.Examples.Pages
{
    public class LineChart1Model : ExamplePageModelBase
    {



        public void OnGet()
        {
            var data = Data.TestData.Data2Dim.ToDataTable(conf =>
            {
                conf.AddColumn(new Column(ColumnType.Number), x => x.Item1);
                conf.AddColumn(new Column(ColumnType.Number), x => x.Item2);
            });

            DataJson = data.ToJson();


            var options = new LineChartOptions
            {
                Height = 200,
                Width = 500,
                Title = "My linechart"
            };

            OptionsJson = options.ToJson();

        }
    }
}
