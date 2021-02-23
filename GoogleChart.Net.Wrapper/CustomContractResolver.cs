using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GoogleChart.Net.Wrapper
{
    public sealed class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            var typeInfo = objectType.GetTypeInfo();
            if (typeInfo.IsGenericType && !typeInfo.IsGenericTypeDefinition)
            {
                var jsonConverterAttribute = typeInfo.GetCustomAttribute<JsonConverterAttribute>();
                if (jsonConverterAttribute != null && jsonConverterAttribute.ConverterType.GetTypeInfo().IsGenericTypeDefinition)
                {
                    return (JsonConverter)Activator.CreateInstance(jsonConverterAttribute.ConverterType.MakeGenericType(typeInfo.GenericTypeArguments), jsonConverterAttribute.ConverterParameters);
                }
            }
            return base.ResolveContractConverter(objectType)!;
        }
    }
}
