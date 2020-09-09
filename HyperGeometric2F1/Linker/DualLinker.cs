using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HyperGeometric2F1.Linker
{
	[ComVisible(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public class DualLinker<TData, TLink, TClone>
		: MonoLinker<TLink, TClone>, IList<TData>
		where TLink : MonoLinker<TData, TLink>, new()
		where TClone : DualLinker<TData, TLink, TClone>
	{
		// Тут потрібно все переписати !

		public DualLinker() : base(new TLink()) { }
		public DualLinker(TData data) : base(new TLink { Data = data }) { }
		public DualLinker(TData data, TClone link) : base(new TLink { Data = data }, link) { }
		public DualLinker(IEnumerable<TData> enumerable) : this(enumerable == null ? null : enumerable.GetEnumerator()) { }
		public DualLinker(IEnumerator<TData> enumerator)
		{
			if(enumerator == null || !enumerator.MoveNext()) return;
			var item = (TClone)this; item.Data = new TLink { Data = enumerator.Current };
			while(enumerator.MoveNext()) item = item.Link = Clone(new TLink { Data = enumerator.Current }, null);
		}

		public int IndexOf(TData item) { throw new System.NotImplementedException(); }

		public void Insert(int index, TData item) { throw new System.NotImplementedException(); }

		TData IList<TData>.this[int index]
		{
			get { var t = this[index]; return t == null ? default(TData) : t.Data; }
			set { var t = this[index]; if(t != null) t.Data = value; }
		}
		IEnumerator<TData> IEnumerable<TData>.GetEnumerator() { return GetEnumerator(); }

		public void Add(TData item) { throw new System.NotImplementedException(); }

		public bool Contains(TData item) { throw new System.NotImplementedException(); }

		public void CopyTo(TData[] array, int arrayIndex) { throw new System.NotImplementedException(); }

		public bool Remove(TData item) { throw new System.NotImplementedException(); }

		/// <summary> Ітератор для класу <see cref="TClone"/> і всіх його нащадків. </summary>
		public new class MonoEnumerator
			: MonoLinker<TLink, TClone>.MonoEnumerator, IEnumerator<TData>
		{
			public MonoEnumerator() { }
			/// <param name="list"> Список елементів. </param>
			/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
			public MonoEnumerator(TClone list, int jump = 1) : base(list, list == null ? 0 : list.GetCount(jump), jump) { }
			/// <param name="list"> Список елементів. </param>
			/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
			/// <param name="count"> Загальна кількість ітерацій. </param>
			public MonoEnumerator(TClone list, int count, int jump = 1) : base(list, count, jump) { }

			TData IEnumerator<TData>.Current { get { return Current == null || Current.Data == null ? default(TData) : Current.Data.Data; } }
		}
	}

	public sealed class DualLinker<TData>
		: DualLinker<TData, MonoLinker<TData>, DualLinker<TData>>
	{
		public DualLinker() { }
		public DualLinker(TData data) : base(data) { }
		public DualLinker(TData data, DualLinker<TData> link) : base(data, link) { }
		public DualLinker(IEnumerable<TData> enumerable) : base(enumerable) { }
		public DualLinker(IEnumerator<TData> enumerator) : base(enumerator) { }
	}
}
