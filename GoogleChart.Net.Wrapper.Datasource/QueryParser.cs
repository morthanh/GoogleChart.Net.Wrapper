using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class QueryParser
    {

        public static QueryParserResult Parse(string query)
        {

            var clauses = FindClauses(query);


            var result = new QueryParserResult();

            var selectClause = clauses.FirstOrDefault(x => x.Keyword == "select");
            if (selectClause != null)
            {
                

                result.SelectTokens = query
                    .Substring(selectClause.ParameterStart, selectClause.ParameterEnd - selectClause.ParameterStart)
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x=>x.Trim()).ToArray();
            }



            return result;
        }



        private static List<Clause> FindClauses(string query)
        {

            int selectPos = query.IndexOf("select", StringComparison.InvariantCultureIgnoreCase);
            int wherePos = query.IndexOf("where", StringComparison.InvariantCultureIgnoreCase);

            var res = new List<Clause>();


            int lastClausePos = query.Length;
            int l = 0;

            if (wherePos >= 0)
            {
                l = lastClausePos - wherePos;
                res.Add(new Clause(wherePos, "where", l));
                lastClausePos = wherePos;
            }


            if (selectPos >= 0)
            {
                l = lastClausePos - selectPos;
                res.Add(new Clause(selectPos, "select", l));
                lastClausePos = selectPos;
            }

            return res;
        }



    }

    internal class Clause
    {
        public Clause(int position, string keyword, int length)
        {
            Position = position;
            Keyword = keyword;
            Length = length;
        }

        public int Position { get; }
        public string Keyword { get; }
        public int Length { get; }
        public int ParameterStart=> Position + Keyword.Length;
        public int ParameterEnd => Position + Length;
    }

    public class QueryParserResult
    {
        public string[] SelectTokens { get; internal set; }
    }
}
