using System.Collections.Generic;

namespace HyperGeoMetric2F1.Linker
{
	public class MonoStack<TData, TClone> : MonoLinker<TData, TClone>
		where TClone : MonoStack<TData, TClone>
	{
		public MonoStack() { }
		public MonoStack(TData data) : base(data) { }
		public MonoStack(TData data, TClone link) : base(data, link) { }
		public MonoStack(IEnumerable<TData> enumerable) : base(enumerable) { }
		public MonoStack(IEnumerator<TData> enumerator) : base(enumerator) { }

		public override sealed int GetCyclePart(int jump = 1) { return jump == 0 ? 1 : 0; }
		public override TClone Link
		{
			get { return base.Link; }
			set
			{
				if(value == null || !value.Contains((TClone)this))
					base.Link = value;
			}
		}
	}

	public sealed class MonoStack<TData> : MonoStack<TData, MonoStack<TData>>
	{
		public MonoStack() { }
		public MonoStack(TData data) : base(data) { }
		public MonoStack(TData data, MonoStack<TData> link) : base(data, link) { }
		public MonoStack(IEnumerable<TData> enumerable) : base(enumerable) { }
		public MonoStack(IEnumerator<TData> enumerator) : base(enumerator) { }
	}
}
