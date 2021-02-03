using NUnit.Framework;
using GoogleChart.Net.Wrapper.Datasource;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace GoogleChart.Net.Wrapper.Datasource.Query.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Parse_Select_Simple()
        {
            string str = "select col1, col2, col3";

            using var memStream = new MemoryStream(Encoding.Default.GetBytes(str));
            var scanner = new Scanner(memStream);
            var parser = new Parser(scanner);
            parser.Parse();

            var queryRoot = parser.Tree.Root;
            Assert.IsTrue(queryRoot.Count == 1);
            Assert.IsTrue(queryRoot[0] is SelectNode);
            Assert.IsTrue(queryRoot[0].Count == 3);
        }


        [Test]
        public void Parse_Select_WithAddAndMultiply()
        {
            string str = "select col1, col2 + col3, col4 + 2 * 5";

            using var memStream = new MemoryStream(Encoding.Default.GetBytes(str));
            var scanner = new Scanner(memStream);
            var parser = new Parser(scanner);
            parser.Parse();

            var root = parser.Tree.Root;
            var select = root[0] as SelectNode;
            Assert.IsTrue(select.Count == 3);

            Assert.IsTrue(select[0] is LeafNode);
            Assert.IsTrue(((LeafNode)select[0]).Value == "col1");

            Assert.IsTrue(select[1] is AddNode);
            Assert.IsTrue(select[1][0] is LeafNode);
            Assert.IsTrue(((LeafNode)select[1][0]).Value == "col2");

            Assert.IsTrue(select[2] is AddNode);
            Assert.IsTrue(select[2][1] is MultiplyNode);
            Assert.IsTrue(((LeafNode)select[2][1][1]).Value == "5");
        }

        [Test]
        public void Parse_Select_WithParantheses()
        {
            string str = "select col1, (col2), col3 + (col4 + col5), (x + y) * col6";

            using var memStream = new MemoryStream(Encoding.Default.GetBytes(str));
            var scanner = new Scanner(memStream);
            var parser = new Parser(scanner);
            parser.Parse();

            var root = parser.Tree.Root;
            var select = root[0] as SelectNode;
            Assert.IsTrue(select.Count == 4);

            Assert.IsInstanceOf<AddNode>(select[2]);
            Assert.IsInstanceOf<AddNode>(select[2][1]);
            Assert.IsInstanceOf<LeafNode>(select[2][1][0]);
            Assert.IsInstanceOf<LeafNode>(select[2][1][1]);

        }

        [Test]
        public void Parse_Where_Simple()
        {
            string str = "select * where true";

            var root = QueryParserUtil.Parse(str);

            Assert.IsInstanceOf<WhereNode>(root[1]);
            var whereNode = (WhereNode)root[1];
            Assert.IsTrue(whereNode.Count == 1);
        }


        [Test]
        public void Bla()
        {
            var data = Enumerable.Range(0, 10).Select(x => new { Index = x, Name = "My value: " + x });

            BlaT(data, new string[] { "Index" });

        }

        private void BlaT<T>(IEnumerable<T> data, string[] columns)
        {

            var t = data.GetType().GenericTypeArguments[0];
            var propTypes = t.GetProperties();

            //expression parameter, x => ...
            var itemParam = Expression.Parameter(t, "x");

            //where part
            var left = Expression.PropertyOrField(itemParam, "Index");
            var right = Expression.Constant(5);
            var lessThan = Expression.LessThan(left, right);
            var where = Expression.Lambda<Func<T, bool>>(lessThan, itemParam).Compile();


            //select arguments
            Expression.ListInit(
                Expression.New(typeof(SelectorItem).GetConstructor(new[] { typeof(string), typeof(object), typeof(Type) })),
                columns.Select(field=>Expression.ElementInit(

                )
            var args = new Expression[] { 
                
                Expression.Parameter(propTypes.Single(p => p.Name == x).PropertyType) };

            var selectorItemArguments = columns.Select(x => );


            //var selector = Expression.Call(typeof(Tuple), "Create", 
            //    new[] {left.Type }, left);

            var newSelectorItem = Expression.New(typeof(SelectorItem).GetConstructor(new[] {typeof(string), typeof(object), typeof(Type) }), selectorItemArguments);

            var select = Expression.Lambda<Func<T, SelectorItem>>(newSelectorItem, itemParam).Compile();

            var result = data.Where(where).Select(select).ToList();

            //result.Select(x=>x.)
        }


        private record SelectorItem(string Name, object Value, Type Type);
    }
}