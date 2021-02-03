using NUnit.Framework;
using GoogleChart.Net.Wrapper.Datasource.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GoogleChart.Net.Wrapper.Datasource.Query.Tests
{
    [TestFixture()]
    public class ExpressionGeneratorTests
    {
        [Test()]
        public void CreateTest()
        {
            var data = Enumerable.Range(0, 10).Select(x => new { MyCol = x, MyColNotToSelect = -x});
            var query = "Select MyCol";

            var queryRoot = QueryParserUtil.Parse(query);

            var expr = ExpressionGenerator.Create(queryRoot);


        }
    }
}