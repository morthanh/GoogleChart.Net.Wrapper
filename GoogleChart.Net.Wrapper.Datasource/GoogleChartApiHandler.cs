using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public abstract class GoogleChartApiHandler
    {

        public abstract Task<ApiResponse> HandleRequestAsync(HttpContext context, IReadOnlyDictionary<string, string> parameters, RouteValueDictionary routeValues, string query);

        protected ApiResponse OkResponse(DataTableBase dt)
        {
            return new ApiResponse
            {
                Table = dt,
                Status = ApiResponseStatus.Ok
            };
        }

        protected ApiResponse WarningResponse(DataTableBase dt, IEnumerable<ResponseWarning> warnings)
        {
            return new ApiResponse
            {
                Table = dt,
                Status = ApiResponseStatus.Warning,
                Warnings = warnings
            };
        }

        protected ApiResponse ErrorResponse(IEnumerable<ResponseError> errors)
        {
            return new ApiResponse
            {
                Status = ApiResponseStatus.Error,
                Errors = errors
            };
        }
    }
}
