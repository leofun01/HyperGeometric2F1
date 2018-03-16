using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HyperGeoMetric2F1.Base;
using HyperGeoMetric2F1.Math;
using HyperGeoMetric2F1.Linker;

namespace HyperGeoMetric2F1.Hypergeometric
{
	public struct HyperEquation : ICollection<Coef>, ICloneable<HyperEquation>, IEquatable<HyperEquation>
	{
		private MonoCycle<Coef> _cfs;
		public MonoCycle<HyperEquation> FromEquations;

		//private HyperEquation() { }
		private HyperEquation(Coef c1, Coef c2, Coef c3) : this() { _cfs = new MonoCycle<Coef>(c1, new MonoCycle<Coef>(c2, new MonoCycle<Coef>(c3))); }
		public HyperEquation(Coef c0, Coef c1, Coef c2, params HyperEquation[] from) : this(c0, c1, c2)
		{
			if(from != null)
				foreach(var he in from)
					FromEquations = new MonoCycle<HyperEquation>(he, FromEquations);
		}
		public HyperEquation(Coef c0, Coef c1, Coef c2, MonoCycle<HyperEquation> from) : this(c0, c1, c2) { FromEquations = from; }
		//public HyperEquation(params Coef[] array) : this(ienum: array) { }
		private HyperEquation(IEnumerable<Coef> ienum) : this()
		{
			if(ienum != null)
				foreach(var c in ienum)
					if(_cfs == null) _cfs = new MonoCycle<Coef>(c);
					else _cfs.Add(c);
			//foreach(var c in ienum) _cfs = new MonoCycle<Coef>(c, _cfs);
		}

		public HyperEquation Simplify()
		{
			var he = new HyperEquation();
			foreach(var c in this) he.Add(c.Simplify());
			var cm = Expression.CommonMultiplier(he._cfs.ToArray(m => m.Data.StrFunc));
			if(cm[0] != '-' && he._cfs.Data.StrFunc.StartsWith("-", StringComparison.OrdinalIgnoreCase)) cm = "-" + cm;
			foreach(var c in he._cfs) c.Data = (c.Data / new Coef(cm, c.Data.AbcShift)).Simplify();
			he.FromEquations = FromEquations;
			return he;
		}
		public HyperEquation Flip12() { return new HyperEquation(this[0], this[2], this[1], FromEquations); }
		public HyperEquation Reverse(bool coefs = true, bool fromE = false)
		{
			var clone = Clone();
			if(coefs) clone._cfs = clone._cfs.Reverse();
			if(fromE) clone.FromEquations = clone.FromEquations.Reverse();
			return clone;
		}
		public string ToWolframString()
		{
			var s = ToString().Replace("F + ", "F[a,b,c,z] + ");
			/*while(s.Contains("++") || s.Contains("--") || s.Contains("+-") || s.Contains("-+"))
				s = s.Replace("++", "+").Replace("--", "+").Replace("+-", "-").Replace("-+", "-");//*/
			return s.Replace("F", "Hypergeometric2F1").Replace(" + -", "-").Replace(" + ", "+");
		}
		public string ToWolframFullSimplify() { return ToWolframString() + "//FullSimplify"; }
		//public string ToWolframFullSimplify() { return "FullSimplify[Series[" + ToWolframString() + ",{z,0,5}]]"; }
		public Coef[] Coefs { get { return _cfs.ToArray(m => m.Data); } }
		public Coef this[int index] { get { return _cfs[index].Data; } }
		public Coef this[Abc shift]
		{
			get
			{
				int i = GetIndex(shift);
				if(i < 0) throw new ArgumentException("HyperEquation not contains coef where shift == " + shift, "shift");
				return this[i];
				//foreach(var cf in (IEnumerable<Coef>)_cfs) if(cf.AbcShift == shift) return cf;
			}
		}
		public int GetIndex(Predicate<Coef> pred)
		{
			if(pred == null || _cfs == null) return -1;
			int i = 0;
			foreach(var v in this) { if(pred(v)) return i; ++i; }
			return -1;
		}
		public int GetIndex(Abc shift) { return GetIndex(c => c.AbcShift == shift); }
		private static HyperEquation UnOperation(HyperEquation v, Func<Coef> f)
		{
			var he = new HyperEquation();
			foreach(var c in v) he._cfs = new MonoCycle<Coef>(f(c), he._cfs);
			he.FromEquations = v.FromEquations;
			return he;
		}
		/*private static string GetStr(string s, int i, bool brkt) { return i == 0 ? s : string.Concat(brkt ? "(" : "", (i > 0 ? s + "+" : s) + i, brkt ? ")" : ""); }
		public static string GetShiftedStr(string s, Abc shift, bool brkt)
		{
			return s.Replace("a", GetStr("a", shift.A, brkt))
					.Replace("b", GetStr("b", shift.B, brkt))
					.Replace("c", GetStr("c", shift.C, brkt));
		}//*/

