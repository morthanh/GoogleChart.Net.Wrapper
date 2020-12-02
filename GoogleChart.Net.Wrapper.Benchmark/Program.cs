using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using GoogleChart.Net.Wrapper.Extensions;

namespace GoogleChart.Net.Wrapper.Benchmark
{
    class Program
    {

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Test>();


            Console.ReadLine();
        }


    }

    [MemoryDiagnoser]
    public class Test
    {
        readonly Random rand = new Random(1234);

        IList<(int, int)> xyPoints;
        object t;

        [Params(100, 10000, 1000000)]
        public int Size { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            xyPoints = Enumerable.Range(0, Size).Select(x => (x, rand.Next(0, 100))).ToList();

            t =
                new {
                    cols = new[]
                    {
                        new { id = "Column1", type = "number" },
                        new { id = "Column2", type = "number" }
                    },
                    rows = xyPoints.Select(xy => new { c = new[] { new { v = xy.Item1 }, new { v = xy.Item2 } } }).ToList()
                };
        }






        [Benchmark(Baseline =true)]
        public void Raw()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(t);
        }

        
        [Benchmark]
        public void Manuel()
        {
            var dt = new DataTable();

            dt.AddColumn(new Column(ColumnType.Number));
            dt.AddColumn(new Column(ColumnType.Number));

            foreach (var point in xyPoints)
            {
                dt.AddRow(new Cell[] { new Cell(point.Item1), new Cell(point.Item2) });
            }

            var json = dt.ToJson();
        }

        [Benchmark]
        public void LinqExtension()
        {
            var json = xyPoints.ToDataTable(conf =>
            {
                conf.AddColumn(ColumnType.Number, x => x.Item1);
                conf.AddColumn(ColumnType.Number, x => x.Item2);
            }).ToJson();
        }
    }
}
