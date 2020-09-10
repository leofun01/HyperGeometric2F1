using System;
using System.Drawing;
using System.Windows.Forms;
using HyperGeometric2F1.Linker;
using HyperGeometric2F1.Hypergeometric;
using HyperGeometric2F1.Math;
using System.Collections.Generic;

namespace Form2F1
{
	public partial class FormFraction : FormBase
	{
		public int K = 1, M;
		public Abc SumAbc, ShiftAbc;
		public HyperSet Hset;
		public MonoLinker<UserControlXi> Xi;
		public ContinuedFractionA ContFrac;
		public SizeF PreScale = new SizeF(1F, 1F);
		public FormFraction()
		{
			InitializeComponent();
			ClientSize = Program.FormFractionClientSize;
		}
		private void InitPanelXi()
		{
			label1.Font = new Font(Font.Name,
				(float)Math.Floor(Font.Size * 4F / 3F),
				Font.Style, Font.Unit, Font.GdiCharSet);
			panelXi.SuspendLayout();
			UserControlXi uc = null;
			int i = 0;
			while(i < M) {
				uc = new UserControlXi {
					Font = new Font(Font.Name, Font.Size,
						Font.Style, Font.Unit, Font.GdiCharSet),
					labelName = {
						Font = new Font(Font.Name,
							(float)Math.Floor(Font.Size * 4F / 3F),
							Font.Style, Font.Unit, Font.GdiCharSet)
					},
					labelIndex = { Text = M + @"n+" + ++i },
					Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
					Width = panelXi.ClientSize.Width,
					Top = uc == null ? 0 : uc.Bottom
				};
				panelXi.Controls.Add(uc);
				if(Xi == null) Xi = new MonoLinker<UserControlXi>(uc);
				else Xi.Add(uc);
			}
			if(Xi != null) Xi.GetLast().Link = Xi;
			panelXi.ResumeLayout();
		}
		public void InitMono()
		{
			numericUpDownK.Maximum = (M = Hset.Count) + 1;
			numericUpDownK.Enabled = M > 1;
			ToolStripMenuItemEvaluate.Enabled = SumAbc != Abc.Zero;
			string text2 = @"F[a,b,c,z]";
			/*
			if(domainUpDownA.SelectedIndex == 3)
				text2 = text2.Replace(@"a", numericUpDownA.Value.ToString(CultureInfo.InvariantCulture));
			if(domainUpDownB.SelectedIndex == 3)
				text2 = text2.Replace(@"b", numericUpDownB.Value.ToString(CultureInfo.InvariantCulture));
			if(domainUpDownC.SelectedIndex == 3)
				text2 = text2.Replace(@"c", numericUpDownC.Value.ToString(CultureInfo.InvariantCulture));
			//*/
			labelFs.Text = Coef.GetShiftedStr(text2, Hset.List.Data[1].AbcShift + ShiftAbc, false);
			labelF0.Text = Coef.GetShiftedStr(text2, ShiftAbc, false);
			Coef cf = new Coef(string.Concat(@"-(",
				Hset.List.Data[0].StrFunc, @")/(",
				Hset.List.Data[1].StrFunc, @")"))[ShiftAbc].Simplify();
			ContFrac = new ContinuedFractionA(SumAbc, new MonoLinker<Coef>(cf));
			string text1 = cf.StrFunc;
			text2 = Coef.One.StrFunc;
			/*
			if(domainUpDownA.SelectedIndex == 3)
				text1 = text1.Replace(@"a", numericUpDownA.Value.ToString(CultureInfo.InvariantCulture));
			if(domainUpDownB.SelectedIndex == 3)
				text1 = text1.Replace(@"b", numericUpDownB.Value.ToString(CultureInfo.InvariantCulture));
			if(domainUpDownC.SelectedIndex == 3)
				text1 = text1.Replace(@"c", numericUpDownC.Value.ToString(CultureInfo.InvariantCulture));
			text1 = Expression.Simplify(Expression.Simplify(text1), Coef.SortComparison);
			//*/
			var product = Expression.SplitSum(text1);
			if(product.Link == null) {
				product = Expression.SplitProduct(text1);
				text1 = text2;
				foreach(var v in (IEnumerable<KeyValuePair<char, string>>)product)
					if(v.Key == '/') text2 += string.Concat(@"*(", v.Value, @")");
					else text1 += string.Concat(@"*(", v.Value, @")");
			}
			textBox1.Text = Expression.Simplify(text1, Coef.SortComparison);
			textBox2.Text = Expression.Simplify(text2, Coef.SortComparison);
			if(Xi == null) InitPanelXi();
			Abc abc = ShiftAbc;
			foreach(var he in Hset) {
				cf = new Coef(string.Concat(@"-(",
					Coef.GetShiftedStr(he.Link.Data[1].StrFunc, he.Data[2].AbcShift, true), @")*(",
					he.Data[2].StrFunc, @")/(",
					Coef.GetShiftedStr(he.Link.Data[0].StrFunc, he.Data[2].AbcShift, true), @")/(",
					he.Data[0].StrFunc, @")"))[abc].Simplify();
				ContFrac.Coefs.Add(cf);
				abc += he.Data[2].AbcShift;
				text1 = cf[SumAbc * 10000].StrFunc.Replace(@"0000", @"*n");
				text2 = Coef.One.StrFunc;
				/*
				if(domainUpDownA.SelectedIndex == 3)
					text1 = text1.Replace(@"a", numericUpDownA.Value.ToString(CultureInfo.InvariantCulture));
				if(domainUpDownB.SelectedIndex == 3)
					text1 = text1.Replace(@"b", numericUpDownB.Value.ToString(CultureInfo.InvariantCulture));
				if(domainUpDownC.SelectedIndex == 3)
					text1 = text1.Replace(@"c", numericUpDownC.Value.ToString(CultureInfo.InvariantCulture));
				text1 = Expression.Simplify(Expression.Simplify(text1), Coef.SortComparison);
				//*/
				product = Expression.SplitSum(text1);
				if(product.Link == null) {
					product = Expression.SplitProduct(text1);
					text1 = text2;
					foreach(var v in (IEnumerable<KeyValuePair<char, string>>)product)
						if(v.Key == '/') text2 += string.Concat(@"*(", v.Value, @")");
						else text1 += string.Concat(@"*(", v.Value, @")");
				}
				Xi.Data.textBoxNumerator.Text = Expression.Simplify(text1, Coef.SortComparison);
				Xi.Data.textBoxDenominator.Text = Expression.Simplify(text2, Coef.SortComparison);
				Xi = Xi.Link;
			}
			ContFrac.Coefs.GetLast().Link = ContFrac.Coefs.Link;
		}
		private void numericUpDownK_ValueChanged(object sender, EventArgs e)
		{
			var numeric = sender as NumericUpDown;
			if(numeric == null) return;
			var newK = (int)numeric.Value;
			//if(newK == K) return;
			if(newK < 1) { numeric.Value = M; return; }
			if(newK > M) { numeric.Value = 1; return; }
			Hset.List = Hset.List[newK + M - K];
			K = newK;
			InitMono();
		}
		private void Reverse_Click(object sender, EventArgs e)
		{
			Hset.Reverse();
			SumAbc = -SumAbc;
			if(K + K != M + 1) numericUpDownK.Value = K = M - K + 1;
			else InitMono();
		}
		private void ShowList_Click(object sender, EventArgs e)
		{
			var form = new FormBase { Text = @"Послідовність співвідношень", ClientSize = Program.ShowListClientSize };
			form.ClientSizeChanged += (o, ea) =>
			{
				if(form.WindowState == FormWindowState.Normal)
					Program.ShowListClientSize = form.ClientSize;
			};
			var textBox = new RichTextBox {
				Multiline = true,
				WordWrap = false,
				Dock = DockStyle.Fill,
				ScrollBars = RichTextBoxScrollBars.Both,
				DetectUrls = false,
				AcceptsTab = true,
				ShortcutsEnabled = true,
				RichTextShortcutsEnabled = true,
				TabIndex = 0,
				Lines = Hset.List.ToArray(m => "\r\n" + m.Data[1].AbcShift + @"," + m.Data[2].AbcShift + @"  " + m.Data + " = 0")
			};
			form.Controls.Add(textBox);
			form.Show(); //*/ Program.StartForm(form);
		}
		private void numericUpDownAbc_ValueChanged(object sender, EventArgs e)
		{
			int a = (int)numericUpDownA.Value,
				b = (int)numericUpDownB.Value,
				c = (int)numericUpDownC.Value;
			ShiftAbc = new Abc(a, b, c);
			InitMono();
		}
		private void FormFraction_ClientSizeChanged(object sender, EventArgs e)
		{
			if(WindowState == FormWindowState.Normal)
				Program.FormFractionClientSize = new Size(
					(int)(ClientSize.Width / PreScale.Width),
					(int)(ClientSize.Height / PreScale.Height));
		}
		private void ShowCopy_Click(object sender, EventArgs e)
		{
			var ff = new FormFraction {
				Hset = Hset.Clone(),
				SumAbc = SumAbc,
				M = M, K = K,
				ShiftAbc = ShiftAbc,
				ContFrac = ContFrac.Clone(),
				PreScale = PreScale,
				Font = Font,
				ClientSize = ClientSize
			};
			ff.InitMono();
			ff.numericUpDownK.Value = K;
			ff.numericUpDownA.Value = ShiftAbc.A;
			ff.numericUpDownB.Value = ShiftAbc.B;
			ff.numericUpDownC.Value = ShiftAbc.C;
			ff.Show();
		}
		private void domainUpDownAbc_SelectedItemChanged(object sender, EventArgs e)
		{
			var domain = sender as DomainUpDown;
			if(domain == null || domain.Items.Count < 3) return;
			if(domain.SelectedIndex == 0) { domain.SelectedIndex = domain.Items.Count - 2; return; }
			if(domain.SelectedIndex == domain.Items.Count - 1) { domain.SelectedIndex = 1; return; }
			numericUpDownAbc_ValueChanged(numericUpDownA, null);
		}
		private void Evaluate_Click(object sender, EventArgs e)
		{
			;
		}
	}
}
