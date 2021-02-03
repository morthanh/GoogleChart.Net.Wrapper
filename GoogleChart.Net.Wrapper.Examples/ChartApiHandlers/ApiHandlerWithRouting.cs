using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Datasource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GoogleChart.Net.Wrapper.Examples.ChartApiHandlers
{
    [Route("handlerWithRoute/{myIntParam:int}")]
    public class ApiHandlerWithRouting : GoogleChartApiHandler
    {
        public override Task<ApiResponse> HandleRequestAsync(HttpContext context, IReadOnlyDictionary<string, string> parameters, RouteValueDictionary routeValues, string query)
        {
            var rand = new Random();
            var dataTable = Enumerable.Range(0, int.Parse((string)routeValues["myIntParam"])).ToDataTable(conf =>
            {
                conf.AddColumn(x => x);
                conf.AddColumn(x => rand.Next(0, 100));
            });

            return Task.FromResult(OkResponse(dataTable));
        }
    }
}
