using GoogleChart.Net.Wrapper.Options;
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
        private readonly List<Column> columns = new List<Column>();
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

        public DataTableConfiguration<T> AddColumn<TReturn>(string? label, Func<T, TReturn> valueSelector)
        {
            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            var returnTypeCode = Type.GetTypeCode(typeof(TReturn));

            Column columnToAdd;

            switch (returnTypeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                    columnToAdd = new Column(ColumnType.Number, label);
                    break;
                case TypeCode.String:
                    columnToAdd = new Column(ColumnType.String, label);
                    break;
                case TypeCode.DateTime:
                    columnToAdd = new Column(ColumnType.Datetime, label);
                    break;
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }

            return AddColumn(columnToAdd, c => valueSelector(c));
        }


        public DataTableConfiguration<T> AddColumn(Column column, Func<T, object> valueSelector)
        {
            columns.Add(column);
            valueSelectors.Add(valueSelector);
            return this;
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, Func<T, object> valueSelector)
        {
            return AddColumn(new Column(columnType), valueSelector);
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
