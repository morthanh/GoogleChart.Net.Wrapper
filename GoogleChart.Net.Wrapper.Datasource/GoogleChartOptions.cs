using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class GoogleChartOptions
    {
        public PathString RouteBasePath { get; set; } = "/api/gc";

        public bool IsDevelopment { get; set; } = false;

        internal List<GoogleChartHandlerOption> Handlers { get; set; } = new List<GoogleChartHandlerOption>();

        public GoogleChartHandlerOption AddHandler<THandler>() where THandler : GoogleChartApiHandler
        {
            var handlerOptions = new GoogleChartHandlerOption(typeof(THandler));
            Handlers.Add(handlerOptions);
            return handlerOptions;
        }
        public GoogleChartHandlerOption AddHandler<THandler>(PathString path) where THandler : GoogleChartApiHandler
        {
            var handlerOptions = AddHandler<THandler>();
            handlerOptions.WithRoute(path);
            return handlerOptions;
        }




    
    }

    public class GoogleChartHandlerOption
    {
        internal GoogleChartHandlerOption(Type handlerType)
        {
            HandlerType = handlerType;

            
            Route = GetHandlerRouteAttributeTemplate(HandlerType);
            AuthorizeAttributes = GetAuthorizeAttributes(HandlerType);
        }

        internal Type HandlerType { get; }
        internal string Route { get; private set; }
        
        internal List<AuthorizeAttribute> AuthorizeAttributes { get; private set; }

        public void WithRoute(string route)
        {
            if (route is null)
            {
                throw new ArgumentNullException(nameof(route));
            }
            Route = route;
        }


        private List<AuthorizeAttribute> GetAuthorizeAttributes(Type handlerType)
        {
            return handlerType.GetCustomAttributes<AuthorizeAttribute>().ToList();
        }


        private string GetHandlerRouteAttributeTemplate(Type handlerType)
        {
            var attr = FindRouteAttribute(handlerType);
            return attr?.Template;
        }

        private RouteAttribute FindRouteAttribute(Type handlerType)
        {
            var foundRouteAttributes = handlerType.GetCustomAttributes<RouteAttribute>();

            if (foundRouteAttributes.Count() == 0)
                return null;
            else if (foundRouteAttributes.Count() > 1)
                throw new InvalidOperationException("Google chart handler cannot have more than one route attribute");

            var routeAttribute = foundRouteAttributes.First();
            return routeAttribute;

        }
    }
}
