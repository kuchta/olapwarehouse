using OlapWarehouseApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;


namespace ImportWDI {
	public partial class MainForm : Form {
		Queue<DoWorkEventHandler> _backgroundWorkerOperationQueue;
		ProcessWdiData _processData;

		bool _isCountriesShown = false;
		bool _isIndicatorsShown = false;
		bool _isYearsShown = false;

		string _inputFile;

		public MainForm() {
			InitializeComponent();

			_backgroundWorkerOperationQueue = new Queue<DoWorkEventHandler>();
		}

		public new void Dispose() {
			Dispose(true);
			if (_processData != null) {
				_processData.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		Dimension _countries;
		private Dimension Countries {
			get {
				if (_countries == null) {
					_countries = Data.Countries;
				}
				return _countries;
			}
		}

		Dimension _indicators;
		private Dimension Indicators {
			get {
				if (_indicators == null) {
					_indicators = Data.Indicators;
				}
				return _indicators;
			}
		}

		Dimension _years;
		private Dimension Years {
			get {
				if (_years == null) {
					_years = Data.Years;
				}
				return _years;
			}
		}

		private ProcessWdiData Data {
			get {
				if (_processData == null || inputFileTextBox.Text != _inputFile) {
					_inputFile = inputFileTextBox.Text;
					ReportStatus("Opening input file...");
					EnableControl(controlsGroupBox, false);
					_processData = new ProcessWdiData(_inputFile);
					EnableControl(controlsGroupBox, true);
					ReportStatus("Done opening input file.");
					_processData.StatusChanged += new StatusChangedEventHandler(backgroundWorker_StatusChanged);
					_countries = null;
					_indicators = null;
					_years = null;
				}
				return _processData;
			}
			set {
				_processData = value;
				_countries = null;
				_indicators = null;
				_years = null;
			}
		}

		private void tbInputFile_TextChanged(object sender, EventArgs e) {
			EnableControl(controlsGroupBox, File.Exists(inputFileTextBox.Text));
			Data = null;
			ReportStatus("Ready to serve you, Master!");
		}

		private void btnInputFile_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".xls"; // Default file extension 
			ofd.Filter = "Excel documents|*.xlsx"; // Filter files by extension

			if (ofd.ShowDialog() == DialogResult.OK) {
				inputFileTextBox.Text = ofd.FileName;
				inputFileTextBox.SelectionStart = inputFileTextBox.Text.Length;
				inputFileTextBox.ScrollToCaret();
				inputFileTextBox.Refresh();
			}

			Data = null;
		}

		private void fileTextBox_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true) {
				e.Effect = DragDropEffects.All;
			}
		}

