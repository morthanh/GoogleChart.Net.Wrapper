using NUnit.Framework;
using GoogleChart.Net.Wrapper.Datasource;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleChart.Net.Wrapper.Datasource.Tests
{
    [TestFixture()]
    public class QueryParserTests
    {
        [Test()]
        public void Parse_Select_FindTokens()
        {

            var parserResult = QueryParser.Parse("select col1, col2  ,col3, col    4");


            Assert.IsTrue(parserResult.SelectTokens.Length == 4);

        }

        [Test]
        public void Parse_Where_FindTokens()
        {

        }
    }

}