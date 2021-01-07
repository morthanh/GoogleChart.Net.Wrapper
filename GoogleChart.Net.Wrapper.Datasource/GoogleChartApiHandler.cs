using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public abstract class GoogleChartApiHandler
    {

        public abstract Task<ApiResponse> HandleRequestAsync(HttpContext context, IReadOnlyDictionary<string, string> parameters, string query);

        protected ApiResponse OkResponse(DataTable dt)
        {
            return new ApiResponse
            {
                Table = dt,
                Status = ApiResponseStatus.Ok
            };
        }

        protected ApiResponse WarningResponse(DataTable dt, IEnumerable<ResponseWarning> warnings)
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
