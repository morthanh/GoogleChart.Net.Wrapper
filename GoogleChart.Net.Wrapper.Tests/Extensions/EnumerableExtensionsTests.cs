using NUnit.Framework;
using GoogleChart.Net.Wrapper.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using GoogleChart.Net.Wrapper.Tests;
using System.Threading;
using GoogleChart.Net.Wrapper.Options;

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
                    config.AddColumn(ColumnType.String, x => x.Name);
                    config.AddColumn(ColumnType.Number, x => x.Value);
                }).ToJson();

            Assert.NotNull(dataTable);

        }

        [Test]
        public void CreateDataTable_FromLinq_Serialized()
        {

            var dt = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToDataTable(config =>
                {
                    config.AddColumn(ColumnType.String, x => x.Name);
                    config.AddColumn(ColumnType.Number, x => x.Value);
                });

            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            Assert.AreEqual(10, jelem["rows"].Count());
        }



        [Test]
        public void CreateDataTable_FromLinqManyElements_Serialized()
        {

            var dt = Enumerable.Range(0, 100000).Select(x => new { Name = "Test", Value = x })
                .ToDataTable(config =>
                {
                    config.AddColumn(ColumnType.String, x => x.Name);
                    config.AddColumn(ColumnType.Number, x => x.Value);
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

            var json = dt.ToJson(true);

            var jelem = JsonHelper.Deserialize(json);
            var jrowsElem = jelem.Property("rows").Value;
            Assert.AreEqual(10, jrowsElem.Count());
        }


        [Test]
        public void ToDataTableLinqTest()
        {

            var values = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToList();

            var dt = values
                .ToDataTable(config =>
                {
                    config.AddColumn(x => x.Name);
                    config.AddColumn(ColumnType.Number, x => x.Value);
                });


            var json = dt.ToJson();

            var jelem = JsonHelper.Deserialize(json);
            var jrowsElem = jelem.Property("rows").Value;
            Assert.AreEqual(10, jrowsElem.Count());
        }

        [Test]
        public void ToDataTableLinq_WithOptions_SerializeSubclassProperty()
        {
            var values = Enumerable.Range(0, 10).Select(x => new { Name = "Test", Value = x })
                .ToList();

            var dt = values
                .ToDataTable(conf =>
                {
                    conf.AddColumn(ColumnType.String, x => x.Name);
                    conf.AddColumn(x => x.Value);
                    conf.WithOptions<LineChartOptions>(options =>
                    {
                        options.LineWidth = 1;
                    });
                });

            var (dataJson, optionsJson) = dt;

            Assert.IsTrue(optionsJson.Contains("lineWidth", StringComparison.OrdinalIgnoreCase));
        }

    }
}