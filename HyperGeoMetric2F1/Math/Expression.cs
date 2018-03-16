using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HyperGeoMetric2F1.Base;
using HyperGeoMetric2F1.Linker;

namespace HyperGeoMetric2F1.Math
{
	public static class Expression
	{
		private static readonly string Zero = Hypergeometric.Coef.Zero.StrFunc, One = Hypergeometric.Coef.One.StrFunc;
		public static bool IsNum(char ch) { return char.IsDigit(ch) || ch == '.'; }
		public static bool IsNum(string s)
		{
			return Array.TrueForAll(s.ToCharArray(), IsNum);
			//if(Array.TrueForAll(s.ToCharArray(), IsNum)) return true;
			//try { ToNum(s); return true; }
			//catch { return false/*Array.TrueForAll(s.ToCharArray(), IsNum)*/; }
		}
		public static bool IsWord(string s) { return Array.TrueForAll(s.ToCharArray(), char.IsLetter); }
		public static MonoLinker<KeyValuePair<char, string>> SplitSum(string s, bool rvrs = false) { return SplitSome(s, @"+-", @"*/%^&|", Zero, rvrs); }
		public static MonoLinker<KeyValuePair<char, string>> SplitProduct(string s, bool rvrs = false) { return SplitSome(s, @"*/ ", @"+-%^&|", One, rvrs); }
		private static MonoLinker<KeyValuePair<char, string>> SplitSome(string s, string separats, string conducts, string dflt, bool rvrs = false)
		{
			if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(separats) ||
				string.IsNullOrEmpty(conducts) || separats.Length < 2) return null;
			char[] sprt = separats.ToCharArray(), cndc = conducts.ToCharArray();
			char flag = (char)(sprt[0] ^ sprt[1]), chng = sprt[1];
			MonoLinker<KeyValuePair<char, string>> ms = null;
			int i = 0, len = s.Length, br = 0;
			do {
				var sign = sprt[0];
				while(i < len && Array.Exists(sprt, s[i].Equals))
					if(s[i++] == chng) sign ^= flag;
				var start = i;
				while(i < len && (br > 0 || !Array.Exists(sprt, s[i].Equals))) {
					if(s[i] == '(') ++br;
					if(s[i] == ')') --br;
					if(br > 0 || !Array.Exists(cndc, s[i].Equals)) ++i;
					else while(++i < len && Array.Exists(sprt, s[i].Equals)) { }
				}
				if(br < 0) i = len;
				ms = new MonoLinker<KeyValuePair<char, string>>(new KeyValuePair<char, string>(sign, i == start ? dflt : s.Substring(start, i - start)), ms);
			} while(i < len);
			return rvrs ? ms : ms.Reverse();
		}
		public static string TrimBrackets(string s)
		{
			if(string.IsNullOrEmpty(s)) return s;
			int i = -1, br = 0, len = s.Length, trim = len >> 1;
			while(++i < len) {
				if(br < i && br < trim && br < len - i) trim = br;
				switch(s[i]) {
					case '(': ++br; break;
					case ')': --br; break;
				}
			}
			return s.Substring(trim, len - (trim << 1));
		}
		public static int FindCloseBracket(string s, int open)
		{
			int br = 0, len = s.Length;
			if(string.IsNullOrEmpty(s) || open >= len) return -1; //--open;
			while(++open < len)
				switch(s[open]) {
					case '(': ++br; break;
					case ')': if(--br < 0/*== 0*/) return open; break;
				}
			return -1;
		}
		//private static string StrAdd(string s1, string s2) { return s1 == Zero ? s2 : s2 == Zero ? s1 : string.Concat(s1, @"+", s2); }
		//private static string StrMult(string s1, string s2) { return s1 == One ? s2 : s2 == One ? s1 : string.Concat(s1, @"*", s2); }
		public static MultiLinker<KeyValuePair<char, string>> ArithmeticGraph(string s, char ch = '+')
		{
			var g = new MultiLinker<KeyValuePair<char, string>>(new KeyValuePair<char, string>(ch, s = TrimBrackets(s)));
			MonoLinker<KeyValuePair<char, string>> split;
			if((split = SplitSum(s, true)).Link == null && s == split.Data.Value &&
				(split = SplitProduct(s, true)).Link == null && s == split.Data.Value) return g;
			foreach(var pair in split)
				g.Link = new MonoLinker<MultiLinker<KeyValuePair<char, string>>>(
					ArithmeticGraph(pair.Data.Value, pair.Data.Key), g.Link);
			return g;
		}
		private static KeyValuePair<char, string> ToSignedString(MultiLinker<KeyValuePair<char, string>> s)
		{
			if(s == null) return default(KeyValuePair<char, string>);
			if(s.Link == null) return s.Data;
			var sb = new StringBuilder();
			bool t = true,
				b = (s.Data.Key == '*' || s.Data.Key == '/') && s.Link.Data.Data.Key == '-' ||
				s.Link.Link != null && (s.Data.Key == '/' ||
				((s.Data.Key == '-' || s.Data.Key == '*') &&
				(s.Link.Data.Data.Key == '+' || s.Link.Data.Data.Key == '-')));
			if(b) sb.Append(@"(");
			foreach(var pair in s) {
				var c = ToSignedString(pair.Data);
				if((EqualsPorN(c.Key, '+', '-') && c.Value == Zero) ||
					(EqualsPorN(c.Key, '*', '/') && c.Value == One)) continue;
				if(EqualsPorN(c.Key, '*', '/') && c.Value == Zero) return new KeyValuePair<char, string>('+', Zero);
				if(t && c.Key == '/') sb.Append(One);
				if(!t || (c.Key != '+' && c.Key != '*')) sb.Append(c.Key);
				sb.Append(c.Value);
				t = false;
			}
			if(t) sb.Append(EqualsPorN(s.Link.Data.Data.Key, '*', '/') ? One : Zero);
			if(b) sb.Append(@")");
			return new KeyValuePair<char, string>(s.Data.Key, sb.ToString());
		}
		public static string ToString(KeyValuePair<char, string> pair)
		{
			return (pair.Key == '/' ? @"1/" :
				pair.Key == '-' && pair.Value != Zero && !string.IsNullOrEmpty(pair.Value) ? @"-" : string.Empty) +
				(string.IsNullOrEmpty(pair.Value) ? EqualsPorN(pair.Key, '*', '/') ? One : Zero : pair.Value);
		}
		public static string ToString(MultiLinker<KeyValuePair<char, string>> s) { return ToString(ToSignedString(s)); }
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		/// <exception cref="OverflowException"></exception>
		public static double ToNum(string s)
		{
			try { return double.Parse(s); }
			catch {
				var separator = NumberFormatInfo.InvariantInfo.NumberDecimalSeparator;
				try { return double.Parse(s.Replace(".", separator).Replace(",", separator)
					.Replace(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, separator)); }
				catch {
					separator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
					return double.Parse(s.Replace(".", separator).Replace(",", separator)
						.Replace(NumberFormatInfo.InvariantInfo.NumberDecimalSeparator, separator));
				}
			}
		}
		public static string StrFuncStr(string s, Func<MultiLinker<KeyValuePair<char, string>>> meth) { return ToString(meth(ArithmeticGraph(s))); }
		//
		public static string Normalize(string s) { return StrFuncStr(s, Normalize); }
		/// <summary> Розкриває всі дужки, в яких операції такі як за дужками </summary>
		public static MultiLinker<KeyValuePair<char, string>> Normalize(MultiLinker<KeyValuePair<char, string>> s)
		{
			if(s == null) return null;
			while(s.Link != null && s.Link.Link == null &&
				(EqualsPorN(s.Link.Data.Data.Key, '+', '*') ||
				_check21(s.Link.Data.Data.Key, s.Data.Key, '-', '+')))
			{
				if(s.Link.Data.Link == null) s.Data = new KeyValuePair<char, string>(s.Data.Key, s.Link.Data.Data.Value);
				(s.Link.Data.Data.Key == '-' ? ChangeSign(s, '+', '-') : s).Link = s.Link.Data.Link;
			}
			NormSignDown(s, '+', '-'); NormSignDown(s, '*', '/');
			if(s.Link == null) return s;
			foreach(var pair in s) {
				if(pair.Data.Link == null) continue;
				pair.Data = Normalize(pair.Data);
				NormGetUp(s, pair);
			}
			NormMinusUp(s, m => { foreach(var pair in m) if(pair.Data.Data.Key == '+') return false; return true; });//*/
			//_prepare(s, null/*Normalize*/);
			return s;
		}
		public static bool NormSignDown(MultiLinker<KeyValuePair<char, string>> s, char pstv, char ngtv)
		{
			if(s == null || s.Link == null || !_check21(s.Data.Key, s.Link.Data.Data.Key, ngtv, pstv)) return false;
			s.Data = new KeyValuePair<char, string>(pstv, s.Data.Value);
			foreach(var pair in (IEnumerable<MultiLinker<KeyValuePair<char, string>>>)s) ChangeSign(pair, pstv, ngtv);
			return true;
		}
		public static bool NormMinusUp(MultiLinker<KeyValuePair<char, string>> s, Predicate<MultiLinker<KeyValuePair<char, string>>> pred)
		{
			if(s == null || s.Link == null || !EqualsPorN(s.Data.Key, '+', '-')/* || !EqualsPorN(s.Link.Data.Data.Key, '*', '/')*/) return false;
			var b = false;
			Action<MultiLinker<KeyValuePair<char, string>>> act = m => {
				if(m.Link == null || !EqualsPorN(m.Link.Data.Data.Key, '+', '-') || !pred(m)) return;
				b ^= true;
				foreach(var p in (IEnumerable<MultiLinker<KeyValuePair<char, string>>>)m) ChangeSign(p, '+', '-');
			};
			if(EqualsPorN(s.Link.Data.Data.Key, '*', '/'))
				foreach(var pair in (IEnumerable<MultiLinker<KeyValuePair<char, string>>>)s) act(pair);
			else act(s);
			if(b) ChangeSign(s, '+', '-');
			return true;
		}
		private static bool NormGetUp(MultiLinker<KeyValuePair<char, string>> s, MonoLinker<MultiLinker<KeyValuePair<char, string>>> mono)
		{
			if(mono.Data.Link == null || mono.Data.Link.Data == null) return false;
			if(!_check20(mono.Data.Data.Key, mono.Data.Link.Data.Data.Key)) return false;
			//var p = pair.Data; KeyValuePair<char, string> v1 = p.Data, v2 = p.Link.Data.Data;
			//if((v1.Key != '+' || (v2.Key != '+' && v2.Key != '-')) && (v1.Key != '*' || (v2.Key != '*' && v2.Key != '/'))) return false;
			var copy = mono.Data.Link.GetCopy();
			copy.GetLast().Link = mono.Link;
			//pair.Link = copy; s.Remove(pair);
			if(s.Link == mono) s.Link = copy;
			else s.Link.GetBefore(mono).Link = copy;
			return true;
		}
		private static bool _check20(char c1, char c2) { return _check21(c1, c2, '+', '-') || _check21(c1, c2, '*', '/'); }
		private static bool _check21(char c1, char c2, char pstv, char ngtv) { return c1 == pstv && EqualsPorN(c2, pstv, ngtv); }
		private static bool _prepare(MultiLinker<KeyValuePair<char, string>> s, Func<MultiLinker<KeyValuePair<char, string>>> func)
		{
			if(s == null || s.Link == null) return false;
			//NormSignDown(s, '+', '-'); NormSignDown(s, '*', '/');
			foreach(var pair in s) {
				var p = pair.Data;
				if(p.Link == null) continue;
				NormSignDown(p, '+', '-'); NormSignDown(p, '*', '/');
				if(NormGetUp(s, pair)) continue;
				pair.Data = (func ?? (m => { _prepare(m, null); return m; }))(p);
				NormGetUp(s, pair);
			}
			NormMinusUp(s, m => m.Link.Data.Data.Key == '-'
				//{ foreach(var pair in m) if(pair.Data.Data.Key == '+') return false; return true; }
				);
			return true;
		}
		public static bool EqualsPorN(char c, char pstv, char ngtv) { return c == pstv || c == ngtv; }
		public static MultiLinker<KeyValuePair<char, string>> ChangeSign(MultiLinker<KeyValuePair<char, string>> s, char pstv, char ngtv)
		{
			s.Data = new KeyValuePair<char, string>(s.Data.Key == pstv ? ngtv : pstv, s.Data.Value);
			return s;
		}
		/*public static string Expand(string s) { return StrFuncStr(s, Expand); }
		public static MultiLinker<KeyValuePair<char, string>> Expand(MultiLinker<KeyValuePair<char, string>> s)
		{
			if(s == null || s.Link == null) return s;
			if((s.Data.Key != '+' && s.Data.Key != '-') || (s.Link.Data.Data.Key != '*' && s.Link.Data.Data.Key != '/')) return s;
			var clone = s.Clone();
			clone.Link = null;
			clone.Add(new KeyValuePair<char, string>('+', _one));
			foreach(var pair in s) {
				if(pair.Data.Link == null) {
					foreach(var c in clone)
						if(pair.Data.Data.Value != _one) c.Data.Add(pair.Data);
				}
				else {
					var temp = s.Clone();
					temp.Link = null;
					foreach(var c in clone) {
						foreach(var p in pair.Data) {
							var copy = c.Data.GetCopy();
							if(p.Data.Link != null) copy.Add(p.Data.Link);
							else if(p.Data.Data.Value != _one)
								copy.Add(new MultiLinker<KeyValuePair<char, string>>(new KeyValuePair<char, string>(
									pair.Data.Data.Key, p.Data.Data.Value), p.Data.Link));
							if(p.Data.Data.Key == '-')
								copy.Data = new KeyValuePair<char, string>(copy.Data.Key == '+' ? '-' : '+', copy.Data.Value);
							temp.Add(copy);
						}
					}
					clone = temp;
				}
			}
			NormCheck1(clone, '+', '-');
			return clone.Link.Link == null ? clone.Link.Data : clone;
		}//*/
		public static string FullExpand(string s) { return StrFuncStr(s, FullExpand); }
		/// <summary> Відкриває всі дужки і повертає<returns> суму добутків </returns></summary>
		public static MultiLinker<KeyValuePair<char, string>> FullExpand(MultiLinker<KeyValuePair<char, string>> s)
		{
			if(!_prepare(s, FullExpand)) return s;
			if((s.Data.Key != '+' && s.Data.Key != '-') || (s.Link.Data.Data.Key != '*' && s.Link.Data.Data.Key != '/')) return s;
			var clone = s.Clone();
			clone.Link = null;
			clone.Add(new KeyValuePair<char, string>('+', One));
			foreach(var pair in s) {
				if(pair.Data.Link == null || pair.Data.Data.Key == '/') {
					foreach(var c in clone)
						if(pair.Data.Data.Value != One) c.Data.Add(pair.Data);
				}
				else {
					var temp = s.Clone();
					temp.Link = null;
					foreach(var c in clone) {
						foreach(var p in pair.Data) {
							var copy = c.Data.GetCopy();
							if(p.Data.Link != null) copy.Add(p.Data.Link);
							else if(p.Data.Data.Value != One)
								copy.Add(s.Clone(new KeyValuePair<char, string>(
									pair.Data.Data.Key, p.Data.Data.Value), p.Data.Link));
							temp.Add(p.Data.Data.Key == '-' ? ChangeSign(copy, '+', '-') : copy);
						}
					}
					clone = temp;
				}
			}
			NormSignDown(clone, '+', '-');
			return clone.Link.Link == null ? clone.Link.Data : clone;
		}
		public static bool EqualsByValue(string s1, string s2, bool ignoreSign = false) { return EqualsByValue(ArithmeticGraph(s1), ArithmeticGraph(s2), ignoreSign); }
		public static bool EqualsByValue(MultiLinker<KeyValuePair<char, string>> s1, MultiLinker<KeyValuePair<char, string>> s2, bool ignoreSign = false)
		{
			if(s1 == s2) return true;
			if(s1 == null || s2 == null) return false;
			bool b1 = s1.Link == null, b2 = s2.Link == null;
			if(b1 || b2) return (b1 ? s1.Data : ToSignedString(s1)).Value == (b2 ? s2.Data : ToSignedString(s2)).Value;
			/*s1 = FullExpand(s1);
			s2 = FullExpand(s2);
			;//*/
			return ToSignedString(s1).Value == ToSignedString(s2).Value;
		}
		/// <summary><returns> Common factor </returns></summary>
		public static string CommonMultiplier(params string[] ss)
		{
			int len = ss.GetLength(0), i = -1;
			var gg = new MultiLinker<KeyValuePair<char, string>>[len];
			while(++i < len) gg[i] = ArithmeticGraph(ss[i]);
			return ToString(CommonMultiplier((IEnumerable<MultiLinker<KeyValuePair<char, string>>>)gg));
		}
		public static MultiLinker<KeyValuePair<char, string>> CommonMultiplier(params MultiLinker<KeyValuePair<char, string>>[] ss) { return CommonMultiplier((IEnumerable<MultiLinker<KeyValuePair<char, string>>>)ss); }
		public static MultiLinker<KeyValuePair<char, string>> CommonMultiplier(IEnumerable<MultiLinker<KeyValuePair<char, string>>> ss)
		{
			MultiLinker<KeyValuePair<char, string>> r = ArithmeticGraph(One), t;
			if(ss == null) return r;
			var e = ss.GetEnumerator();
			if(!e.MoveNext() || e.Current == null) return r;
			bool minus = false;
			if(EqualsPorN(e.Current.Data.Key, '*', '/')) r.Add(e.Current);
			else {
				minus = e.Current.Data.Key == '-';
				if(e.Current.Link != null && EqualsPorN(e.Current.Link.Data.Data.Key, '*', '/'))
					foreach(var mono in e.Current) r.Add(mono.Data);
				else r.Add(e.Current.Clone(new KeyValuePair<char, string>('*', e.Current.Data.Value), e.Current.Link));
			}
			while(e.MoveNext()) {
				t = ArithmeticGraph(One);
				if(e.Current == null) return t;
				minus &= e.Current.Data.Key == '-';
				if(EqualsPorN(e.Current.Data.Key, '*', '/')) t.Add(e.Current);
				else {
					if(e.Current.Link != null && EqualsPorN(e.Current.Link.Data.Data.Key, '*', '/'))
						foreach(var mono in e.Current) t.Add(mono.Data);
					else t.Add(e.Current.Clone(new KeyValuePair<char, string>('*', e.Current.Data.Value), e.Current.Link));
				}
				foreach(var pair in r) {
					var copy = pair.Data;
					if(!t.Remove(mono => mono != null &&
						(mono.Data.Data.Key == copy.Data.Key && EqualsByValue(mono.Data, copy))))
						r.Remove(pair);
					else if(t.Link == null) pair.Link = null;
				}
			}
			return minus ? ChangeSign(r, '+', '-') : r;
		}
		public static string Cancel(string s) { return StrFuncStr(s, Cancel); }
		/// <summary> Annihilate </summary>
		public static MultiLinker<KeyValuePair<char, string>> Cancel(MultiLinker<KeyValuePair<char, string>> s)
		{
			if(!_prepare(s, Cancel)) return s;
			bool mult = EqualsPorN(s.Link.Data.Data.Key, '*', '/');
			double sum = mult ? 1d : 0d, n = 0d;
			foreach(var pair in s) {
				var p = pair.Data;
				if(IsNum(p.Data.Value)) {
				//try {
					var num = ToNum(p.Data.Value);
					if(num.Equals(0d) && mult)
						return s.Clone(new KeyValuePair<char, string>(s.Data.Key,
							num.ToString(CultureInfo.InvariantCulture)), null);
					switch(p.Data.Key) {
						case '+': sum += num; break;
						case '-': sum -= num; break;
						case '*': sum *= num; break;
						case '/': sum /= num; break;
					}
					s.Remove(pair);
				}
				else {
				//catch {
					var c = (char)(mult ? (p.Data.Key ^ '*' ^ '/') : (p.Data.Key ^ '+' ^ '-'));
					if(pair.Remove(mono => mono != null && mono.Data != null &&
						(mono.Data.Data.Key == c && EqualsByValue(mono.Data, p)/* ||
						mono.Data.Data.Key == p.Data.Key && EqualsByValue(mono.Data, p)*/)))
					{ s.Remove(pair); continue; }
					/*if(pair.Data.Link == null)
					{
						var temp = s.Link.Find(mono => mono != null && mono.Data != null &&
							mono.Data.Count > 1 &&
							IsNum(mono.Data.Link.Data.Data.Value) &&
							EqualsByValue(mono.Data.Link.Link.Data.Data.Value, p.Value));
						if(false)
							s.Remove(pair);
					}//*/
					//
					if(EqualsPorN(p.Data.Key, '+', '-') && (p.Data.Value == @"n" ||
						(p.Link != null && p.Link.Link != null && p.Link.Link.Link == null &&
						IsNum(p.Link.Data.Data.Value) && p.Link.Link.Data.Data.Value == @"n")))
					{
						n += (p.Data.Key == '-' ? -1d : 1d) * (p.Data.Value == @"n" ? 1d : double.Parse(p.Link.Data.Data.Value));
						s.Remove(pair);
					}//*/
				}
			}
			if(s.Link == null)
				return s.Clone(new KeyValuePair<char, string>(s.Data.Key,
					sum.ToString(CultureInfo.InvariantCulture)), null);
			if(mult) {
				if(!sum.Equals(1d))
					s.AddFirst(new KeyValuePair<char, string>(/*sum < 1d ? '/' :*/ '*',
						(/*sum < 1d ? 1d / sum : */sum).ToString(CultureInfo.InvariantCulture)));
			}
			if(!n.Equals(0d)) {
				string abs = System.Math.Abs(n).ToString(CultureInfo.InvariantCulture);
				var c = s.Clone(new KeyValuePair<char, string>(n < 0d ? '-' : '+', abs + @"*n"), null);
				c.Add(new KeyValuePair<char, string>('*', abs));
				c.Add(new KeyValuePair<char, string>('*', @"n"));
				s.Add(c);
			}
			if(!mult) {
			//else {
				if(!sum.Equals(0d))
					s.Add(new KeyValuePair<char, string>(sum < 0d ? '-' : '+',
						System.Math.Abs(sum).ToString(CultureInfo.InvariantCulture)));
			}
			return s;
		}
		public static string Sort(string s, Comparison<MultiLinker<KeyValuePair<char, string>>> compar = null) { return ToString(Sort(ArithmeticGraph(s), compar)); }
		public static MultiLinker<KeyValuePair<char, string>> Sort(MultiLinker<KeyValuePair<char, string>> s, Comparison<MultiLinker<KeyValuePair<char, string>>> compar = null)
		{
			if(s == null || s.Link == null) return s;
			if(compar == null) compar = (m1, m2) => string.CompareOrdinal(m1.Data.Value, m2.Data.Value);
			foreach(var pair in s) pair.Data = Sort(pair.Data, compar);
			foreach(var pair in s) {
				if(pair.Link == null) continue;
				var t = pair;
				foreach(var p in pair.Link) if(compar(p.Data, t.Data) < 0) t = p;
				if(t == pair) continue;
				var data = pair.Data;
				pair.Data = t.Data;
				t.Data = data;
			}
			return s;
		}
		public static string Factor(string s) { return StrFuncStr(s, Factor); }
		public static MultiLinker<KeyValuePair<char, string>> Factor(MultiLinker<KeyValuePair<char, string>> s)
		{
			return s;
		}
		public static string Simplify(string s) { return StrFuncStr(s, Simplify); }
		public static string Simplify(string s, Comparison<MultiLinker<KeyValuePair<char, string>>> compar) { return ToString(Simplify(ArithmeticGraph(s), compar)); }
		public static MultiLinker<KeyValuePair<char, string>> Simplify(MultiLinker<KeyValuePair<char, string>> s) { return Simplify(s, null); }
		public static MultiLinker<KeyValuePair<char, string>> Simplify(MultiLinker<KeyValuePair<char, string>> s, Comparison<MultiLinker<KeyValuePair<char, string>>> compar)
		{
			var g = s;
			string gS = ToString(s), sS;
			do { /*s = g;//*/ sS = gS;
				g = ArithmeticGraph(gS);
				//g = Normalize(g);
				//g = FullExpand(g);
				g = Cancel(g);
				//g = Factor(g);
				g = Sort(g, compar);
				g = Normalize(g);
				gS = ToString(g);
			} while(gS != sS);
			return g;
		}
	}
}
