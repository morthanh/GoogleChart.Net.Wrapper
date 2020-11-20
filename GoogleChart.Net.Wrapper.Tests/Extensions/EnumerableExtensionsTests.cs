﻿using NUnit.Framework;
using GoogleChart.Net.Wrapper;
using GoogleChart.Net.Wrapper.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using GoogleChart.Net.Wrapper.Tests;

namespace GoogleChart.Net.Wrapper.Extensions.Tests
{
    [TestFixture()]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void CreateDataTable_FromLinq()
        {

            var dataTable = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToDataTable(config =>
                {
                    config.AddColumn(new Column(ColumnType.String), x => new Cell(x.Name));
                    config.AddColumn(new Column(ColumnType.Number), x => new Cell(x.Value));
                });

            Assert.NotNull(dataTable);

        }

        [Test]
        public void CreateDataTable_FromLinq_Serialized()
        {

            var dt = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToDataTable(config =>
                {
                    config.AddColumn(new Column(ColumnType.String), x => x.Name);
                    config.AddColumn(new Column(ColumnType.Number), x => x.Value);
                });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowsElem = jelem.GetProperty("rows");
            Assert.AreEqual(10, jrowsElem.GetArrayLength());
        }


        [Test]
        public void CreateDataTable_MethodChainning_Serialized()
        {

            var dt = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToDataTable()
                .AddColumn(new Column(ColumnType.String), x => x.Name)
                .AddColumn(new Column(ColumnType.Number), x => x.Value)
                .Build();

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowsElem = jelem.GetProperty("rows");
            Assert.AreEqual(10, jrowsElem.GetArrayLength());
        }


        [Test]
        public void CreateDataTable_FromLinqManyElements_Serialized()
        {

            var dt = Enumerable.Range(0, 100000).Select(x => new { Name = "Test", Value = x })
                .ToDataTable(config =>
                {
                    config.AddColumn(new Column(ColumnType.String), x => x.Name);
                    config.AddColumn(new Column(ColumnType.Number), x => x.Value);
                });

            var json = dt.ToJson();
        }


        [Test]
        public void CreateDataTable_DefaultColumn_Serialized()
        {

            var dt = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToDataTable(config =>
                {
                    config.AddColumn(x => x.Name);
                    config.AddColumn(ColumnType.Number, x => x.Value);
                });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowsElem = jelem.GetProperty("rows");
            Assert.AreEqual(10, jrowsElem.GetArrayLength());
        }
    }
}