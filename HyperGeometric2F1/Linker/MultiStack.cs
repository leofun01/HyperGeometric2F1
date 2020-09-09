namespace HyperGeometric2F1.Linker
{
	public class MultiStack<TData, TLink, TClone>
		: MultiLinker<TData, TLink, TClone>
		where TLink : MonoStack<TClone, TLink>, new()
		where TClone : MultiStack<TData, TLink, TClone>
	{
		public MultiStack() { }
		public MultiStack(TData data) : base(data) { }
		public MultiStack(TData data, TLink link) : base(data, link) { }
	}

	public sealed class MultiStack<TData>
		: MultiStack<TData, MonoStack<MultiStack<TData>>, MultiStack<TData>>
	{
		public MultiStack() { }
		public MultiStack(TData data) : base(data) { }
		public MultiStack(TData data, MonoStack<MultiStack<TData>> link) : base(data, link) { }
	}
}
