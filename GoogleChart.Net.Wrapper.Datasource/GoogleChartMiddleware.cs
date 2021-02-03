using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class GoogleChartMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<GoogleChartOptions> options;

        private GoogleChartOptions Options => options.Value;

        public GoogleChartMiddleware(RequestDelegate next, IOptions<GoogleChartOptions> options)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request.Path.StartsWithSegments(Options.RouteBasePath, out PathString subPath))
            {

                var handlerResult = FindHandler(subPath);

                //var handler = Options.Handlers.Values.FirstOrDefault(handler => subPath.StartsWithSegments(handler.Path));
                if (handlerResult.handlerOption != null)
                {
                    string tqx = context.Request.Query["tqx"];
                    string tq = context.Request.Query["tq"];
                    Dictionary<string, string> parameters = ParseParameterString(tqx);

                    parameters.TryGetValue("reqId", out var reqId);

                    try
                    {
                        var handlerInstance = (GoogleChartApiHandler)context.RequestServices.GetService(handlerResult.handlerOption.HandlerType);
                        
                        if (handlerInstance == null)
                        {
                            throw new InvalidOperationException($"Handler '{handlerResult.handlerOption.HandlerType}' was not found in services. Remember to register handlers in Startup with services.AddScoped.");
                        }

                        var resp = await handlerInstance.HandleRequestAsync(context, parameters, handlerResult.routeValues, tq);

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


        private (GoogleChartHandlerOption handlerOption, RouteValueDictionary routeValues) FindHandler(string requestPath)
        {
            foreach (var handler in Options.Handlers) 
            {
                var result = RouteMatcher.Match(handler.Route, requestPath);
                if (result != null)
                {
                    return (handler, result);
                }
            }
            return (null,null);
        }


        private static Dictionary<string, string> ParseParameterString(string tqx)
        {
            return !string.IsNullOrEmpty(tqx) ?
                tqx.Split(";").Select(x => x.Split(":")).ToDictionary(x => x.First(), x => x.Last()) :
                new Dictionary<string, string>();
        }
    }

    internal static class RouteMatcher
    {
        public static RouteValueDictionary Match(string routeTemplate, string requestPath)
        {
            var template = TemplateParser.Parse(routeTemplate);

            var matcher = new TemplateMatcher(template, GetDefaults(template));

            var values = new RouteValueDictionary();

            return matcher.TryMatch(requestPath, values) ? values : null;
        }

        private static RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();

            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }
    }
}
