using NUnit.Framework;
using GoogleChart.Net.Wrapper;
using GoogleChart.Net.Wrapper.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using GoogleChart.Net.Wrapper.Tests;


namespace GoogleChart.Net.Wrapper.Tests
{
    [TestFixture()]
    public class OptionsSerializationTest
    {

        [Test]
        public void BaseChartOptions_Serialize()
        {
            var opt = new ChartOptions
            {
                Title = "Hello word",
                TitlePosition = TitlePosition.In,
                AxisTitlesPosition = AxisTitlesPosition.Out,
                Height = 200,
                Width = 500
            };


            var json = opt.ToJson();

        }


    }
}
