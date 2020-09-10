using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HyperGeometric2F1.Linker;
using HyperGeometric2F1.Hypergeometric;

namespace Form2F1
{
	public class TreeNodeHyperF : TreeNode
	{
		public MultiCycle<HyperEquation> G;
		public TreeNodeHyperF() { }
		public TreeNodeHyperF(string text) : base(text) { }
		public TreeNodeHyperF(string text, TreeNode[] children) : base(text, children) { }
		public TreeNodeHyperF(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
		public TreeNodeHyperF(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children) { }
	}
}
