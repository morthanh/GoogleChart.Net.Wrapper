﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper.Extensions
{
    public static class EnumerableExtensions
    {


        public static DataTable<T> ToDataTable<T>(this IEnumerable<T> source, Action<DataTableConfiguration<T>> config)
        {
            var configurations = new DataTableConfiguration<T>(source);
            config(configurations);
            return configurations.DataTable;
        }


    }
}
