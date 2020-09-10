using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using HyperGeometric2F1.Base;
using HyperGeometric2F1.Math;
using HyperGeometric2F1.Linker;

namespace HyperGeometric2F1.Hypergeometric
{
	public delegate ComplexDouble Fabcz(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z = new ComplexDouble());

	public struct Coef : ICloneable<Coef>, IEquatable<Coef>
	{
		public readonly string StrFunc;
		public readonly Abc AbcShift;
		private Fabcz _func;

		public Coef(string s, Abc shift = new Abc()) : this() { StrFunc = ValidateString(s); AbcShift = shift; }
		public Func<ComplexDouble> Calc(ComplexDouble a, ComplexDouble b, ComplexDouble c)
		{
			var f = (_func ?? (_func = _strToFunc(StrFunc)));
			return z => f(a, b, c, z);
		}
		public ComplexDouble Calc(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z/* = new ComplexDouble()*/)
		{
			return (_func ?? (_func = _strToFunc(StrFunc)))
				(a/* + AbcShift.A*/, b/* + AbcShift.B*/, c/* + AbcShift.C*/, z);
		}

		public static readonly Coef Zero = new Coef(@"0"), One = new Coef(@"1");
		#region SortComparison
		public static readonly Comparison<MultiLinker<KeyValuePair<char, string>>> SortComparison = (m1, m2) =>
		{
			if(m1 == m2) return 0;
			if(m1 == null || string.IsNullOrEmpty(m1.Data.Value)) return -1;
			if(m2 == null || string.IsNullOrEmpty(m2.Data.Value)) return 1;
			if(m1.Data.Value == m2.Data.Value) return 0;
			//
			bool m1Num = Expression.IsNum(m1.Data.Value), m2Num = Expression.IsNum(m2.Data.Value),
				m1I = m1.Data.Value == @"i", m2I = m2.Data.Value == @"i",
				m1A = m1.Data.Value == @"a", m2A = m2.Data.Value == @"a",
				m1B = m1.Data.Value == @"b", m2B = m2.Data.Value == @"b",
				m1C = m1.Data.Value == @"c", m2C = m2.Data.Value == @"c",
				m1N = m1.Data.Value == @"n", m2N = m2.Data.Value == @"n",
				m1Z = m1.Data.Value == @"z", m2Z = m2.Data.Value == @"z",
				//m1ContainI = m1I || m1.Data.Value.Contains(@"i"), m2ContainI = m2I || m2.Data.Value.Contains(@"i"),
				//m1ContainA = m1A || m1.Data.Value.Contains(@"a"), m2ContainA = m2A || m2.Data.Value.Contains(@"a"),
				//m1ContainB = m1B || m1.Data.Value.Contains(@"b"), m2ContainB = m2B || m2.Data.Value.Contains(@"b"),
				//m1ContainC = m1C || m1.Data.Value.Contains(@"c"), m2ContainC = m2C || m2.Data.Value.Contains(@"c"),
				m1ContainN = m1N || m1.Data.Value.Contains(@"n"), m2ContainN = m2N || m2.Data.Value.Contains(@"n"),
				m1ContainZ = m1Z || m1.Data.Value.Contains(@"z"), m2ContainZ = m2Z || m2.Data.Value.Contains(@"z");
			int len1 = m1.Data.Value.Length, len2 = m2.Data.Value.Length;
			if(Expression.EqualsPorN(m1.Data.Key, '*', '/') && Expression.EqualsPorN(m2.Data.Key, '*', '/')) {
				if(m1.Data.Key == '*' && m2.Data.Key == '/') return -1;
				if(m1.Data.Key == '/' && m2.Data.Key == '*') return 1;
				if(m1Num ^ m2Num) return m1Num ? -1 : 1;
				if(m1I || m2I) return m1I ? -1 : 1;
				if(m1Z || m2Z) return m1Z ? 1 : -1;
				if(m1ContainZ && !m2ContainZ) return 1;
				if(!m1ContainZ && m2ContainZ) return -1;
				if(m1N || m2N) return m1N ? 1 : -1;
				if(m1ContainN && !m2ContainN) return 1;
				if(!m1ContainN && m2ContainN) return -1;
				if(m1A || m2A) return m1A ? -1 : 1;
				if(m1B || m2B) return m1B ? -1 : 1;
				if(m1C || m2C) return m1C ? -1 : 1;//*/
			}
			else {
				if(m1I || m2I) return m1I ? 1 : -1;
				if(m1Num ^ m2Num) return m1Num ? 1 : -1;
				if(m1N || m2N) return m1N ? 1 : -1;
				if(m1ContainN && !m2ContainN) return 1;
				if(!m1ContainN && m2ContainN) return -1;
				if(m1ContainN && m2ContainN && m1ContainZ && !m2ContainZ) return -1;
				if(m1ContainN && m2ContainN && !m1ContainZ && m2ContainZ) return 1;
				if(m1Z || m2Z) return m1Z ? 1 : -1;
				if(m1ContainZ && !m2ContainZ) return 1;
				if(!m1ContainZ && m2ContainZ) return -1;
				if(m1C || m2C) return m1C ? -1 : 1;
				if(m1A || m2A) return m1A ? -1 : 1;
				if(m1B || m2B) return m1B ? -1 : 1;//*/
			}
			return len1 == len2 ? string.CompareOrdinal(m1.Data.Value, m2.Data.Value) : len1 - len2;
		};
		#endregion
		public Coef Simplify()
		{
			////string s, t = StrFunc;
			//var g = Expression.ArithmeticGraph(StrFunc);
			////do { s = t;
			//g = Expression.Simplify(g, SortComparison);
			//	/*Expression.NormMinusUp(g, m => {
			//		if(m == null || m.Link == null) return false;
			//		if(m.Contains(mono => mono != null && mono.Data.Data.Value == @"c")) return m.Contains(new KeyValuePair<char, string>('-', @"c"));
			//		if(m.Contains(new KeyValuePair<char, string>('-', @"a")) && m.Contains(new KeyValuePair<char, string>('-', @"b"))) return true;
			//		if(m.Contains(new KeyValuePair<char, string>('+', @"a")) && m.Contains(new KeyValuePair<char, string>('+', @"b"))) return false;
			//		//if(m.Contains(new KeyValuePair<char, string>('-', @"z"))) return true;
			//		return m.Contains(new KeyValuePair<char, string>('-', @"z")) || m.Link.Data.Data.Key == '-';
			//	});//*/
			//	//t = Expression.ToString(g);
			////} while(t != s); return new Coef(t, AbcShift);
			//return new Coef(Expression.ToString(g), AbcShift);
			return new Coef(Expression.Simplify(StrFunc, SortComparison), AbcShift);
		}

