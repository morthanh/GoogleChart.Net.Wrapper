using GoogleChart.Net.Wrapper.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{
    [JsonConverter(typeof(RowConverter))]
    public sealed class Row
    {
        private IEnumerable<Cell> cells = new List<Cell>();
        private readonly DataTable dataTable;

        internal IEnumerable<Cell> Cells { get => cells; set => cells = value; }

        [JsonIgnore]
        internal DataTable DataTable => dataTable;

        [JsonIgnore]
        internal bool IsLabels { get; set; }

        internal Row(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }

        internal Row(DataTable dataTable, IEnumerable<Cell> cells) : this(dataTable)
        {
            AddCells(cells);
        }



        
        internal void AddCells(IEnumerable<Cell> cells)
        {
            if (cells is null)
            {
                throw new ArgumentNullException(nameof(cells));
            }

            this.cells = cells;
        }

    }
}
