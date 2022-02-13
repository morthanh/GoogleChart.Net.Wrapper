using GoogleChart.Net.Wrapper.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper
{



    public class DataTableConfiguration<TSource>
    {
        private readonly IEnumerable<TSource> source;
        private ChartOptions? options = null;
        private readonly List<ColumnMeta<TSource>> columns = new List<ColumnMeta<TSource>>();
        private List<string>? columnlabels;

        internal DataTableConfiguration(IEnumerable<TSource> source)
        {
            this.source = source;
        }

        internal DataTable<TSource> DataTable => Build();

        /// <summary>
        /// Adds a chart options instance used when serializing. Use this to customize the appearance of the chart. 
        /// </summary>
        /// <typeparam name="TOptions">The type of options instance to use. Use <see cref="TableChartOptions"/> for table options, <see cref="GaugeOptions"/> for gauge options etc.</typeparam>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public DataTableConfiguration<TSource> WithOptions<TOptions>(Action<TOptions> optionsAction) where TOptions : ChartOptions
        {
            var option = Activator.CreateInstance(typeof(TOptions)) as TOptions;
            optionsAction(option);
            options = option;
            return this;
        }

        public DataTableConfiguration<TSource> AddColumnLabels(params string[] colLabels)
        {
            columnlabels = colLabels.ToList();
            return this;
        }

        public DataTableConfiguration<TSource> AddColumn<TReturn>(Func<TSource, TReturn> valueSelector)
        {
            return AddColumn(null, valueSelector, null);
        }

        public DataTableConfiguration<TSource> AddColumn<TReturn>(Func<TSource, TReturn> valueSelector, Func<TSource, string>? formattedSelector)
        {
            return AddColumn(valueSelector, formattedSelector != null ? (s,v)=>formattedSelector(s) : default(Func<TSource, TReturn, string>));
        }
        public DataTableConfiguration<TSource> AddColumn<TReturn>(Func<TSource, TReturn> valueSelector, Func<TSource, TReturn, string>? formattedSelector)
        {
            return AddColumn(null, valueSelector, formattedSelector);
        }

        public DataTableConfiguration<TSource> AddColumn<TReturn>(string? label, Func<TSource, TReturn> valueSelector)
        {
            return AddColumn(label, valueSelector, null);
        }

        public DataTableConfiguration<TSource> AddColumn<TReturn>(string? label, Func<TSource, TReturn> valueSelector, Func<TSource, TReturn, string>? formattedSelector)
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

            var formattedSelectorParm = formattedSelector != null ? (s, v) => formattedSelector!(s, (TReturn)v) : default(Func<TSource, object, string>);

            switch (returnTypeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return AddColumn(ColumnType.Number, label, null, null, null, null, c => valueSelector(c)!, formattedSelectorParm);
                case TypeCode.Boolean:
                    return AddColumn(ColumnType.Boolean, label, null, null, null, null, c => valueSelector(c)!, formattedSelectorParm);
                case TypeCode.String:
                    return AddColumn(ColumnType.String, label, null, null, null, null, c => valueSelector(c)!, formattedSelectorParm);
                case TypeCode.DateTime:
                    return AddColumn(ColumnType.Datetime, label, null, null, null, null, c => valueSelector(c)!, formattedSelectorParm);
                case TypeCode.Object when returnType.FullName is "System.TimeSpan":
                    return AddColumn(ColumnType.Timeofday, label, null, null, null, null, c => valueSelector(c)!, formattedSelectorParm);
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }
        }

        public DataTableConfiguration<TSource> AddColumn(ColumnType columnType, Func<TSource, object> valueSelector)
        {
            return AddColumn(columnType, null, null, null, null, null, valueSelector, null);
        }
        public DataTableConfiguration<TSource> AddColumn(ColumnType columnType, string? label, Func<TSource, object> valueSelector)
        {
            return AddColumn(columnType, label, null, null, null, null, valueSelector, null);
        }
        public DataTableConfiguration<TSource> AddColumn(ColumnType columnType, ColumnRole? role, Func<TSource, object> valueSelector)
        {
            return AddColumn(columnType, null, null, role, null, null, valueSelector, null);
        }

        private DataTableConfiguration<TSource> AddColumn(ColumnType columnType, string? label, string? id, ColumnRole? role, Type? valueType, Action<JsonWriter>? valueWriter, Func<TSource, object> valueSelector, Func<TSource,object, string>? formattedSelector)
        {
            columns.Add(new ColumnMeta<TSource>(id, label, columnType,role,valueType,valueWriter, valueSelector, formattedSelector));
            return this;
        }




        private DataTable<TSource> Build()
        {
            var dataTable = new DataTable<TSource>(ValueSourceEnumerator()) { Options = options, ColumnLabels = columnlabels };
            foreach (var column in columns)
            {
                dataTable.AddColumn(column);
            }
            return dataTable;
        }

        private IEnumerable<ValueSourceItem<TSource>> ValueSourceEnumerator()
        {
            int cIdx;
            foreach (var elem in source)
            {
                for (cIdx = 0; cIdx < columns.Count; cIdx++)
                {
                    yield return new ValueSourceItem<TSource>(columns[cIdx], elem);
                }
            }
        }

    }


    internal struct ValueSourceItem<T>
    {
        public ValueSourceItem(ColumnMeta<T> columnMeta, T value)
        {
            ColumnMeta = columnMeta;
            ValueRaw = value;
            Value = columnMeta.ValueSelector(value);
            ValueFormatted = columnMeta.FormattedSelector != null ? columnMeta.FormattedSelector(ValueRaw, Value) : null;
        }

        internal ColumnMeta<T> ColumnMeta { get; }
        internal T ValueRaw { get; }
        internal object Value { get; }
        internal string? ValueFormatted { get; }
    }
}
