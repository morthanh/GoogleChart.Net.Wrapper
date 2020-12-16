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
    public class Gauge1Model : ExamplePageModelBase
    {
        public void OnGet()
        {

            var dt = Data.TestData.Data1DimWithLabels.Take(1).ToDataTable(conf =>
            {
                //conf.AddColumnLabels("Label", "Value");
                conf.AddColumn(x => x.Item1);
                conf.AddColumn(x => x.Item2);
                conf.WithOptions<GaugeOptions>(options =>
                {

                    options.GreenFrom = 10;
                    options.GreenTo = 40;
                    options.Max = 50;
                    options.Min = 0;
                });
            });


            (DataJson, OptionsJson) = dt;

        }
    }
}
