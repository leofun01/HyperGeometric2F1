
namespace HyperGeometric2F1.Math
{
	/// <summary> Комплексне число (z=x+i*y), кожна складова (x,y) якого є decimal. </summary>
	[System.Serializable, System.CLSCompliant(true)]
	public struct ComplexDecimal : System.IEquatable<ComplexDecimal>, System.IFormattable
	{
		#region Константи
		/// <summary> π=3.141592653589793238462643383279502884197. </summary>
		public const decimal Pi = 3.141592653589793238462643383279502884197m;
		/// <summary> √π=1.772453850905516027298167483341145182798. </summary>
		public const decimal SqrtPi = 1.772453850905516027298167483341145182798m;
		/// <summary> e=2.718281828459045235360287471352662497757. </summary>
		public const decimal E = 2.718281828459045235360287471352662497757m;
		/// <summary> √e=1.648721270700128146848650787814163571654. </summary>
		public const decimal SqrtE = 1.648721270700128146848650787814163571654m;
		/// <summary> √2=1.41421356237309504880168872420969807857. </summary>
		public const decimal Sqrt2 = 1.41421356237309504880168872420969807857m;
		/// <summary> √3=1.732050807568877293527446341505872366943. </summary>
		public const decimal Sqrt3 = 1.732050807568877293527446341505872366943m;
		/// <summary> √5=2.236067977499789696409173668731276235441. </summary>
		public const decimal Sqrt5 = 2.236067977499789696409173668731276235441m;
		/// <summary> Ln2=0.6931471805599453094172321214581765680755. </summary>
		public const decimal Log2 = 0.6931471805599453094172321214581765680755m;
		/// <summary> φ=1.61803398874989484820458683436563811772. </summary>
		public const decimal GoldenRatio = 1.61803398874989484820458683436563811772m;
		/// <summary> Euler-Mascheroni constant γ=0.5772156649015328606065120900824024310422. </summary>
		public const decimal EulerGamma = 0.5772156649015328606065120900824024310422m;
		/// <summary> C=0.9159655941772190150546035149323841107741. </summary>
		public const decimal Catalan = 0.9159655941772190150546035149323841107741m;
		/// <summary> K=2.68545200106530644530971483548179569382. </summary>
		public const decimal Khinchin = 2.68545200106530644530971483548179569382m;
		/// <summary> A=1.282427129100622636875342568869791727768. </summary>
		public const decimal Glaisher = 1.282427129100622636875342568869791727768m;
		/// <summary> Apery's constant = 1.202056903159594285399738161511449990765. </summary>
		public const decimal Apery = 1.202056903159594285399738161511449990765m;
		/// <summary> Mertens constant = 0.261497212847642783755426838608695859052. </summary>
		public const decimal Mertens = 0.261497212847642783755426838608695859052m;
		/// <summary> Twin prime constant = 0.6601618158468695739278121100145557784326. </summary>
		public const decimal TwinPrime = 0.6601618158468695739278121100145557784326m;
		#endregion

		/// <summary> Корінь квадратний від -1 (±i=√-1, i*i=-1). </summary>
		public static readonly ComplexDecimal I = new ComplexDecimal(0m, 1m);
		/// <summary> Дійсна складова (x) комплексного числа. </summary>
		public decimal Re { get; set; }
		/// <summary> Уявна складова (y) комплексного числа. </summary>
		public decimal Im { get; set; }
		/// <summary> Модуль (√(x*x+y*y), абсолютне значення) комплексного числа. </summary>
		public decimal Abs
		{
			get { return (decimal)System.Math.Sqrt((double)SqrAbs); }
			set { this *= value / Abs; }
		}
		/// <summary> Квадрат модуля (x*x+y*y) комплексного числа. </summary>
		public decimal SqrAbs
		{
			get { return Re * Re + Im * Im; }
			set { this *= (decimal)System.Math.Sqrt((double)(value / SqrAbs)); }
		}
		/// <summary> Головне значення аргументу (-π,+π] (кут, фаза) комплексного числа. </summary>
		public decimal Arg
		{
			get { return (decimal)System.Math.Atan2((double)Im, (double)Re); }
			set
			{
				var abs = Abs;
				Re = abs * (decimal)System.Math.Cos((double)value);
				Im = abs * (decimal)System.Math.Sin((double)value);
			}
		}
		/// <summary> Добуток i на дане число. </summary>
		public ComplexDecimal MultipleI { get { return new ComplexDecimal(-Im, Re); } }
		/// <summary> Добуток -i на дане число. </summary>
		public ComplexDecimal DivideI { get { return new ComplexDecimal(Im, -Re); } }

		/// <param name="x"> Дійсна складова (x) комплексного числа. </param>
		/// <param name="y"> Уявна складова (y) комплексного числа. </param>
		public ComplexDecimal(decimal x, decimal y) : this() { Re = x; Im = y; }
		/// <summary> Перетворює<returns> число з полярної системи в декартову (звичайну). </returns></summary>
		public static ComplexDecimal FromPolar(decimal abs, decimal arg) { return new ComplexDecimal { Abs = abs, Arg = arg }; }

