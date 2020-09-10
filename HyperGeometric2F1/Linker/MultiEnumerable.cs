using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HyperGeometric2F1.Linker
{
	[ComVisible(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Obsolete("Замість класу MultiEnumerable використовуй клас MultiLinker, або його нащадків.")]
	public abstract class MultiEnumerable<TData, TLink, TClone>
		: DataLinker<TData, TLink, TClone>, IList<TClone>//, IList<TLink>, IList<TData>
		where TLink : MonoEnumerable<TClone, TLink>, new()
		where TClone : MultiEnumerable<TData, TLink, TClone>
	{
		protected MultiEnumerable() { }
		protected MultiEnumerable(TData data) : base(data) { }
		protected MultiEnumerable(TData data, TLink link) : base(data, link) { }

		#region Private
		private static Predicate<TLink> GetPredicate0(TLink link) { return m => m == link; }
		private static Predicate<TLink> GetPredicate1(TClone item) { return m => m != null && m.Data == item; }
		private static Predicate<TLink> GetPredicate2(TData data) { return m => m != null && m.Data != null && Equals(m.Data.Data, data); }
		private ArgumentOutOfRangeException _outOfRange(int index)
		{
			return new ArgumentOutOfRangeException("index", index,
				"Параметр має задовільняти умову: " + 0 + " ≤ index ≤ " + (Link == null ? 0 : Link.Count));
		}
		#endregion

		public TData[] DataArray
		{
			get
			{
				return Link == null ? null :
					Link.ToArray(m => m.Data == null ? default(TData) : m.Data.Data, Count);
			}
		}
		public override string ToString()
		{
			return string.Concat("[ " + Data, " → ", Link == null ? "[]" :
				Link.ToString(m => m.Data == null || Equals(m.Data.Data, null) ? "" : m.Data.Data.ToString()), "]");
		}
		public virtual TClone GetCopy() { return Clone(Data, Link == null ? null : Link.GetCopy()); }

		#region Implement IList<TClone>, IList<TData>

		TClone IList<TClone>.this[int index]
		{
			get { var t = this[index]; return t == null ? null : t.Data; }
			set { var t = this[index]; if(t != null) t.Data = value; }
		}
		/// <exception cref="ArgumentOutOfRangeException"> Параметр має задовільняти умову:
		/// <value>0</value> ≤ <paramref name="index"/> ≤ <see cref="Count"/> </exception>
		public virtual TLink this[int index]
		{
			get { return Link[index]; }
			set
			{
				if(index == 0) Link = value;
				else if(Link == null) throw _outOfRange(index);
					else try { Link[index] = value; }
						catch(ArgumentOutOfRangeException) { throw _outOfRange(index); }
			}
		}
		public void Insert(int index, TData data) { Insert(index, Clone(data, null)); }
		public void Insert(int index, TClone clone) { Insert(index, new TLink { Data = clone }); }
		public void Insert(int index, TLink link)
		{
			if(index == 0) AddFirst(link);
			else if(Link == null) throw _outOfRange(index);
				else try { Link.Insert(index, link); }
					catch(ArgumentOutOfRangeException) { throw _outOfRange(index); }
		}

		public int IndexOf(TData data) { return IndexOf(GetPredicate2(data)); }
		public int IndexOf(TClone item) { return IndexOf(GetPredicate1(item)); }
		public int IndexOf(TLink link) { return IndexOf(GetPredicate0(link)); }
		public virtual int IndexOf(Predicate<TLink> pred) { return Link == null ? -1 : Link.IndexOf(pred); }

		public virtual bool Remove()
		{
			if(Link == null) return false;
			var last = Link.GetLast();
			if(last.Link == Link)
				if(last == Link) Link = null;
				else last.Link = Link = Link.Link;
			else Link = Link.Link;
			return true;
		}
		public void RemoveAt(int index) { if(index == 0) Remove(); else Link.RemoveAt(index); }

		#endregion

		#region Implement ICollection<TClone>, ICollection<TData>

		public virtual int Count { get { return Link == null ? 0 : Link.Count; } }
		public void Add(TData data) { Add(Clone(data, null)); }
		public void Add(TClone clone) { Add(new TLink { Data = clone }); }
		public virtual void Add(TLink link) { if(Link == null) Link = link; else Link.Add(link); }
		public void AddFirst(TData data) { AddFirst(Clone(data, null)); }
		public void AddFirst(TClone clone) { AddFirst(new TLink { Data = clone }); }
		public virtual void AddFirst(TLink link) { if(link != null) link.GetLast().Link = Link; Link = link; }
		/// <summary> Розриває всі звязки даного елемента. </summary>
		public virtual void Clear() { if(Link != null) Link.Clear(); Link = null; }

		public bool Contains(TData data) { return Contains(GetPredicate2(data)); }
		public bool Contains(TClone item) { return Contains(GetPredicate1(item)); }
		public bool Contains(TLink link) { return Contains(GetPredicate0(link)); }
		public virtual bool Contains(Predicate<TLink> pred) { return Link != null && Link.Contains(pred); }

		public void CopyTo(TData[] array, int arrayIndex) { if(Link != null) Link.CopyTo(array, arrayIndex, 1, item => item.Data.Data, item => item != null); }
		public void CopyTo(TClone[] array, int arrayIndex) { if(Link != null) Link.CopyTo(array, arrayIndex); }
		public void CopyTo(TLink[] array, int arrayIndex) { if(Link != null) Link.CopyTo(array, arrayIndex); }

		public bool Remove(TData data) { return Remove(GetPredicate2(data)); }
		public bool Remove(TClone item) { return Remove(GetPredicate1(item)); }
		public bool Remove(TLink link) { return Remove(GetPredicate0(link)); }
		public virtual bool Remove(Predicate<TLink> pred) { return Link != null && (pred(Link) ? Remove() : Link.Remove(pred)); }

		public void RemoveAll(TData data) { RemoveAll(GetPredicate2(data)); }
		public void RemoveAll(TClone item) { RemoveAll(GetPredicate1(item)); }
		public virtual void RemoveAll(Predicate<TLink> pred) { if(Link == null) return; Link.RemoveAll(pred); if(pred(Link)) Link = null; }

		#endregion

		#region Implement IEnumerable<TClone>, IEnumerable<TData>

		/// <summary> Повертає<returns> ітератор, який послідовно дає доступ до кожного елемента. </returns></summary>
		public virtual MultiEnumerator GetEnumerator() { return new MultiEnumerator((TClone)this); }
		//IEnumerator<TData> IEnumerable<TData>.GetEnumerator() { return GetEnumerator(); }
		IEnumerator<TClone> IEnumerable<TClone>.GetEnumerator() { return GetEnumerator(); }
		//IEnumerator<TLink> IEnumerable<TLink>.GetEnumerator() { return GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		#endregion

		public class MultiEnumerator
			: MonoEnumerable<TClone, TLink>.MonoEnumerator, IEnumerator<TData>
		{
			public MultiEnumerator() { }
			/// <param name="list"> Список елементів. </param>
			/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
			public MultiEnumerator(TClone list, int jump = 1) : base(list == null ? null : list.Link, jump) { }
			/// <param name="list"> Список елементів. </param>
			/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
			/// <param name="count"> Загальна кількість ітерацій. </param>
			public MultiEnumerator(TClone list, int count, int jump = 1) : base(list == null ? null : list.Link, count, jump) { }

			TData IEnumerator<TData>.Current { get { return Current == null || Current.Data == null ? default(TData) : Current.Data.Data; } }
		}
	}
}
