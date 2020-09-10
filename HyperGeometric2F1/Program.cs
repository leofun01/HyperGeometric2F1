using System;
using System.Collections.Generic;
using HyperGeometric2F1.Linker;
using HyperGeometric2F1.Hypergeometric;

namespace HyperGeometric2F1
{
	public static class Program
	{
		public static void Main()
		{
			var g = HyperSet.G;
			Console.WriteLine("Fa2cz Count = " + HyperSet.Fa2czSet.Count);
			foreach(var v in (IEnumerable<HyperEquation>)HyperSet.Fa2czSet) {
				Console.WriteLine((v[0].AbcShift + "," + v[1].AbcShift + "," + v[2].AbcShift).Replace(" 0", " ").Replace("+1", "+").Replace("-1", "-") + "  " + v);
			}
			Console.WriteLine();
			Console.WriteLine("Press Enter to continue ...");
			Console.ReadLine();
			int i = 0, j;
			foreach(var gi in g) {
				j = 0;
				foreach(var gj in gi) {
					Console.WriteLine(i + "." + (j++) + " : " + gj.Count);
					Console.WriteLine();
					foreach(var v in (IEnumerable<HyperEquation>)gj) {
						/*
						Console.WriteLine(v[0].AbcShift + " -> " + v[0].GetShiftedStr());
						Console.WriteLine(v[1].AbcShift + " -> " + v[1].GetShiftedStr());
						Console.WriteLine(v[2].AbcShift + " -> " + v[2].GetShiftedStr());
						Console.WriteLine();
						//*/
						//Console.WriteLine(v + "\r\n");
						//Console.WriteLine((v[1].AbcShift + "," + v[2].AbcShift).Replace(" 0", " ").Replace("+1", "+").Replace("-1", "-"));
						//Console.WriteLine(v.ToWolframFullSimplify() + ","/* + "\r\n"*/);
						Console.WriteLine(("0," + v[1].AbcShift + "," + v[2].AbcShift).Replace(" 0", " ").Replace("+1", "+").Replace("-1", "-") + "  " + v.ToString().Replace("*", "").Replace("F", "*F"));
					}
					/*
					Console.WriteLine();
					Console.WriteLine("{");
					foreach(var v in (IEnumerable<HyperEquation>)gi) Console.WriteLine(v.FullSimplify() + ",");
					Console.WriteLine("}");
					Console.WriteLine();
					//*/
					Console.WriteLine();
					Console.WriteLine("Press Enter to continue ...");
					Console.ReadLine();
					//*/
				} ++i;
			}
			foreach(var v in (IEnumerable<HyperEquation>)HyperSet.Gplus) {
				Console.WriteLine((v[0].AbcShift + "," + v[1].AbcShift + "," + v[2].AbcShift).Replace(" 0", " ").Replace("+1", "+").Replace("-1", "-") + "  " + v.ToString().Replace("*", "").Replace("F", "*F"));
			}
			Console.WriteLine();
			/*
			Console.WriteLine(g[5][2].Link[0].Data.Data);
			Console.WriteLine();
			foreach(var x in g[5][2].Link[0].Data.Link) {
				if(x.Data.Data.FromEquations == null) continue;
				foreach(var y in x.Data.Data.FromEquations) Console.WriteLine(y.Data);
				Console.WriteLine();
			}
			//*/
			Console.WriteLine("Press Enter to continue ...");
			Console.ReadLine();
			Console.Write("{");
			foreach(var gi in g) {
				foreach(var gj in gi) {
					Console.Write("{");
					foreach(var v in (IEnumerable<HyperEquation>)gj) {
						Console.Write("\r\n" + v.ToWolframFullSimplify() + ","/* + "\r\n"*/);
					}
					Console.Write("\b \r\n},");
				}
			}
			Console.Write("{");
			foreach(var hi in (IEnumerable<HyperEquation>)HyperSet.Gplus) {
				Console.Write("\r\n" + hi.ToWolframFullSimplify() + ","/* + "\r\n"*/);
			}
			Console.Write("\b \r\n},");
			Console.WriteLine("\b}\r\n\nPress Enter to continue ...");
			//*/
			Console.ReadLine();
			Console.Write("{");
			foreach(var hs0 in HyperSet.G) {
				foreach(var hs1 in hs0) {
					foreach(var v in (IEnumerable<MultiCycle<HyperEquation>>)hs1) {
						Console.Write("\r\n" + v.Data.ToWolframFullSimplify() + ","/* + "\r\n"*/);
						foreach(var vv in v) {
							Console.Write("\r\n" + vv.Data.Data.ToWolframFullSimplify() + ","/* + "\r\n"*/);
						}
					}
				}
			}
			Console.WriteLine("\b}\r\n\nPress Enter to EXIT.");
			//*/
			Console.ReadLine();
		}
	}
}
