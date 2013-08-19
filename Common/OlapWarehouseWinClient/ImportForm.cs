using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OlapWarehouseApi;


namespace OlapWarehouseWinClient {
	public partial class ImportForm : Form {
		FileInfo _file;

		public string Sheet { get; set; }
		public string IdColumn { get; set; }
		public string ParentColumn { get; set; }
		public char ParentSeparator { get; set; }
		public string CaptionColumn { get; set; }
		public ICollection<string> Columns { get; set; }

		public ImportForm(FileInfo file) {
			InitializeComponent();

			_file = file;

			IEnumerable<string> sheets = null;

			jobBackgroundWorker.DoWork += (a, b) => {
				MakeControlEnabled(this, false);
				MakeControlUseWaitCursor(this, true);

				sheets = Utils.GetSheets(file);
			};

			jobBackgroundWorker.RunWorkerCompleted += (a, b) => {
				MakeControlUseWaitCursor(this, false);
				MakeControlEnabled(this, true);

				sheetComboBox.Items.Clear();
				foreach (var sheet in sheets) {
					sheetComboBox.Items.Add(sheet);
				}
				Sheet = sheetComboBox.Text;
				sheetComboBox.Enabled = true;

				sheetComboBox.SelectedIndex = 0;
			};

			jobBackgroundWorker.RunWorkerAsync();
		}

		public new void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		//private void tbInputFile_TextChanged(object sender, EventArgs e) {
			//if (!File.Exists(inputFileTextBox.Text)) {
			//	if (_processData != null) {
			//		Data.Dispose();
			//		Data = null;
			//	}

			//	sheetComboBox.Items.Clear();
			//	foreach (var sheet in Data.Sheets) {
			//		sheetComboBox.Items.Add(sheet);
			//	}
			//	sheetComboBox.Enabled = true;
			//}
			//ReportStatus("Ready to serve you, Master!");
		//}

		//private void btnInputFile_Click(object sender, EventArgs e) {
			//OpenFileDialog ofd = new OpenFileDialog();
			//ofd.DefaultExt = ".xls"; // Default file extension 
			//ofd.Filter = "Excel documents|*.xlsx"; // Filter files by extension

			//if (ofd.ShowDialog() == DialogResult.OK) {
			//	inputFileTextBox.Text = ofd.FileName;
			//	inputFileTextBox.SelectionStart = inputFileTextBox.Text.Length;
			//	inputFileTextBox.ScrollToCaret();
			//	inputFileTextBox.Refresh();

			//	if (_processData != null) {
			//		Data.Dispose();
			//		Data = null;
			//	}

			//	sheetComboBox.Items.Clear();
			//	foreach (var sheet in Data.Sheets) {
			//		sheetComboBox.Items.Add(sheet);
			//	}
			//	sheetComboBox.Enabled = true;
			//}
		//}

		//private void fileTextBox_DragEnter(object sender, DragEventArgs e) {
		//	if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true) {
		//		e.Effect = DragDropEffects.All;
		//	}
		//}

		//private void fileTextBox_DragDrop(object sender, DragEventArgs e) {
		//	TextBox s = (TextBox)sender;
		//	string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
		//	if (files != null && files.Length != 0) {
		//		s.Text = files[0];
		//		s.SelectionStart = s.Text.Length;
		//		s.ScrollToCaret();
		//		s.Refresh();
		//	}
		//}

		private void sheetComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			Sheet = (string) sheetComboBox.SelectedItem;

			string[] columns = Utils.GetColumns(_file, Sheet).ToArray();

			var columnCount = columns.Length;

			idColumnComboBox.Items.AddRange(columns);
			idColumnComboBox.SelectedIndex = 0;
			idColumnComboBox.Enabled = true;

			parentColumnComboBox.Items.AddRange(columns);
			if (1 < columnCount) {
				parentColumnComboBox.SelectedIndex = 1;
			}
			parentColumnComboBox.Enabled = true;

			parentSeparatorComboBox.SelectedIndex = 0;
			parentSeparatorComboBox.Enabled = true;

			captionColumnComboBox.Items.AddRange(columns);
			if (1 < columnCount) {
				captionColumnComboBox.SelectedIndex = 2;
			}
			captionColumnComboBox.Enabled = true;

			importColumnsListBox.Items.AddRange(columns);
			for (int i = 0; i <= 2 && i < columnCount; i++) {
				importColumnsListBox.SetItemChecked(i, true);
			}
			importColumnsListBox.Enabled = true;

			startButton.Enabled = true;
		}

		private void idColumnComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			IdColumn = (string)idColumnComboBox.SelectedItem;
		}

		private void parentColumnComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			ParentColumn = (string)parentColumnComboBox.SelectedItem;
		}

		private void parentSeparatorComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			ParentSeparator = parentSeparatorComboBox.Text[0];
		}


		private void parentSeparatorComboBox_TextChanged(object sender, EventArgs e) {
			ParentSeparator = parentSeparatorComboBox.Text[0];
		}

		private void captionColumnComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			CaptionColumn = (string)captionColumnComboBox.SelectedItem;
		}

		private void importColumnsListBox_ItemCheck(object sender, ItemCheckEventArgs e) {
			Columns = new List<string>();
			foreach (var item in importColumnsListBox.CheckedItems) {
				Columns.Add(item.ToString());
			}
		}

		public void MakeControlUseWaitCursor(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeControlUseWaitCursor(control, enabled)));
				return;
			}

			control.UseWaitCursor = enabled;
		}

		public void MakeControlEnabled(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeControlEnabled(control, enabled)));
				return;
			}

			//Console.WriteLine("Making control " + control.Name + " " + (enabled ? "enabled" : "disabled"));

			control.Enabled = enabled;
		}
	}
}