		#region Методи предків System.[Object, IFormattable, IEquatable<ComplexDecimal>]
		/// <summary> Визначає чи рівні два числа. </summary>
		/// <param name="other"> Комплексне число, яке порівнюється з даним. </param>
		/// <returns> true - рівні, false - не рівні. </returns>
		public bool Equals(ComplexDecimal other) { return Re.Equals(other.Re) && Im.Equals(other.Im); }
		/// <summary> Порівнює даний екземпляр з <see cref="obj"/>. </summary>
		public override bool Equals(object obj) { return obj is ComplexDecimal && Equals((ComplexDecimal)obj) || obj is decimal && Equals((decimal)obj); }
		public override int GetHashCode() { return Re.GetHashCode() ^ (Im.GetHashCode() >> 16); }
		public override string ToString() { return Re + (Im < 0 ? " " : " +") + Im + 'i'; }
		public string ToString(string format, System.IFormatProvider formatProvider) { return Re.ToString(format, formatProvider) + (Im < 0 ? " " : " +") + Im.ToString(format, formatProvider) + 'i'; }
		#endregion

		#region Тригонометричні функції
		//delete  me  ComplexDecimal Exp(ComplexDecimal z) { return System.Math.Exp(z.Re) * (new ComplexDecimal(System.Math.Cos(z.Im), System.Math.Sin(z.Im))); }
		public static ComplexDecimal Exp(ComplexDecimal z) { return FromPolar((decimal)System.Math.Exp((double)z.Re), z.Im); }
		//public static ComplexDecimal Log(ComplexDecimal z) { return Log(z, 0); }
		public static ComplexDecimal Log(ComplexDecimal z, int k = 0) { return new ComplexDecimal((decimal)System.Math.Log((double)z.Abs), z.Arg + 2m * k * Pi); }
		public static ComplexDecimal Log(ComplexDecimal z, ComplexDecimal a, int kz = 0, int ka = 0) { return Log(z, kz) / Log(a, ka); }
		//delete  me  ComplexDecimal Pow(ComplexDecimal z, decimal n) { return System.Math.Pow(z.Abs, n) * new ComplexDecimal(System.Math.Cos(z.Arg * n), System.Math.Sin(z.Arg * n)); }
		public static ComplexDecimal Pow(ComplexDecimal z, decimal n) { return FromPolar((decimal)System.Math.Pow((double)z.Abs, (double)n), z.Arg * n); }
		//public static ComplexDecimal Pow(ComplexDecimal z, ComplexDecimal n) { return Pow(z, n, 0); }
		public static ComplexDecimal Pow(ComplexDecimal z, ComplexDecimal n, int k = 0) { return Exp(n * Log(z, k)); }
		//public static ComplexDecimal Pow(ComplexDecimal z, ComplexDecimal n, int k = 0) { return Exp(n * Log(z, k)); }
		public static ComplexDecimal Sqrt(ComplexDecimal z, int k = 0) { return Pow(z, 0.5m, k); }
		//
		public static ComplexDecimal Sin(ComplexDecimal z) { return Sinh(z.MultipleI).DivideI; }
		public static ComplexDecimal Cos(ComplexDecimal z) { return Cosh(z.MultipleI); }
		public static ComplexDecimal Tan(ComplexDecimal z) { return Tanh(z.MultipleI).DivideI; }
		public static ComplexDecimal Cot(ComplexDecimal z) { return Coth(z.MultipleI).MultipleI; }
		public static ComplexDecimal Sec(ComplexDecimal z) { return Sech(z.MultipleI); }
		public static ComplexDecimal Csc(ComplexDecimal z) { return Csch(z.MultipleI).MultipleI; }
		public static ComplexDecimal Sinh(ComplexDecimal z) { return Sinh2(z) / 2m; }
		public static ComplexDecimal Sinh2(ComplexDecimal z) { return (Exp(z) - Exp(-z)); }
		public static ComplexDecimal Cosh(ComplexDecimal z) { return Cosh2(z) / 2m; }
		public static ComplexDecimal Cosh2(ComplexDecimal z) { return (Exp(z) + Exp(-z)); }
		public static ComplexDecimal Tanh(ComplexDecimal z) { return Sinh2(z) / Cosh2(z); }
		public static ComplexDecimal Coth(ComplexDecimal z) { return Cosh2(z) / Sinh2(z); }
		public static ComplexDecimal Sech(ComplexDecimal z) { return 2m / Cosh2(z); }
		public static ComplexDecimal Csch(ComplexDecimal z) { return 2m / Sinh2(z); }
		//
		public static ComplexDecimal Asin(ComplexDecimal z, int kLog, int kSqrt) { return Asinh(z.MultipleI, kLog, kSqrt).DivideI; }
		public static ComplexDecimal Acos(ComplexDecimal z, int kLog, int kSqrt) { return Acosh(z, kLog, kSqrt).DivideI; }
		public static ComplexDecimal Atan(ComplexDecimal z, int kLog) { return Atanh(z.DivideI, kLog).MultipleI; }
		public static ComplexDecimal Acot(ComplexDecimal z, int kLog) { return Atan(1m / z, kLog); }
		public static ComplexDecimal Asec(ComplexDecimal z, int kLog, int kSqrt) { return Acos(1m / z, kLog, kSqrt); }
		public static ComplexDecimal Acsc(ComplexDecimal z, int kLog, int kSqrt) { return Asin(1m / z, kLog, kSqrt); }
		public static ComplexDecimal Asinh(ComplexDecimal z, int kLog, int kSqrt) { return Log(z + Sqrt(z * z + 1m, kSqrt), kLog); }
		public static ComplexDecimal Acosh(ComplexDecimal z, int kLog, int kSqrt) { return Log(z + Sqrt(z * z - 1m, kSqrt), kLog); }
		public static ComplexDecimal Atanh(ComplexDecimal z, int kLog) { return Log((1m + z) / (1m - z), kLog) / 2m; }
		public static ComplexDecimal Acoth(ComplexDecimal z, int kLog) { return Atanh(1m / z, kLog); }
		public static ComplexDecimal Asech(ComplexDecimal z, int kLog, int kSqrt) { return Acosh(1m / z, kLog, kSqrt); }
		public static ComplexDecimal Acsch(ComplexDecimal z, int kLog, int kSqrt) { return Asinh(1m / z, kLog, kSqrt); }
		#endregion

