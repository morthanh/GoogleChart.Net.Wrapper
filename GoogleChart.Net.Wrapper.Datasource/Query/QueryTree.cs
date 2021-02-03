using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GoogleChart.Net.Wrapper.Datasource.Query
{
    public class QueryTree
    {
        readonly QueryRoot root = new QueryRoot();
        QueryNode current;

        public QueryRoot Root => root;

        public QueryTree()
        {
            current = root;
        }





        public void Add(QueryNode node)
        {
            current.Add(node);
        }


        public void AddLeaf(string value)
        {
            current.Add(new LeafNode(value));
        }


        public void Pop()
        {
            if (current.Parent != null)
            {
                current = current.Parent;
            }
            else
            {
                throw new InvalidOperationException("Cannot pop root node");
            }
        }


        private QueryNode Push(QueryNode newNode)
        {
            current.Add(newNode);
            current = newNode;
            return current;
        }




        public override string ToString()
        {
            return root.ToString();
        }
    }
}
