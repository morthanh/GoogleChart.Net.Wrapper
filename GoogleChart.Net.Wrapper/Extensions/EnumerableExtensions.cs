using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper.Extensions
{
    public static class EnumerableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, Action<DataTableConfiguration<T>> config)
        {
            var configurations = new DataTableConfiguration<T>(source);
            config(configurations);
            configurations.Build();
            return configurations.DataTable;
        }

        public static DataTableConfiguration<T> ToDataTable<T>(this IEnumerable<T> source)
        {
            return new DataTableConfiguration<T>(source);
        }
    }
}
