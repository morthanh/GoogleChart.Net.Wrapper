using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleChart.Net.Wrapper.Datasource.Query
{
    public static class QueryParserUtil
    {

        public static QueryRoot Parse(string query)
        {
            using var memStream = new MemoryStream(Encoding.Default.GetBytes(query));
            var scanner = new Scanner(memStream);
            var parser = new Parser(scanner);
            parser.Parse();

            return parser.Tree.Root;
        }

    }
}
