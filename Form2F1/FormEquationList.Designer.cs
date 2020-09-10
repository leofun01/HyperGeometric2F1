namespace Form2F1
{
	partial class FormEquationList
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeViewList = new System.Windows.Forms.TreeView();
			this.buttonRemoveLast = new System.Windows.Forms.Button();
			this.buttonFindCycle = new System.Windows.Forms.Button();
			this.buttonFind = new System.Windows.Forms.Button();
			this.numericUpDownC2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownB2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownA2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownC1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownB1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownA1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownShift = new System.Windows.Forms.NumericUpDown();
			this.buttonBuild = new System.Windows.Forms.Button();
			this.buttonRemoveFirst = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxF = new System.Windows.Forms.TextBox();
			this.treeViewCycle = new System.Windows.Forms.TreeView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItemPanelSplit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemHor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemVer = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemFont = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.textBoxDetale = new System.Windows.Forms.TextBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownC2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownB2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownA2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownC1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownB1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownShift)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 44);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeViewList);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonRemoveLast);
			this.splitContainer1.Panel2.Controls.Add(this.buttonFindCycle);
			this.splitContainer1.Panel2.Controls.Add(this.buttonFind);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownC2);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownB2);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownA2);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownC1);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownB1);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownA1);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownShift);
			this.splitContainer1.Panel2.Controls.Add(this.buttonBuild);
			this.splitContainer1.Panel2.Controls.Add(this.buttonRemoveFirst);
			this.splitContainer1.Panel2.Controls.Add(this.buttonAdd);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxF);
			this.splitContainer1.Panel2.Controls.Add(this.treeViewCycle);
			this.splitContainer1.Size = new System.Drawing.Size(533, 303);
			this.splitContainer1.SplitterDistance = 151;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 2;
			// 
			// treeViewList
			// 
			this.treeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewList.LabelEdit = true;
			this.treeViewList.Location = new System.Drawing.Point(0, 0);
			this.treeViewList.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.treeViewList.Name = "treeViewList";
			this.treeViewList.Size = new System.Drawing.Size(533, 151);
			this.treeViewList.TabIndex = 0;
			this.treeViewList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			this.treeViewList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// buttonRemoveLast
			// 
			this.buttonRemoveLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveLast.Enabled = false;
			this.buttonRemoveLast.Location = new System.Drawing.Point(424, 24);
			this.buttonRemoveLast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonRemoveLast.Name = "buttonRemoveLast";
			this.buttonRemoveLast.Size = new System.Drawing.Size(107, 19);
			this.buttonRemoveLast.TabIndex = 15;
			this.buttonRemoveLast.Text = "Видалити ↓";
			this.buttonRemoveLast.UseVisualStyleBackColor = true;
			this.buttonRemoveLast.Click += new System.EventHandler(this.buttonRemove_Click);
			this.buttonRemoveLast.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// buttonFindCycle
			// 
			this.buttonFindCycle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFindCycle.Enabled = false;
			this.buttonFindCycle.Location = new System.Drawing.Point(330, 24);
			this.buttonFindCycle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonFindCycle.Name = "buttonFindCycle";
			this.buttonFindCycle.Size = new System.Drawing.Size(89, 19);
			this.buttonFindCycle.TabIndex = 14;
			this.buttonFindCycle.Text = "Замикання";
			this.buttonFindCycle.UseVisualStyleBackColor = true;
			this.buttonFindCycle.Click += new System.EventHandler(this.buttonFindCycle_Click);
			this.buttonFindCycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// buttonFind
			// 
			this.buttonFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFind.Location = new System.Drawing.Point(268, 24);
			this.buttonFind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.Size = new System.Drawing.Size(56, 19);
			this.buttonFind.TabIndex = 13;
			this.buttonFind.Text = "Пошук";
			this.buttonFind.UseVisualStyleBackColor = true;
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			this.buttonFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// numericUpDownC2
			// 
			this.numericUpDownC2.Location = new System.Drawing.Point(224, 24);
			this.numericUpDownC2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownC2.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericUpDownC2.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			this.numericUpDownC2.Name = "numericUpDownC2";
			this.numericUpDownC2.ReadOnly = true;
			this.numericUpDownC2.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownC2.TabIndex = 12;
			this.numericUpDownC2.ValueChanged += new System.EventHandler(this.numericUpDownLimit_ValueChanged);
			// 
			// numericUpDownB2
			// 
			this.numericUpDownB2.Location = new System.Drawing.Point(183, 24);
			this.numericUpDownB2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownB2.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericUpDownB2.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			this.numericUpDownB2.Name = "numericUpDownB2";
			this.numericUpDownB2.ReadOnly = true;
			this.numericUpDownB2.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownB2.TabIndex = 11;
			this.numericUpDownB2.ValueChanged += new System.EventHandler(this.numericUpDownLimit_ValueChanged);
			// 
			// numericUpDownA2
			// 
			this.numericUpDownA2.Location = new System.Drawing.Point(142, 24);
			this.numericUpDownA2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownA2.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericUpDownA2.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			this.numericUpDownA2.Name = "numericUpDownA2";
			this.numericUpDownA2.ReadOnly = true;
			this.numericUpDownA2.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownA2.TabIndex = 10;
			this.numericUpDownA2.ValueChanged += new System.EventHandler(this.numericUpDownLimit_ValueChanged);
			// 
			// numericUpDownC1
			// 
			this.numericUpDownC1.Location = new System.Drawing.Point(92, 24);
			this.numericUpDownC1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownC1.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericUpDownC1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			this.numericUpDownC1.Name = "numericUpDownC1";
			this.numericUpDownC1.ReadOnly = true;
			this.numericUpDownC1.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownC1.TabIndex = 9;
			this.numericUpDownC1.ValueChanged += new System.EventHandler(this.numericUpDownLimit_ValueChanged);
			// 
			// numericUpDownB1
			// 
			this.numericUpDownB1.Location = new System.Drawing.Point(52, 24);
			this.numericUpDownB1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownB1.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericUpDownB1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			this.numericUpDownB1.Name = "numericUpDownB1";
			this.numericUpDownB1.ReadOnly = true;
			this.numericUpDownB1.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownB1.TabIndex = 8;
			this.numericUpDownB1.ValueChanged += new System.EventHandler(this.numericUpDownLimit_ValueChanged);
			// 
			// numericUpDownA1
			// 
			this.numericUpDownA1.Location = new System.Drawing.Point(11, 24);
			this.numericUpDownA1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownA1.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericUpDownA1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			this.numericUpDownA1.Name = "numericUpDownA1";
			this.numericUpDownA1.ReadOnly = true;
			this.numericUpDownA1.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownA1.TabIndex = 7;
			this.numericUpDownA1.ValueChanged += new System.EventHandler(this.numericUpDownLimit_ValueChanged);
			// 
			// numericUpDownShift
			// 
			this.numericUpDownShift.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownShift.Location = new System.Drawing.Point(224, 0);
			this.numericUpDownShift.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDownShift.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.numericUpDownShift.Name = "numericUpDownShift";
			this.numericUpDownShift.ReadOnly = true;
			this.numericUpDownShift.Size = new System.Drawing.Size(40, 20);
			this.numericUpDownShift.TabIndex = 3;
			this.numericUpDownShift.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownShift.ValueChanged += new System.EventHandler(this.numericUpDownShift_ValueChanged);
			// 
			// buttonBuild
			// 
			this.buttonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBuild.Enabled = false;
			this.buttonBuild.Location = new System.Drawing.Point(0, 129);
			this.buttonBuild.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonBuild.Name = "buttonBuild";
			this.buttonBuild.Size = new System.Drawing.Size(533, 19);
			this.buttonBuild.TabIndex = 17;
			this.buttonBuild.Text = "Побудувати дріб";
			this.buttonBuild.UseVisualStyleBackColor = true;
			this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
			this.buttonBuild.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// buttonRemoveFirst
			// 
			this.buttonRemoveFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveFirst.Enabled = false;
			this.buttonRemoveFirst.Location = new System.Drawing.Point(424, 0);
			this.buttonRemoveFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonRemoveFirst.Name = "buttonRemoveFirst";
			this.buttonRemoveFirst.Size = new System.Drawing.Size(107, 19);
			this.buttonRemoveFirst.TabIndex = 6;
			this.buttonRemoveFirst.Text = "Видалити ↑";
			this.buttonRemoveFirst.UseVisualStyleBackColor = true;
			this.buttonRemoveFirst.Click += new System.EventHandler(this.buttonRemove_Click);
			this.buttonRemoveFirst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAdd.Enabled = false;
			this.buttonAdd.Location = new System.Drawing.Point(268, 0);
			this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(150, 19);
			this.buttonAdd.TabIndex = 5;
			this.buttonAdd.Text = "Додати";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			this.buttonAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "F →";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBoxF
			// 
			this.textBoxF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxF.Location = new System.Drawing.Point(47, 0);
			this.textBoxF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.textBoxF.Name = "textBoxF";
			this.textBoxF.Size = new System.Drawing.Size(172, 20);
			this.textBoxF.TabIndex = 2;
			this.textBoxF.Text = "F";
			// 
			// treeViewCycle
			// 
			this.treeViewCycle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewCycle.LabelEdit = true;
			this.treeViewCycle.Location = new System.Drawing.Point(0, 49);
			this.treeViewCycle.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.treeViewCycle.Name = "treeViewCycle";
			this.treeViewCycle.Size = new System.Drawing.Size(534, 76);
			this.treeViewCycle.TabIndex = 16;
			this.treeViewCycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPanelSplit,
            this.ToolStripMenuItemFont,
            this.toolStripMenuItemHelp});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(533, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			// 
			// toolStripMenuItemPanelSplit
			// 
			this.toolStripMenuItemPanelSplit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHor,
            this.toolStripMenuItemVer});
			this.toolStripMenuItemPanelSplit.Name = "toolStripMenuItemPanelSplit";
			this.toolStripMenuItemPanelSplit.Size = new System.Drawing.Size(59, 20);
			this.toolStripMenuItemPanelSplit.Text = "Панель";
			// 
			// toolStripMenuItemHor
			// 
			this.toolStripMenuItemHor.Checked = true;
			this.toolStripMenuItemHor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItemHor.Name = "toolStripMenuItemHor";
			this.toolStripMenuItemHor.Size = new System.Drawing.Size(161, 22);
			this.toolStripMenuItemHor.Text = "Горизонтально";
			this.toolStripMenuItemHor.Click += new System.EventHandler(this.toolStripMenuItemGor_Click);
			// 
			// toolStripMenuItemVer
			// 
			this.toolStripMenuItemVer.Name = "toolStripMenuItemVer";
			this.toolStripMenuItemVer.Size = new System.Drawing.Size(161, 22);
			this.toolStripMenuItemVer.Text = "Вертикально";
			this.toolStripMenuItemVer.Click += new System.EventHandler(this.toolStripMenuItemGor_Click);
			// 
			// ToolStripMenuItemFont
			// 
			this.ToolStripMenuItemFont.Name = "ToolStripMenuItemFont";
			this.ToolStripMenuItemFont.Size = new System.Drawing.Size(52, 20);
			this.ToolStripMenuItemFont.Text = "Шрифт";
			this.ToolStripMenuItemFont.Click += new System.EventHandler(this.ToolStripMenuItemFont_Click);
			// 
			// toolStripMenuItemHelp
			// 
			this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
			this.toolStripMenuItemHelp.Size = new System.Drawing.Size(66, 20);
			this.toolStripMenuItemHelp.Text = "Довідка";
			this.toolStripMenuItemHelp.Click += new System.EventHandler(this.toolStripMenuItemHelp_Click);
			// 
			// textBoxDetale
			// 
			this.textBoxDetale.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBoxDetale.Location = new System.Drawing.Point(0, 24);
			this.textBoxDetale.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.textBoxDetale.Name = "textBoxDetale";
			this.textBoxDetale.Size = new System.Drawing.Size(533, 20);
			this.textBoxDetale.TabIndex = 1;
			// 
			// FormEquationList
			// 
			this.AcceptButton = this.buttonAdd;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 347);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.textBoxDetale);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.Name = "FormEquationList";
			this.Text = "HyperGeometric 2F1 (List of Equations)";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEquationList_KeyPress);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownC2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownB2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownA2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownC1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownB1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownShift)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeViewList;
		private System.Windows.Forms.TreeView treeViewCycle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxF;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonRemoveFirst;
		private System.Windows.Forms.Button buttonBuild;
		private System.Windows.Forms.NumericUpDown numericUpDownShift;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPanelSplit;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHor;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVer;
		private System.Windows.Forms.TextBox textBoxDetale;
		private System.Windows.Forms.NumericUpDown numericUpDownC2;
		private System.Windows.Forms.NumericUpDown numericUpDownB2;
		private System.Windows.Forms.NumericUpDown numericUpDownA2;
		private System.Windows.Forms.NumericUpDown numericUpDownC1;
		private System.Windows.Forms.NumericUpDown numericUpDownB1;
		private System.Windows.Forms.NumericUpDown numericUpDownA1;
		private System.Windows.Forms.Button buttonFindCycle;
		private System.Windows.Forms.Button buttonFind;
		private System.Windows.Forms.Button buttonRemoveLast;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFont;
	}
}