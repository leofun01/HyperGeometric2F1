using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HyperGeometric2F1.Base;

namespace HyperGeometric2F1.Hypergeometric
{
	public struct Abc : ICloneable<Abc>, IEquatable<Abc>, IEnumerable<int>
	{
		public readonly int A, B, C;
		//public int Sum { get { return A + B + C; } }

		public Abc(int a, int b, int c) { A = a; B = b; C = c; }

		public static readonly Abc Zero = default(Abc);

		private static Abc UnOperation(Abc v, Func<int> f) { return new Abc(f(v.A), f(v.B), f(v.C)); }
		private static Abc BiOperation(Abc l, Abc r, Func2<int> f) { return new Abc(f(l.A, r.A), f(l.B, r.B), f(l.C, r.C)); }
		private static Abc BiOperation(Abc l, int r, Func2<int> f) { return new Abc(f(l.A, r), f(l.B, r), f(l.C, r)); }
		private static Abc BiOperation(int l, Abc r, Func2<int> f) { return new Abc(f(l, r.A), f(l, r.B), f(l, r.C)); }

		#region Implements & Overrides
		public override string ToString()
		{
			var sb = new StringBuilder(); sb.Append("[");
			sb.AppendFormat(A > 0 ? "{0,2:+0}" : "{0,2:0}", A); sb.Append(":");
			sb.AppendFormat(B > 0 ? "{0,2:+0}" : "{0,2:0}", B); sb.Append(":");
			sb.AppendFormat(C > 0 ? "{0,2:+0}" : "{0,2:0}", C); sb.Append("]");
			return sb.ToString();
		}
		public Abc Clone() { return (Abc)MemberwiseClone(); }
		object ICloneable.Clone() { return Clone(); }
		public override bool Equals(object obj) { return !ReferenceEquals(null, obj) && obj is Abc && Equals((Abc)obj); }
		public bool Equals(Abc other) { return A == other.A && B == other.B && C == other.C; }
		public override int GetHashCode() { unchecked { return (((A * 263) ^ B) * 263) ^ C; } }
		public IEnumerator<int> GetEnumerator() { yield return A; yield return B; yield return C; }
		//public IEnumerator<int> GetEnumerator() { return new Enumerator<int>(A, B, C); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
		#endregion

		#region Operators
		public static Abc operator +(Abc v) { return v; }
		public static Abc operator -(Abc v) { return UnOperation(v, a => -a); }
		public static Abc operator ~(Abc v) { return UnOperation(v, a => ~a); }

		public static Abc operator +(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a + b); }
		public static Abc operator -(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a - b); }
		public static Abc operator *(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a * b); }
		public static Abc operator /(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a / b); }
		public static Abc operator ^(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a ^ b); }
		public static Abc operator &(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a & b); }
		public static Abc operator |(Abc l, Abc r) { return BiOperation(l, r, (a, b) => a | b); }

		public static Abc operator +(Abc l, int r) { return BiOperation(l, r, (a, b) => a + b); }
		public static Abc operator -(Abc l, int r) { return BiOperation(l, r, (a, b) => a - b); }
		public static Abc operator *(Abc l, int r) { return BiOperation(l, r, (a, b) => a * b); }
		public static Abc operator /(Abc l, int r) { return BiOperation(l, r, (a, b) => a / b); }
		public static Abc operator ^(Abc l, int r) { return BiOperation(l, r, (a, b) => a ^ b); }
		public static Abc operator &(Abc l, int r) { return BiOperation(l, r, (a, b) => a & b); }
		public static Abc operator |(Abc l, int r) { return BiOperation(l, r, (a, b) => a | b); }

		public static Abc operator +(int l, Abc r) { return BiOperation(l, r, (a, b) => a + b); }
		public static Abc operator -(int l, Abc r) { return BiOperation(l, r, (a, b) => a - b); }
		public static Abc operator *(int l, Abc r) { return BiOperation(l, r, (a, b) => a * b); }
		public static Abc operator /(int l, Abc r) { return BiOperation(l, r, (a, b) => a / b); }
		public static Abc operator ^(int l, Abc r) { return BiOperation(l, r, (a, b) => a ^ b); }
		public static Abc operator &(int l, Abc r) { return BiOperation(l, r, (a, b) => a & b); }
		public static Abc operator |(int l, Abc r) { return BiOperation(l, r, (a, b) => a | b); }

		public static Abc operator >>(Abc v, int i)
		{
			switch((i %= 3) < 0 ? i + 3 : i)
			{
				case 1: return new Abc(v.C, v.A, v.B);
				case 2: return new Abc(v.B, v.C, v.A);
				default: return v;
			}
		}
		public static Abc operator <<(Abc v, int i) { return v >> -i; }

		public static bool operator ==(Abc l, Abc r) { return l.Equals(r); }
		public static bool operator !=(Abc l, Abc r) { return !l.Equals(r); }
		#endregion
	}
}
