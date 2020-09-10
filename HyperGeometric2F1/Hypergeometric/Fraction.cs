using System;
using System.Collections.Generic;
using HyperGeometric2F1.Base;
using HyperGeometric2F1.Math;

namespace HyperGeometric2F1.Hypergeometric
{
	/// <summary> Звичайний дріб, який містить чисельник і знаменник </summary>
	public struct Fraction : ICloneable<Fraction>, IEquatable<Fraction>
	{
		/// <summary> Чисельник </summary>
		public readonly Coef Numerator;
		/// <summary> Знаменник </summary>
		public readonly Coef Denominator;

		public Fraction(string s, Abc shift = default(Abc)) : this(Coef.One[shift], Coef.One[shift])
		{
			var sum = Expression.SplitSum(s);
			if(sum != null && sum.Link == null) {
				//if(sum.Data.Key == '-') Numerator = -Coef.One;
				foreach(var pair in (IEnumerable<KeyValuePair<char, string>>)Expression.SplitProduct(s))
					switch(pair.Key) {
						case '*': Numerator *= new Coef(pair.Value, shift); break;
						case '/': Denominator *= new Coef(pair.Value, shift); break;
					}
			}
			else Numerator = new Coef(s, shift);
		}
		public Fraction(Coef c, Abc shift = default(Abc)) : this(c.StrFunc, shift) { }
		public Fraction(Coef numerator, Coef denominator) { Numerator = numerator; Denominator = denominator; }

		public Func<ComplexDouble> Calc(ComplexDouble a, ComplexDouble b, ComplexDouble c)
		{
			var copy = this;
			return z => copy.Calc(a, b, c, z);
		}
		public ComplexDouble Calc(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z/* = new ComplexDouble()*/)
		{
			Coef num = Numerator, den = Denominator;
			return num.Calc(a, b, c, z) / den.Calc(a, b, c, z);
		}
		public Fraction Simplify() { return new Fraction(ToCoef().Simplify()); }
		public Fraction this[Abc shift] { get { return new Fraction(Numerator[shift], Denominator[shift]); } }
		public Fraction this[int a, int b, int c] { get { return this[new Abc(a, b, c)]; } }

		#region Implements & Overrides
		public Fraction Reverse() { return new Fraction(Denominator, Numerator); }
		public Coef ToCoef() { return Numerator / Denominator; }
		public override string ToString() { return ToCoef().ToString(); }
		public Fraction Clone() { return (Fraction)MemberwiseClone(); }
		object ICloneable.Clone() { return Clone(); }
		public override bool Equals(object obj) { return !ReferenceEquals(null, obj) && obj is Fraction && Equals((Fraction)obj); }
		public bool Equals(Fraction other) { return base.Equals(other) || (Numerator == other.Numerator && Denominator == other.Denominator); }
		public override int GetHashCode() { unchecked { return (Numerator.GetHashCode() * 46337) ^ Denominator.GetHashCode(); } }
		#endregion

		#region Operators
		public static Fraction operator +(Fraction v) { return v; }
		public static Fraction operator -(Fraction v) { return new Fraction(-v.Numerator, v.Denominator); }
		public static Fraction operator ~(Fraction v) { return v.Reverse(); }

		public static Fraction operator +(Fraction l, Fraction r) { return new Fraction(l.Numerator * r.Denominator + r.Numerator * l.Denominator, l.Denominator * r.Denominator); }
		public static Fraction operator -(Fraction l, Fraction r) { return new Fraction(l.Numerator * r.Denominator - r.Numerator * l.Denominator, l.Denominator * r.Denominator); }
		public static Fraction operator *(Fraction l, Fraction r)
		{
			Coef num = Coef.One, den = Coef.One;
			if(l.Numerator != r.Denominator) { num *= l.Numerator; den *= r.Denominator; }
			if(r.Numerator != l.Denominator) { num *= r.Numerator; den *= l.Denominator; }
			return new Fraction(num, den);
			//bool lnrd = l.Numerator == r.Denominator, rnld = r.Numerator == l.Denominator;
			//return new Fraction((lnrd ? Coef.One : l.Numerator) * (rnld ? Coef.One : r.Numerator), (rnld ? Coef.One : l.Denominator) * (lnrd ? Coef.One : r.Denominator));
			//return new Fraction(l.Numerator * r.Numerator, l.Denominator * r.Denominator);
		}
		public static Fraction operator /(Fraction l, Fraction r) { return l * r.Reverse(); }

		public static explicit operator Coef(Fraction v) { return v.ToCoef(); }
		public static implicit operator Fraction(Coef v) { return new Fraction(v); }

		public static bool operator ==(Fraction l, Fraction r) { return l.Equals(r); }
		public static bool operator !=(Fraction l, Fraction r) { return !l.Equals(r); }
		#endregion
	}
}
