using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Form2F1
{
	static class Program
	{
		public static FormBase FormList;
		public static readonly Rectangle Wa = Screen.PrimaryScreen.WorkingArea;
		public static Size FormFractionClientSize = new Size(Math.Min(450, Wa.Width), Math.Min(300, Wa.Height));
		public static Size ShowListClientSize = new Size(Math.Min(800, Wa.Width), Math.Min(200, Wa.Height));
		//public static Size FormCalcClientSize = new Size(Math.Min(600, Wa.Width), Math.Min(400, Wa.Height));
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			FormBase formLoading = new FormBase
			{
				ControlBox = true,
				MaximizeBox = false,
				MinimizeBox = false,
				WindowState = FormWindowState.Normal,
				StartPosition = FormStartPosition.CenterScreen,
				FormBorderStyle = FormBorderStyle.FixedSingle,
				ClientSize = new Size(256, 128),
				ShowIcon = true,
				ShowInTaskbar = false
			};
			Label label = new Label
			{
				AutoSize = false,
				Dock = DockStyle.Fill,
				Text = @"Завантаження ...",
				TextAlign = ContentAlignment.MiddleCenter
			};
			formLoading.Controls.Add(label);
			var t = StartForm(formLoading);
			//Thread.Sleep(10000);
			FormList = new FormEquationList
			{
				StartPosition = FormStartPosition.CenterParent
			};
			FormList.Load += (sender, args) => FormList.Activate();
			try { t.Abort(); }
			catch(System.Security.SecurityException) { }
			catch(ThreadStartException) { }
			Application.Run(FormList);
		}
		public static Thread StartForm(Form form)
		{
			var thread = new Thread(() => Application.Run(form));
			thread.Start();
			return thread;
		}
	}
}