		private void fileTextBox_DragDrop(object sender, DragEventArgs e) {
			TextBox s = (TextBox)sender;
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files != null && files.Length != 0) {
				s.Text = files[0];
				s.SelectionStart = s.Text.Length;
				s.ScrollToCaret();
				s.Refresh();
			}
		}

		private void integratedSecurityCheckBox_CheckedChanged(object sender, EventArgs e) {
			bool enabled = ((CheckBox)sender).Checked;
			userTextBox.Enabled = !enabled;
			passwordTextBox.Enabled = !enabled;
		}

		private void showCountriesButton_Click(object sender, EventArgs e) {
			RunInNewBackgroundWorker((a, b) => {
				ShowDimension(Countries);
			});
		}

		private void showIndicatorsButton_Click(object sender, EventArgs e) {
			RunInNewBackgroundWorker((a, b) => {
				ShowDimension(Indicators);
			});
		}

		private void showYearsButton_Click(object sender, EventArgs e) {
			RunInNewBackgroundWorker((a, b) => {
				ShowDimension(Years);
			});
		}

		private void transferDimensionsButton_Click(object sender, EventArgs e) {
			RunInBackgroundWorker((a, b) => {
				MakeProgressBarVisible(false);
				EnableControl(transferDataButton, false);
				EnableControl(transferDimensionsButton, false);
				EnableControl(transferFactsButton, false);
				Data.TransferDimensions(serverTextBox.Text, databaseTextBox.Text, (integratedSecurityCheckBox.Checked ? null : userTextBox.Text), passwordTextBox.Text);
				EnableControl(transferDataButton, true);
				EnableControl(transferDimensionsButton, true);
				EnableControl(transferFactsButton, true);
			});
		}

		private void transferFactsButton_Click(object sender, EventArgs e) {
			RunInBackgroundWorker((a, b) => {
				MakeProgressBarVisible(false);
				EnableControl(transferDataButton, false);
				EnableControl(transferDimensionsButton, false);
				EnableControl(transferFactsButton, false);
				Data.TransferFacts(serverTextBox.Text, databaseTextBox.Text, (integratedSecurityCheckBox.Checked ? null : userTextBox.Text), passwordTextBox.Text);
				EnableControl(transferDataButton, true);
				EnableControl(transferDimensionsButton, true);
				EnableControl(transferFactsButton, true);
			});
		}


		private void transferDataButton_Click(object sender, EventArgs e) {
			RunInBackgroundWorker((a, b) => {
				MakeProgressBarVisible(false);
				EnableControl(transferDataButton, false);
				EnableControl(transferDimensionsButton, false);
				EnableControl(transferFactsButton, false);
				Data.TransferData(serverTextBox.Text, databaseTextBox.Text, (integratedSecurityCheckBox.Checked ? null : userTextBox.Text), passwordTextBox.Text);
				EnableControl(transferDataButton, true);
				EnableControl(transferDimensionsButton, true);
				EnableControl(transferFactsButton, true);
			});
		}

		private void saveFactsToFileButton_Click(object sender, EventArgs e) {
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = ".txt";
			sfd.Filter = "Data files|*.txt";

			int[] arr = new int[] { 1, 2, 3, 4, 5 };

			if (sfd.ShowDialog() == DialogResult.OK) {
				RunInBackgroundWorker((a, b) => {
					Data.TransferFacts(sfd.FileName);
				});
			}
		}

		private void stopButton_Click(object sender, EventArgs e) {
			Data.CancellationPending = true;
		}

		private void ShowDimension(Dimension dimension) {
			switch (dimension.Name) {
				case ProcessWdiData.COUNTRIES:
					EnableControl(showCountriesButton, false);
					_isCountriesShown = true;
					TreeForm.Show(dimension);
					_isCountriesShown = false;
					EnableControl(showCountriesButton, true);
					break;
				case ProcessWdiData.INDICATORS:
					EnableControl(showIndicatorsButton, false);
					_isIndicatorsShown = true;
					TreeForm.Show(dimension);
					_isIndicatorsShown = false;
					EnableControl(showIndicatorsButton, true);
					break;
				case ProcessWdiData.YEARS:
					EnableControl(showYearsButton, false);
					_isYearsShown = true;
					TreeForm.Show(dimension);
					_isYearsShown = false;
					EnableControl(showYearsButton, true);
					break;
			}
		}

		private void RunInNewBackgroundWorker(DoWorkEventHandler d) {
			//Console.WriteLine("RunInNewBackgroundWorker started");

			BackgroundWorker bw = new BackgroundWorker();
			bw.DoWork += d;
			bw.RunWorkerAsync();

			//Console.WriteLine("RunInNewBackgroundWorker finished");
		}

		private void RunInBackgroundWorker(DoWorkEventHandler d) {
			//Console.WriteLine("RunInBackgroundWorker started");

			_backgroundWorkerOperationQueue.Enqueue(d);
			if (!jobBackgroundWorker.IsBusy) {
				jobBackgroundWorker.RunWorkerAsync();
			}

			//Console.WriteLine("RunInBackgroundWorker finished");
		}

		private void jobBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
			//Console.WriteLine("jobBackgroundWorker_DoWork started");

			if (_backgroundWorkerOperationQueue.Count > 0) {
				_backgroundWorkerOperationQueue.Dequeue()(sender, e);
			}

			//Console.WriteLine("jobBackgroundWorker_DoWork finished");
		}

		private void jobBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			//Console.WriteLine("jobBackgroundWorker_RunWorkerCompleted started");

			if (e.Error != null) {
				MakeProgressBarVisible(false);
				MakeControlVisible(stopButton, false);
				string status = GetExceptionMessage(e.Error, "Error: ");
				ReportStatus(status);
				MessageBox.Show(status);
			}

			if (_backgroundWorkerOperationQueue.Count > 0) {
				_backgroundWorkerOperationQueue.Dequeue()(sender, null);
			}

			if (Data.CancellationPending) {
				Data.CancellationPending = false;
			}

			//Console.WriteLine("jobBackgroundWorker_RunWorkerCompleted finished");
		}

		private void backgroundWorker_StatusChanged(object sender, StatusChangedEventArgs e) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => backgroundWorker_StatusChanged(sender, e)));
				return;
			}

			switch (e.StatusType) {
				case StatusType.Message:
					ReportStatus((string)e.Argument);
					break;
				case StatusType.ReadingInput:
					bool reading = (bool)e.Argument;
					if (reading) {
						if (_countries == null) {
							EnableControl(showCountriesButton, false);
						}
						if (_indicators == null) {
							EnableControl(showIndicatorsButton, false);
						}
						if (_years == null) {
							EnableControl(showYearsButton, false);
						}
						EnableControl(transferDataButton, false);
						EnableControl(transferDimensionsButton, false);
						EnableControl(transferFactsButton, false);
					} else {
						EnableControl(showCountriesButton, !_isCountriesShown);
						EnableControl(showIndicatorsButton, !_isIndicatorsShown);
						EnableControl(showYearsButton, !_isYearsShown);
						EnableControl(transferDataButton, true);
						EnableControl(transferDimensionsButton, true);
						EnableControl(transferFactsButton, true);
					}
					break;
				case StatusType.Progress:
					int progress = (int)((int[])e.Argument)[0];
					int total = (int)((int[])e.Argument)[1];
					if (progress >= 0) {
						progressBar.ProgressBar.Value = progress;
						progressBar.ProgressBar.Maximum = total;
						progressLabel.Text = progress.ToString() + "/" + total.ToString();
						MakeProgressBarVisible(true);
					}
					MakeControlVisible(stopButton, progress >= 0 && progress < total);
					break;
			}
		}

		private void EnableControl(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => EnableControl(control, enabled)));
				return;
			}

			control.Enabled = enabled;
		}

		private void MakeControlVisible(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeControlVisible(control, enabled)));
				return;
			}

			control.Visible = enabled;
		}

		private void MakeProgressBarVisible(bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeProgressBarVisible(enabled)));
				return;
			}

			progressBar.Visible = enabled;
			progressLabel.Visible = enabled;
		}

		private void ReportStatus(string message) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => ReportStatus(message)));
				return;
			}


			statusLabel.Text = message;

			Program.LogMessage(message);
		}

		private string GetExceptionMessage(Exception e, string prefix = null) {
			prefix = (prefix == null ? e.Message : prefix + ": " + e.Message);
			if (e.InnerException != null) {
				prefix = GetExceptionMessage(e.InnerException, prefix);
			}

			return prefix;
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}

	}
}
