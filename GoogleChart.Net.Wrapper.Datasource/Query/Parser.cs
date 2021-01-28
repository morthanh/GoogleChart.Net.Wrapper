using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleChart.Net.Wrapper.Datasource.Query
{

	using System;



	public class Parser
	{
		public const int _EOF = 0;
		public const int _ident = 1;
		public const int _number = 2;
		public const int maxT = 22;

		const bool _T = true;
		const bool _x = false;
		const int minErrDist = 2;

		public Scanner scanner;
		public Errors errors;

		public Token t;    // last recognized token
		public Token la;   // lookahead token
		int errDist = minErrDist;

		private readonly QueryTree tree = new QueryTree();
		public QueryTree Tree => tree;



		public Parser(Scanner scanner)
		{
			this.scanner = scanner;
			errors = new Errors();
		}

		void SynErr(int n)
		{
			if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
			errDist = 0;
		}

		public void SemErr(string msg)
		{
			if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
			errDist = 0;
		}

		void Get()
		{
			for (; ; )
			{
				t = la;
				la = scanner.Scan();
				if (la.kind <= maxT) { ++errDist; break; }

				la = t;
			}
		}

		void Expect(int n)
		{
			if (la.kind == n) Get(); else { SynErr(n); }
		}

		bool StartOf(int s)
		{
			return set[s, la.kind];
		}

		void ExpectWeak(int n, int follow)
		{
			if (la.kind == n) Get();
			else
			{
				SynErr(n);
				while (!StartOf(follow)) Get();
			}
		}


		bool WeakSeparator(int n, int syFol, int repFol)
		{
			int kind = la.kind;
			if (kind == n) { Get(); return true; }
			else if (StartOf(repFol)) { return false; }
			else
			{
				SynErr(n);
				while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind]))
				{
					Get();
					kind = la.kind;
				}
				return StartOf(syFol);
			}
		}


		void GoogleChartApi()
		{
			QueryNode root = tree.Root;
			if (la.kind == 3)
			{
				Get();
				SelectClause(root);
			}
			if (la.kind == 4)
			{
				Get();
				WhereClause(root);
			}
		}

		void SelectClause(QueryNode parent)
		{
			QueryNode node, expr = null;
			node = new SelectNode();
			parent.Add(node);
			while (StartOf(1))
			{
				if (la.kind == 3 || la.kind == 4)
				{
					ReservedWords();
					return;
				}
				if (la.kind == 5)
				{
					Get();
				}
				if (la.kind == 11)
				{
					SelectStar();
					node.Add(new LeafNode(t.val));
				}
				else if (StartOf(2))
				{
					ExprSpec(out expr);
					node.Add(expr);
				}
				else SynErr(23);
			}
		}

		void WhereClause(QueryNode parent)
		{
			QueryNode node, expr;
			node = new WhereNode();
			ExprSpec(out expr);
			node.Add(expr);
			parent.Add(node);
		}

		void ReservedWords()
		{
			if (la.kind == 3)
			{
				Get();
			}
			else if (la.kind == 4)
			{
				Get();
			}
			else SynErr(24);
		}

		void SelectStar()
		{
			Expect(11);
		}

		void ExprSpec(out QueryNode node)
		{
			QueryNode term;
			SimExpr(out term);
			node = term;
			if (StartOf(3))
			{
				RelOp();
				SimExpr(out term);
				node = term;
			}
		}

		void SimExpr(out QueryNode node)
		{
			QueryNode term;
			Term(out term);
			node = term;
			while (la.kind == 8 || la.kind == 13)
			{
				AddOp();
				node = new AddNode();
				node.Add(term);
				Term(out term);
				node.Add(term);
			}
		}

		void RelOp()
		{
			switch (la.kind)
			{
				case 14:
					{
						Get();
						break;
					}
				case 15:
					{
						Get();
						break;
					}
				case 16:
					{
						Get();
						break;
					}
				case 17:
					{
						Get();
						break;
					}
				case 18:
					{
						Get();
						break;
					}
				case 19:
					{
						Get();
						break;
					}
				default: SynErr(25); break;
			}
		}

		void Term(out QueryNode node)
		{
			QueryNode term;
			Factor(out term);
			node = term;
			while (la.kind == 11 || la.kind == 12)
			{
				MulOp();
				node = new MultiplyNode();
				node.Add(term);
				Factor(out term);
				node.Add(term);
			}
		}

		void AddOp()
		{
			if (la.kind == 13)
			{
				Get();
			}
			else if (la.kind == 8)
			{
				Get();
			}
			else SynErr(26);
		}

		void Factor(out QueryNode node)
		{
			node = null;
			switch (la.kind)
			{
				case 6:
					{
						Get();
						ExprSpec(out node);
						Expect(7);
						break;
					}
				case 20:
				case 21:
					{
						MethodCall(out node);
						break;
					}
				case 1:
					{
						Get();
						node = new LeafNode(t.val);
						break;
					}
				case 2:
					{
						Get();
						node = new LeafNode(t.val);
						break;
					}
				case 8:
					{
						Get();
						Factor(out node);
						node = new LeafNode(t.val);
						break;
					}
				case 9:
					{
						Get();
						node = new LeafNode(t.val);
						break;
					}
				case 10:
					{
						Get();
						node = new LeafNode(t.val);
						break;
					}
				default: SynErr(27); break;
			}
		}

		void MulOp()
		{
			if (la.kind == 11)
			{
				Get();
			}
			else if (la.kind == 12)
			{
				Get();
			}
			else SynErr(28);
		}

		void MethodCall(out QueryNode node)
		{
			node = null;
			Methods();
			Expect(6);
			ExprSpec(out node);
			Expect(7);
		}

		void Methods()
		{
			if (la.kind == 20)
			{
				Get();
			}
			else if (la.kind == 21)
			{
				Get();
			}
			else SynErr(29);
		}



		public void Parse()
		{
			la = new Token();
			la.val = "";
			Get();
			GoogleChartApi();
			Expect(0);

		}

		static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_T,_x, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x},
		{_x,_T,_T,_x, _x,_x,_T,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x}

	};
	} // end Parser


	public class Errors
	{
		public int count = 0;                                    // number of errors detected
		public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
		public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

		public virtual void SynErr(int line, int col, int n)
		{
			string s;
			switch (n)
			{
				case 0: s = "EOF expected"; break;
				case 1: s = "ident expected"; break;
				case 2: s = "number expected"; break;
				case 3: s = "\"select\" expected"; break;
				case 4: s = "\"where\" expected"; break;
				case 5: s = "\",\" expected"; break;
				case 6: s = "\"(\" expected"; break;
				case 7: s = "\")\" expected"; break;
				case 8: s = "\"-\" expected"; break;
				case 9: s = "\"true\" expected"; break;
				case 10: s = "\"false\" expected"; break;
				case 11: s = "\"*\" expected"; break;
				case 12: s = "\"/\" expected"; break;
				case 13: s = "\"+\" expected"; break;
				case 14: s = "\"==\" expected"; break;
				case 15: s = "\"<\" expected"; break;
				case 16: s = "\"<=\" expected"; break;
				case 17: s = "\">\" expected"; break;
				case 18: s = "\">=\" expected"; break;
				case 19: s = "\"<>\" expected"; break;
				case 20: s = "\"sum\" expected"; break;
				case 21: s = "\"max\" expected"; break;
				case 22: s = "??? expected"; break;
				case 23: s = "invalid SelectClause"; break;
				case 24: s = "invalid ReservedWords"; break;
				case 25: s = "invalid RelOp"; break;
				case 26: s = "invalid AddOp"; break;
				case 27: s = "invalid Factor"; break;
				case 28: s = "invalid MulOp"; break;
				case 29: s = "invalid Methods"; break;

				default: s = "error " + n; break;
			}
			errorStream.WriteLine(errMsgFormat, line, col, s);
			count++;
		}

		public virtual void SemErr(int line, int col, string s)
		{
			errorStream.WriteLine(errMsgFormat, line, col, s);
			count++;
		}

		public virtual void SemErr(string s)
		{
			errorStream.WriteLine(s);
			count++;
		}

		public virtual void Warning(int line, int col, string s)
		{
			errorStream.WriteLine(errMsgFormat, line, col, s);
		}

		public virtual void Warning(string s)
		{
			errorStream.WriteLine(s);
		}
	} // Errors


	public class FatalError : Exception
	{
		public FatalError(string m) : base(m) { }
	}

}
