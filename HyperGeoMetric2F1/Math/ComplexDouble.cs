
namespace HyperGeoMetric2F1.Math
{
	/// <summary> Комплексне число (z=x+i*y), кожна складова (x,y) якого є double
	/// (число подвійної точності з плаваючою точкою). </summary>
	[System.Serializable, System.CLSCompliant(true)]
	public struct ComplexDouble : System.IEquatable<ComplexDouble>,
		System.IEquatable<double>, System.IFormattable,
		System.Collections.Generic.IEqualityComparer<ComplexDouble>
	{
		#region Константи
		/// <summary> π=3.141592653589793238462643383279502884197. </summary>
		public const double Pi = 3.141592653589793238462643383279502884197d;
		/// <summary> √π=1.772453850905516027298167483341145182798. </summary>
		public const double SqrtPi = 1.772453850905516027298167483341145182798d;
		/// <summary> e=2.718281828459045235360287471352662497757. </summary>
		public const double E = 2.718281828459045235360287471352662497757d;
		/// <summary> √e=1.648721270700128146848650787814163571654. </summary>
		public const double SqrtE = 1.648721270700128146848650787814163571654d;
		/// <summary> √2=1.41421356237309504880168872420969807857. </summary>
		public const double Sqrt2 = 1.41421356237309504880168872420969807857d;
		/// <summary> √3=1.732050807568877293527446341505872366943. </summary>
		public const double Sqrt3 = 1.732050807568877293527446341505872366943d;
		/// <summary> √5=2.236067977499789696409173668731276235441. </summary>
		public const double Sqrt5 = 2.236067977499789696409173668731276235441d;
		/// <summary> Ln2=0.6931471805599453094172321214581765680755. </summary>
		public const double Log2 = 0.6931471805599453094172321214581765680755d;
		/// <summary> φ=1.61803398874989484820458683436563811772. </summary>
		public const double GoldenRatio = 1.61803398874989484820458683436563811772d;
		/// <summary> Euler-Mascheroni constant γ=0.5772156649015328606065120900824024310422. </summary>
		public const double EulerGamma = 0.5772156649015328606065120900824024310422d;
		/// <summary> C=0.9159655941772190150546035149323841107741. </summary>
		public const double Catalan = 0.9159655941772190150546035149323841107741d;
		/// <summary> K=2.68545200106530644530971483548179569382. </summary>
		public const double Khinchin = 2.68545200106530644530971483548179569382d;
		/// <summary> A=1.282427129100622636875342568869791727768. </summary>
		public const double Glaisher = 1.282427129100622636875342568869791727768d;
		/// <summary> Apery's constant = 1.202056903159594285399738161511449990765. </summary>
		public const double Apery = 1.202056903159594285399738161511449990765d;
		/// <summary> Mertens constant = 0.261497212847642783755426838608695859052. </summary>
		public const double Mertens = 0.261497212847642783755426838608695859052d;
		/// <summary> Twin prime constant = 0.6601618158468695739278121100145557784326. </summary>
		public const double TwinPrime = 0.6601618158468695739278121100145557784326d;
		/// <summary> Gompertz's constant = 0.59634736232319407434107849936927937607418. </summary>
		public const double Gompertz = 0.5963473623231940743410784993692793760742d;
		#endregion

		/// <summary> Корінь квадратний від -1 (±i=√-1, i*i=-1). </summary>
		public static readonly ComplexDouble I = new ComplexDouble(0d, 1d);
		/// <summary> Дійсна складова (x) комплексного числа. </summary>
		public double Re { get; set; }
		/// <summary> Уявна складова (y) комплексного числа. </summary>
		public double Im { get; set; }
		/// <summary> Модуль (√(x*x+y*y), абсолютне значення) комплексного числа. </summary>
		public double Abs
		{
			get { return System.Math.Sqrt(SqrAbs); }
			set { this *= value / Abs; }
		}
		/// <summary> Квадрат модуля (x*x+y*y) комплексного числа. </summary>
		public double SqrAbs
		{
			get { return Re * Re + Im * Im; }
			set { this *= System.Math.Sqrt(value / SqrAbs); }
		}
		/// <summary> Головне значення аргументу (-π,+π] (кут, фаза) комплексного числа. </summary>
		public double Arg
		{
			get { return System.Math.Atan2(Im, Re); }
			set
			{
				var abs = Abs;
				Re = abs * System.Math.Cos(value);
				Im = abs * System.Math.Sin(value);
			}
		}
		/// <summary> Добуток i на дане число. </summary>
		public ComplexDouble MultipleI { get { return new ComplexDouble(-Im, Re); } }
		/// <summary> Добуток -i на дане число. </summary>
		public ComplexDouble DivideI { get { return new ComplexDouble(Im, -Re); } }

		/// <param name="x"> Дійсна складова (x) комплексного числа. </param>
		/// <param name="y"> Уявна складова (y) комплексного числа. </param>
		public ComplexDouble(double x, double y) : this() { Re = x; Im = y; }
		/// <summary> Перетворює<returns> число з полярної системи в декартову (звичайну). </returns></summary>
		public static ComplexDouble FromPolar(double abs, double arg) { return new ComplexDouble(1d, 0d) { Abs = abs, Arg = arg }; }
		public static bool IsInfinity(ComplexDouble z) { return double.IsInfinity(z.Re) || double.IsInfinity(z.Im); }
		public static bool IsNaN(ComplexDouble z) { return double.IsNaN(z.Re) || double.IsNaN(z.Im); }

		#region Методи предків System.[Object, IFormattable, IEquatable]
		public override string ToString() { return Re + (Im < 0 ? @" " : @" +") + Im + @"*i"; }
		public string ToString(string format) { return Re.ToString(format) + (Im < 0 ? @" " : @" +") + Im.ToString(format) + @"*i"; }
		public string ToString(System.IFormatProvider formatProvider) { return Re.ToString(formatProvider) + (Im < 0 ? @" " : @" +") + Im.ToString(formatProvider) + @"*i"; }
		public string ToString(string format, System.IFormatProvider formatProvider) { return Re.ToString(format, formatProvider) + (Im < 0 ? @" " : @" +") + Im.ToString(format, formatProvider) + @"*i"; }
		/// <summary> Визначає чи рівні два числа. </summary>
		/// <param name="other"> Комплексне число, яке порівнюється з даним. </param>
		/// <returns> true - рівні, false - не рівні. </returns>
		public bool Equals(ComplexDouble other) { return Re.Equals(other.Re) && Im.Equals(other.Im); }
		/// <summary> Визначає чи рівні два числа. </summary>
		/// <param name="other"> Число, яке порівнюється з даним. </param>
		/// <returns> true - рівні, false - не рівні. </returns>
		public bool Equals(double other) { return Re.Equals(other) && Im.Equals(0d); }
		/// <summary> Порівнює даний екземпляр з <see cref="obj"/>. </summary>
		public override bool Equals(object obj) { return obj is ComplexDouble && Equals((ComplexDouble)obj) || obj is double && Equals((double)obj); }
		public override int GetHashCode() { return Re.GetHashCode() ^ (Im.GetHashCode() >> 16); }
		public bool Equals(ComplexDouble x, ComplexDouble y) { return x.Equals(y); }
		public int GetHashCode(ComplexDouble obj) { return obj.GetHashCode(); }
		#endregion

		#region Тригонометричні функції
		public static ComplexDouble Exp(ComplexDouble z) { return FromPolar(System.Math.Exp(z.Re), z.Im); }
		public static ComplexDouble Log(ComplexDouble z, int k = 0) { return new ComplexDouble(System.Math.Log(z.SqrAbs) / 2d, z.Arg + 2d * k * Pi); }
		public static ComplexDouble Log(ComplexDouble z, ComplexDouble a, int kz = 0, int ka = 0) { return Log(z, kz) / Log(a, ka); }
		public static ComplexDouble Pow(ComplexDouble z, double n) { return FromPolar(System.Math.Pow(z.SqrAbs, n / 2d), z.Arg * n); }
		public static ComplexDouble Pow(ComplexDouble z, ComplexDouble n, int k = 0) { return Exp(n * Log(z, k)); }
		public static ComplexDouble Sqrt(ComplexDouble z, int k = 0) { return Pow(z, 0.5d, k); }
		//
		public static ComplexDouble Sin(ComplexDouble z) { return Sinh(z.MultipleI).DivideI; }
		public static ComplexDouble Cos(ComplexDouble z) { return Cosh(z.MultipleI); }
		public static ComplexDouble Tan(ComplexDouble z) { return Tanh(z.MultipleI).DivideI; }
		public static ComplexDouble Cot(ComplexDouble z) { return Coth(z.MultipleI).MultipleI; }
		public static ComplexDouble Sec(ComplexDouble z) { return Sech(z.MultipleI); }
		public static ComplexDouble Csc(ComplexDouble z) { return Csch(z.MultipleI).MultipleI; }
		public static ComplexDouble Sinh(ComplexDouble z) { return Sinh2(z) / 2d; }
		public static ComplexDouble Sinh2(ComplexDouble z) { return (Exp(z) - Exp(-z)); }
		public static ComplexDouble Cosh(ComplexDouble z) { return Cosh2(z) / 2d; }
		public static ComplexDouble Cosh2(ComplexDouble z) { return (Exp(z) + Exp(-z)); }
		public static ComplexDouble Tanh(ComplexDouble z) { return Sinh2(z) / Cosh2(z); }
		public static ComplexDouble Coth(ComplexDouble z) { return Cosh2(z) / Sinh2(z); }
		public static ComplexDouble Sech(ComplexDouble z) { return 2d / Cosh2(z); }
		public static ComplexDouble Csch(ComplexDouble z) { return 2d / Sinh2(z); }
		//
		public static ComplexDouble Asin(ComplexDouble z, int kLog, int kSqrt) { return Asinh(z.MultipleI, kLog, kSqrt).DivideI; }
		public static ComplexDouble Acos(ComplexDouble z, int kLog, int kSqrt) { return Acosh(z, kLog, kSqrt).DivideI; }
		public static ComplexDouble Atan(ComplexDouble z, int kLog) { return Atanh(z.DivideI, kLog).MultipleI; }
		public static ComplexDouble Acot(ComplexDouble z, int kLog) { return Atan(1d / z, kLog); }
		public static ComplexDouble Asec(ComplexDouble z, int kLog, int kSqrt) { return Acos(1d / z, kLog, kSqrt); }
		public static ComplexDouble Acsc(ComplexDouble z, int kLog, int kSqrt) { return Asin(1d / z, kLog, kSqrt); }
		public static ComplexDouble Asinh(ComplexDouble z, int kLog, int kSqrt) { return Log(z + Sqrt(z * z + 1d, kSqrt), kLog); }
		public static ComplexDouble Acosh(ComplexDouble z, int kLog, int kSqrt) { return Log(z + Sqrt(z * z - 1d, kSqrt), kLog); }
		public static ComplexDouble Atanh(ComplexDouble z, int kLog) { return Log((1d + z) / (1d - z), kLog) / 2d; }
		public static ComplexDouble Acoth(ComplexDouble z, int kLog) { return Atanh(1d / z, kLog); }
		public static ComplexDouble Asech(ComplexDouble z, int kLog, int kSqrt) { return Acosh(1d / z, kLog, kSqrt); }
		public static ComplexDouble Acsch(ComplexDouble z, int kLog, int kSqrt) { return Asinh(1d / z, kLog, kSqrt); }
		#endregion

		#region Оператори
		/// <summary><returns> Спряжене комплексне число. </returns></summary>
		public static ComplexDouble operator ~(ComplexDouble z) { return new ComplexDouble(z.Re, -z.Im); }
		/*/// <summary><returns> Спряжене комплексне число. </returns></summary>
		public static ComplexDouble operator !(ComplexDouble z) { return ~z; }//*/
		public static ComplexDouble operator ++(ComplexDouble z) { ++z.Re; return z; }
		public static ComplexDouble operator --(ComplexDouble z) { --z.Re; return z; }
		public static ComplexDouble operator +(ComplexDouble z) { return z; }
		public static ComplexDouble operator -(ComplexDouble z) { return new ComplexDouble(-z.Re, -z.Im); }
		public static ComplexDouble operator +(ComplexDouble z1, ComplexDouble z2) { return new ComplexDouble(z1.Re + z2.Re, z1.Im + z2.Im); }
		public static ComplexDouble operator -(ComplexDouble z1, ComplexDouble z2) { return new ComplexDouble(z1.Re - z2.Re, z1.Im - z2.Im); }
		public static ComplexDouble operator *(ComplexDouble z1, ComplexDouble z2) { return new ComplexDouble(z1.Re * z2.Re - z1.Im * z2.Im, z1.Re * z2.Im + z1.Im * z2.Re); }
		public static ComplexDouble operator /(ComplexDouble z1, ComplexDouble z2) { return ~z2 * z1 / z2.SqrAbs; }
		//public static ComplexDouble operator %(ComplexDouble z1, ComplexDouble z2) { return new ComplexDouble(z1.Re % z2.Re, z1.Im % z2.Im); }
		public static bool operator ==(ComplexDouble z1, ComplexDouble z2) { return z1.Equals(z2); }
		public static bool operator !=(ComplexDouble z1, ComplexDouble z2) { return !z1.Equals(z2); }
		//
		public static ComplexDouble operator +(ComplexDouble z1, double d2) { return new ComplexDouble(z1.Re + d2, z1.Im); }
		public static ComplexDouble operator -(ComplexDouble z1, double d2) { return new ComplexDouble(z1.Re - d2, z1.Im); }
		public static ComplexDouble operator *(ComplexDouble z1, double d2) { return new ComplexDouble(z1.Re * d2, z1.Im * d2); }
		public static ComplexDouble operator /(ComplexDouble z1, double d2) { return new ComplexDouble(z1.Re / d2, z1.Im / d2); }
		//public static ComplexDouble operator %(ComplexDouble z1, double d2) { return new ComplexDouble(z1.Re % d2, z1.Im % d2); }
		public static bool operator ==(ComplexDouble z1, double d2) { return z1.Equals(d2); }
		public static bool operator !=(ComplexDouble z1, double d2) { return !z1.Equals(d2); }
		//
		public static ComplexDouble operator +(double d1, ComplexDouble z2) { return z2 + d1; }
		public static ComplexDouble operator -(double d1, ComplexDouble z2) { return new ComplexDouble(d1 - z2.Re, -z2.Im); }
		public static ComplexDouble operator *(double d1, ComplexDouble z2) { return z2 * d1; }
		public static ComplexDouble operator /(double d1, ComplexDouble z2) { return ~z2 * (d1 / z2.SqrAbs); }
		public static bool operator ==(double d1, ComplexDouble z2) { return z2.Equals(d1); }
		public static bool operator !=(double d1, ComplexDouble z2) { return !z2.Equals(d1); }
		//
		public static implicit operator ComplexDouble(double d) { return new ComplexDouble(d, 0d); }
		public static explicit operator ComplexDouble(decimal d) { return new ComplexDouble((double)d, 0d); }
		//public static explicit operator ComplexDouble(ComplexDecimal cd) { return new ComplexDouble((double)cd.Re, (double)cd.Im); }
		#endregion
	}
}

// (±™©‰®π¬°№§√)
