namespace HyperGeometric2F1.Base
{
	/// <summary> Задає метод клонування. </summary>
	/// <typeparam name="T"> Тип клонованого об'єкта. </typeparam>
	[System.CLSCompliant(true)]
	public interface ICloneable<out T> : System.ICloneable
		where T : ICloneable<T>
	{
		/// <summary> Повертає<returns> об'єкт, який є [клоном | копією] даного об'єкта. </returns></summary>
		new T Clone();
	}
}
