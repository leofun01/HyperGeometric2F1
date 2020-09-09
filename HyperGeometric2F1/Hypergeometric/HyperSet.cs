using System;
using System.Collections;
using System.Collections.Generic;
using HyperGeometric2F1.Base;
using HyperGeometric2F1.Linker;
using HyperGeometric2F1.Math;

namespace HyperGeometric2F1.Hypergeometric
{
	public class HyperSet : ICloneable<HyperSet>, IEnumerable<MonoLinker<HyperEquation>>, ICollection<HyperEquation>
	{
		#region Static
		private const int CountG = 325; // = (3^3-1)*(3^3-2)/2 = 72+253 = 72+(24-1)*(24-2)/2
		public static readonly MultiCycle<HyperEquation>[][] G;
		public static readonly MultiCycle<HyperEquation> Gplus;
		public static readonly MultiCycle<HyperEquation> Fa2czSet;
		public static readonly MultiCycle<HyperEquation>[] Fa1czSet;
		static HyperSet()
		{
			#region Create objects & arrays for G
			var mono3 = new MonoCycle<Abc>(new Abc(1, 0, 0)) { new Abc(0, 1, 0), new Abc(0, 0, 1) };
			G = new[] {
				new MultiCycle<HyperEquation>[2], new MultiCycle<HyperEquation>[3], new MultiCycle<HyperEquation>[2],
				new MultiCycle<HyperEquation>[4], new MultiCycle<HyperEquation>[3], new MultiCycle<HyperEquation>[3]
			};
			for(int j = 0, len0 = G.GetLength(0); j < len0; ++j)
				for(int k = 0, len1 = G[j].GetLength(0); k < len1; ++k)
					G[j][k] = new MultiCycle<HyperEquation>();
			//αβγδλμπ
			Coef c1 = new Coef("+a"), c2 = new Coef("+a+b"), c3 = new Coef("+a+b+c");
			G[0][0].Data = new HyperEquation(Coef.Zero, new Coef("-a"), c1);
			G[0][1].Data = new HyperEquation(Coef.Zero, new Coef("+b"), c1);
			G[1][0].Data = new HyperEquation(Coef.Zero, c1, c2);
			G[1][1].Data = new HyperEquation(Coef.Zero, new Coef("-a"), c2);
			G[1][2].Data = new HyperEquation(Coef.Zero, new Coef("+c"), c2);
			G[2][0].Data = new HyperEquation(Coef.Zero, c1, c3);
			G[2][1].Data = new HyperEquation(Coef.Zero, new Coef("-a"), c3);
			G[3][0].Data = new HyperEquation(Coef.Zero, new Coef("+a-b"), c2);
			G[3][1].Data = new HyperEquation(Coef.Zero, new Coef("-a-b"), c2);
			G[3][2].Data = new HyperEquation(Coef.Zero, new Coef("+a+c"), c2);
			G[3][3].Data = new HyperEquation(Coef.Zero, new Coef("-a+c"), c2);
			G[4][0].Data = new HyperEquation(Coef.Zero, c2, c3);
			G[4][1].Data = new HyperEquation(Coef.Zero, new Coef("+a-b"), c3);
			G[4][2].Data = new HyperEquation(Coef.Zero, new Coef("-a-b"), c3);
			G[5][0].Data = new HyperEquation(Coef.Zero, new Coef("+a+b-c"), c3);
			G[5][1].Data = new HyperEquation(Coef.Zero, new Coef("+a-b-c"), c3);
			G[5][2].Data = new HyperEquation(Coef.Zero, new Coef("-a-b-c"), c3);
			//αβγδλμπ
			#endregion
			//
			#region Init_Main_G
			//0.0
			Add2(G[0][0], "-(c-a-b+(a-b)(z-1))", "a(z-1)", 1, 0, 0, "c-a", -1, 0, 0);
			Add1(G[0][0], "-c((c-a-b)z+(c-1)(z-1))", "(c-a)(c-b)z", 0, 0, 1, "c(c-1)(z-1)", 0, 0, -1);
			//0.1
			Add1(G[0][1], "b-a", "a", 1, 0, 0, "-b", 0, 1, 0);
			Add2(G[0][1], "c-1-a", "a", 1, 0, 0, "-(c-1)", 0, 0, -1);
			Add2(G[0][1], "c(z-1)", "c", -1, 0, 0, "-(c-b)z", 0, 0, +1);
			Add2(G[0][1], "-(c-a-b)", "a(z-1)", 1, 0, 0, "c-b", 0, -1, 0);
			Add1(G[0][1], "(b-a)(z-1)", "c-a", -1, 0, 0, "-(c-b)", 0, -1, 0);
			Add2(G[0][1], "a-1-(c-1-b)z", "c-a", -1, 0, 0, "(c-1)(z-1)", 0, 0, -1);
			Add2(G[0][1], "c(a-(c-b)z)", "ac(z-1)", 1, 0, 0, "(c-a)(c-b)z", 0, 0, 1);
			#endregion
			//
			#region Init_Else_G
			#region Init_Else_G
			//1.0
			Add2(G[1][0], "-c", "c-a", 0, 0, 1, "a", 1, 0, 1);
			Add2(G[1][0], "-(a-1)", "a-1-b", -1, 0, 0, "b", -1, 1, 0);
			Add2(G[1][0], "c", "c(z-1)", 1, 0, 0, "-(c-b)z", 1, 0, 1);
			Add2(G[1][0], "a-1", "c-a", -1, 0, 0, "-(c-1)", -1, 0, -1);
			Add2(G[1][0], "c-1-a", "-(c-1-a-b)", 1, 0, 0, "b(z-1)", 1, 1, 0);
			Add2(G[1][0], "-(c-1-b)z", "(c-1)(z-1)", 0, 0, -1, "c-1", -1, 0, -1);
			Add2(G[1][0], "c-1-a", "(b-1-a)(z-1)", 1, 0, 0, "-(c-b)", 1, -1, 0);
			Add2(G[1][0], "(a-1)(z-1)", "-(c-a-b+1)", -1, 0, 0, "c-b", -1, -1, 0);
			Add2(G[1][0], "c(z-1)", "a-1-(c-b)z", 0, 0, 1, "c-a+1", -1, 0, 1);
			Add2(G[1][0], "c-1-a", "a-(c-1-b)z", 1, 0, 0, "(c-1)(z-1)", 1, 0, -1);
			Add2(G[1][0], "c(a-1)(z-1)", "c(a-1-(c-b)z)", -1, 0, 0, "(c-a+1)(c-b)z", -1, 0, 1);
			Add2(G[1][0], "(c-1-a)(c-1-b)z", "(c-1)(a-(c-1-b)z)", 0, 0, -1, "a(c-1)(z-1)", 1, 0, -1);
			//1.1
			Add2(G[1][1], "-(c-1-a+(a-b)z)", "a(z-1)", 1, 0, 0, "c-1", -1, 0, -1);
			Add2(G[1][1], "-c(c-a+(a-b)z)", "c(c-a)", -1, 0, 0, "a(c-b)z", 1, 0, 1);
			Add2(G[1][1], "c(c-1-(c-a)z)", "(c-a)(c-b)z", 0, 0, 1, "-c(c-1)", -1, 0, -1);
			Add2(G[1][1], "c(c-1-(c-1-a)z)", "c(c-1)(z-1)", 0, 0, -1, "-a(c-b)z", 1, 0, 1);
			Add2(G[1][1], "b(c-a)+(a-1-b)(a+(b-a)z)", "a(a-1-b)(z-1)", 1, 0, 0, "-b(c-a)", -1, 1, 0);
			Add2(G[1][1], "-(a(c-b)+(b-1-a)(c-a+(a-b)z))", "(b-1-a)(c-a)", -1, 0, 0, "a(c-b)", 1, -1, 0);
			Add2(G[1][1], "a(c-a-b)-(c-1-b)(c-a-bz)", "(c-a)(c-1-a-b)", -1, 0, 0, "ab(z-1)(z-1)", 1, 1, 0);
			Add2(G[1][1], "-((c-a)(c-a-b)+(b-1)(a-(c-b)z))", "a(c-a-b+1)(z-1)", 1, 0, 0, "(c-a)(c-b)", -1, -1, 0);
			Add2(G[1][1], "(c-1-a)(c-a-b)z+(a+(b-a)z)(a-1-(c-b-1)z)", "(c-a)(a-(c-b-1)z)", -1, 0, 0, "-a(c-1)(z-1)(z-1)", 1, 0, -1);
			Add2(G[1][1], "c((a-(c-b)z)(a-(c-1-b)z)-a(c-1-a)(z-1))", "(c-a)(c-b)(a-(c-b-1)z)z", 0, 0, 1, "-ac(c-1)(z-1)(z-1)", 1, 0, -1);
			Add2(G[1][1], "c((a-1-(c-1-b)z)(a-1-(c-b)z)-(c-a)(a-1)(z-1))", "c(c-1)(a-1-(c-b)z)(z-1)", 0, 0, -1, "-(c-a)(c-b)(c-a+1)z", -1, 0, 1);
			Add2(G[1][1], "-c((a-1)(a-bz)+((c-a-b)(c-a-b+1)+(b-a)(a-1-(c-b)(z-1)))z)", "-ac(a-1-(c-b)z)(z-1)", 1, 0, 0, "(c-b)(c-a)(c-a+1)z", -1, 0, 1);
			//1.2
			Add2(G[1][2], "-c", "c", -1, 0, 0, "bz", 0, 1, 1);
			Add2(G[1][2], "-(c-1-a)", "a(z-1)", 1, 0, 0, "c-1", 0, -1, -1);
			Add1(G[1][2], "c(z-1)", "-(c-a-b+1)z", 0, 0, 1, "c", -1, -1, 0);
			Add2(G[1][2], "a-1+(b-a)z", "c-a", -1, 0, 0, "-(c-1)", 0, -1, -1);
			Add2(G[1][2], "c(a+(b-a)z)", "ac(z-1)", 1, 0, 0, "-b(c-a)z", 0, 1, 1);
			Add2(G[1][2], "-c(a+(b-1-a)z)", "(c-a)(b-1-a)z", 0, 0, 1, "ac", 1, -1, 0);
			Add1(G[1][2], "(c-1-a)(c-1-b)", "-(c-1)(c-1-a-b)", 0, 0, -1, "ab(z-1)", 1, 1, 0);
			Add2(G[1][2], "(c-1-a)(b+(a-b)z)", "a(b-(c-1-a)z)", 1, 0, 0, "b(c-1)(z-1)", 0, 1, -1);
			Add2(G[1][2], "(c-1-a)(b-1-(b-1-a)z)", "(c-1)(b-1-a)(z-1)", 0, 0, -1, "-a(c-b)", 1, -1, 0);
			Add2(G[1][2], "c(b-1+(a-b)z)(z-1)", "c(b-1-(c-a)z)", -1, 0, 0, "(c-b)(c-b+1)z", 0, -1, 1);
			Add1(G[1][2], "(a-1)(b-1)(z-1)-(c-a-b)(c-a-b+1)z", "(c-1)(c-a-b+1)(z-1)", 0, 0, -1, "(c-a)(c-b)", -1, -1, 0);
			Add1(G[1][2], "c(ab(z-1)-(c-1-a-b)(c-a-b)z)", "(c-a)(c-b)(c-1-a-b)z", 0, 0, 1, "abc(z-1)(z-1)", 1, 1, 0);
			Add2(G[1][2], "(c-a-b)(b-(c-1-a)z)+b(c-1-b)(z-1)", "-(c-a)(b-(c-1-a)z)", -1, 0, 0, "b(c-1)(z-1)(z-1)", 0, 1, -1);
			Add2(G[1][2], "c(a(b-1)(z-1)-(c-a-b)(c-a-b+1)z)", "-ac(b-1-(c-a)z)(z-1)", 1, 0, 0, "(c-a)(c-b)(c-b+1)z", 0, -1, 1);
			//2.0
			Add2(G[2][0], "c", "-c", 1, 0, 0, "bz", 1, 1, 1);
			Add2(G[2][0], "(a-1)(z-1)", "-(c-a)", -1, 0, 0, "c-1", -1, -1, -1);
			Add2(G[2][0], "c-1-a", "a+(b-1-a)z", 1, 0, 0, "-(c-1)", 1, -1, -1);
			Add1(G[2][0], "-(c-a-b)z", "(c-1)(z-1)", 0, 0, -1, "c-1", -1, -1, -1);
			Add1(G[2][0], "-c(c-a-b)", "(c-a)(c-b)", 0, 0, 1, "ab(z-1)", 1, 1, 1);
			Add2(G[2][0], "c(a-1)(z-1)", "c(a-1-(a-b-1)z)", -1, 0, 0, "-b(c-a+1)z", -1, 1, 1);
			Add2(G[2][0], "(c-1-a)(b-1-a)z", "-(c-1)(a+(b-1-a)z)", 0, 0, -1, "a(c-1)", 1, -1, -1);
			Add2(G[2][0], "c(b-1-a)(z-1)", "(c-a)(b-1-(b-1-a)z)", 0, 0, 1, "-a(c-b+1)", 1, -1, 1);
			Add2(G[2][0], "(a-1)(b-(c-a)z)", "(c-a)(b+(a-1-b)z)", -1, 0, 0, "b(c-1)(z-1)", -1, 1, -1);
			Add2(G[2][0], "c(b-1-(c-1-a)z)", "c(b-1-(b-1-a)z)(z-1)", 1, 0, 0, "(c-b)(c-b+1)z", 1, -1, 1);
			Add1(G[2][0], "c(c-a-b+2)(z-1)", "(a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z", 0, 0, 1, "(c-a+1)(c-b+1)", -1, -1, 1);
			Add2(G[2][0], "(c-1-a)(b-(c-2-a)z)", "(c-2-a-b)(c-1-a-b)z-ab(z-1)", 1, 0, 0, "-b(c-1)(z-1)(z-1)", 1, 1, -1);
			Add1(G[2][0], "(c-1-a)(c-1-b)(c-2-a-b)z", "-(c-1)((c-2-a-b)(c-1-a-b)z-ab(z-1))", 0, 0, -1, "ab(c-1)(z-1)(z-1)", 1, 1, -1);
			Add2(G[2][0], "-c(a-1)(b-1-(c-a+1)z)(z-1)", "c((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)", -1, 0, 0, "(c-b)(c-a+1)(c-b+1)z", -1, -1, 1);
			//2.1
			Add1(G[2][1], "c(c-1)", "-c(c-1)", 0, 0, -1, "abz", 1, 1, 1);
			Add2(G[2][1], "-(c-a-1-(b-1)z)", "a(z-1)", 1, 0, 0, "c-1", -1, -1, -1);
			Add2(G[2][1], "-c(c-a-bz)", "c(c-a)", -1, 0, 0, "ab(z-1)z", 1, 1, 1);
			Add1(G[2][1], "c(c-1)(z-1)", "-(c-a)(c-b)z", 0, 0, 1, "c(c-1)", -1, -1, -1);
			Add2(G[2][1], "b(c-1-a)+(b-a)(a-1-b)z", "a(b+(a-1-b)z)", 1, 0, 0, "-b(c-1)", -1, 1, -1);
			Add2(G[2][1], "(a-1+(b-a)z)(a+(b-1-a)z)-(c-1-a)(b-1)z", "(c-a)(a+(b-1-a)z)", -1, 0, 0, "a(c-1)(z-1)", 1, -1, -1);
			Add2(G[2][1], "c((b-1)(c-a)+(a-b)(b-1-a)z)(z-1)", "c(c-a)(b-1-(b-a-1)z)", -1, 0, 0, "-a(c-b)(c-b+1)z", 1, -1, 1);
			Add2(G[2][1], "c((a-1-(a-1-b)z)(a+(b-a)z)-b(c-a)z)", "ac(a-1-(a-1-b)z)(z-1)", 1, 0, 0, "b(c-a)(c-a+1)z", -1, 1, 1);
			Add2(G[2][1], "c((a-(c-b)z)(a+(b-1-a)z)-a(c-1-a)(z-1))", "(c-a)(c-b)(a+(b-1-a)z)z", 0, 0, 1, "ac(c-1)(z-1)", 1, -1, -1);
			Add2(G[2][1], "c((c-1-(c-1-b)z)(a-1-(a-1-b)z)-b(c-a)z)", "c(c-1)(a-1-(a-b-1)z)(z-1)", 0, 0, -1, "b(c-a)(c-a+1)z", -1, 1, 1);
			Add2(G[2][1], "(c-1-a-b)(2ab(z-1)-(c-2-a-b)(c-a-bz))z+ab(a-1)(z-1)(z-1)", "(c-a)((c-2-a-b)(c-1-a-b)z-ab(z-1))", -1, 0, 0, "ab(c-1)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add1(G[2][1], "-c((c-1-a-b)(c-2-a-b)(c-a-b)zz-ab(2(c-1-a-b)z+(c-1)(z-1))(z-1))", "(c-a)(c-b)((c-2-a-b)(c-1-a-b)z-ab(z-1))z", 0, 0, 1, "abc(c-1)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add1(G[2][1], "-c((c-a-b)(c-a-b+1)(c-a-b+2)zz-(a-1)(b-1)(2(c-a-b+1)z+(c-1)(z-1))(z-1))", "c(c-1)((c-a-b+1)(c-a-b+2)z-(a-1)(b-1)(z-1))(z-1)", 0, 0, -1, "(c-a)(c-b)(c-a+1)(c-b+1)z", -1, -1, 1);
			Add2(G[2][1], "-c((b-1)(c-b)(a-1-(c-b+1)z)(z-1)+(c-a-b)((c-a-b+1)(c-a-b+2)z-(a-1)(b-1)(z-1)))", "ac((c-a-b+1)(c-a-b+2)z-(a-1)(b-1)(z-1))(z-1)", 1, 0, 0, "(c-a)(c-b)(c-a+1)(c-b+1)z", -1, -1, 1);
			//3.0
			Add2(G[3][0], "c(c-a-b+1+(a-b)(z-1))", "-(c-a)(c-a+1)", -1, 0, 1, "a(a-1-(c-b)z)", 1, 0, 1);
			Add2(G[3][0], "-(a-1)(c-a-b+1-(a-1-b)(z-1))", "b(c-a-b+1)", -1, 1, 0, "(c-b)(a-1-b)", -1, -1, 0);
			Add2(G[3][0], "c((c-1-a-b)z+(c-1)(z-1))", "c(c-1)(z-1)(z-1)", 1, 0, -1, "(c-b)(a-(c-1-b)z)z", 1, 0, 1);
			Add2(G[3][0], "(c-1-a)(c-1-a-b+(b-1-a)(z-1))", "-(c-b)(c-1-a-b)", 1, -1, 0, "b(b-1-a)(z-1)(z-1)", 1, 1, 0);
			Add2(G[3][0], "-c(a-1)(c-1-(c-a+c-b)z)", "(c-a)(c-a+1)(c-b)z", -1, 0, 1, "c(c-1)(a-1-(c-b)z)", -1, 0, -1);
			Add2(G[3][0], "(c-1-b)(c-1-a-b+(a-b)(z-1))z", "-a(c-1)(z-1)(z-1)", 1, 0, -1, "(c-1)(a-(c-1-b)z)", -1, 0, -1);
			//3.1
			Add2(G[3][1], "c(c-1+(a-b)z)", "-a(c-b)z", 1, 0, 1, "-c(c-1)", -1, 0, -1);
			Add1(G[3][1], "(b-a)(c-a(c-b)-b(c-a)-(b-1-a)(a-1-b)z)", "-a(c-b)(a-1-b)", 1, -1, 0, "b(c-a)(b-1-a)", -1, 1, 0);
			Add1(G[3][1], "-(c-a-b)((c-1-a-b)(c-(a+b-1)z)-2ab(z-1))", "ab(c-a-b+1)(z-1)(z-1)", 1, 1, 0, "(c-a)(c-b)(c-1-a-b)", -1, -1, 0);
			Add2(G[3][1], "c((z-1)(c-a)(c-b)(a-(c-1-b)z)z-((a-(c-b)z)(a-(c-1-b)z)-a(c-1-a)(z-1))(a-1-(c-b)z))", "(c-a)(c-b)(c-a+1)(a-(c-1-b)z)z", -1, 0, 1, "ac(c-1)(a-1-(c-b)z)(z-1)(z-1)", 1, 0, -1);
			//3.2
			Add2(G[3][2], "c", "(b-1-a)z", 1, 0, 1, "-c", 1, -1, 0);
			Add1(G[3][2], "(a-b)z", "-(c-1)", -1, 0, -1, "c-1", 0, -1, -1);
			Add1(G[3][2], "c(a-b)", "-a(c-b)", 1, 0, 1, "b(c-a)", 0, 1, 1);
			Add2(G[3][2], "-(a-1)(c-1-b)", "(c-1)(a-1-b)", -1, 0, -1, "b(c-a)", -1, 1, 0);
			Add2(G[3][2], "c(c-a-b+1)", "a(b-1-(c-a)z)", 1, 0, 1, "-(c-a)(c-b+1)", 0, -1, 1);
			Add2(G[3][2], "-(c-1-a)(c-1-b)", "(c-1)(c-1-a-b)", 1, 0, -1, "b(a-(c-1-b)z)", 1, 1, 0);
			Add2(G[3][2], "(a-1)(b-1-(c-a)z)", "(c-1)(c-a-b+1)", -1, 0, -1, "-(c-a)(c-b)", -1, -1, 0);
			Add2(G[3][2], "c(b-(c-1-a)z)", "(c-b)(c-1-a-b)z", 1, 0, 1, "-bc(z-1)(z-1)", 1, 1, 0);
			Add2(G[3][2], "-c(a-1)(z-1)(z-1)", "(c-a+1)(c-a-b+1)z", -1, 0, 1, "c(a-1-(c-b)z)", -1, -1, 0);
			Add2(G[3][2], "(c-1-b)(c-1-a-b)z", "(c-1)(b-(c-1-a)z)", -1, 0, -1, "-b(c-1)(z-1)(z-1)", 0, 1, -1);
			Add1(G[3][2], "c(a-b)(z-1)(z-1)", "(c-a+1)(b-1-(c-a)z)", -1, 0, 1, "-(c-b+1)(a-1-(c-b)z)", 0, -1, 1);
			Add2(G[3][2], "c(a-1)(b-(c-a+1)z)", "(c-b)(c-a+1)(a-1-b)z", -1, 0, 1, "-bc(a-1-(c-b)z)", -1, 1, 0);
			Add1(G[3][2], "(a-b)(c-1-a)(c-1-b)z", "a(c-1)(b-(c-1-a)z)", 1, 0, -1, "-b(c-1)(a-(c-1-b)z)", 0, 1, -1);
			Add2(G[3][2], "-(c-1-a)(b-1-(c-2-a)z)", "(c-1)(b-1-a)(z-1)(z-1)", 1, 0, -1, "(c-b)(a-(c-1-b)z)", 1, -1, 0);
			//3.3
			Add2(G[3][3], "-c(c-1)", "a(c-b)z", 1, 0, 1, "c(c-1)", 0, -1, -1);
			Add2(G[3][3], "-c(c-a-(b-1)z)", "a(c-a-b+1)z", 1, 0, 1, "c(c-a)", -1, -1, 0);
			Add2(G[3][3], "-(c-1-b)(c-1-a-bz)", "(c-1)(c-1-a-b)", -1, 0, -1, "ab(z-1)(z-1)", 1, 1, 0);
			Add2(G[3][3], "-((b-1)(c-1-a)-(b-1-a)(b-a)z)", "(c-1)(b-1-a)", -1, 0, -1, "a(c-b)", 1, -1, 0);
			Add2(G[3][3], "c(b(c-a)-(a-1-b)(a-b)z)", "a(c-b)(a-1-b)z", 1, 0, 1, "-bc(c-a)", -1, 1, 0);
			Add2(G[3][3], "-c(ab-(c-1-b)(bzz-a(z-1)(z-1)))", "b(c-a)(a-(c-1-b)z)z", 0, 1, 1, "ac(c-1)(z-1)(z-1)", 1, 0, -1);
			Add2(G[3][3], "c((a-1)(c-a)(z-1)-(a-1+(b-a)z)(a-1-(c-b)z))", "(c-a)(c-a+1)(c-b)z", -1, 0, 1, "c(c-1)(a-1-(c-b)z)", 0, -1, -1);
			Add2(G[3][3], "c((b-1)(a-1-(c-b)z)+(a-1+(b-a)z)(b-1-a)(z-1))", "(c-a)(c-a+1)(b-1-a)z", -1, 0, 1, "-ac(a-1-(c-b)z)", 1, -1, 0);
			Add2(G[3][3], "(c-1-b)((a-1-b)(bz-a(z-1))(z-1)-b(a-(c-1-b)z))", "a(a-1-b)(c-1)(z-1)(z-1)", 1, 0, -1, "b(c-a)(a-(c-1-b)z)", -1, 1, 0);
			Add2(G[3][3], "c((c-1-a-b)(c-a-b+1)(c-a)z-b(a-1-(c-b)z)(a-(c-1-b)z))", "-(c-a+1)(c-1-a-b)(c-a)(c-b)z", -1, 0, 1, "abc(a-1-(c-b)z)(z-1)(z-1)", 1, 1, 0);
			Add2(G[3][3], "a(c-1-a)(c-a-b+1)(z-1)+((c-a-b)(c-a)+(b-1)(a-(c-b)z))(a-(c-1-b)z)", "a(c-1)(c-a-b+1)(z-1)(z-1)", 1, 0, -1, "-(c-a)(c-b)(a-(c-1-b)z)", -1, -1, 0);
			Add2(G[3][3], "c(b(c-1-b)(a-1-(c-b)z)(z-1)+(b(a-1)(z-1)-(c-b-a+1)(c-b-a)z)(b-(c-1-a)z))", "(c-a)(c-b)(c-a+1)(b-(c-1-a)z)z", -1, 0, 1, "bc(c-1)(a-1-(c-b)z)(z-1)(z-1)", 0, 1, -1);
			//4.0
			Add2(G[4][0], "c", "-(c-b)", 1, 0, 1, "b(z-1)", 1, 1, 1);
			Add2(G[4][0], "(a-1)z", "-(c-1)", -1, 0, -1, "c-1", -1, -1, -1);
			Add1(G[4][0], "c", "c(z-1)", 1, 1, 0, "-(c-1-b-a)z", 1, 1, 1);
			Add2(G[4][0], "-c", "b-1-(b-1-a)z", 1, 0, 1, "c-b+1", 1, -1, 1);
			Add2(G[4][0], "-c(b-1)", "c(b-1-(b-1-a)z)", 1, -1, 0, "(c-b+1)(b-1-a)z", 1, -1, 1);
			Add1(G[4][0], "(a-1)(b-1)(z-1)", "(c-a)(c-b)", -1, -1, 0, "-(c-1)(c-a-b+1)", -1, -1, -1);
			Add2(G[4][0], "-(a-1)(c-1-b)z", "(c-1)(b+(a-1-b)z)", -1, 0, -1, "b(c-1)(z-1)", -1, 1, -1);
			Add2(G[4][0], "c(a-1)(z-1)", "(c-b)(bz-(a-1)(z-1))", -1, 0, 1, "b(a-1-(c-b)z)", -1, 1, 1);
			Add2(G[4][0], "(b-1)(c-1-a)", "-(c-b)(a+(b-1-a)z)", 1, -1, 0, "(c-1)(b-1-a)(z-1)", 1, -1, -1);
			Add2(G[4][0], "(c-a-2)(c-1-a)z", "(c-1)(a+(b-1-a)z)(z-1)", 1, 0, -1, "(c-1)(a-(c-1-b)z)", 1, -1, -1);
			Add1(G[4][0], "(c-1-a)(c-1-b)", "-((c-2-a-b)(c-1-a-b)z-ab(z-1))", 1, 1, 0, "(c-1)(c-1-a-b)(z-1)", 1, 1, -1);
			Add2(G[4][0], "c(a-1)(z-1)(z-1)", "(a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z", -1, 0, 1, "-(c-b+1)(a-1-(c-b)z)", -1, -1, 1);
			Add2(G[4][0], "-(c-1-a)(c-1-b)(c-a-2)z", "(c-1)((c-1-a-b)(c-a-b-2)z-ab(z-1))", 1, 0, -1, "b(c-1)(a-(c-1-b)z)(z-1)", 1, 1, -1);
			Add1(G[4][0], "c(a-1)(b-1)(z-1)(z-1)", "c((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)", -1, -1, 0, "(c-a+1)(c-b+1)(c-a-b+1)z", -1, -1, 1);
			//4.1
			Add2(G[4][1], "c(c-b+(b-1-a)z)", "-c(c-b)", 1, -1, 0, "b(b-1-a)(z-1)z", 1, 1, 1);
			Add2(G[4][1], "-(a-1)(c-1-b-(a-1-b)z)", "b(c-a)", -1, 1, 0, "(c-1)(a-1-b)", -1, -1, -1);
			Add2(G[4][1], "c(c-1-(c-1-b)z)", "c(c-1)(z-1)", 1, 0, -1, "b(a-(c-1-b)z)z", 1, 1, 1);
			Add2(G[4][1], "c(c-1-(c-b)z)", "-(c-b)(a+(b-1-a)z)z", 1, 0, 1, "c(c-1)(z-1)", 1, -1, -1);
			Add2(G[4][1], "(c-1-a)(c-1-b+(b-1-a)z)", "b(a+(b-1-a)z)(z-1)", 1, 1, 0, "-(c-1)(c-1-a-b)", 1, -1, -1);
			Add2(G[4][1], "c(a-1)(c-1-(c-1-b)z)", "-c(c-1)(a-1-(a-b-1)z)", -1, 0, -1, "b(c-a)(c-a+1)z", -1, 1, 1);
			Add2(G[4][1], "c(a-1)(c-1-(c-b)z)(z-1)", "(c-a)(c-a+1)(c-b)z", -1, 0, 1, "c(c-1)(a-1-(c-b)z)", -1, -1, -1);
			Add2(G[4][1], "-(a(c-1-b)+(b-1-a)(c-1-a+(a-b)z))z", "(c-1)(a+(b-1-a)z)", -1, 0, -1, "a(c-1)(z-1)", 1, -1, -1);
			Add2(G[4][1], "-c(b(c-b)-(a-1-b)(a-b)(z-1))", "a(c-b)(a-1-(a-1-b)z)", 1, 0, 1, "b(c-a)(c-a+1)", -1, 1, 1);
			Add2(G[4][1], "c(b-1)(c-a-(b-1-a)z)(z-1)", "c(c-a)(b-1-(b-1-a)z)", -1, -1, 0, "-a(c-b+1)(c-a-b+1)z", 1, -1, 1);
			Add2(G[4][1], "(b(c-a-b)-(c-1-a)(c-1-b-(a-1)z))z", "b(c-1)(z-1)(z-1)", 0, 1, -1, "-(c-1)(b-(c-1-a)z)", -1, -1, -1);
			Add2(G[4][1], "c((c-a-b)(c-a-b+1)-a(c-a)(z-1))", "-(c-b+1)(c-a)(c-b)", 0, -1, 1, "ab(z-1)(b-1-(c-a)z)", 1, 1, 1);
			Add2(G[4][1], "(c-1-b)(b(c-1-a)+(a-1-b)(a+(b-a)z))z", "a(c-1)(b+(a-1-b)z)(z-1)", 1, 0, -1, "b(c-1)(a-(c-1-b)z)", -1, 1, -1);
			Add2(G[4][1], "c((c-b)(a-1)-(a-1-b)(b-1+(a-b)z))(z-1)", "(c-b)(c-b+1)(a-1-(a-1-b)z)", 0, -1, 1, "b(c-a+1)(b-1-(c-a)z)", -1, 1, 1);
			Add2(G[4][1], "(b-1)((c-a-b+1)(a-(c-b)z)+(c-b)(a+(b-1-a)z)(z-1))", "(c-a)(c-b)(a+(b-1-a)z)", -1, -1, 0, "a(c-1)(c-b-a+1)(z-1)", 1, -1, -1);
			Add2(G[4][1], "c((c-a-b+2)(c-b-(a-1)z)-(a-1)(b-1)(z-1))", "b((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)", 0, 1, 1, "-(c-b)(c-a+1)(c-b+1)", -1, -1, 1);
			Add2(G[4][1], "-(c-1-a)((c-2-a-b)(c-1-b-az)-ab(z-1))z", "(c-1)((c-2-a-b)(c-1-a-b)z-ab(z-1))", 0, -1, -1, "ab(c-1)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add2(G[4][1], "c((c-1-b)(a-1-(a-1-b)z)(z-1)+(a-1-(c-1-b)z)(c-1-a-b))", "ac(a-1-(a-1-b)z)(z-1)(z-1)", 1, 1, 0, "(c-a)(c-a+1)(c-1-a-b)z", -1, 1, 1);
			Add2(G[4][1], "c(b-1)((z-1)(c-b)(a+(b-1-a)z)-(a-(c-b)z)(b-1-(c-a)z))", "(c-a)(c-b)(c-b+1)(a+(b-1-a)z)z", 0, -1, 1, "-ac(c-1)(b-1-(c-a)z)(z-1)", 1, -1, -1);
			Add2(G[4][1], "c((c-1-b)(a-1-(a-1-b)z)(z-1)-(a-1-(c-1-b)z)(b-(c-1-a)z))", "c(c-1)(a-1-(a-1-b)z)(z-1)(z-1)", 0, 1, -1, "-(c-a)(c-a+1)(b-(c-1-a)z)z", -1, 1, 1);
			Add2(G[4][1], "c((c-2-a-b)(c-1-a-b)z-(ab+(c-1-b)(a-(c-2-b)z))(z-1))", "-(c-a)((c-2-a-b)(c-1-a-b)z-ab(z-1))z", 0, 1, 1, "ac(c-1)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add2(G[4][1], "(c-1-b)((c-2-a-b)(c-1-a-b)z-(ab+(a-1-b)(a-(c-2-b)z))(z-1))", "-(c-a)((c-2-a-b)(c-1-a-b)z-ab(z-1))", -1, 1, 0, "a(c-1)(a-1-b)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add2(G[4][1], "c(b-1)((c-a-b+1)(c-a-b+2)z-(a(a-1)+(b-1-a)(c-b+1)z)(z-1))", "ac((a-1)(b-1)(z-1)-(c-b-a+1)(c-b-a+2)z)", 1, -1, 0, "-(c-a)(c-a+1)(c-b+1)(b-1-a)z", -1, -1, 1);
			Add2(G[4][1], "c(b-1)((c-a-b+1)(c-a-b+2)z-((a-1)(b-1)+(c-b)(a-1-(c-b+1)z))(z-1))", "c(c-1)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)", 0, -1, -1, "(c-a)(c-b)(c-a+1)(c-b+1)z", -1, -1, 1);
			//4.2
			Add2(G[4][2], "-c(c-1-bz)", "c(c-1)", -1, 0, -1, "ab(z-1)z", 1, 1, 1);
			Add2(G[4][2], "-c(c-1-(b-1)z)", "a(c-b)z", 1, 0, 1, "c(c-1)", -1, -1, -1);
			Add1(G[4][2], "-((c-1-a)(c-1-b)-(ab+(a+b-1)(c-1-a-b))z)", "ab(z-1)(z-1)", 1, 1, 0, "(c-1)(c-1-a-b)", -1, -1, -1);
			Add1(G[4][2], "-c((c-a)(c-b)-(ab+(c-a-b)(a+b-1))z)", "c(c-a)(c-b)", -1, -1, 0, "ab(c-a-b+1)(z-1)z", 1, 1, 1);
			Add2(G[4][2], "c(b(c-1-b)(z-1)-(b+(a-b)z)(b+(a-1-b)z))", "a(c-b)(b+(a-1-b)z)z", 1, 0, 1, "-bc(c-1)(z-1)", -1, 1, -1);
			Add2(G[4][2], "c((b-1+(a-b)z)(b-1-(b-a-1)z)-(b-1)(c-b)(z-1))", "-c(c-1)(b-1-(b-a-1)z)", -1, 0, -1, "a(c-b)(c-b+1)z", 1, -1, 1);
			Add2(G[4][2], "a(c-1-a)(b+(a-1-b)z)-(b(c-1-a)+(b-a)(a-1-b)z)(b-1-a)(z-1)", "-a(c-b)(b+(a-1-b)z)", 1, -1, 0, "b(b-1-a)(c-1)(z-1)", -1, 1, -1);
			Add2(G[4][2], "-c((c-a)(b+(a-1-b)z)(b-1-(b-1-a)z)+(c-b)(b-1-a)(a-1-b)(z-1)z)", "bc(c-a)(b-1-(b-1-a)z)", -1, 1, 0, "a(c-b)(c-b+1)(a-1-b)z", 1, -1, 1);
			Add2(G[4][2], "c((c-b)(a-1+(b-a)z)(a+(b-1-a)z)(z-1)-(b-1)(a-(c-b)z)(a-1-(c-b)z))", "(c-a)(c-a+1)(c-b)(a+(b-1-a)z)z", -1, 0, 1, "-ac(c-1)(a-1-(c-b)z)(z-1)", 1, -1, -1);
			Add2(G[4][2], "c((c-1-b)(a-1-(a-1-b)z)(a+(b-a)z)(z-1)-b(a-1-(c-1-b)z)(a-(c-1-b)z))", "ac(c-1)(a-1-(a-1-b)z)(z-1)(z-1)", 1, 0, -1, "-b(c-a)(c-a+1)(a-(c-1-b)z)z", -1, 1, 1);
			Add1(G[4][2], "(3ab(z-1)-(c-2-a-b)(c-(a+b-1)z))(c-1-a-b)(c-a-b)z-ab(a-1)(b-1)(z-1)(z-1)", "(c-a)(c-b)((c-2-a-b)(c-1-a-b)z-ab(z-1))", -1, -1, 0, "ab(c-1)(c-a-b+1)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add1(G[4][2], "c(ab(a-1)(b-1)(z-1)(z-1)+((c+1-(a+b-1)z)(c-1-a-b)-3ab(z-1))(c-a-b)(c-a-b+1)z)", "abc((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)(z-1)(z-1)", 1, 1, 0, "-(c-a)(c-b)(c-a+1)(c-b+1)(c-1-a-b)z", -1, -1, 1);
			Add2(G[4][2], "c((b(a-1)(z-1)-(c-b-a+1)(c-b-a)z)((c-1-b-a)(c-2-b-a)z-ab(z-1))+b(c-1-b)(a-(c-2-b)z)(a-1-(c-b)z)(z-1))", "(c-a)(c-b)(c-a+1)((c-2-a-b)(c-1-a-b)z-ab(z-1))z", -1, 0, 1, "-abc(c-1)(a-1-(c-b)z)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add2(G[4][2], "c(((c-a-b)(a-(c-1-b)z)+a(c-1-a)(z-1))((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)-(b-1)(c-b)(a-1-(c-b+1)z)(a-(c-1-b)z)(z-1))", "ac(c-1)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)(z-1)(z-1)", 1, 0, -1, "(c-a)(c-b)(c-a+1)(c-b+1)(a-(c-1-b)z)z", -1, -1, 1);
			//5.0
			Add2(G[5][0], "-c(c-a-b+(a-1-b)(z-1))", "(c-a)(c-a+1)", -1, 1, 1, "a(a-1-(a-1-b)z)(z-1)", 1, 1, 1);
			Add2(G[5][0], "-(b-1)(c-a-b-(b-1-a)(z-1))z", "a(c-1)(z-1)", 1, -1, -1, "(c-1)(a+(b-1-a)z)", -1, -1, -1);
			Add1(G[5][0], "-c((c-1)(z-1)+(c-2-a-b)z)", "-c(c-1)(z-1)(z-1)", 1, 1, -1, "((c-2-a-b)(c-1-a-b)z-ab(z-1))z", 1, 1, 1);
			Add2(G[5][0], "c((a-1-(c-1-b)z)(b+(a-1-b)z)-(c-1-b)(a-1-(a-1-b)z)(z-1))", "(c-a)(c-a+1)(b+(a-1-b)z)z", -1, 1, 1, "c(c-1)(a-1-(a-1-b)z)(z-1)", -1, 1, -1);
			Add1(G[5][0], "c(a-1)(b-1)((c-1)(z-1)+(c-a-b+2)z)(z-1)", "(c-a)(c-b)(c-a+1)(c-b+1)z", -1, -1, 1, "c(c-1)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)", -1, -1, -1);
			Add2(G[5][0], "(c-1-a)((b-(c-2-a)z)(a+(b-1-a)z)-((c-2-a-b)(c-1-a-b)z-ab(z-1)))", "(c-1)((c-2-a-b)(c-1-a-b)z-ab(z-1))", 1, -1, -1, "-b(c-1)(a+(b-1-a)z)(z-1)(z-1)", 1, 1, -1);
			Add2(G[5][0], "c((c-a-b+2)(c-b)(a-1-(a-1-b)z)-(a-1-b)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z))(z-1)", "b(c-a+1)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)", -1, 1, 1, "(c-b)(c-a+1)(c-b+1)(a-1-(a-1-b)z)", -1, -1, 1);
			//5.1
			Add2(G[5][1], "c(c-1+(b-1-a)z)", "-c(c-1)", 1, -1, -1, "b(a+(b-1-a)z)z", 1, 1, 1);
			Add2(G[5][1], "c(a-1)(c-1-(a-b-1)z)(z-1)", "-b(c-a)(c-a+1)z", -1, 1, 1, "c(c-1)(a-1-(a-b-1)z)", -1, -1, -1);
			Add1(G[5][1], "(c-1)((c-1-b)(a-1-b)(a+(b-1-a)z)-(c-1-a)(b-1-a)(b+(a-1-b)z))z", "-a(c-1)(c-1)(b+(a-1-b)z)", 1, -1, -1, "b(c-1)(c-1)(a+(b-1-a)z)", -1, 1, -1);
			Add1(G[5][1], "c((c-a)(a-1-b)(b-1-(b-1-a)z)-(c-b)(b-1-a)(a-1-(a-1-b)z))(z-1)", "-b(c-a)(c-a+1)(b-1-(b-1-a)z)", -1, 1, 1, "a(c-b)(c-b+1)(a-1-(a-1-b)z)", 1, -1, 1);
			Add1(G[5][1], "((c-1-a)(c-1-b)(c-2-a-b)(z-1)-(c-a-b)((c-2-a-b)(c-1-a-b)z-ab(z-1)))z", "ab(c-1)(z-1)(z-1)(z-1)", 1, 1, -1, "(c-1)((c-2-a-b)(c-1-a-b)z-ab(z-1))", -1, -1, -1);
			Add1(G[5][1], "c((c-a)(c-b)(c-a-b+2)(z-1)+(c-a-b)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z))", "(c-a)(c-b)(c-a+1)(c-b+1)", -1, -1, 1, "-ab((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)(z-1)", 1, 1, 1);
			Add2(G[5][1], "c((a-1-(c-1-b)z)((c-1-b-a)(c-2-b-a)z-ab(z-1))-(c-1-b)(a-(c-2-b)z)(a-1-(a-1-b)z)(z-1))", "(c-a)(c-a+1)((c-2-b-a)(c-1-b-a)z-ab(z-1))z", -1, 1, 1, "ac(c-1)(a-1-(a-1-b)z)(z-1)(z-1)(z-1)", 1, 1, -1);
			Add2(G[5][1], "c(b-1)((a-(c-b)z)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)+(c-b)(a-1-(c-b+1)z)(a+(b-1-a)z)(z-1))", "ac(c-1)((a-1)(b-1)(z-1)-(c-a-b+1)(c-a-b+2)z)(z-1)", 1, -1, -1, "-(c-a)(c-b)(c-a+1)(c-b+1)(a+(b-1-a)z)z", -1, -1, 1);
			//5.2
			Add1(G[5][2], "-c(c-1-(a+b-1)z)", "ab(z-1)z", 1, 1, 1, "c(c-1)", -1, -1, -1);
			Add2(G[5][2], "c(a(b-1-(c-1-a)z)(b+(a-1-b)z)+((a-b)(b+(a-1-b)z)-b(c-1-b))(b-1-(b-1-a)z)(z-1))", "a(c-b)(c-b+1)(b+(a-1-b)z)z", 1, -1, 1, "bc(c-1)(az-(b-1)(z-1))(z-1)", -1, 1, -1);
			Add1(G[5][2], "-c(((c-b)(c-a-b)(c-a-b+2)z-(a-1)(b-(c-a)z)(b-1-(c-a+1)z))((c-2-a-b)(c-1-a-b)z-ab(z-1))+b(c-1-b)(a-(c-2-b)z)((c-a-b+1)(c-a-b+2)z-(a-1)(b-1)(z-1))(z-1))", "(c-a)(c-b)(c-a+1)(c-b+1)((c-2-a-b)(c-1-a-b)z-ab(z-1))z", -1, -1, 1, "abc(c-1)((c-a-b+1)(c-a-b+2)z-(a-1)(b-1)(z-1))(z-1)(z-1)(z-1)", 1, 1, -1);
			#endregion
			//*/
			#endregion
			//
			#region Find_Else_G
			//
			Func<int, Predicate<Abc>>
				fe1 = k => s => s.A == k || s.B == k || s.C == k,
				fe2 = k => s => s.A == k ? s.B == k || s.C == k : s.B == k && s.C == k,
				fe3 = k => s => s.A == k && s.B == k && s.C == k,
				fn3 = k => s => s.A != k && s.B != k && s.C != k;
			Predicate<Abc> pTrue = s => true, p1M2 = fe1(-2), p2M2 = fe2(-2), p3N0 = fn3(0);
			//
			_shift(G[1][0], G[0][1], 1, 0);
			_shift(G[1][0], G[0][1], 2, 0);
			//
			_combine(G[1][1], G[0][0], G[1][0], pTrue);
			_combine(G[1][1], G[0][1], G[1][0], p1M2);
			_combine(G[1][2], G[0][1], G[1][0], p3N0);
			//
			_shift(G[2][0], G[1][2], 1, 0);
			//
			_combine(G[2][1], G[0][0], G[2][0], pTrue);
			_combine(G[2][1], G[0][1], G[2][0], p1M2);
			//
			_combine(G[3][0], G[1][0], G[1][0], p1M2);
			_combine(G[3][1], G[1][1], G[1][0], p2M2);
			_combine(G[3][2], G[1][0], G[1][0], fn3(-2));
			_combine(G[3][3], G[1][1], G[1][0], p3N0);
			_combine(G[3][3], G[1][2], G[1][0], p3N0);
			//
			_shift(G[4][0], G[1][2], 2, 0);
			_shift(G[4][0], G[2][0], 2, 1);
			//
			_combine(G[4][1], G[1][0], G[2][0], p1M2);
			_combine(G[4][2], G[1][0], G[2][1], p2M2);
			_combine(G[4][2], G[1][1], G[2][0], p2M2);
			_combine(G[4][2], G[1][2], G[2][0], p2M2);
			//
			// // // G[5][0], G[0][0], G[4][0], pTrue);
			_combine(G[5][0], G[2][0], G[2][0], fe2(0));
			_combine(G[5][0], G[4][0], G[4][0], pTrue);
			_combine(G[5][1], G[2][0], G[2][0], p2M2);
			_combine(G[5][2], G[2][1], G[2][0], fe3(-2));
			//*/
			#endregion
			//
			#region Init_Gplus
			Gplus = new MultiCycle<HyperEquation>();
			Add1(Gplus,
				"-(a-1)c((c-(a-1)-(b+1)+1)(c-(a-1)-(b+1))(c-(a-1)-(b+1)-1)z+((b+1)((a-1)-1)(c-1)-((b+1)(c-(b+1)-1)(c-1+c-(a-1)-(b+1))-(c-(a-1)-(b+1))(c-(a-1)-1)(c-(a-1)+1))z)(z-1))", //1, -1, 0,
				"(c-(a-1)+1)(c-(a-1))(c-(a-1)-1)((a-1)(c-(a-1)-(b+1)-1)z+((b+1)((b+1)-1)-((b+1)-(a-1))((b+1)-(a-1)-1)z)(z-1))z", -2, 1, 1,
				"(b+1)c(c-1)(((b+1)-1)(c-(a-1)-(b+1)+1)z+((a-1)((a-1)-1)-((b+1)-(a-1))((b+1)-(a-1)-1)z)(z-1))(z-1)(z-1)", -1, 2, -1);
			#endregion
			//
			#region Init_Fa2czSet
			Fa2czSet = new MultiCycle<HyperEquation>(); // b = 1
			//
			Add1(Fa2czSet, "-(c-2-(a-1)z)", 0, 0, 0, "z-1", 0, 1, 0, "c-1", 0, -1, 0);
			Add1(Fa2czSet, "-a(c-2-(a-1)z)", 1, 0, 0, "c-1-a", 0, 1, 0, "(a-1)(c-1)", 0, -1, 0);
			Add1(Fa2czSet, "-(c-1)(c-2-(a-1)z)", 0, 0, -1, "(c-1-a)z", 0, 1, 0, "(c-2)(c-1)", 0, -1, 0);
			Add1(Fa2czSet, "-a(c-2-(a-1)z)z", 1, 0, 1, "c(z-1)", 0, 1, 0, "c(1+(a-1)z)", 0, -1, 0);
			Add1(Fa2czSet, "-(c-1)(c-2-(a-1)z)", -1, 0, -1, "(a-1)(z-1)z", 0, 1, 0, "(c-1)(c-2)", 0, -1, 0);
			Add1(Fa2czSet, "-(c-a)(c-2-(a-1)z)", -1, 0, 0, "(a-1)(z-1)(z-1)", 0, 1, 0, "(c-1-a)(c-1)", 0, -1, 0);
			Add1(Fa2czSet, "-(c-a)(c-2-(a-1)z)z", 0, 0, 1, "c(z-1)(z-1)", 0, 1, 0, "-c(1-(c-a)z)", 0, -1, 0);
			Add1(Fa2czSet, "-a(c-1)(c-2-(a-1)z)(z-1)", 1, 0, -1, "(c-2-a)(c-1-a)z", 0, 1, 0, "-(c-2)(c-1)(a-(a-1)z)", 0, -1, 0);
			Add1(Fa2czSet, "-(c-a)(c-a+1)(c-2-(a-1)z)z", -1, 0, 1, "c(a-1)(z-1)(z-1)(z-1)", 0, 1, 0, "c((c-1-a)(c-a)z-(a-1)(z-1))", 0, -1, 0);
			//*/
			#endregion
			//
			#region Find_Fa2czSet
			//
			foreach(var multiCycle in new[] { G[0][0], G[1][1], G[2][1],
				G[3][0], G[3][1], G[3][3], G[4][1], G[4][2], G[5][0], G[5][1], G[5][2] }) {
				foreach(var mc in (IEnumerable<HyperEquation>)multiCycle) {
					if(!((mc[1].AbcShift.B == 1 && mc[2].AbcShift.B == -1) || (mc[1].AbcShift.B == -1 && mc[2].AbcShift.B == 1))) continue;
					var h = (mc[1].AbcShift.B == 1 && mc[2].AbcShift.B == -1 ? mc : new HyperEquation(mc[0], mc[2], mc[1]))/*.Simplify()*/;
					h = (h - new Abc(h[1].AbcShift.A, 0, h[1].AbcShift.C))/*.Simplify()*/;
					h = new HyperEquation(new Coef(h[0].StrFunc.Replace('b', '1'), h[0].AbcShift),
										  new Coef(h[1].StrFunc.Replace('b', '1'), h[1].AbcShift), // h[1].AbcShift = new Abc(0, 1, 0)
										  new Coef(h[2].StrFunc.Replace('b', '1'), new Abc(0, -1, 0)), mc);
					var find = Fa2czSet.Link == null ? null : Fa2czSet.Link.Find(m => m.Data.Data[0].AbcShift == h[0].AbcShift);
					if(find == null) Fa2czSet.Add(h);
					else {
						var fdd = find.Data.Data;
						if(fdd.ToString().Length > h.ToString().Length)
							(find.Data.Data = h).FromEquations.Add(fdd.FromEquations);
						else if(fdd.FromEquations == null) fdd.FromEquations = h.FromEquations;
						else fdd.FromEquations.Add(h.FromEquations);
					}
				}
			}//*/
			#endregion
			//
			#region Init_Fa1czSet
			Fa1czSet = new MultiCycle<HyperEquation>[CountG]; // b = 0 | 1
			for(int i = 0; i < CountG; ++i) Fa1czSet[i] = new MultiCycle<HyperEquation>();
			//
			#endregion
			//
			#region Find_Fa1czSet
			//
			#endregion
			//
		}
		private static void Add1(MultiCycle<HyperEquation> g, string s0,
			string s1, int a1, int b1, int c1, string s2, int a2, int b2, int c2) { Add1(g, s0, 0, 0, 0, s1, a1, b1, c1, s2, a2, b2, c2); }
		private static void Add1(MultiCycle<HyperEquation> g, string s0, int a0, int b0, int c0,
			string s1, int a1, int b1, int c1, string s2, int a2, int b2, int c2)
		{
			g.Add((new HyperEquation(
				new Coef(s0, new Abc(a0, b0, c0)),
				new Coef(s1, new Abc(a1, b1, c1)),
				new Coef(s2, new Abc(a2, b2, c2)))).Simplify());
		}
		private static void Add2(MultiCycle<HyperEquation> g, string s0,
			string s1, int a1, int b1, int c1, string s2, int a2, int b2, int c2)
		{
			Add1(g, s0, 0, 0, 0, s1, a1, b1, c1, s2, a2, b2, c2);
			if(a1 == b1 && a2 == b2) return;
			Add1(g, s0.Replace('a', '#').Replace('b', 'a').Replace('#', 'b'), 0, 0, 0,
					s1.Replace('a', '#').Replace('b', 'a').Replace('#', 'b'), b1, a1, c1,
					s2.Replace('a', '#').Replace('b', 'a').Replace('#', 'b'), b2, a2, c2);
		}
		private static void _shift(MultiCycle<HyperEquation> g, IEnumerable<HyperEquation> g1, int i0, int i1)
		{
			var i2 = i0 ^ i1 ^ 3;
			foreach(var v in g1) {
				var vv = (new HyperEquation(v[i0], v[i1], v[i2], v) - v[i0].AbcShift)/*.Simplify()*/;
				var find = g.Link == null ? null : g.Link.Find(m =>
					m.Data.Data[1].AbcShift == vv[1].AbcShift && m.Data.Data[2].AbcShift == vv[2].AbcShift);
				(find == null ? g : find.Data).Add(vv);
			}
			//_minToTop(g);
		}
		private static void _combine(MultiCycle<HyperEquation> g, IEnumerable<HyperEquation> g1, IEnumerable<HyperEquation> g2, Predicate<Abc> pred)
		{
			if(g == null || g1 == null || g2 == null) return;
			foreach(var v2 in g2) // Комбінує пари рівнянь
				foreach(var v1 in g1)
					if((v1[1].AbcShift == v2[1].AbcShift && v1[2].AbcShift != v2[2].AbcShift && pred(v1[2].AbcShift ^ v2[2].AbcShift)) ||
						(v1[2].AbcShift == v2[1].AbcShift && v1[1].AbcShift != v2[2].AbcShift && pred(v1[1].AbcShift ^ v2[2].AbcShift)))
					{
						int i = v1.GetIndex(v2[1].AbcShift);
						var vv = (new HyperEquation(v1[0] * v2[1] - v2[0] * v1[i], v1[i ^ 3] * v2[1], -(v2[2] * v1[i]), v1, v2))/*.Simplify()*/;
						//vv.FromEquations = new MonoCycle<HyperEquation>(v1) { v2 };
						var find = g.Link == null ? null : g.Link.Find(m =>
							(m.Data.Data[1].AbcShift == vv[1].AbcShift && m.Data.Data[2].AbcShift == vv[2].AbcShift) ||
							(m.Data.Data[2].AbcShift == vv[1].AbcShift && m.Data.Data[1].AbcShift == vv[2].AbcShift));
						if(find == null) g.Add(vv);
						else find.Data.Add(find.Data.Data[1].AbcShift == vv[1].AbcShift ? vv :
							new HyperEquation(vv[0], vv[2], vv[1], v1, v2));
					}
			//_minToTop(g);
		}
		/// <summary> Знаходить мінімальну стрічку і міняє місцями з вершиною </summary>
		/// <param name="g"> Вершина </param>
		private static void _minToTop(IEnumerable<MultiCycle<HyperEquation>> g)
		{
			foreach(var v in g) {
				var min = v; int len = v.Data.ToString().Length, c;
				foreach(var vv in (IEnumerable<MultiCycle<HyperEquation>>)v)
					if((c = vv.Data.ToString().Length) < len) { min = vv; len = c; }
				var h = v.Data; v.Data = min.Data; min.Data = h;
			}
		}
		public static List<MultiCycle<HyperEquation>> GetAllInOneList()
		{
			var gg = new List<MultiCycle<HyperEquation>>(CountG);
			foreach(var gi in G)
				foreach(var gij in gi)
					foreach(var v in gij)
						gg.Add(v.Data);
			return gg;
		}
		public static void CheckAll()
		{
			int i = 0, j, k, l, m, n;
			#region CheckAll
			foreach(var gi in G) {
				j = 0;
				foreach(var gj in gi) {
					k = 0;
					foreach(var v1 in gj) {
						l = 0;
						if(v1.Data.Data[0].AbcShift != Abc.Zero)
							throw new Exception("v[0].Shift != 0,  i=" + i + " j=" + j + " k=" + k);
						if(v1.Data.Data[1].AbcShift == Abc.Zero)
							throw new Exception("v[1].Shift == 0,  i=" + i + " j=" + j + " k=" + k);
						if(v1.Data.Data[2].AbcShift == Abc.Zero)
							throw new Exception("v[2].Shift == 0,  i=" + i + " j=" + j + " k=" + k);
						if(v1.Data.Data[1].AbcShift == v1.Data.Data[2].AbcShift)
							throw new Exception("v[1].Shift == v[2].Shift,  i=" + i + " j=" + j + " k=" + k);
						if(v1.Data.Link != null && (v1.Data.Link.Data.Data[0].AbcShift != Abc.Zero ||
							v1.Data.Link.Data.Data[1].AbcShift != v1.Data.Data[1].AbcShift ||
							v1.Data.Link.Data.Data[2].AbcShift != v1.Data.Data[2].AbcShift))
							throw new Exception("v.Shift != v.Link.Shift,  i=" + i + " j=" + j + " k=" + k);
						foreach(var g1 in G) {
							m = 0;
							foreach(var g2 in g1) {
								n = 0;
								foreach(var v2 in (IEnumerable<HyperEquation>)g2) {
									if(v1.Data.Data == v2) continue;
									if((v1.Data.Data[1].AbcShift == v2[1].AbcShift && v1.Data.Data[2].AbcShift == v2[2].AbcShift) ||
									   (v1.Data.Data[1].AbcShift == v2[2].AbcShift && v1.Data.Data[2].AbcShift == v2[1].AbcShift))
									{
										//Console.WriteLine((v1.Data.Data[1].AbcShift + "," + v1.Data.Data[2].AbcShift).Replace(" 0", " ").Replace("+1", "+").Replace("-1", "-"));
										//Console.WriteLine((v2[1].AbcShift + "," + v2[2].AbcShift).Replace(" 0", " ").Replace("+1", "+").Replace("-1", "-"));
										throw new Exception("v1.Shift == v2.Shift,  i=" + i + " j=" + j + " k=" + k + " l=" + l + " m=" + m + " n=" + n);
										//Console.WriteLine(v.ToWolframFullSimplify() + ",");
									}
									++n;
								} ++m;
							} ++l;
						} ++k;
					} ++j;
				} ++i;
			}
			#endregion
			i = 0;
			foreach(var gi in G) foreach(var gij in gi) foreach(var v in gij) ++i;
			if(i != CountG) throw new Exception("Формул " + i + "/" + CountG);
		}
		public static MultiCycle<HyperEquation> GetEquation(Abc shift1, Abc shift2, bool flip = false)
		{
			Predicate<MonoCycle<MultiCycle<HyperEquation>>> pred = mono =>
				mono != null && mono.Data != null &&
				(mono.Data.Data[1].AbcShift == shift1 &&
				mono.Data.Data[2].AbcShift == shift2 ||
				(flip &&
				(mono.Data.Data[1].AbcShift == shift2 &&
				mono.Data.Data[2].AbcShift == shift1)));
			foreach(var gi in G)
				foreach(var gij in gi) {
					var v = gij.Link.Find(pred);
					if(v != null && v.Data != null) return v.Data;
				}
			var vp = Gplus.Link.Find(pred);
			if(vp != null && vp.Data != null) return vp.Data;
			return null;
		}
		/// <exception cref="ArgumentException"></exception>
		public static HyperEquation GetEquation(Abc shift0, Abc shift1, Abc shift2)
		{
			if(shift0 == shift1 || shift1 == shift2 || shift2 == shift0)
				throw new ArgumentException("shift0 == shift1 || shift1 == shift2 || shift2 == shift0");
			var abc = new MonoCycle<Abc>(shift0) { shift1, shift2 };
			MultiCycle<HyperEquation> r;
			HyperEquation h;
			Abc sh1, sh2;
			foreach(var shift in abc) {
				sh1 = shift[1].Data - shift.Data;
				sh2 = shift[2].Data - shift.Data;
				if(System.Math.Abs(sh1.A) > 1 || System.Math.Abs(sh1.B) > 1 || System.Math.Abs(sh1.C) > 1 ||
					System.Math.Abs(sh2.A) > 1 || System.Math.Abs(sh2.B) > 1 || System.Math.Abs(sh2.C) > 1) continue;
				r = GetEquation(sh1, sh2, true); // ?? GetEquation(sh2, sh1);
				if(r == null) continue;
				h = (r.Data + shift.Data).Simplify();
				if(h[0].AbcShift == shift0) {
					if(h[1].AbcShift == shift1) return h;
					if(h[1].AbcShift == shift2) return h.Reverse();
				}
				if(h[0].AbcShift == shift1) {
					if(h[1].AbcShift == shift2) return h >> 1;
					if(h[1].AbcShift == shift0) return new HyperEquation(h[1], h[0], h[2], h.FromEquations); // h.Reverse() >> 1;
				}
				if(h[0].AbcShift == shift2) {
					if(h[1].AbcShift == shift0) return h << 1;
					if(h[1].AbcShift == shift1) return new HyperEquation(h[2], h[1], h[0], h.FromEquations); // h.Reverse() << 1;
				}
			}
			return default(HyperEquation);
		}
		//
		public static Func<ComplexDouble> Fa2c(HyperEquation h, Fabcz fa1c, ComplexDouble a, ComplexDouble c)
		{
			return z => (
				h[2].Calc(a, 0d, c, z) +
				h[0].Calc(a, 0d, c, z) * fa1c(a + h[0].AbcShift.A, 0d, c + h[0].AbcShift.C, z)
				) / -h[1].Calc(a, 1d, c, z);
		}
		public static Func<ComplexDouble> Fa2c(int h, Fabcz fa1c, ComplexDouble a, ComplexDouble c) { return Fa2c(Fa2czSet[h].Data.Data, fa1c, a, c); }
		//
		#endregion
		//
		public MonoLinker<HyperEquation> List;
		public Abc SumAbc
		{
			get
			{
				var sum = new Abc();
				foreach(var he in List) sum += he.Data[2].AbcShift;
				return sum;
			}
		}
		public HyperSet() { }
		public HyperSet(params HyperEquation[] array) : this((IEnumerable<HyperEquation>)array) { }
		public HyperSet(IEnumerable<HyperEquation> collection) { foreach(var he in collection) Add(he); }
		protected HyperSet(MonoLinker<HyperEquation> list) { List = list; }
		//
		public bool AddFirstAble(HyperEquation he)
		{
			return he[0].AbcShift == Abc.Zero && (List == null ||
				List.Data[1].AbcShift == -he[2].AbcShift);
		}
		public bool AddLastAble(HyperEquation he)
		{
			return he[0].AbcShift == Abc.Zero && (List == null ||
				List.GetLast().Data[2].AbcShift == -he[1].AbcShift);
		}
		public bool AddFirst(HyperEquation he)
		{
			if(!AddFirstAble(he)) return false;
			List = new MonoLinker<HyperEquation>(he, List);
			return true;
		}
		public bool AddLast(HyperEquation he)
		{
			if(!AddLastAble(he)) return false;
			if(List == null) List = new MonoLinker<HyperEquation>(he);
			else List.Add(he);
			return true;
		}
		public bool RemoveFirst()
		{
			if(List == null) return false;
			var first = List;
			return (List = List.Link) == null || List.Remove(first);
		}
		public bool RemoveLast()
		{
			if(List == null) return false;
			if(List.Link == null) { List = null; return true; }
			return List.Remove(List.GetLast());
		}
		public void Reverse()
		{
			foreach(var v in List)
				v.Data = new HyperEquation(v.Data[0], v.Data[2], v.Data[1], v.Data.FromEquations);
			List = List.Reverse().Link;
		}
		//
		public void Add(HyperEquation item) { bool b = AddLast(item) || AddFirst(item); }
		public bool Remove(HyperEquation item) { return RemoveLast() || RemoveFirst(); }
		public void Clear() { List = null; }
		public bool Contains(HyperEquation item) { return List.Contains(item); }
		public void CopyTo(HyperEquation[] array, int arrayIndex) { List.CopyTo(array, arrayIndex); }
		public int Count { get { return List == null ? 0 : List.Count; } }
		public bool IsReadOnly { get { return false; } }
		//
		public HyperSet Clone()
		{
			var clone = (HyperSet)MemberwiseClone();
			if(clone.List != null) clone.List = clone.List.GetCopy();
			return clone;
		}
		object ICloneable.Clone() { return Clone(); }
		public MonoLinker<HyperEquation>.MonoEnumerator GetEnumerator() { return new MonoLinker<HyperEquation>.MonoEnumerator(List); }
		IEnumerator<MonoLinker<HyperEquation>> IEnumerable<MonoLinker<HyperEquation>>.GetEnumerator()
		{
			/*foreach(var gi in G)
				foreach(var gij in gi)
					foreach(var v in gij)
						yield return v.Data;
			foreach(var hi in Gplus)
				yield return hi.Data;//*/
			return GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
		IEnumerator<HyperEquation> IEnumerable<HyperEquation>.GetEnumerator() { return GetEnumerator(); }
	}
}
