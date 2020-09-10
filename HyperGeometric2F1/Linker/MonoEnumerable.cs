using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HyperGeometric2F1.Linker
{
	[ComVisible(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Obsolete("Замість MonoEnumerable використовуй MonoLinker, або його нащадків.")]
	public abstract class MonoEnumerable<TData, TClone>
		: DataLinker<TData, TClone, TClone>, IList<TClone>//, IList<TData>
		where TClone : MonoEnumerable<TData, TClone>
	{
		protected MonoEnumerable() { }
		protected MonoEnumerable(TData data) : base(data) { }
		protected MonoEnumerable(TData data, TClone link) : base(data, link) { }
		protected MonoEnumerable(IEnumerable<TData> enumerable) : this(enumerable == null ? null : enumerable.GetEnumerator()) { }
		protected MonoEnumerable(IEnumerator<TData> enumerator)
		{
			if(enumerator == null || !enumerator.MoveNext()) return;
			var item = (TClone)this; item.Data = enumerator.Current;
			while(enumerator.MoveNext()) item = item.Link = Clone(enumerator.Current, null);
		}

		private int _s, _c;
		private TClone _setLink(TClone link) { return base.Link = link; }
		public TData[] DataArray { get { return ToArray(m => m.Data, Count); } }
		private ArgumentOutOfRangeException _outOfRange(int index)
		{
			return new ArgumentOutOfRangeException("index", index,
				"Параметр має задовільняти умову: " + 1 + " ≤ index ≤ " + Count);
		}
		/*
		public T[] ToArray<T>(Converter<TClone, T> conv, int len, int jump = 1)
		{
			int i = -1;
			T[] array = new T[len];
			var e = new MonoEnumerator((TClone)this, len, jump);
			while(e.MoveNext()) array[++i] = conv(e.Current);
			return array;
		}
		//*/
		public T[] ToArray<T>(Converter<TClone, T> conv) { return ToArray(conv, Count); }
		public T[] ToArray<T>(Converter<TClone, T> conv, int len, int jump = 1)
		{
			var array = new T[len];
			CopyTo(array, 0, jump, conv, item => item != null);
			return array;
		}
		public void FromArray<T>(IEnumerable<T> array, Converter<T, TData> conv, int len)
		{
			int i = 0;
			TClone item = (TClone)this, link = Link;
			foreach(var a in array) {
				if(++i > len) break;
				item = item.Link = item.Clone(conv(a), link);
			}
		}

		public override string ToString() { return ToString(1); }
		public string ToString(int jump) { return ToString(m => Equals(m.Data, null) ? "" : m.Data.ToString(), jump); }
		public virtual string ToString(Converter<TClone, string> conv, int jump = 1)
		{
			TClone item = (TClone)this, end = GetLast(jump)[jump];
			Converter<TClone, string> c = m => { var str = "";
				do str += string.Concat(" ", conv(m), " →");
				while((m = m[jump]) != end); return str; };
			return string.Concat("[", item == end ? "" : c(item), "(", null == end ? "" : c(end), ")]");
		}

		#region Search / Пошук

		public TClone Find(TData data, int jump = 1) { return Find(m => Equals(m.Data, data), jump); }
		public TClone Find(Predicate<TClone> pred, int jump = 1)
		{
			var item = (TClone)this;
			return pred != null && (pred(item) || pred(item = GetLast(pred, jump)[jump])) ? item : null;
		}

		public TClone GetBefore(TData data, int jump = 1) { return GetBefore(m => m != null && Equals(m.Data, data), jump); }
		public TClone GetBefore(TClone item, int jump = 1) { return GetBefore(m => m == item, jump); }
		public TClone GetBefore(Predicate<TClone> pred, int jump = 1) { var bfor = GetLast(pred, jump); return pred(bfor[jump]) ? bfor : null; }

		public TClone GetLast(int jump = 1) { return GetLast(m => false, jump); }
		protected virtual TClone GetLast(Predicate<TClone> pred, int jump = 1)
		{
			if(pred == null) throw new ArgumentNullException("pred");
			TClone last, next = (TClone)this, end = next;
			do (last = end).IsReadOnly = true; while(!((end = last[jump]) == null || pred(end) || end.IsReadOnly));
			for(_s = _c = 0; next != end; ++_s) { next.IsReadOnly = false; next = next[jump]; }
			if(next == null || pred(next)) return last;
			do { next.IsReadOnly = false; ++_c; } while((next = next[jump]).IsReadOnly);
			return last;
		}

		public virtual int GetCount(int jump = 1) { GetLast(jump); return _s + _c; }
		/// <summary> Кільуість елементів, які доступні через <see cref="MonoLinker{TData,TClone}.Link"/> і не входять в цикл. </summary>
		public virtual int GetStackPart(int jump = 1) { GetLast(jump); return _s; }
		/// <summary> Кільуість елементів, які доступні через <see cref="MonoLinker{TData,TClone}.Link"/> і утворюють цикл. </summary>
		public virtual int GetCyclePart(int jump = 1) { GetLast(jump); return _c; }

		#endregion

		#region Reconstruction / Перебудова

		public virtual void DoForEach(Action<MonoEnumerator> actn, int jump = 1)
		{
			if(actn == null) throw new ArgumentNullException("actn");
			var e = new MonoEnumerator((TClone)this, jump);
			while(e.MoveNext()) actn(e);
		}
		public virtual TClone GetCopy(int jump = 1)
		{
			TClone copy = Clone(), item = (TClone)this, next = copy, end = GetLast(jump)[jump];
			while((item = item[jump]) != end) next = next._setLink(item.Clone());
			if(end == null) return copy; if(end == this) return next._setLink(copy);
			do next = next._setLink(item.Clone()); while((item = item[jump]) != end);
			next._setLink(copy[_s]); return copy;
		}
		public TClone Reverse() { return Reverse(m => m != null); }
		public virtual TClone Reverse(Predicate<TClone> pred)
		{
			TClone item, next = (TClone)this, prev = null;
			do { next = (item = next).Link; item._setLink(prev); prev = item; }
			while(pred(next)); _setLink(next ?? Link); return item;
		}
		/*
		public virtual TClone SortSlow(Comparison<TClone> cmpr)
		{
			TClone rtrn = (TClone)this, item = rtrn, next, prev;
			//
			return rtrn;
		}
		/*
		public virtual TClone SortFast(Comparison<TClone> cmpr)
		{
			TClone rtrn = (TClone)this, item, next, prev;
			//
			return rtrn;
		}
		//*/

		#endregion

		#region Implement IList<TClone>, IList<TData>

		/*
		TData IList<TData>.this[int index]
		{
			get { var t = this[index]; return t == null ? default(TData) : t.Data; }
			set { var t = this[index]; if(t != null) t.Data = value; }
		}
		//*/
		/// <exception cref="ArgumentOutOfRangeException"> Параметр має задовільняти умову:
		/// <value>1</value> ≤ <paramref name="index"/> ≤ <see cref="Count"/> </exception>
		public virtual TClone this[int index]
		{
			get
			{
				if(index < 0) return null; var item = (TClone)this;
				while(--index > -1 && (item = item.Link) != null) { }
				return item;
			}
			set
			{
				var bfor = this[index - 1];
				if(bfor == null) throw _outOfRange(index);
				bfor.Link = value;
			}
		}
		public void Insert(int index, TData data) { Insert(index, Clone(data, null)); }
		public void Insert(int index, TClone item)
		{
			var bfor = index > 0 ? this[index - 1] : null;
			if(bfor == null) throw _outOfRange(index);
			bfor.AddFirst(item);
		}

		public int IndexOf(TData data) { return IndexOf(data, 1); }
		public int IndexOf(TData data, int jump) { return IndexOf(m => m != null && Equals(m.Data, data), jump); }
		public int IndexOf(TClone item) { return IndexOf(item, 1); }
		public int IndexOf(TClone item, int jump) { return IndexOf(m => m == item, jump); }
		public virtual int IndexOf(Predicate<TClone> pred, int jump = 1) { return Contains(pred, jump) ? _s : -1; }

		public virtual bool RemoveFirst()
		{
			if(Link == null) return false;
			var last = GetLast();
			if(last.Link == Link)
				if(last == Link) Link = null;
				else last.Link = Link = Link.Link;
			else Link = Link.Link;
			return true;
		}
		public void RemoveAt(int index) { RemoveAt(index > 0 ? this[index - 1] : null); }
		private static bool RemoveAt(TClone bfor) { return bfor != null && bfor.RemoveFirst(); }

		#endregion

		#region Implement ICollection<TClone>, ICollection<TData>

		/// <summary> Кільуість всіх елементів, які доступні через <see cref="MonoLinker{TData,TClone}.Link"/>. </summary>
		public int Count { get { return GetCount(); } }
		/// <summary> Додає елемент <paramref name="data"/> після останнього елемента. </summary>
		public void Add(TData data) { Add(Clone(data, null)); }
		/// <summary> Додає послідовність елементів <paramref name="item"/> після останнього елемента. </summary>
		public virtual void Add(TClone item) { if(item == null) return; GetLast().Link = item; }
		/// <summary> Додає елемент <paramref name="data"/> одразу після даного елемента. </summary>
		public void AddFirst(TData data) { AddFirst(Clone(data, null)); }
		/// <summary> Додає послідовність елементів <paramref name="item"/> одразу після даного елемента. </summary>
		public virtual void AddFirst(TClone item) { if(item == null) return; item.GetLast().Link = Link; Link = item; }
		/// <summary> Розриває всі звязки між елементами (в кожному елементі
		/// <see cref="MonoLinker{TData,TClone}.Link"/> = null). </summary>
		public virtual void Clear()
		{
			TClone item = (TClone)this, next;
			while((next = item.Link) != null && next != next.Link)
			{ item.Link = null; item = next; }
		}

		public bool Contains(TData data) { return Contains(data, 1); }
		public bool Contains(TData data, int jump) { return Contains(m => m != null && Equals(m.Data, data), jump); }
		public bool Contains(TClone item) { return Contains(item, 1); }
		public bool Contains(TClone item, int jump) { return Contains(m => m == item, jump); }
		public virtual bool Contains(Predicate<TClone> pred, int jump = 1) { return pred != null && Find(pred, jump) != null; }

		public void CopyTo(TData[] array, int arrayIndex) { CopyTo(array, arrayIndex, 1); }
		public void CopyTo(TData[] array, int arrayIndex, int jump)
		{
			CopyTo(array, arrayIndex, jump, item => item.Data, item => item != null);
		}
		public void CopyTo(TClone[] array, int arrayIndex) { CopyTo(array, arrayIndex, 1); }
		public void CopyTo(TClone[] array, int arrayIndex, int jump)
		{
			CopyTo(array, arrayIndex, jump, item => item, item => item != null);
		}
		public virtual void CopyTo<T>(T[] array, int arrayIndex, int jump, Converter<TClone, T> conv, Predicate<TClone> whil)
		{
			if(array == null) throw new ArgumentNullException("array");
			if(conv == null) throw new ArgumentNullException("conv");
			if(whil == null) throw new ArgumentNullException("whil");
			var length = array.GetLength(0); var item = (TClone)this;
			while(arrayIndex < length && whil(item))
			{ array[arrayIndex++] = conv(item); item = item[jump]; }
		}

		public bool Remove(TData data) { return Remove(data, 1); }
		public bool Remove(TData data, int jump) { return Remove(m => m != null && Equals(m.Data, data), jump); }
		public bool Remove(TClone item) { return Remove(item, 1); }
		public bool Remove(TClone item, int jump) { return item != null && Remove(m => m == item, jump); }
		public virtual bool Remove(Predicate<TClone> pred, int jump = 1) { return pred != null && !pred((TClone)this) && RemoveAt(GetBefore(pred, jump)); }

		public void RemoveAll(TData data, int jump = 1) { RemoveAll(m => m != null && Equals(m.Data, data), jump); }
		public virtual void RemoveAll(Predicate<TClone> pred, int jump = 1)
		{
			if(pred == null || pred((TClone)this)) return;
			var bfor = (TClone)this;
			while(RemoveAt(bfor = bfor.GetBefore(pred, jump))) { }
		}

		#endregion

		#region Implement IEnumerable<TClone>, IEnumerable<TData>

		/// <summary> Повертає<returns> ітератор, який послідовно дає доступ до кожного елемента. </returns></summary>
		public virtual MonoEnumerator GetEnumerator() { return new MonoEnumerator((TClone)this); }
		//IEnumerator<TData> IEnumerable<TData>.GetEnumerator() { return GetEnumerator(); }
		IEnumerator<TClone> IEnumerable<TClone>.GetEnumerator() { return GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		#endregion

		/// <summary> Ітератор для класу <see cref="TClone"/> і всіх його нащадків. </summary>
		public class MonoEnumerator
			: Base.Enumerator<TClone>, IEnumerator<TData>
		{
			public MonoEnumerator() { }
			/// <param name="list"> Список елементів. </param>
			/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
			public MonoEnumerator(TClone list, int jump = 1) : base(list, list == null ? 0 : list.GetCount(jump), jump) { }
			/// <param name="list"> Список елементів. </param>
			/// <param name="jump"> Номер наступного елемента відносно даного для кожної ітерації. </param>
			/// <param name="count"> Загальна кількість ітерацій. </param>
			public MonoEnumerator(TClone list, int count, int jump = 1) : base(list, count, jump) { }

			TData IEnumerator<TData>.Current { get { return Current == null ? default(TData) : Current.Data; } }
			public override bool MoveNext()
			{
				#pragma warning disable 665
				//if(!Movable || !(Movable = (List != null))) return false;
				if(Movable = (((++Done < Count/* && List != null*/) || (List != null && Done < (Count = List.Count))) && Movable/* && List != null*/))
				#pragma warning restore 665
				{
					Index += Jump;
					Movable = ((Current = (Done < 0 || Current == null ? List[Index] : Current[Jump])) != null);
				}
				else --Done; return Movable;
			}
		}
	}
}
