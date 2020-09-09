using System.Collections.Generic;

namespace HyperGeometric2F1.Linker
{
	public class MonoCycle<TData, TClone> : MonoLinker<TData, TClone>
		where TClone : MonoCycle<TData, TClone>
	{
		public MonoCycle() { _setLink((TClone)this); }
		public MonoCycle(TData data) : base(data) { _setLink((TClone)this); }
		public MonoCycle(TData data, TClone link) : base(data, link) { (link ?? this).GetLast()._setLink((TClone)this); }
		public MonoCycle(IEnumerable<TData> enumerable) : base(enumerable) { }
		public MonoCycle(IEnumerator<TData> enumerator) : base(enumerator) { }

		public override sealed int GetStackPart(int jump = 1) { return 0; }
		public override TClone Link
		{
			get { return base.Link; }
			set
			{
				(value = (value ?? (TClone)this)).GetLast()._setLink(base.Link);
				_setLink(value);
			}
		}
		public override TClone Clone() { var clone = base.Clone(); clone._setLink(clone); return clone; }
		public override bool RemoveFirst() { return Link != this && base.RemoveFirst(); }
		private void _setLink(TClone link) { base.Link = link; }
	}

	public sealed class MonoCycle<TData> : MonoCycle<TData, MonoCycle<TData>>
	{
		public MonoCycle() { }
		public MonoCycle(TData data) : base(data) { }
		public MonoCycle(TData data, MonoCycle<TData> link) : base(data, link) { }
		public MonoCycle(IEnumerable<TData> enumerable) : base(enumerable) { }
		public MonoCycle(IEnumerator<TData> enumerator) : base(enumerator) { }
	}
}
