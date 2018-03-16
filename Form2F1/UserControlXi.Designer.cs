namespace Form2F1
{
	partial class UserControlXi
	{
		/// <summary> 
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Обязательный метод для поддержки конструктора - не изменяйте 
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxDenominator = new System.Windows.Forms.TextBox();
			this.textBoxNumerator = new System.Windows.Forms.TextBox();
			this.labelIndex = new System.Windows.Forms.Label();
			this.labelName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxDenominator
			// 
			this.textBoxDenominator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDenominator.Location = new System.Drawing.Point(63, 30);
			this.textBoxDenominator.Name = "textBoxDenominator";
			this.textBoxDenominator.Size = new System.Drawing.Size(133, 22);
			this.textBoxDenominator.TabIndex = 13;
			// 
			// textBoxNumerator
			// 
			this.textBoxNumerator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNumerator.Location = new System.Drawing.Point(63, 7);
			this.textBoxNumerator.Name = "textBoxNumerator";
			this.textBoxNumerator.Size = new System.Drawing.Size(133, 22);
			this.textBoxNumerator.TabIndex = 12;
			// 
			// labelIndex
			// 
			this.labelIndex.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelIndex.Location = new System.Drawing.Point(0, 37);
			this.labelIndex.Name = "labelIndex";
			this.labelIndex.Size = new System.Drawing.Size(56, 15);
			this.labelIndex.TabIndex = 10;
			this.labelIndex.Text = "i";
			this.labelIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelName
			// 
			this.labelName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("Lucida Console", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.labelName.Location = new System.Drawing.Point(0, 18);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(57, 20);
			this.labelName.TabIndex = 8;
			this.labelName.Text = "X  =";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UserControlXi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBoxDenominator);
			this.Controls.Add(this.textBoxNumerator);
			this.Controls.Add(this.labelIndex);
			this.Controls.Add(this.labelName);
			this.Font = new System.Drawing.Font("Lucida Console", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "UserControlXi";
			this.Size = new System.Drawing.Size(200, 60);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.TextBox textBoxDenominator;
		public System.Windows.Forms.TextBox textBoxNumerator;
		public System.Windows.Forms.Label labelIndex;
		public System.Windows.Forms.Label labelName;

	}
}
