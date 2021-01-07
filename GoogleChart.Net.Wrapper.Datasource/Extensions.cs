using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Web;
using System.Text;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public static class Extensions
    {

        public static void AddGoogleChartApi(this IServiceCollection services, Action<GoogleChartOptions> options = null)
        {

            if (options != null)
            {
                services.Configure(options);
            }

        }

    }
}
