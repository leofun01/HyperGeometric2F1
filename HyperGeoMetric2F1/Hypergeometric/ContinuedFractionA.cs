using System;
using System.Collections;
using System.Collections.Generic;
using HyperGeoMetric2F1.Base;
using HyperGeoMetric2F1.Linker;

namespace HyperGeoMetric2F1.Hypergeometric
{
	public class ContinuedFractionA : ICloneable<ContinuedFractionA>, IEnumerable<Coef>
	{
		public readonly MonoLinker<Coef> Coefs;
		public readonly Abc SumAbc;

		public ContinuedFractionA() { }
		public ContinuedFractionA(Abc sum, MonoLinker<Coef> cfs = null) { SumAbc = sum; Coefs = cfs; }

		public ContinuedFractionA Clone() { return new ContinuedFractionA(SumAbc, Coefs.GetCopy()); }
		object ICloneable.Clone() { return Clone(); }
		public IEnumerator<Coef> GetEnumerator() { return Coefs.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}
}
