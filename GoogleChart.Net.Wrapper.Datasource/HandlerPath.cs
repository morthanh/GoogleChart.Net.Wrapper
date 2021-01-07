using System;
using Microsoft.AspNetCore.Http;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class HandlerPath
    {
        public HandlerPath(Type type, PathString path)
        {
            Type = type;
            Path = path;
        }

        public Type Type { get; }
        public PathString Path { get; }
    }
}
