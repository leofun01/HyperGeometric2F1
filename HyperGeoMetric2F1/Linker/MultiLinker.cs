using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HyperGeoMetric2F1.Linker
{
	[ComVisible(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	#pragma warning disable 612,618
	public class MultiLinker<TData, TLink, TClone>
		: MultiEnumerable<TData, TLink, TClone>, IList<TData>//, IList<TClone>, IList<TLink>
		where TLink : MonoLinker<TClone, TLink>, new()
		where TClone : MultiLinker<TData, TLink, TClone>
	#pragma warning restore 612,618
	{
		public MultiLinker() { }
		public MultiLinker(TData data) : base(data) { }
		public MultiLinker(TData data, TLink link) : base(data, link) { }

		TData IList<TData>.this[int index]
		{
			get { var t = this[index]; return t == null || t.Data == null ? default(TData) : t.Data.Data; }
			set { var t = this[index]; if(t != null && t.Data != null) t.Data.Data = value; }
		}
		IEnumerator<TData> IEnumerable<TData>.GetEnumerator() { return GetEnumerator(); }
	}

	public sealed class MultiLinker<TData>
		: MultiLinker<TData, MonoLinker<MultiLinker<TData>>, MultiLinker<TData>>
	{
		public MultiLinker() { }
		public MultiLinker(TData data) : base(data) { }
		public MultiLinker(TData data, MonoLinker<MultiLinker<TData>> link) : base(data, link) { }
	}
}
