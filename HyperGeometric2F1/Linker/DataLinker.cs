namespace HyperGeometric2F1.Linker
{
	/// <summary> Містить дані і посилання. </summary>
	/// <typeparam name="TData"> Тип даних. </typeparam>
	/// <typeparam name="TLink"> Тип посилання. </typeparam>
	/// <typeparam name="TClone"> Тип клона. </typeparam>
	[System.CLSCompliant(true)]
	public class DataLinker<TData, TLink, TClone> : Base.ICloneable<TClone>
		where TClone : DataLinker<TData, TLink, TClone>
	{
		public DataLinker() { }
		public DataLinker(TData data) { _data = data; }
		public DataLinker(TData data, TLink link) : this(data) { _link = link; }

		#region Properties / Властивості

		private TData _data;
		private TLink _link;
		/// <summary> Дані. </summary>
		public virtual TData Data { get { return _data; } set { _data = value; } }
		/// <summary> Посилання. </summary>
		public virtual TLink Link { get { return _link; } set { _link = value; } }
		/// <summary> [Мітка | Позначка], яка вказує, чи поле <see cref="Link"/> тільки для читання. </summary>
		public bool IsReadOnly { get; protected set; }

		#endregion

		#region Implement ICloneable<TClone>

		/// <summary> Повертає<returns> об'єкт, який є [клоном | копією] даного об'єкта. </returns></summary>
		public virtual TClone Clone() { return (TClone)MemberwiseClone(); }
		object System.ICloneable.Clone() { return Clone(); }
		/// <summary> Повертає<returns> [клон | копію] і присвоює полям значення <paramref name="data"/> і <paramref name="link"/>. </returns></summary>
		public TClone Clone(TData data, TLink link) { var c = Clone(); c.Data = data; c.Link = link; return c; }

		#endregion

		/// <summary> Повертає<returns> стрічкове представлення даного об'єкта. </returns></summary>
		/// <exception cref="System.StackOverflowException"> Може викликати рекурсію. </exception>
		public override string ToString()
		{
			return string.Concat("[" + Data, " → ", Link + "]");
		}
	}

	/// <summary> Містить дані і посилання. </summary>
	/// <typeparam name="TData"> Тип даних. </typeparam>
	/// <typeparam name="TLink"> Тип посилання. </typeparam>
	public sealed class DataLinker<TData, TLink> : DataLinker<TData, TLink, DataLinker<TData, TLink>>
	{
		public DataLinker() { }
		public DataLinker(TData data) : base(data) { }
		public DataLinker(TData data, TLink link) : base(data, link) { }
	}
}
