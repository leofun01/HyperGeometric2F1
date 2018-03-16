using System;
using HyperGeoMetric2F1.Base;
using HyperGeoMetric2F1.Linker;

namespace HyperGeoMetric2F1.Math
{
	public static class Functions
	{
		public static bool ThrowExceptions = true;
		private const double Eps = 1e-16d; // 1e-15d;
		//
		public static ComplexDouble HyperSum2F1(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z, double eps = Eps, bool thrw = true) { return HyperSum2F1(a, b, c, z, d => d.SqrAbs > eps * eps, thrw); }
		public static ComplexDouble HyperSum2F1(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z, int n, bool thrw = true) { return HyperSum2F1(a, b, c, z, d => n-- > 0, thrw); }
		public static ComplexDouble HyperSum2F1(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z, Predicate<ComplexDouble> pred, bool thrw = true)
		{
			if(ComplexDouble.IsNaN(a))
				if(thrw) throw new ArgumentOutOfRangeException("a", "a is NaN");
				else return a;
			if(ComplexDouble.IsNaN(b))
				if(thrw) throw new ArgumentOutOfRangeException("b", "b is NaN");
				else return b;
			if(ComplexDouble.IsNaN(c))
				if(thrw) throw new ArgumentOutOfRangeException("c", "c is NaN");
				else return c;
			if(ComplexDouble.IsNaN(z))
				if(thrw) throw new ArgumentOutOfRangeException("z", "z is NaN");
				else return z;
			if(c.Im.Equals(0d) && c.Re <= 0d && (c.Re % 1d).Equals(0d))
				if(thrw) throw new ArgumentOutOfRangeException("c", "c is 0 or negative integer");
				else return double.PositiveInfinity;
			double i = z.SqrAbs;
			if(i > 1d || i.Equals(1d) && (c - a - b).Re <= 0d)
				if(thrw) throw new ArgumentOutOfRangeException("z", "|z| >= 1");
				else return double.PositiveInfinity;
			i = 0d;
			ComplexDouble prod = 1d, sum = 0d;
			while(pred(prod)) sum += prod *= (z * a++ * b++) / (++i * c++);
			return sum + 1d;
		}
		public static ComplexDouble TryAnyHyperSum2F1(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z, Predicate<ComplexDouble> pred, bool thrw = true)
		{
			ComplexDouble r = double.NaN;
			try { r = HyperSum2F1(a, b, c, z, pred); }
			catch(ArgumentOutOfRangeException) { }
			return r;
		}
		public static MonoStack<ComplexDouble> SeriesHyperSum2F1(ComplexDouble a, ComplexDouble b, ComplexDouble c, ComplexDouble z, Predicate<ComplexDouble> pred, bool reverse = false)
		{
			MonoStack<ComplexDouble> mono = null;
			HyperSum2F1(a, b, c, z, d => { mono = new MonoStack<ComplexDouble>(d, mono); return pred(d); });
			return reverse ? mono : mono.Reverse();
		}
		//
		public static double Pow(double a, int n) { double r = 1d; while(--n >= 0) r *= a; while(++n < 0) r /= a; return r; }
		public static ComplexDouble Pow(ComplexDouble a, int n) { ComplexDouble r = 1d; while(--n >= 0) r *= a; while(++n < 0) r /= a; return r; }
		//
		#region Gamma & Pochhammer
		private static long _gamma(int n) { long r = 1L; while(--n > 1L) r *= n; return r; }
		public static long Factorial(int n) {
			if(n < 0)
				if(ThrowExceptions) throw new ArgumentOutOfRangeException("n", "n < " + 0 + " => n! = Infinity");
				else return long.MinValue;
			if(n > 20)
				if(ThrowExceptions) throw new ArgumentOutOfRangeException("n", "n > " + 20 + " => n! > " + long.MaxValue);
				else return long.MaxValue;
			/*if(ThrowExceptions) {
				if(n < 0) throw new ArgumentOutOfRangeException("n", "n < " + 0 + " => n! = Infinity");
				if(n > 20) throw new ArgumentOutOfRangeException("n", "n > " + 20 + " => n! > " + long.MaxValue);
			}//*/
			return _gamma(++n);
		}
		public static long Gamma(int n) {
			if(n < 1)
				if(ThrowExceptions) throw new ArgumentOutOfRangeException("n", "n < " + 1 + " => Gamma(n) = Infinity");
				else return long.MinValue;
			if(n > 21)
				if(ThrowExceptions) throw new ArgumentOutOfRangeException("n", "n > " + 21 + " => Gamma(n) > " + long.MaxValue);
				else return long.MaxValue;
			/*if(ThrowExceptions) {
				if(n < 1) throw new ArgumentOutOfRangeException("n", "n < " + 1 + " => Gamma(n) = Infinity");
				if(n > 21) throw new ArgumentOutOfRangeException("n", "n > " + 21 + " => Gamma(n) > " + long.MaxValue);
			}//*/
			return _gamma(n);
		}
		public static double Pochhammer(double a, int n) { double r = 1d; while(--n >= 0) r *= a + n; while(++n < 0) r /= a + n; return r; }
		public static ComplexDouble Pochhammer(ComplexDouble a, int n) { ComplexDouble r = 1d; while(--n >= 0) r *= a + n; while(++n < 0) r /= a + n; return r; }
		//
		/// <summary><param name="n"><remarks> if n &gt; 171.62437695630272 then return Infinity. </remarks></param></summary>
		public static double SlowGamma(double n, double eps = Eps)
		{
			if(double.IsNaN(n))
				if(ThrowExceptions) throw new ArgumentException("n is NaN", "n");
				else return double.NaN;
			if(double.IsPositiveInfinity(n)) return double.PositiveInfinity;
			if(double.IsNegativeInfinity(n)) return double.NaN;
			double q = 171.62437695630272d;
			if(n > q)
				if(ThrowExceptions) throw new ArgumentOutOfRangeException("n", "n > " + q + " => Gamma(n) > " + double.MaxValue);
				else return double.PositiveInfinity;
			q = n % 1d;
			if(q < 0d) ++q;
			var z = (int)(n - q);
			n = q + q + 1d;
			Func<double> f = t => System.Math.Exp(n * System.Math.Log(t) - t * t);
			return (q.Equals(0d) || q.Equals(1d) ? 1d : q.Equals(0.5d) ? ComplexDouble.SqrtPi / 2d : 2d * (
				Integrate(f, 0d, 1d, 0d, 1d / ComplexDouble.E, eps) +
				Integrate(f, 1d, 26.641747557046328d, 1d / ComplexDouble.E, 0d, eps)
				)) * Pochhammer(++q, --z);
		}
		public static double FastGamma(double n)
		{
			if(double.IsNaN(n))
				if(ThrowExceptions) throw new ArgumentException("n is NaN", "n");
				else return double.NaN;
			if(double.IsPositiveInfinity(n)) return double.PositiveInfinity;
			if(double.IsNegativeInfinity(n)) return double.NaN;
			double q = 171.62437695630272d;
			if(n > q)
				if(ThrowExceptions) throw new ArgumentOutOfRangeException("n", "n > " + q + " => Gamma(n) > " + double.MaxValue);
				else return double.PositiveInfinity;
			q = n % 1d;
			if(q < 0d) ++q;
			var z = (int)(n - q);
			n = q + 8d;
			return q.Equals(0d) || q.Equals(1d) ? Pochhammer(q + 1d, z - 1) :
				q.Equals(0.5d) ? 0.5d * ComplexDouble.SqrtPi * Pochhammer(q + 1d, z - 1) :
				System.Math.Exp(
				0.5d * System.Math.Log(2d * ComplexDouble.Pi)
				+ (n - 0.5d) * System.Math.Log(n) - n
				+ Pow(n, -1) / 12d
				- Pow(n, -3) / 360d
				+ Pow(n, -5) / 1260d
				- Pow(n, -7) / 1680d
				+ Pow(n, -9) / 1188d
				- Pow(n, -11) / 360360d * 691d
				+ Pow(n, -13) / 156d
				) * Pochhammer(n, z - 8);
		}
		public static ComplexDouble SlowGamma(ComplexDouble n, double eps = Eps)
		{
			if(ComplexDouble.IsNaN(n))
				if(ThrowExceptions) throw new ArgumentException("n is NaN", "n");
				else return double.NaN;
			if(n.Im.Equals(0d)) return SlowGamma(n.Re, eps);
			if(ComplexDouble.IsInfinity(n)) return 0d;
			var q = new ComplexDouble(n.Re % 1d, n.Im);
			if(q.Re < 0d) ++q;
			var z = (int)(n.Re - q.Re);
			n = new ComplexDouble(q.Re + q.Re + 1d, q.Im + q.Im);
			Func<double, ComplexDouble> f = t => ComplexDouble.Exp(n * System.Math.Log(t) - t * t);
			return 2d * (
				IntegrateComplex(f, 0d, 1d, 0d, 1d / ComplexDouble.E, eps) +
				IntegrateComplex(f, 1d, 26.641747557046328d, 1d / ComplexDouble.E, 0d, eps)
				) * Pochhammer(++q, --z);
		}
		public static ComplexDouble FastGamma(ComplexDouble n)
		{
			if(ComplexDouble.IsNaN(n))
				if(ThrowExceptions) throw new ArgumentException("n is NaN", "n");
				else return double.NaN;
			if(n.Im.Equals(0d)) return FastGamma(n.Re);
			if(ComplexDouble.IsInfinity(n)) return 0d;
			var q = new ComplexDouble(n.Re % 1d, n.Im);
			if(q.Re < 0d) ++q;
			var z = (int)(n.Re - q.Re);
			n = q + 8d;
			return q.Equals(0d) || q.Equals(1d) ? Pochhammer(q + 1d, z - 1) :
				q.Equals(0.5d) ? 0.5d * ComplexDouble.SqrtPi * Pochhammer(q + 1d, z - 1) :
				ComplexDouble.Exp(
				0.5d * System.Math.Log(2d * ComplexDouble.Pi)
				+ (n - 0.5d) * ComplexDouble.Log(n) - n
				+ Pow(n, -1) / 12d
				- Pow(n, -3) / 360d
				+ Pow(n, -5) / 1260d
				- Pow(n, -7) / 1680d
				+ Pow(n, -9) / 1188d
				- Pow(n, -11) / 360360d * 691d
				+ Pow(n, -13) / 156d
				) * Pochhammer(n, z - 8);
			/*n = q + 17d;
			return
				ComplexDouble.Pow(2d * ComplexDouble.Pi * n, 0.5d) * ComplexDouble.Pow(n, n - 1d)
				* ComplexDouble.Exp(-n + 1d / (12d * n) - 1d / (360d * ComplexDouble.Pow(n, 3d))
				+ 1d / (1260d * ComplexDouble.Pow(n, 5d)) - 1d / (1686d * ComplexDouble.Pow(n, 7d)))
				* Pochhammer(n, z - 17);//*/
		}
		//
		public static double FastPochhammer(double a, double n) { return FastGamma(a + n) / FastGamma(a); }
		public static ComplexDouble FastPochhammer(ComplexDouble a, ComplexDouble n) { return FastGamma(a + n) / FastGamma(a); }
		#endregion
		//
		#region Integrate
		public static double Integrate(Func<double> f, double a, double b, double eps = Eps) { return Integrate(f, a, b, f(a), f(b), eps); }
		public static double Integrate(Func<double> f, double a, double b, double fa, double fb, double eps = Eps) { return Integrate(f, a, b, fa, fb, (l, r) => 0.5d * (l + r), eps); }
		public static double Integrate(Func<double> f, double a, double b, double fa, double fb, Func2<double> nextPoint, double eps = Eps)
		{
			bool ia = ComplexDouble.IsInfinity(fa) || ComplexDouble.IsNaN(fa),
				ib = ComplexDouble.IsInfinity(fb) || ComplexDouble.IsNaN(fb);
			if((ia || ib) && eps <= Eps) return 0d;
			double p = nextPoint(a, b);
			double fp = f(p),
				ab = 0.5d * (b - a) * (fa + fb),
				ap = 0.5d * (p - a) * (fa + fp),
				pb = 0.5d * (b - p) * (fp + fb),
				s = ap + pb;
			if(System.Math.Abs((ab - s) / (b - a)) <= eps) return s;
			return Integrate(f, a, p, fa, fp, nextPoint, eps)
				 + Integrate(f, p, b, fp, fb, nextPoint, eps);//*/
		}
		public static ComplexDouble IntegrateComplex(Func<double, ComplexDouble> f, double a, double b, double eps = Eps) { return IntegrateComplex(f, a, b, f(a), f(b), eps); }
		public static ComplexDouble IntegrateComplex(Func<double, ComplexDouble> f, double a, double b, ComplexDouble fa, ComplexDouble fb, double eps = Eps) { return IntegrateComplex(f, a, b, fa, fb, (l, r) => 0.5d * (l + r), eps); }
		public static ComplexDouble IntegrateComplex(Func<double, ComplexDouble> f, double a, double b, ComplexDouble fa, ComplexDouble fb, Func2<double> nextPoint, double eps = Eps)
		{
			bool ia = ComplexDouble.IsInfinity(fa) || ComplexDouble.IsNaN(fa),
				ib = ComplexDouble.IsInfinity(fb) || ComplexDouble.IsNaN(fb);
			if((ia || ib) && eps <= Eps) return 0d;
			double p = nextPoint(a, b);
			ComplexDouble fp = f(p),
				ab = 0.5d * (b - a) * (fa + fb),
				ap = 0.5d * (p - a) * (fa + fp),
				pb = 0.5d * (b - p) * (fp + fb),
				s = ap + pb;
			if((ab - s).SqrAbs <= eps * eps * (b - a) * (b - a)) return s;
			return IntegrateComplex(f, a, p, fa, fp, nextPoint, eps)
				 + IntegrateComplex(f, p, b, fp, fb, nextPoint, eps);//*/
		}
		#endregion
	}
}
