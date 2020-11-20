using NUnit.Framework;
using GoogleChart.Net.Wrapper;
using GoogleChart.Net.Wrapper.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace GoogleChart.Net.Wrapper.Tests
{
    [TestFixture()]
    public class DataTableTests
    {
        [Test()]
        public void DataTable_Empty_Serialized()
        {
            var dt = new DataTable();

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            Assert.IsNotNull(jelem.GetProperty("cols"));
            Assert.IsNotNull(jelem.GetProperty("rows"));

        }

        [Test()]
        public void AddColumn_NoArgs_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column());

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            Assert.AreEqual(1, jelem.GetProperty("cols").GetArrayLength());

            var jcolElem = jelem.GetProperty("cols")[0];
            Assert.IsNotNull(jcolElem);
            Assert.IsNotNull(jcolElem.GetProperty("id"));
            Assert.IsNotNull(jcolElem.GetProperty("type"));
            Assert.AreEqual("string", jcolElem.GetProperty("type").GetString());
        }

        [Test]
        public void AddRow_CellInteger_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Number));


            dt.AddRow(Enumerable.Range(1, 1).Select(x => new Cell(x)));

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem.GetProperty("rows")[0];
            var jcElem = jrowElem.GetProperty("c");
            Assert.IsTrue(jcElem[0].GetProperty("v").GetInt32() == 1);

        }

        [Test]
        public void AddRow_CellDateTime_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Datetime));

            var date = new DateTime(2000, 1, 1, 12, 30, 15);

            dt.AddRow(new List<Cell> { new Cell(date) });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem.GetProperty("rows")[0];
            var jcElem = jrowElem.GetProperty("c");
            Assert.IsTrue(jcElem[0].GetProperty("v").GetString() == "new Date(2000, 0, 1, 12, 30, 15)");
        }

        [Test]
        public void AddRow_CellDate_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Date));

            var date = new DateTime(2000, 1, 1, 12, 30, 15);

            dt.AddRow(new List<Cell> { new Cell(date) });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem.GetProperty("rows")[0];
            var jcElem = jrowElem.GetProperty("c");
            Assert.IsTrue(jcElem[0].GetProperty("v").GetString() == "new Date(2000, 0, 1)");
        }


        [Test]
        public void AddRow_CellTimeOfDayWithDateTime_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Timeofday));

            var date = new DateTime(2000, 1, 1, 12, 30, 15);

            dt.AddRow(new List<Cell> { new Cell(date) });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem.GetProperty("rows")[0];
            var jcElem = jrowElem.GetProperty("c");
            Assert.IsTrue(jcElem[0].GetProperty("v").GetString() == "[\"12, 30, 15\"]");
        }

        [Test]
        public void AddRow_CellTimeOfDayWithTimeSpan_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Timeofday));

            var date = new TimeSpan(12, 30, 15);

            dt.AddRow(new List<Cell> { new Cell(date) });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem.GetProperty("rows")[0];
            var jcElem = jrowElem.GetProperty("c");
            Assert.IsTrue(jcElem[0].GetProperty("v").GetString() == "[\"12, 30, 15\"]");
        }

        [Test]
        public void AddRow_CellBoolean_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Boolean));

            dt.AddRow(new List<Cell> { new Cell(true) });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem.GetProperty("rows")[0];
            var jcElem = jrowElem.GetProperty("c");
            Assert.IsTrue(jcElem[0].GetProperty("v").GetBoolean());
        }

        [Test()]
        public void ToJson_Evaluration_LateTimeEvaluation()
        {
            bool hasRunSelector = false;
            var dt = Enumerable.Range(0, 10)
                .ToDataTable(conf =>
                {
                    conf.AddColumn(x =>
                    {
                        hasRunSelector = true;
                        return x;
                    });
                });

            Assert.IsFalse(hasRunSelector);
            var json = dt.ToJson();
            Assert.IsTrue(hasRunSelector);

        }
    }
}