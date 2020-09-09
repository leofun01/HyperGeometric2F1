using System.Collections.Generic;

namespace HyperGeometric2F1.Base
{
	/// <summary> Ітератор, який послідовно дає доступ до кожного елемента типу <see cref="T"/>. </summary>
	/// <typeparam name="T"> Тип елементів списку <see cref="IList{T}"/>. </typeparam>
	[System.CLSCompliant(true)]
	public class Enumerator<T> : IEnumerator<T>, ICloneable<Enumerator<T>>
	{
		#region Ctors / Конструктори

		public Enumerator() { _baseReset(); }
		/// <param name="array"> Масив елементів. </param>
		public Enumerator(params T[] array) : this(array, array == null ? 0 : array.GetLength(0)) { }
		/// <param name="list"> Список елементів. </param>
		/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
		public Enumerator(IList<T> list, int jump = 1) : this(list, list == null ? 0 : list.Count, jump) { }
		/// <param name="list"> Список елементів. </param>
		/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
		/// <param name="count"> Загальна кількість ітерацій. </param>
		public Enumerator(IList<T> list, int count, int jump = 1) { Count = count; Jump = jump; List = list; _baseReset(); }

		#endregion

		#region Fields & Properties / Поля & Властивості

		/// <summary> Загальна кількість ітерацій. </summary>
		public int Count;
		/// <summary> Кількість завершених ітерацій (<value>-1</value>, якщо ітерування не розпочато). </summary>
		public int Done { get; protected set; }
		/// <summary> Номер елемента в списку (<value>-<see cref="Jump"/></value>, якщо ітерування не розпочато). </summary>
		public int Index;
		/// <summary> Номер наступного елемента відносно даного для кожної наступної ітерації. </summary>
		public int Jump;
		/// <summary> Вказує, чи можна виконати наступну ітерацію. </summary>
		public bool Movable;
		/// <summary> Список елементів типу <see cref="T"/> (<see cref="IList{T}"/>). </summary>
		public IList<T> List;
		/// <summary> Елемент даної ітерації. </summary>
		public T Current { get; set; }
		object System.Collections.IEnumerator.Current { get { return Current; } }

		#endregion

		#region Implement IEnumerator<T>

		/// <returns> Вказує, чи успішно<summary> виконується наступна ітерація. </summary></returns>
		public virtual bool MoveNext()
		{
			#pragma warning disable 665
			if(Movable = (++Done < Count && Movable && List != null)) Current = List[Index += Jump];
			#pragma warning restore 665
			else { --Done; Current = default(T); } return Movable;
		}
		/// <summary> Відновлює початковий стан ітератора. </summary>
		public virtual void Reset() { _baseReset(); }
		private void _baseReset() { Done = -1; Index = -Jump; Movable = true; }
		public virtual void Dispose() { }

		#endregion

		#region Implement ICloneable<Enumerator<T>>

		public virtual Enumerator<T> Clone() { return (Enumerator<T>)MemberwiseClone(); }
		object System.ICloneable.Clone() { return Clone(); }

		#endregion
	}
}
