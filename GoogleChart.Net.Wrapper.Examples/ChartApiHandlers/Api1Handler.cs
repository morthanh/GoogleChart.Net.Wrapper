using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Options;
using GoogleChart.Net.Wrapper.Datasource;

namespace GoogleChart.Net.Wrapper.Examples.ChartApiHandlers
{
    public class Api1Handler : GoogleChartApiHandler
    {
        public override async Task<ApiResponse> HandleRequestAsync(HttpContext context, IReadOnlyDictionary<string, string> parameters, string query)
        {
            var rand = new Random();
            var dataTable = Enumerable.Range(0, 100).ToDataTable(conf =>
            {
                conf.AddColumn(x => x);
                conf.AddColumn(x => rand.Next(0, 100));
            });

            return OkResponse(dataTable);
            
        }
    }
}
