using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleChart.Net.Wrapper.Examples.Data
{
    public static class TestData
    {
        private static readonly Random rand = new Random();



        public static IEnumerable<int> Data1Dim => Enumerable.Range(0, 20)
            .Select(x => rand.Next(0, 50));


        public static IEnumerable<(string, int)> Data1DimWithLabels => Enumerable.Range(0, 20)
            .Select((x, i) => ("Item" + i, rand.Next(50)));





        public static IEnumerable<(int, int)> Data2Dim => Enumerable.Range(0, 20)
            .Select(x => (x, rand.Next(0, 50)));

        public static IEnumerable<(int, int)> Data2DimRandom => Enumerable.Range(0, 20)
            .Select(x => (rand.Next(0, 50), rand.Next(0, 50)));

        public static IEnumerable<(string, int, int)> Data2DimWithLabels => Enumerable.Range(0, 20)
            .Select((x, i) => ("Item" + i, rand.Next(0, 50), rand.Next(50)));



        public static IEnumerable<(int, int, int, int)> Data4DimRandom => Enumerable.Range(0, 20)
            .Select(x => (rand.Next(0, 10), rand.Next(10, 20), rand.Next(20, 40), rand.Next(40, 50)));



    }
}
