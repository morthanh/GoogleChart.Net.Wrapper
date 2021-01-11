using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Linq;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class GoogleChartMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<GoogleChartOptions> options;
        private readonly IServiceProvider serviceProvider;

        private GoogleChartOptions Options => options.Value;

        public GoogleChartMiddleware(RequestDelegate next, IOptions<GoogleChartOptions> options, IServiceProvider serviceProvider)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request.Path.StartsWithSegments(Options.RouteBasePath, out PathString subPath))
            {

                var handler = Options.Handlers.Values.FirstOrDefault(handler => subPath.StartsWithSegments(handler.Path));
                if (handler != null)
                {
                    string tqx = context.Request.Query["tqx"];
                    string tq = context.Request.Query["tq"];
                    Dictionary<string, string> parameters = ParseParameterString(tqx);

                    parameters.TryGetValue("reqId", out var reqId);

                    try
                    {
                        var handlerInstance = (GoogleChartApiHandler)serviceProvider.GetService(handler.Type);
                        var resp = await handlerInstance.HandleRequestAsync(context, parameters, tq);

                        resp.RegId = reqId;

                        var jsonResponse = SerializerHelper.Serialize(resp);

                        await context.Response.WriteAsync(jsonResponse);

                        return;
                    }
                    catch(Exception ex)
                    {
                        //return generic error message
                        await context.Response.WriteAsync(SerializerHelper.Serialize(
                            new ApiResponse
                            {
                                RegId = reqId,
                                Status = ApiResponseStatus.Error,
                                Errors = new List<ResponseError> { new ResponseError(ErrorReason.InternalError, "Internal server error", Options.IsDevelopment ? ex.ToString() : null) }
                            }));

                        return;
                    }
                }
            }

            await next.Invoke(context);
        }

        private static Dictionary<string, string> ParseParameterString(string tqx)
        {
            return !string.IsNullOrEmpty(tqx) ?
                tqx.Split(";").Select(x => x.Split(":")).ToDictionary(x => x.First(), x => x.Last()) :
                new Dictionary<string, string>();
        }
    }
}
