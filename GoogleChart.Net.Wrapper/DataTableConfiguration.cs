using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper
{
    public class DataTableConfiguration<T>
    {
        private readonly IEnumerable<T> source;
        private readonly DataTable dataTable = new DataTable();
        private readonly List<Func<T, Cell>> valueSelectors = new List<Func<T, Cell>>();

        internal DataTableConfiguration(IEnumerable<T> source)
        {
            this.source = source;
        }

        internal DataTable DataTable => dataTable;

        public DataTableConfiguration<T> AddColumn(Column column, Func<T, Cell> valueSelector)
        {
            dataTable.AddColumn(column);
            valueSelectors.Add(valueSelector);
            return this;
        }

        public DataTableConfiguration<T> AddColumn(Column column, Func<T, object> valueSelector)
        {
            dataTable.AddColumn(column);
            valueSelectors.Add(x => new Cell(valueSelector(x)));
            return this;
        }

        public DataTableConfiguration<T> AddColumn(Func<T, object> valueSelector)
        {
            dataTable.AddColumn(new Column());
            valueSelectors.Add(x => new Cell(valueSelector(x)));
            return this;
        }

        public DataTableConfiguration<T> AddColumn<TReturn>(Func<T, TReturn> valueSelector)
        {
            var returnTypeCode = Type.GetTypeCode(typeof(TReturn));

            switch (returnTypeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                    dataTable.AddColumn(new Column(ColumnType.Number));
                    break;
                case TypeCode.String:
                    dataTable.AddColumn(new Column(ColumnType.String));
                    break;
                default:
                    throw new NotSupportedException($"Returtype '{returnTypeCode}' is not yet supported");
            }

            valueSelectors.Add((c) => new Cell(valueSelector(c)));
            return this;
        }

        public DataTableConfiguration<T> AddColumn(ColumnType columnType, Func<T, object> valueSelector)
        {
            dataTable.AddColumn(new Column(columnType));
            valueSelectors.Add(x => new Cell(valueSelector(x)));
            return this;
        }

        public DataTableConfiguration<T> AddColumn(ColumnType columnType, string id, Func<T, object> valueSelector)
        {
            dataTable.AddColumn(new Column(columnType, id));
            valueSelectors.Add(x => new Cell(valueSelector(x)));
            return this;
        }

        public DataTableConfiguration<T> AddColumn(ColumnType columnType, string id, string label, Func<T, object> valueSelector)
        {
            dataTable.AddColumn(new Column(columnType, id, label));
            valueSelectors.Add(x => new Cell(valueSelector(x)));
            return this;
        }


        public DataTable Build()
        {
            dataTable.AddRowsSource(source.Select(x => new Row(dataTable, valueSelectors.Select(f => f(x)))));

            return dataTable;
        }

    }
}
