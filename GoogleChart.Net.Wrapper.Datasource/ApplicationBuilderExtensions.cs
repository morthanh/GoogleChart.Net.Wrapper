using System;
using Microsoft.AspNetCore.Builder;

namespace GoogleChart.Net.Wrapper.Mvc
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGoogleChartApi(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<GoogleChartMiddleware>();
        }
    }
}
