using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoogleChart.Net.Wrapper.Extensions;
using GoogleChart.Net.Wrapper.Options;
using System.Drawing;

namespace GoogleChart.Net.Wrapper.Examples.Pages.Examples
{
    public class Candlestick1Model : ExamplePageModelBase
    {

        public void OnGet()
        {

            var data = GetData()
                .ToDataTable(conf =>
                {
                    conf.AddColumn(x => x.Time);
                    conf.AddColumn(x => x.Min);
                    conf.AddColumn(x => x.Initial);
                    conf.AddColumn(x => x.Final);
                    conf.AddColumn(x => x.Max);
                });

            DataJson = data.ToJson();


            var options = new CandlestickChartOptions
            {
                Height = 400,
                Width = UnitSize.Percent(100),
                Title = "My sin approximation",
                HorizontalAxis = new HorizontalAxis
                {
                    TextPosition = AxisTextPosition.Out,
                    Gridlines = new AxisGridLines
                    {
                        Interval = new double[] { Math.PI / 4 }
                    },
                    Format = "#.##"
                },
                Bar = new ColumnChartBar { GroupWidth = UnitSize.Percent(100) },
                Legend = new LegendOptions { Position = LegendPosition.None },
                Candlestick = new CandlestickOptions
                {
                    HollowIsRising = true,
                    FallingColor = Color.OrangeRed,
                    RisingColor = Color.MediumVioletRed
                },
                ChartArea = new ChartArea
                {
                    BackgroundColor = Color.AliceBlue,
                    Left = UnitSize.Percent(5),
                    Width = UnitSize.Percent(95)
                },
                DataOpacity = 0.8,
                TitlePosition = TitlePosition.In
            };

            OptionsJson = options.ToJson();

        }


        /// <summary>
        /// Just generates som sample data based on a sinus curve
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<DataPoint> GetData()
        {
            var rand = new Random();
            double lastValue = double.MinValue;
            return Enumerable.Range(0, 40).Select(x => new { x = Math.PI * 2 / 40 * x, val = Math.Sin(Math.PI * 2 / 40 * x) })
                .Select(x =>
                {
                    var t = x.x;
                    var y = x.val;

                    if (lastValue == double.MinValue)
                        lastValue = y;

                    var valToReturn = y - lastValue >= 0 ?
                        new DataPoint(t, y - rand.Next(10, 20) / 50d, y - rand.Next(0, 10) / 50d, y + rand.Next(0, 10) / 50d, y + rand.Next(10, 20) / 50d) :
                        new DataPoint(t, y + rand.Next(10, 20) / 50d, y + rand.Next(0, 10) / 50d, y - rand.Next(0, 10) / 50d, y - rand.Next(10, 20) / 50d);

                    lastValue = y;

                    return valToReturn;

                });
        }

        private record DataPoint(double Time, double Min, double Initial, double Final, double Max);
    }
}
