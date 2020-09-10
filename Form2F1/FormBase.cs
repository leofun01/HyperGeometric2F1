using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Form2F1
{
	public class FormBase : Form
	{
	
		public FormBase()
		{
			InitializeComponent();
		}

		#region Initialize
		private void InitializeComponent()
		{
			SuspendLayout();
			// 
			// FormBase
			// 
			ClientSize = new Size(600, 400);
			Font = new Font(@"Lucida Console", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
			Name = @"FormBase";
			Text = @"HyperGeometric 2F1";
			ResumeLayout(false);
		}
		#endregion
	}
}