		#region Functions
		public static Fabcz StringToFunc(string s) { return _strToFunc(ValidateString(s)); }
		private static Fabcz _strToFunc(string s)
		{
			switch(s) {
				case "a": return (a, b, c, z) => a;
				case "b": return (a, b, c, z) => b;
				case "c": return (a, b, c, z) => c;
				case "z": return (a, b, c, z) => z;
				case "i": return (a, b, c, z) => ComplexDouble.I;
				//case "e": return (a, b, c, z) => ComplexDouble.E;
				//case "p": return (a, b, c, z) => ComplexDouble.Pi;
			}
			if(Expression.IsNum(s)) {
				var d = double.Parse(s, NumberStyles.None, NumberFormatInfo.InvariantInfo);
				return (a, b, c, z) => d;
			}
			//if(Expression.IsWord(s)) return (a, b, c, z) => dfltN;
			IEnumerable<KeyValuePair<char, string>> sum = Expression.SplitSum(s);
			Fabcz addf = (a, b, c, z) => 0d;
			//if(sum == null) return addf;
			foreach(var pairS in sum) {
				IEnumerable<KeyValuePair<char, string>> prod = Expression.SplitProduct(pairS.Value);
				Fabcz af = addf, mulf = (a, b, c, z) => 1d;
				//if(prod == null) return mulf;
				foreach(var pairP in prod) {
					Fabcz mf = mulf, stf = _strToFunc(Expression.TrimBrackets(pairP.Value));
					if(pairP.Key == '*') mulf = (a, b, c, z) => mf(a, b, c, z) * stf(a, b, c, z);
					else mulf = (a, b, c, z) => mf(a, b, c, z) / stf(a, b, c, z);
				}
				if(pairS.Key == '+') addf = (a, b, c, z) => af(a, b, c, z) + mulf(a, b, c, z);
				else addf = (a, b, c, z) => af(a, b, c, z) - mulf(a, b, c, z);
			}
			return addf;
		}
		public static string ValidateString(string s)
		{
			if(string.IsNullOrEmpty(s)) throw new ArgumentNullException("s", "String is null or empty");
			var sb = (new StringBuilder(s.ToLowerInvariant())).Replace(" ", string.Empty).Replace(',', '.');
			int i = -1, br = 0, len = sb.Length;
			var charray = "abczi+-*/".ToCharArray();
			var pnt = false;
			while(++i < len) {
				pnt = pnt && Expression.IsNum(sb[i]);
				switch(sb[i]) {
					case '.':
						if(pnt) throw new ArgumentException("String contains invalid char \'" + sb[i] + "\'", "s");
						pnt = true;
						if((i == 0 || !char.IsDigit(sb[i - 1])) && (i == len - 1 || !char.IsDigit(sb[i + 1])))
							sb[i] = '0';
						break;
					case '(': ++br; break;
					case ')':
						if(--br < 0) throw new OverflowException("Invalid structure of brackets, see s[" + i + "] in " + sb);
						break;
					default:
						if(!(char.IsDigit(sb[i]) || Array.Exists(charray, sb[i].Equals)))
							throw new ArgumentException("String contains invalid char \'" + sb[i] + "\'", "s");
						break;
				}
			}
			if(br > 0) throw new OverflowException("Count of opened brackets = " + br + " in " + sb);
			//
			while(--i > 0) {
				if((Expression.IsNum(sb[i]) || char.IsLetter(sb[i]) || sb[i] == '(') &&
					(Expression.IsNum(sb[i - 1]) || char.IsLetter(sb[i - 1]) || sb[i - 1] == ')') &&
					!(Expression.IsNum(sb[i]) && Expression.IsNum(sb[i - 1])))
					sb.Insert(i, "*");
				if((sb[i] == '*' || sb[i] == '/') && (sb[i - 1] == '+' || sb[i - 1] == '-') ||
					(sb[i] == ')' && sb[i - 1] == '('))
					throw new ArgumentException("String contains invalid chars \"" + sb[i - 1] + sb[i] + "\"", "s");
				/*// abczi +*-/ ().
				 * +* error (throw ok)
				 * +) // (no)
				 * *) // (no)
				 * (* // (no)
				 * () error (throw ok) // */
			}
			return sb.ToString();
		}
		//
		public Coef this[Abc shift] { get { return new Coef(GetShiftedStr(StrFunc, shift, true), AbcShift + shift); } }
		public Coef this[int a, int b, int c] { get { return this[new Abc(a, b, c)]; } }
		//
		private static string GetStr(string s, int i, bool brkt) { return i == 0 ? s : string.Concat(brkt ? "(" : "", (i > 0 ? s + "+" : s) + i, brkt ? ")" : ""); }
		public static string GetShiftedStr(string s, Abc shift, bool brkt)
		{
			return s.Replace("a", GetStr("a", shift.A, brkt))
					.Replace("b", GetStr("b", shift.B, brkt))
					.Replace("c", GetStr("c", shift.C, brkt));
		}
		//public string GetShiftedStr() { return GetShiftedStr(StrFunc, AbcShift, true); }
		//public Coef GetShiftedObj() { return new Coef(GetShiftedStr()); }
		//public Coef GetShiftedObj(Abc shift) { return this[shift - AbcShift].GetShiftedObj()[AbcShift - shift]; }
		//public Coef GetShiftedObj(Abc shift) { return this[-shift].GetShiftedObj()[shift]; }
		public Coef GetShiftedObj(Abc shift) { return new Coef(GetShiftedStr(StrFunc, shift, true), AbcShift + shift); }
		public Coef GetShiftedObj(int a, int b, int c) { return GetShiftedObj(new Abc(a, b, c)); }//*/
		//
		private static Coef BiOperation(Coef l, Coef r, string s)
		{
			bool add = s == @"+", mlt = s == @"*", sum = add || s == @"-", prd = mlt || s == @"/", sign = false;
			MonoLinker<KeyValuePair<char, string>> ls = Expression.SplitSum(l.StrFunc), rs = Expression.SplitSum(r.StrFunc);
			if(prd && rs.Link == null) {
				if(ls.Data.Key == '-' && ls.Link == null) sign ^= true;
				if(rs.Data.Key == '-') sign ^= true;
				var rsp = Expression.SplitProduct(rs.Data.Value);
				return new Coef(string.Concat(sign ? @"-" : string.Empty,
					ls.Link == null ? ls.Data.Value : string.Concat(@"(", l.StrFunc, @")"), s,
					mlt || rsp.Link == null ? rs.Data.Value : string.Concat(@"(", rs.Data.Value, @")")), l.AbcShift);
			}
			return new Coef(string.Concat(
				sum || (prd && ls.Link == null) ? l.StrFunc : string.Concat(@"(", l.StrFunc, @")"), s,
				add || (mlt && rs.Link == null) ? r.StrFunc : string.Concat(@"(", r.StrFunc, @")")), l.AbcShift);
		}
		#endregion

