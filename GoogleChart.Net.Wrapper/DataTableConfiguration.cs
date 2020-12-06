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
        private List<Column> columns = new List<Column>();

        internal DataTableConfiguration(IEnumerable<T> source)
        {
            this.source = source;
        }

        internal DataTable DataTable => dataTable;



        public DataTableConfiguration<T> AddColumn<TReturn>(Func<T, TReturn> valueSelector)
        {
            var returnTypeCode = Type.GetTypeCode(typeof(TReturn));

            switch (returnTypeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                    columns.Add(new Column(ColumnType.Number));
                    break;
                case TypeCode.String:
                    columns.Add(new Column(ColumnType.String));
                    break;
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }

            valueSelectors.Add((c) => valueSelector(c));
            return this;
        }

        public DataTableConfiguration<T> AddColumn<TReturn>(string label, Func<T, TReturn> valueSelector)
        {
            var returnTypeCode = Type.GetTypeCode(typeof(TReturn));

            switch (returnTypeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                    columns.Add(new Column(ColumnType.Number, label));
                    break;
                case TypeCode.String:
                    columns.Add(new Column(ColumnType.String, label));
                    break;
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }

            valueSelectors.Add((c) => valueSelector(c));
            return this;
        }

        public DataTableConfiguration<T> WithOptions<TOptions>(Action<TOptions> optionsAction) where TOptions : ChartOptions
        {
            var option = Activator.CreateInstance(typeof(TOptions)) as TOptions;
            optionsAction(option);
            options = option;
            return this;
        }




        public DataTableConfiguration<T> AddColumn(Column column, Func<T, object> valueSelector)
        {
            columns.Add(column);
            valueSelectors.Add(x => valueSelector(x));
            return this;
        }
        public DataTableConfiguration<T> AddColumn(ColumnType columnType, Func<T, object> valueSelector)
        {
            return AddColumn(new Column(columnType), valueSelector);
        }




        public DataTable Build()
        {
            dataTable = new DataTable(ValueSourceEnumerator()) { Options = options };
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
