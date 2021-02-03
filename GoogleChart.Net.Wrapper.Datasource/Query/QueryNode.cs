using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GoogleChart.Net.Wrapper.Datasource.Query
{
    public abstract class QueryNode: IList<QueryNode>
    {
        public string Name { get; }
        public QueryNode Parent { get; internal set; }


        protected QueryNode(string name)
        {
            Name = name;
        }

        private readonly List<QueryNode> children = new List<QueryNode>();


        public QueryNode this[int index] { get => children[index]; set => children[index] = value; }

        public int Count => children.Count;

        public bool IsReadOnly => false;

        public virtual void Add(QueryNode item)
        {
            children.Add(item);
            item.Parent = this;
        }

        public void Clear()
        {
            children.Clear();
        }

        public bool Contains(QueryNode item)
        {
            return children.Contains(item);
        }

        public void CopyTo(QueryNode[] array, int arrayIndex)
        {
            children.CopyTo(array, arrayIndex);
        }

        public IEnumerator<QueryNode> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        public int IndexOf(QueryNode item)
        {
            return children.IndexOf(item);
        }

        public void Insert(int index, QueryNode item)
        {
            children.Insert(index, item);
        }

        public bool Remove(QueryNode item)
        {
            return children.Remove(item);
        }

        public void RemoveAt(int index)
        {
            children.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class QueryRoot : QueryNode
    {
        public QueryRoot() : base("query") { }

        public override string ToString()
        {
            return $"{string.Join(' ', this)}";
        }
    }

    public class SelectNode : QueryNode
    {
        public SelectNode() : base( "select") { }

        public override string ToString()
        {
            return $"select {string.Join(", ", this)}";
        }
    }

    public class WhereNode : QueryNode
    {
        public WhereNode() : base("where") { }

        public override string ToString()
        {
            return $"where {string.Join(", ", this)}";
        }
    }


    public class BinaryOperatorNode : QueryNode
    {
        public BinaryOperatorNode(string name) : base( name) { }
        public override void Add(QueryNode item)
        {
            if (Count >= 2)
            {
                throw new InvalidOperationException("AddNode cannot have more than 2 child nodes");
            }

            base.Add(item);
            item.Parent = this;
        }
    }

    public class AddNode : BinaryOperatorNode
    {
        public AddNode() : base("add") { }


        public override string ToString()
        {
            return $"{string.Join(" + ",this)}";
        }

    }

    public class MultiplyNode : BinaryOperatorNode
    {
        public MultiplyNode() : base( "mul") { }

        public override string ToString()
        {
            return $"{string.Join(" * ", this)}";
        }
    }






    public class SingleNode : QueryNode
    {
        public SingleNode(string name) : base(name)
        {
        }

        public QueryNode Child { get; internal set; }

        public override void Add(QueryNode child)
        {
            Child = child;
            child.Parent = this;
        }
    }











    public class LeafNode : QueryNode
    {
        public LeafNode(string value) : base( "Leaf")
        {
            Value = value;
        }

        public string Value { get;  }

        public override string ToString()
        {
            return $"{Value}";
        }
    }








}
