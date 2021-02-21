using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper
{

    public class DataTableConfiguration<T>
    {
        private readonly IEnumerable<T> source;
        private DataTable dataTable;
        private readonly List<Func<T, object>> valueSelectors = new List<Func<T, object>>();
        private ChartOptions? options = null;
        private readonly List<ColumnMeta> columns = new List<ColumnMeta>();
        private List<string> columnlabels;

        internal DataTableConfiguration(IEnumerable<T> source)
        {
            this.source = source;
        }

        internal DataTable DataTable => dataTable;

        /// <summary>
        /// Adds a chart options instance used when serializing. Use this to customize the appearance of the chart. 
        /// </summary>
        /// <typeparam name="TOptions">The type of options instance to use. Use <see cref="TableChartOptions"/> for table options, <see cref="GaugeOptions"/> for gauge options etc.</typeparam>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public DataTableConfiguration<T> WithOptions<TOptions>(Action<TOptions> optionsAction) where TOptions : ChartOptions
        {
            var option = Activator.CreateInstance(typeof(TOptions)) as TOptions;
            optionsAction(option);
            options = option;
            return this;
        }

        public DataTableConfiguration<T> AddColumnLabels(params string[] colLabels)
        {
            columnlabels = colLabels.ToList();
            return this;
        }

        public DataTableConfiguration<T> AddColumn<TReturn>(Func<T, TReturn> valueSelector)
        {
            return AddColumn(null, valueSelector);
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, Func<T, object> valueSelector)
        {
            return AddColumn(columnType, null, null, null, null, null, valueSelector);
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, string? label, Func<T, object> valueSelector)
        {
            return AddColumn(columnType, label, null, null, null, null, valueSelector);
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, ColumnRole? role, Func<T, object> valueSelector)
        {
            return AddColumn(columnType, null, null, role, null, null, valueSelector);
        }

        public DataTableConfiguration<T> AddColumn<TReturn>(string? label, Func<T, TReturn> valueSelector)
        {
            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            var returnType = typeof(TReturn);

            if (returnType.IsConstructedGenericType)
            {
                var nullableType = Nullable.GetUnderlyingType(returnType);
                if (nullableType != null)
                {
                    returnType = nullableType;
                }
            }

            var returnTypeCode = Type.GetTypeCode(returnType);


            switch (returnTypeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return AddColumn(ColumnType.Number, label, null, null, null, null, c => valueSelector(c));
                case TypeCode.String:
                    return AddColumn(ColumnType.String, label, null, null, null, null, c => valueSelector(c));
                case TypeCode.DateTime:
                    return AddColumn(ColumnType.Datetime, label, null, null, null, null, c => valueSelector(c));
                case TypeCode.Object when returnType.FullName is "System.TimeSpan":
                    return AddColumn(ColumnType.Timeofday, label, null, null, null, null, c => valueSelector(c));
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }
        }


        private DataTableConfiguration<T> AddColumn(ColumnType columnType, string? label, string? id, ColumnRole? role, Type? valueType, Action<JsonWriter>? valueWriter, Func<T, object> valueSelector)
        {
            columns.Add(new ColumnMeta(id, label, columnType,role,valueType,valueWriter));
            valueSelectors.Add(valueSelector);
            return this;
        }




        public DataTable Build()
        {
            dataTable = new DataTable(ValueSourceEnumerator()) { Options = options, ColumnLabels = columnlabels };
            foreach (var column in columns)
            {
                dataTable.AddColumn(column);
            }
            return dataTable;
        }

        private IEnumerable<object> ValueSourceEnumerator()
        {
            int cIdx;
            foreach (var elem in source)
            {
                for (cIdx = 0; cIdx < valueSelectors.Count; cIdx++)
                {
                    yield return valueSelectors[cIdx](elem);
                }
            }
        }

    }
}
