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
        private ChartOptions? options = null;
        private readonly List<ColumnMeta<T>> columns = new List<ColumnMeta<T>>();
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
            return AddColumn(null, valueSelector, null);
        }

        public DataTableConfiguration<T> AddColumn<TReturn>(Func<T, TReturn> valueSelector, Func<T, string> formattedSelector)
        {
            return AddColumn(null, valueSelector, formattedSelector);
        }

        public DataTableConfiguration<T> AddColumn<TReturn>(string? label, Func<T, TReturn> valueSelector, Func<T, string>? formattedSelector)
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
                    return AddColumn(ColumnType.Number, label, null, null, null, null, c => valueSelector(c)!, formattedSelector);
                case TypeCode.Boolean:
                    return AddColumn(ColumnType.Boolean, label, null, null, null, null, c => valueSelector(c)!, formattedSelector);
                case TypeCode.String:
                    return AddColumn(ColumnType.String, label, null, null, null, null, c => valueSelector(c)!, formattedSelector);
                case TypeCode.DateTime:
                    return AddColumn(ColumnType.Datetime, label, null, null, null, null, c => valueSelector(c)!, formattedSelector);
                case TypeCode.Object when returnType.FullName is "System.TimeSpan":
                    return AddColumn(ColumnType.Timeofday, label, null, null, null, null, c => valueSelector(c)!, formattedSelector);
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }
        }

        public DataTableConfiguration<T> AddColumn(ColumnType columnType, Func<T, object> valueSelector)
        {
            return AddColumn(columnType, null, null, null, null, null, valueSelector, null);
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, string? label, Func<T, object> valueSelector)
        {
            return AddColumn(columnType, label, null, null, null, null, valueSelector, null);
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, ColumnRole? role, Func<T, object> valueSelector)
        {
            return AddColumn(columnType, null, null, role, null, null, valueSelector, null);
        }

        private DataTableConfiguration<T> AddColumn(ColumnType columnType, string? label, string? id, ColumnRole? role, Type? valueType, Action<JsonWriter>? valueWriter, Func<T, object> valueSelector, Func<T, string>? formattedSelector)
        {
            columns.Add(new ColumnMeta<T>(id, label, columnType,role,valueType,valueWriter, valueSelector, formattedSelector));
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

        private IEnumerable<ValueSourceItem<T>> ValueSourceEnumerator()
        {
            int cIdx;
            foreach (var elem in source)
            {
                for (cIdx = 0; cIdx < columns.Count; cIdx++)
                {
                    yield return new ValueSourceItem<T>(columns[cIdx], elem);
                }
            }
        }

    }


    internal struct ValueSourceItem<T>
    {
        public ValueSourceItem(ColumnMeta<T> columnMeta, T value)
        {
            ColumnMeta = columnMeta;
            Value = value;
        }

        internal ColumnMeta<T> ColumnMeta { get; }
        internal T Value { get; }
    }
}
