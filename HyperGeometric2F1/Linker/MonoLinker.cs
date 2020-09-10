using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HyperGeometric2F1.Linker
{
	[ComVisible(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	#pragma warning disable 612,618
	public class MonoLinker<TData, TClone>
		: MonoEnumerable<TData, TClone>, IList<TData>//, IList<TClone>
		where TClone : MonoLinker<TData, TClone>
	#pragma warning restore 612,618
	{
		public MonoLinker() { }
		public MonoLinker(TData data) : base(data) { }
		public MonoLinker(TData data, TClone link) : base(data, link) { }
		public MonoLinker(IEnumerable<TData> enumerable) : base(enumerable) { }
		public MonoLinker(IEnumerator<TData> enumerator) : base(enumerator) { }

		TData IList<TData>.this[int index]
		{
			get { var t = this[index]; return t == null ? default(TData) : t.Data; }
			set { var t = this[index]; if(t != null) t.Data = value; }
		}
		IEnumerator<TData> IEnumerable<TData>.GetEnumerator() { return GetEnumerator(); }
	}

	public sealed class MonoLinker<TData>
		: MonoLinker<TData, MonoLinker<TData>>
	{
		public MonoLinker() { }
		public MonoLinker(TData data) : base(data) { }
		public MonoLinker(TData data, MonoLinker<TData> link) : base(data, link) { }
		public MonoLinker(IEnumerable<TData> enumerable) : base(enumerable) { }
		public MonoLinker(IEnumerator<TData> enumerator) : base(enumerator) { }
	}
}
