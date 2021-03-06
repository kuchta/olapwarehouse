﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ImportWDI {
	static class Program {
		private static StreamWriter _logFileStreamWriter;
		private static Stopwatch _stopwatch = Stopwatch.StartNew();

		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			if (_logFileStreamWriter != null) {
				_logFileStreamWriter.Close();
			}
		}

		public static void LogMessage(string message) {
			message = string.Format("{0} ({1}secs): {2}", DateTime.Now, (int)_stopwatch.Elapsed.TotalSeconds, message);
			_stopwatch.Restart();

			Console.WriteLine(message);
			LogFile.WriteLine(message);
		}

		public static string LogFileName {
			get {
				return System.IO.Path.GetTempPath() + Path.DirectorySeparatorChar + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".log";
			}
		}

		private static StreamWriter LogFile {
			get {
				if (_logFileStreamWriter == null) {
					_logFileStreamWriter = new StreamWriter(LogFileName);
				}
				return _logFileStreamWriter;
			}
		}
	}
}