		#region Implements & Overrides

		public override string ToString()
		{
			var sb = new StringBuilder();
			foreach(var c in this) {
				sb.Append(" + ");
				//sb.Append(c.AbcShift);
				bool br = Expression.SplitSum(c.StrFunc).Link != null;
				if(br) sb.Append("(");
				sb.Append(c.StrFunc);
				if(br) sb.Append(")");
				sb.Append(Coef.GetShiftedStr("*F[a,b,c,z]", c.AbcShift, false));
			}
			return sb.Remove(0, 3).Replace("F[a,b,c,z]", "F").ToString();
		}
		public override bool Equals(object obj) { return !ReferenceEquals(null, obj) && obj is HyperEquation && Equals((HyperEquation)obj); }
		public bool Equals(HyperEquation other)
		{
			if(_cfs == null && other._cfs == null || base.Equals(other)) return true;
			if(_cfs == null || other._cfs == null || _cfs.Count != other._cfs.Count) return false;
			foreach(var v in other) if(!_cfs.Contains(v)) return false;
			return true;
		}
		public override int GetHashCode() { unchecked { var hash = 0; foreach(var c in this) hash ^= c.GetHashCode(); return hash; } }

		public void Add(Coef item) { if(_cfs == null) _cfs = new MonoCycle<Coef>(item); else (_cfs = _cfs.GetCopy()).Add(item); }
		public void Clear() { _cfs = null; }
		public bool Contains(Coef item) { return _cfs != null && _cfs.Contains(item); }
		public bool Contains(Predicate<Coef> pred) { return GetIndex(pred) >= 0; }
		public void CopyTo(Coef[] array, int arrayIndex) { if(_cfs != null) _cfs.CopyTo(array, arrayIndex); }
		public bool Remove(Coef item)
		{
			if(_cfs == null || !_cfs.Contains(item)) return false;
			if((_cfs = _cfs.GetCopy()).Remove(item) || (_cfs = _cfs.Link).Remove(item)) return true;
			_cfs = null;
			return true;
		}

		public int Count { get { return _cfs == null ? 0 : _cfs.Count; } }
		public bool IsReadOnly { get { if(_cfs == null) return false; foreach(var c in _cfs) if(c.IsReadOnly) return true; return false; } }

		public HyperEquation Clone()
		{
			var clone = (HyperEquation)MemberwiseClone();
			clone._cfs = _cfs == null ? null : _cfs.GetCopy();
			FromEquations = FromEquations == null ? null : FromEquations.GetCopy();
			return clone;
		}
		object ICloneable.Clone() { return Clone(); }

		public IEnumerator<Coef> GetEnumerator() { return new MonoCycle<Coef>.MonoEnumerator(_cfs); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		#endregion

		#region Operators

		public static HyperEquation operator +(HyperEquation v) { return UnOperation(v, a => +a); }
		public static HyperEquation operator -(HyperEquation v) { return UnOperation(v, a => -a); }

		public static HyperEquation operator +(HyperEquation v, Abc shift)
		{
			var he = new HyperEquation();
			//foreach(var vi in v) he.Add(vi[shift - vi.AbcShift].GetShiftedObj()[shift + vi.AbcShift]);
			foreach(var vi in v) he.Add(vi.GetShiftedObj(shift));
			he.FromEquations = v.FromEquations;
			return he;
		}
		public static HyperEquation operator -(HyperEquation v, Abc shift) { return v + -shift; }

		public static HyperEquation operator >>(HyperEquation v, int i) { return v << -i; }
		public static HyperEquation operator <<(HyperEquation v, int i)
		{
			var clone = v.Clone();
			if(clone._cfs == null) return clone;
			i = i % clone.Count;
			if(i < 0) i += clone.Count;
			clone._cfs = clone._cfs[i];
			return clone;
		}

		public static bool operator ==(HyperEquation l, HyperEquation r) { return l.Equals(r); }
		public static bool operator !=(HyperEquation l, HyperEquation r) { return !l.Equals(r); }

		#endregion
	}
}
