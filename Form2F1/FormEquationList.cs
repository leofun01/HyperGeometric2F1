using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using HyperGeometric2F1.Hypergeometric;
using HyperGeometric2F1.Linker;

namespace Form2F1
{
	public partial class FormEquationList : FormBase
	{
		private readonly MultiCycle<HyperEquation>[][] _g = HyperSet.G;
		public HyperSet Hset = new HyperSet();
		private int _k = 1;
		public FormEquationList()
		{
			InitializeComponent();
			InitList();
		}
		private void InitList()
		{
			for(int i = 0; i < _g.Length; ++i)
			{
				#region switchs
				int l = 0, m = 0;
				switch(i) {
					case 0:
					case 1:
					case 2: l = 1; break;
					case 3:
					case 4: l = 2; break;
					case 5: l = 3; break;
				}
				switch(i) {
					case 2:
					case 4:
					case 5: m = 3; break;
					case 1:
					case 3: m = 2; break;
					case 0: m = 1; break;
				}
				#endregion
				var node0 = new TreeNode("#" + i + " {0:" + l + ":" + m + "}");
				treeViewList.Nodes.Add(node0);
				for(int j = 0; j < _g[i].Length; ++j)
				{
					var g = _g[i][j];
					var node1 = new TreeNode("#" + i + "." + j + " {0," + (g.Data[1].StrFunc + "," + g.Data[2].StrFunc).Replace('a', 'α').Replace('b', 'β').Replace('c', 'γ') + "}");
					node0.Nodes.Add(node1);
					for(int k = 0; k < _g[i][j].Count; ++k)
					{
						g = _g[i][j][k].Data;
						//αβγδλμπ
						var node2 = new TreeNodeHyperF("#" + i + "." + j + "." + (k + 1).ToString("00") + "  " + g.Data[1].AbcShift + "," + g.Data[2].AbcShift + "  " + g.Data.ToString().Replace("*", "").Replace("F", "*F") + " = 0") { G = g };
						node1.Nodes.Add(node2);
						for(int kk = 0; kk < _g[i][j][k].Data.Count; ++kk)
						{
							g = _g[i][j][k].Data[kk].Data;
							var node3 = new TreeNodeHyperF(/*kk + "  " + */g.Data.ToString().Replace("*", "").Replace("F", "*F") + " = 0") { G = g };
							node3.ForeColor = Color.Gray;
							node2.Nodes.Add(node3);
							for(int kkk = 0; kkk < _g[i][j][k].Data[kk].Data.Data.FromEquations.Count; ++kkk)
							{
								var he = _g[i][j][k].Data[kk].Data.Data.FromEquations[kkk].Data;
								var node4 = new TreeNodeHyperF(he[1].AbcShift + "," + he[2].AbcShift + "  " + he.ToString().Replace("*", "").Replace("F", "*F") + " = 0") { G = g };
								node3.Nodes.Add(node4);
							}
						}
					}
				}
			}
			Colorize();
		}
		private void treeView_AfterSelect(object o, TreeViewEventArgs e)
		{
			//if(e.Action == TreeViewAction.Collapse || e.Action == TreeViewAction.Expand) return;
			var node = e.Node as TreeNodeHyperF;
			if(node != null && node.Level == 2)
			{
				var he = node.G.Data;
				if(_k == 2) he = (new HyperEquation(he[1], he[0], he[2]) - he[1].AbcShift).Simplify();
				if(_k == 3) he = (new HyperEquation(he[2], he[0], he[1]) - he[2].AbcShift).Simplify();
				textBoxDetale.Text = he.ToString()
					.Replace(@"F + ", @"F[a,b,c,z] + ")
					.Replace(@" + -", @"-").Replace(@" + ", @"+")
					.Replace(@"F", textBoxF.Text) + @"=0";
				numericUpDownA1.Value = he[1].AbcShift.A;
				numericUpDownB1.Value = he[1].AbcShift.B;
				numericUpDownC1.Value = he[1].AbcShift.C;
				numericUpDownA2.Value = he[2].AbcShift.A;
				numericUpDownB2.Value = he[2].AbcShift.B;
				numericUpDownC2.Value = he[2].AbcShift.C;
				buttonAdd.Enabled = Hset.List == null;
				if(buttonAdd.Enabled) { buttonAdd.Text = @"Додати"; return; }
				buttonAdd.Enabled = Hset.AddLastAble(he) || Hset.AddLastAble(he.Flip12());
				if(buttonAdd.Enabled) { buttonAdd.Text = @"Додати в кінець"; return; }
				buttonAdd.Enabled = Hset.AddFirstAble(he) || Hset.AddFirstAble(he.Flip12());
				if(buttonAdd.Enabled) { buttonAdd.Text = @"Додати на початок"; return; }
				buttonAdd.Text = @"Додати";
				return;
			}
			buttonAdd.Enabled = false;
			buttonAdd.Text = @"Додати";
			textBoxDetale.Text = e.Node.Text;
		}
		private void Colorize()
		{
			foreach(var node1 in treeViewList.Nodes) {
				foreach(var node2 in ((TreeNode)node1).Nodes) {
					foreach(var node3 in ((TreeNode)node2).Nodes) {
						var nod = node3 as TreeNodeHyperF;
						if(nod == null) continue;
						if(Hset.List == null) {
							//nod.NodeFont = new Font(treeViewList.Font, FontStyle.Regular);
							nod.BackColor = Color.FromArgb(255, 255, 255);
						}
						else {
							var he = nod.G.Data;
							if(_k == 2) he = (he << 1).Flip12() - he[1].AbcShift;
							if(_k == 3) he = (he >> 1) - he[2].AbcShift;
							if(Hset.AddLastAble(he) || Hset.AddLastAble(he.Flip12())) {
								//nod.NodeFont = new Font(treeViewList.Font, FontStyle.Regular);
								//nod.BackColor = Color.FromArgb(63, 255, 255);
								//nod.BackColor = Color.FromArgb(255, 191, 255);
								nod.BackColor = Color.FromArgb(255, 255, 127);
							}
							else if(Hset.AddFirstAble(he) || Hset.AddFirstAble(he.Flip12())) {
								//nod.NodeFont = new Font(treeViewList.Font, FontStyle.Regular);
								//nod.BackColor = Color.FromArgb(191, 255, 127);
								//nod.BackColor = Color.FromArgb(255, 255, 159);
								nod.BackColor = Color.FromArgb(127, 255, 255);
							}
							else {
								//nod.NodeFont = new Font(treeViewList.Font, FontStyle.Regular);
								nod.BackColor = Color.FromArgb(255, 255, 255);
							}
						}
					}
				}
			}
		}
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			var tn = treeViewList.SelectedNode as TreeNodeHyperF;
			if(tn == null) return;
			treeViewList.SuspendLayout();
			treeViewCycle.SuspendLayout();
			var he = tn.G.Data;
			if(_k == 2) he = (new HyperEquation(he[1], he[0], he[2]) - he[1].AbcShift).Simplify();
			if(_k == 3) he = (new HyperEquation(he[2], he[0], he[1]) - he[2].AbcShift).Simplify();
			if(Hset.List != null) {
				if(Hset.List.GetLast().Data[2].AbcShift == -he[2].AbcShift)
					he = new HyperEquation(he[0], he[2], he[1]) { FromEquations = he.FromEquations };
			}
			Converter<HyperEquation, TreeNodeHyperF> converter = h => new TreeNodeHyperF(
				h[1].AbcShift + @"," + h[2].AbcShift + @"  " + h.ToString()
				.Replace(@"*", @"").Replace(@"F", @"*F") + @" = 0")
				{ G = new MultiCycle<HyperEquation>(h, tn.G.Link) };
			if(Hset.AddLast(he) || Hset.AddLast(he = he.Flip12())) treeViewCycle.Nodes.Add(converter(he));
			else if(Hset.AddFirst(he) || Hset.AddFirst(he = he.Flip12())) treeViewCycle.Nodes.Insert(0, converter(he));
			buttonBuild.Enabled = Hset.List != null && Hset.AddLastAble(Hset.List.Data);
			buttonBuild.Text = @"Побудувати дріб (m = " + (Hset.List == null ? @"0" : Hset.Count.ToString(CultureInfo.InvariantCulture)) + @")";
			buttonRemoveFirst.Enabled = buttonRemoveLast.Enabled =
				buttonFindCycle.Enabled = true;
			AcceptButton = buttonBuild.Enabled ? buttonBuild : buttonAdd;
			Colorize();
			treeViewList.ResumeLayout();
			treeViewCycle.ResumeLayout();
			//treeViewList.Select();
			treeView_AfterSelect(treeViewList, new TreeViewEventArgs(treeViewList.SelectedNode, TreeViewAction.Unknown));
		}
		private void buttonRemove_Click(object sender, EventArgs e)
		{
			var btn = sender as Button;
			if(btn == null) return;
			var tnc = treeViewCycle.Nodes;
			treeViewList.SuspendLayout();
			treeViewCycle.SuspendLayout();
			if(btn == buttonRemoveFirst) { tnc.RemoveAt(0); Hset.RemoveFirst(); }
			if(btn == buttonRemoveLast) { tnc.RemoveAt(tnc.Count - 1); Hset.RemoveLast(); }
			buttonBuild.Enabled = Hset.List != null && Hset.AddLastAble(Hset.List.Data);
			buttonBuild.Text = @"Побудувати дріб (m = " + Hset.Count + @")";
			buttonRemoveFirst.Enabled = buttonRemoveLast.Enabled =
				buttonFindCycle.Enabled = Hset.List != null;
			AcceptButton = buttonBuild.Enabled ? buttonBuild : buttonAdd;
			Colorize();
			treeViewList.ResumeLayout();
			treeViewCycle.ResumeLayout();
			//treeViewList.Select();
			treeView_AfterSelect(treeViewList, new TreeViewEventArgs(treeViewList.SelectedNode, TreeViewAction.Unknown));
		}
		private void buttonBuild_Click(object sender, EventArgs e)
		{
			var hs = Hset.Clone();
			hs.List.GetLast().Link = hs.List;
			Size cs0 = Program.FormFractionClientSize;
			var ff = new FormFraction
			{
				Hset = hs,
				SumAbc = hs.SumAbc,
				Font = Font
			};
			Size cs1 = ff.ClientSize;
			Program.FormFractionClientSize = cs0;
			ff.PreScale = new SizeF((float)cs1.Width / cs0.Width, (float)cs1.Height / cs0.Height);
			//ff.ClientSize = cs0;
			ff.InitMono();
			ff.Show();
		}
		private void numericUpDownShift_ValueChanged(object sender, EventArgs e)
		{
			var numeric = sender as NumericUpDown;
			if(numeric == null || numeric != numericUpDownShift) return;
			numericUpDownLimit_ValueChanged(numeric, null);
			var i = (int)numeric.Value;
			if(_k == i) return;
			_k = i;
			treeViewList.SuspendLayout();
			Colorize();
			treeViewList.ResumeLayout();
			treeView_AfterSelect(treeViewList, new TreeViewEventArgs(treeViewList.SelectedNode, TreeViewAction.Unknown));
		}
		private void FormEquationList_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch(e.KeyChar) {
				case '1': numericUpDownShift.Value = 1; break;
				case '2': numericUpDownShift.Value = 2; break;
				case '3': numericUpDownShift.Value = 3; break;
				case 'A': case 'a': case '>':
				case '+': if(buttonAdd.Enabled) buttonAdd_Click(buttonAdd, null); break;
				case 'C': case 'c': case '_':
				case '*': if(buttonFindCycle.Enabled) buttonFindCycle_Click(buttonFindCycle, null); break;
				case 'X': case 'x': case '<':
				case '-': if(buttonRemoveLast.Enabled) buttonRemove_Click(buttonRemoveLast, null); break;
				case 'Z': case 'z': case '^':
				case '\'': if(buttonRemoveFirst.Enabled) buttonRemove_Click(buttonRemoveFirst, null); break;
				case '=': if(buttonBuild.Enabled) buttonBuild_Click(buttonBuild, null); break;
			}
		}
		private void toolStripMenuItemGor_Click(object sender, EventArgs e)
		{
			splitContainer1.SuspendLayout();
			double split = (double)splitContainer1.SplitterDistance /
				(splitContainer1.Orientation == Orientation.Horizontal ?
				splitContainer1.ClientSize.Height : splitContainer1.ClientSize.Width);
			if(sender == toolStripMenuItemHor) splitContainer1.Orientation = Orientation.Horizontal;
			if(sender == toolStripMenuItemVer) splitContainer1.Orientation = Orientation.Vertical;
			toolStripMenuItemHor.Checked = splitContainer1.Orientation == Orientation.Horizontal;
			toolStripMenuItemVer.Checked = splitContainer1.Orientation == Orientation.Vertical;
			splitContainer1.SplitterDistance = (int)(split *
				(splitContainer1.Orientation == Orientation.Horizontal ?
				splitContainer1.ClientSize.Height : splitContainer1.ClientSize.Width));
			splitContainer1.ResumeLayout();
		}
		private void toolStripMenuItemHelp_Click(object sender, EventArgs e) { (new FormHelp()).ShowDialog(this); }
		private void numericUpDownLimit_ValueChanged(object sender, EventArgs e)
		{
			var numeric = sender as NumericUpDown;
			if(numeric == null) return;
			numeric.SuspendLayout();
			if(numeric.Value + numeric.Increment > numeric.Maximum)
				numeric.Value = numeric.Minimum + numeric.Increment;
			if(numeric.Value - numeric.Increment < numeric.Minimum)
				numeric.Value = numeric.Maximum - numeric.Increment;
			numeric.ResumeLayout();
		}
		private int GetMask(HyperEquation he)
		{
			int mask = 0x0;
			if(Hset.AddLastAble(he) && Hset.AddFirstAble(he)) mask |= 0x80;
			if(Hset.AddLastAble(he.Flip12()) && Hset.AddFirstAble(he.Flip12())) mask |= 0x40;
			//if(Hset.AddLastAble(he) || Hset.AddLastAble(he.Flip12())) mask |= 0x20;
			if(Hset.AddLastAble(he)) mask |= 0x10;
			if(Hset.AddLastAble(he.Flip12())) mask |= 0x8;
			//if(Hset.AddFirstAble(he) || Hset.AddFirstAble(he.Flip12())) mask |= 0x4;
			if(Hset.AddFirstAble(he)) mask |= 0x2;
			if(Hset.AddFirstAble(he.Flip12())) mask |= 0x1;
			return mask;
		}
		private MultiCycle<HyperEquation> FindEquation(Abc shift1, Abc shift2)
		{
			int k = _k - 1, t = k;
			bool condition = false;
			HyperGeometric2F1.Base.Func<int> nextValue = i => {
				int p = condition ? 2 : 0;
				condition ^= i != (p ^ 2);
				return i == p ? 1 : p;
			};
			var array = new[] { new[] { shift1, shift2, shift2, shift1 },
				new[] { -shift1, shift2 - shift1, -shift2, shift1 - shift2 },
				new[] { shift2 - shift1, -shift1, shift1 - shift2, -shift2 }
			};
			MultiCycle<HyperEquation> mh, e1, e2;
			do {
				e1 = HyperSet.GetEquation(array[t][0], array[t][1]);
				e2 = HyperSet.GetEquation(array[t][2], array[t][3]);
				if((mh = (e1 == null || e2 == null) ? (e1 ?? e2) :
					GetMask(e2.Data) > GetMask(e1.Data) ? e2 : e1) != null) k = t;
			} while(mh == null && (t = nextValue(t)) + 1 != _k);
			if(mh == null) return null;
			foreach(var node1 in treeViewList.Nodes) {
				foreach(var node2 in ((TreeNode)node1).Nodes) {
					foreach(var node3 in ((TreeNode)node2).Nodes) {
						var nod = node3 as TreeNodeHyperF;
						if(nod == null || nod.G != mh) continue;
						numericUpDownShift.Value = k + 1;
						treeViewList.SelectedNode = nod;
						treeViewList.Select();
						return mh;
					}
				}
			}
			return null;
		}
		private void buttonFind_Click(object sender, EventArgs e)
		{
			Abc shift1 = new Abc((int)numericUpDownA1.Value, (int)numericUpDownB1.Value, (int)numericUpDownC1.Value),
				shift2 = new Abc((int)numericUpDownA2.Value, (int)numericUpDownB2.Value, (int)numericUpDownC2.Value);
			if(FindEquation(shift1, shift2) == null) {
				MessageBox.Show(this, string.Join("\r\n", new[] {
					"Не знайдено жодного рівняння",
					"з такими зміщеннями",
					"параметрів a, b, c."
				}), @"Не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			/*textBoxDetale.Text = mh.Data.ToString()
				.Replace(@"F + ", @"F[a,b,c,z] + ")
				.Replace(@" + -", @"-").Replace(@" + ", @"+")
				.Replace(@"F", textBoxF.Text) + @"=0";//*/
		}
		private void buttonFindCycle_Click(object sender, EventArgs e)
		{
			if(Hset.List == null) return;
			Abc shift1 = -Hset.List.GetLast().Data[2].AbcShift,
				shift2 = -Hset.List.Data[1].AbcShift;
			numericUpDownA1.Value = shift1.A;
			numericUpDownB1.Value = shift1.B;
			numericUpDownC1.Value = shift1.C;
			numericUpDownA2.Value = shift2.A;
			numericUpDownB2.Value = shift2.B;
			numericUpDownC2.Value = shift2.C;
			var mh = FindEquation(shift1, shift2);
			if(mh == null) {
				MessageBox.Show(this, string.Join("\r\n", new[] {
					@"Не знайдено жодного рівняння",
					@"з такими зміщеннями",
					@"параметрів a, b, c."
				}), @"Не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			buttonAdd_Click(buttonAdd, null);
		}
		private void ToolStripMenuItemFont_Click(object sender, EventArgs e)
		{
			var fd = new FontDialog
			{
				Font = Font,
				AllowScriptChange = true,
				AllowSimulations = true,
				AllowVectorFonts = true,
				Color = ForeColor,
				FontMustExist = false,
				MaxSize = 60,
				MinSize = 2,
				ShowApply = true,
				ShowColor = true,
				ShowEffects = true,
			};
			fd.Apply += (obj, ea) => { Font = fd.Font; };
			fd.ShowDialog();
		}
	}
}
