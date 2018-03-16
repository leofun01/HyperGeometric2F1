namespace HyperGeoMetric2F1.Base
{
	public delegate T Func<T>(T v);
	public delegate TOut Func<in TIn, out TOut>(TIn v);
	public delegate T Func2<T>(T l, T r);
	public delegate TOut Func2<in TIn, out TOut>(TIn l, TIn r);
	public delegate T FuncParams<T>(params T[] v);
	public delegate TOut FuncParams<in TIn, out TOut>(params TIn[] v);
}