		#region Implements & Overrides
		public override string ToString() { return string.Concat(@"G" + AbcShift, @" = " + StrFunc); }
		public Coef Clone() { return (Coef)MemberwiseClone(); }
		object ICloneable.Clone() { return Clone(); }
		public override bool Equals(object obj) { return !ReferenceEquals(null, obj) && obj is Coef && Equals((Coef)obj); }
		public bool Equals(Coef other) { return base.Equals(other) || StrFunc/*GetShiftedStr()*/ == other.StrFunc/*GetShiftedStr()*/; }
		public override int GetHashCode() { unchecked { return (AbcShift.GetHashCode() * 44519) ^ base.GetHashCode(); } }
		#endregion

		#region Operators
		public static Coef operator +(Coef v) { return new Coef(Expression.TrimBrackets(v.StrFunc), v.AbcShift); }
		public static Coef operator -(Coef v)
		{
			string s = Expression.TrimBrackets(v.StrFunc);
			//return new Coef(s.StartsWith("-") ? Expression.TrimBrackets(s.Substring(1, s.Length - 1) : ), v.AbcShift);
			return new Coef(s.StartsWith("-(") && Expression.FindCloseBracket(s, 1) == s.Length - 1 ?
				s.Substring(2, s.Length - 3) : string.Concat("-(", s, ")"), v.AbcShift);//*/
		}

		public static Coef operator +(Coef l, Coef r) { return l == -r ? Zero[l.AbcShift] : BiOperation(l, r, "+"); }
		public static Coef operator -(Coef l, Coef r) { return l == r ? Zero[l.AbcShift] : BiOperation(l, r, "-"); }
		public static Coef operator *(Coef l, Coef r) { return l == One ? new Coef(r.StrFunc, l.AbcShift) : r == One ? l : BiOperation(l, r, "*"); }
		public static Coef operator /(Coef l, Coef r) { return l == r ? One[l.AbcShift] : l == -r ? new Coef("-1", l.AbcShift) : BiOperation(l, r, "/"); }

		public static bool operator ==(Coef l, Coef r) { return l.Equals(r); }
		public static bool operator !=(Coef l, Coef r) { return !l.Equals(r); }
		#endregion
	}
}