		#region Оператори
		/// <summary><returns> Спряжене комплексне число. </returns></summary>
		public static ComplexDecimal operator ~(ComplexDecimal z) { return new ComplexDecimal(z.Re, -z.Im); }
		/*
		/// <summary><returns> Спряжене комплексне число. </returns></summary>
		public static ComplexDecimal operator !(ComplexDecimal z) { return ~z; }
		//*/
		public static ComplexDecimal operator ++(ComplexDecimal z) { ++z.Re; return z; }
		public static ComplexDecimal operator --(ComplexDecimal z) { --z.Re; return z; }
		public static ComplexDecimal operator +(ComplexDecimal z) { return z; }
		public static ComplexDecimal operator -(ComplexDecimal z) { return new ComplexDecimal(-z.Re, -z.Im); }
		public static ComplexDecimal operator +(ComplexDecimal z1, ComplexDecimal z2) { return new ComplexDecimal(z1.Re + z2.Re, z1.Im + z2.Im); }
		public static ComplexDecimal operator -(ComplexDecimal z1, ComplexDecimal z2) { return new ComplexDecimal(z1.Re - z2.Re, z1.Im - z2.Im); }
		public static ComplexDecimal operator *(ComplexDecimal z1, ComplexDecimal z2) { return new ComplexDecimal(z1.Re * z2.Re - z1.Im * z2.Im, z1.Re * z2.Im + z1.Im * z2.Re); }
		public static ComplexDecimal operator /(ComplexDecimal z1, ComplexDecimal z2) { return ~z2 * z1 / z2.SqrAbs; }
		//public static ComplexDecimal operator %(ComplexDecimal z1, ComplexDecimal z2) { return new ComplexDecimal(z1.Re % z2.Re, z1.Im % z2.Im); }
		public static ComplexDecimal operator +(ComplexDecimal z1, decimal d2) { return new ComplexDecimal(z1.Re + d2, z1.Im); }
		public static ComplexDecimal operator -(ComplexDecimal z1, decimal d2) { return new ComplexDecimal(z1.Re - d2, z1.Im); }
		public static ComplexDecimal operator *(ComplexDecimal z1, decimal d2) { return new ComplexDecimal(z1.Re * d2, z1.Im * d2); }
		public static ComplexDecimal operator /(ComplexDecimal z1, decimal d2) { return new ComplexDecimal(z1.Re / d2, z1.Im / d2); }
		//public static ComplexDecimal operator %(ComplexDecimal z1, decimal d2) { return new ComplexDecimal(z1.Re % d2, z1.Im % d2); }
		public static ComplexDecimal operator +(decimal d1, ComplexDecimal z2) { return z2 + d1; }
		public static ComplexDecimal operator -(decimal d1, ComplexDecimal z2) { return new ComplexDecimal(d1 - z2.Re, -z2.Im); }
		public static ComplexDecimal operator *(decimal d1, ComplexDecimal z2) { return z2 * d1; }
		public static ComplexDecimal operator /(decimal d1, ComplexDecimal z2) { return ~z2 * (d1 / z2.SqrAbs); }
		public static bool operator ==(ComplexDecimal z1, ComplexDecimal z2) { return z1.Equals(z2); }
		public static bool operator !=(ComplexDecimal z1, ComplexDecimal z2) { return !z1.Equals(z2); }
		public static implicit operator ComplexDecimal(decimal d) { return new ComplexDecimal(d, 0m); }
		public static explicit operator ComplexDecimal(double d) { return new ComplexDecimal((decimal)d, 0m); }
		public static explicit operator ComplexDecimal(ComplexDouble cd) { return new ComplexDecimal((decimal)cd.Re, (decimal)cd.Im); }
		#endregion
	}
}
