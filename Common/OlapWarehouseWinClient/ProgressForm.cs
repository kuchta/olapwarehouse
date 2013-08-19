using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OlapWarehouseApi;


namespace OlapWarehouseWinClient {
	public partial class ProgressForm : Form {
		Queue<DoWorkEventHandler> _backgroundWorkerOperationQueue;

		DoWorkEventHandler _method;

		public ProgressForm(DoWorkEventHandler method, Action<StatusChangedEventHandler> statusChanged = null) {
			InitializeComponent();

			_backgroundWorkerOperationQueue = new Queue<DoWorkEventHandler>();

			_method = method;

			if (statusChanged != null) {
				statusChanged(backgroundWorker_StatusChanged);
			}
		}

		private void ProgressForm_Shown(object sender, EventArgs e) {
			RunInBackgroundWorker(_method);
		}

		//private void RunInNewBackgroundWorker(DoWorkEventHandler d) {
		//	BackgroundWorker bw = new BackgroundWorker();
		//	bw.DoWork += d;
		//	bw.RunWorkerAsync();
		//}

		private void RunInBackgroundWorker(DoWorkEventHandler d) {
			_backgroundWorkerOperationQueue.Enqueue(d);
			if (!jobBackgroundWorker.IsBusy) {
				jobBackgroundWorker.RunWorkerAsync();
			}
		}

		private void jobBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
			if (_backgroundWorkerOperationQueue.Count > 0) {
				_backgroundWorkerOperationQueue.Dequeue()(sender, e);
			}
		}

		private void jobBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if (e.Error != null) {
				progressBar.Visible = false;
				MakeControlVisible(stopButton, false);
				string status = GetExceptionMessage(e.Error, "Error: ");
				ReportStatus(status);
				MessageBox.Show(status);
			}

			if (_backgroundWorkerOperationQueue.Count > 0) {
				_backgroundWorkerOperationQueue.Dequeue()(sender, null);
			}
		}

		private void backgroundWorker_StatusChanged(StatusChangedEventArgs e) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => backgroundWorker_StatusChanged(e)));
				return;
			}

			switch (e.StatusType) {
				case StatusType.Message:
					statusLabel.Text = (string)e.Argument;
					break;
				case StatusType.Progress:
					int progress = (int)((int?[])e.Argument)[0];
					int? total = (int?)((int?[])e.Argument)[1];
					if (progress >= 0) {
						progressBar.Value = progress;
						if (total != null) {
							progressBar.Maximum = (int) total;
						}
						MakeControlVisible(progressBar, true);
						progressLabel.Text = progress.ToString() + "/" + progressBar.Maximum.ToString();
						progressLabel.Visible = true;
					}
					MakeControlEnabled(stopButton, progress >= 0 && progress < total);
					break;
			}
		}

		public void MakeControlEnabled(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeControlEnabled(control, enabled)));
				return;
			}

			//Console.WriteLine("Making control " + control.Name + " " + (enabled ? "enabled" : "disabled"));

			control.Enabled = enabled;
		}

		public void MakeControlVisible(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeControlVisible(control, enabled)));
				return;
			}

			//Console.WriteLine("Making control " + control.Name + " " + (enabled ? "visible" : "invisible"));

			control.Visible = enabled;
		}

		public void MakeProgressBarVisible(bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeProgressBarVisible(enabled)));
				return;
			}

			progressBar.Visible = enabled;
		}

		private void ReportStatus(string message) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => ReportStatus(message)));
				return;
			}

			statusLabel.Text = message;
		}

		private string GetExceptionMessage(Exception e, string prefix = null) {
			prefix = (prefix == null ? e.Message : prefix + ": " + e.Message);
			if (e.InnerException != null) {
				prefix = GetExceptionMessage(e.InnerException, prefix);
			}

			return prefix;
		}

		//private void stopButton_Click(object sender, EventArgs e) {
		//	Utils.CancellationPending = true;
		//	closeButton.DialogResult = DialogResult.Cancel;
		//}
	}
}
