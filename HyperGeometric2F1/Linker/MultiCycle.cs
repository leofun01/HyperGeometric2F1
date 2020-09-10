namespace HyperGeometric2F1.Linker
{
	public class MultiCycle<TData, TLink, TClone>
		: MultiLinker<TData, TLink, TClone>
		where TLink : MonoCycle<TClone, TLink>, new()
		where TClone : MultiCycle<TData, TLink, TClone>
	{
		public MultiCycle() { }
		public MultiCycle(TData data) : base(data) { }
		public MultiCycle(TData data, TLink link) : base(data, link) { }
	}

	public sealed class MultiCycle<TData>
		: MultiCycle<TData, MonoCycle<MultiCycle<TData>>, MultiCycle<TData>>
	{
		public MultiCycle() { }
		public MultiCycle(TData data) : base(data) { }
		public MultiCycle(TData data, MonoCycle<MultiCycle<TData>> link) : base(data, link) { }
	}
}
