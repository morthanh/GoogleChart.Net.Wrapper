using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Options;

namespace GoogleChart.Net.Wrapper.Examples.Pages
{
    public class LineChart2Model : ExamplePageModelBase
    {



        public void OnGet()
        {
            int i = 0;
            var data = Data.TestData.Data2DimRandom.ToDataTable(conf =>
            {
                conf.AddColumn(ColumnType.Number, x => i++);
                conf.AddColumn(ColumnType.Number, x => i++);
                conf.AddColumn(x => x.Item1 < 10 ? default(int?) : x.Item1);
                conf.AddColumn(ColumnType.Number, x => x.Item1 < 20 ? default(int?) : x.Item1 + 5);
                conf.AddColumn(x => x.Item2);
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
