using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Form2F1
{
	public class FormHelp : FormBase
	{
		private Label label1;

		public FormHelp()
		{
			InitializeComponent();
			label1.Text = string.Join("\r\n\n", new[] {
				@"Гарячі клавіші:",
				@"1, 2, 3 - Задає зміщення параметрів співвідношень;",
				@"A, +, > - Додає вибране співвідношення до списку;",
				@"С, *, _ - Замикає послідовність для побудови дробу;",
				@"Z, /, ^ - Видаляє перше співвідношення зі списку;",
				@"X, -, < - Видаляє останнє співвідношення зі списку;",
				@"= - Будує неперервний дріб, коли список утворює цикл;",
				@"Enter - Будує неперервний дріб (якщо доступно), або"
				+ "\r\n" + @"        додає вибране співвідношення до списку;",
				@"[Shift +] Tab - Перехід до наступного елемента керування."
			});
		}

		#region Initialize
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(496, 262);
			this.label1.TabIndex = 0;
			// 
			// FormHelp
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(520, 280);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormHelp";
			this.Text = "Help";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
