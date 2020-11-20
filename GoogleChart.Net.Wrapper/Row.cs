using GoogleChart.Net.Wrapper.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoogleChart.Net.Wrapper
{
    [JsonConverter(typeof(RowConverter))]
    public class Row
    {
        private readonly IList<Cell> cells = new List<Cell>();
        private readonly DataTable dataTable;

        internal IList<Cell> Cells => cells;

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


        private void AddCell(Cell cell)
        {
            if (cell is null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            Cells.Add(cell);
        }

        internal void AddCells(IEnumerable<Cell> cells)
        {
            if (cells is null)
            {
                throw new ArgumentNullException(nameof(cells));
            }

            foreach (var cell in cells)
            {
                AddCell(cell);
            }
        }

    }
}
