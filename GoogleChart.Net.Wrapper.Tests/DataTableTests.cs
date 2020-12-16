using NUnit.Framework;
using GoogleChart.Net.Wrapper;
using GoogleChart.Net.Wrapper.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Globalization;

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
            Assert.IsNotNull(jelem["cols"]);
            Assert.IsNotNull(jelem["rows"]);

        }

        [Test()]
        public void AddColumn_NoArgs_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column());

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            Assert.AreEqual(2, jelem.Count); //cols and rows

            Assert.AreEqual(0, jelem["rows"].Count()); // no rows
            Assert.AreEqual(1, jelem["cols"].Count()); // one column

            var jCol = jelem["cols"][0];

            Assert.IsNotEmpty((string)jCol["id"]);
            Assert.AreEqual("string", (string)jCol["type"]);
            Assert.IsNull(jCol["label"]);
            Assert.IsNull(jCol["role"]);
        }

        [Test]
        public void AddRow_CellInteger_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Number));


            dt.AddRow(Enumerable.Range(1, 1).Select(x => new Cell(x)));

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jValue = jelem["rows"][0]["c"][0]["v"];
            Assert.IsTrue((int)jValue == 1);

        }

        [Test]
        public void AddRow_CellDateTime_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Datetime));

            var date = new DateTime(2000, 1, 1, 12, 30, 15);

            dt.AddRow(new List<Cell> { new Cell(date) });

            var json = dt.ToJson(true);

            var jelem = JsonHelper.Deserialize(json);
            var jValElem = jelem["rows"][0]["c"][0]["v"];
            Assert.IsTrue(string.Compare(jValElem.ToString(), "Date(2000, 0, 1, 12, 30, 15)", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0);
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
            var jValElem = jelem["rows"][0]["c"][0]["v"];
            Assert.IsTrue((string)jValElem == "Date(2000, 0, 1)");
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
            var jValElem = jelem["rows"][0]["c"][0]["v"];
            Assert.IsTrue((string)jValElem == "[\"12, 30, 15\"]");
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
            var jrowElem = jelem["rows"][0]["c"][0]["v"];
            Assert.IsTrue((string)jrowElem == "[\"12, 30, 15\"]");
        }

        [Test]
        public void AddRow_CellBoolean_Serialized()
        {
            var dt = new DataTable();
            dt.AddColumn(new Column(ColumnType.Boolean));

            dt.AddRow(new List<Cell> { new Cell(true) });

            var json = dt.ToJson(true);

            var jelem = JsonHelper.Deserialize(json);
            var jrowElem = jelem["rows"][0];
            var jcElem = jrowElem["c"];
            Assert.IsTrue(bool.Parse((string)jcElem[0]["v"]));
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