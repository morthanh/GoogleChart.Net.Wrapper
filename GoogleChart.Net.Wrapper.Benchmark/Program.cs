using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using GoogleChart.Net.Wrapper.Extensions;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.DotNetCli;
using BenchmarkDotNet.Toolchains.CsProj;

namespace GoogleChart.Net.Wrapper.Benchmark
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var config = ManualConfig.Create(DefaultConfig.Instance);
        //    _ = config.AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));
        //    var summery = BenchmarkRunner.Run<Test>(config);
        //}

        //static void Main(string[] args) => BenchmarkSwitcher.FromAssemblies(new[] { typeof(Program).Assembly }).Run(args);

        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .AddJob(
                    Job.Default.WithToolchain(
                        CsProjCoreToolchain.From(
                            new NetCoreAppSettings(
                                targetFrameworkMoniker: "net5.0",  // the key to make it work
                                runtimeFrameworkVersion: null,
                                name: "5.0")))
                                .AsDefault());

            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
            BenchmarkRunner.Run<Test>(config);
        }


    }

    [MemoryDiagnoser]
    public class Test
    {
        Random rand = new Random(1234);

        IEnumerable<(int, int)> xyPoints;

        [Params()]
        public int Size { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            xyPoints = Enumerable.Range(0, Size).Select(x => (x, rand.Next(0, 100)));
        }



        [Benchmark]
        public void ManuelBenchmark() => Manuel();


        public void Manuel()
        {
            //var dt = new DataTable();

            //dt.AddColumn(new Column(ColumnType.Number));
            //dt.AddColumn(new Column(ColumnType.Number));

            //foreach (var point in xyPoints)
            //{
            //    dt.AddRow(new Cell[] { new Cell(point.Item1), new Cell(point.Item2) });
            //}

            //var json = dt.ToJson();
        }

        [Benchmark]
        public void LinqExtension()
        {
            //var json = xyPoints.ToDataTable(conf =>
            //{
            //    conf.AddColumn(new Column(ColumnType.Number), x => x.Item1);
            //    conf.AddColumn(new Column(ColumnType.Number), x => x.Item2);
            //});
        }
    }
}
