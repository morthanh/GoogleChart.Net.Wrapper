using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class GoogleChartOptions
    {


        public PathString RouteBasePath { get; set; } = "/api/gc";


        internal Dictionary<string, HandlerPath> Handlers { get; set; } = new Dictionary<string, HandlerPath>();

        public void AddHandler<THandler>(PathString pathSegment) where THandler : GoogleChartApiHandler
        {
            Handlers.Add(pathSegment, new HandlerPath(typeof(THandler), pathSegment));
        }

    }
}
