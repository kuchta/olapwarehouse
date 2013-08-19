using OlapWarehouseApi;
using BrightIdeasSoftware;

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Infor.BI.Applications.OlapApi;


namespace OlapWarehouseWinClient {
	public partial class MainForm : Form {
		Queue<DoWorkEventHandler> _backgroundWorkerOperationQueue;

		private ConnectionForm _connectionForm;

		public MainForm() {
			InitializeComponent();
			_backgroundWorkerOperationQueue = new Queue<DoWorkEventHandler>();

			_connectionForm = new ConnectionForm();
			OlapWarehouse = OlapWarehouse;

			//elementsListView1.RootKeyValue = null;

			elementsListView.CanExpandGetter = (object x) => {
				return ((Element)x).Children.Count > 0;
			};

			elementsListView.ChildrenGetter = (object x) => {
				return ((Element)x).Children;
			};
		}

		private OlapWarehouse _olapWarehouse;
		public OlapWarehouse OlapWarehouse {
			get {
				if (_olapWarehouse == null) {
					_olapWarehouse = new OlapWarehouse(_connectionForm.Server, _connectionForm.Database, _connectionForm.UserName, _connectionForm.Password);

					_olapWarehouse.Servers.Load();
					_olapWarehouse.Cubes.Load();
					_olapWarehouse.Dimensions.Load();
					Servers = _olapWarehouse.Servers.Local;
					DimensionTemplates = new ObservableCollection<Dimension>(from dimension in _olapWarehouse.Dimensions.Local where dimension.Server == null && dimension.Cube == null select dimension);
				}

				return _olapWarehouse;
			}
			set {
				_olapWarehouse = value;
			}
		}

		private ObservableCollection<Server> _servers;
		public ObservableCollection<Server> Servers {
			get {
				return _servers;
			}
			set {
				_servers = value;

				if (_servers != null) {
					//Console.WriteLine("Setting Servers: {0}", value.);

					serversListView.DataSource = _servers.ToBindingList();
					if (_servers.Count > 0) {
						serversListView.SelectObject(_servers.First());
					}
				} else {
					//Console.WriteLine("Setting Servers: null");

					serversListView.DataSource = null;
				}
			}
		}

		private Server _selectedServer;
		public Server SelectedServer {
			get {
				return _selectedServer;
			}
			set {
				_selectedServer = value;

				if (_selectedServer != null) {
					Console.WriteLine("Selecting {0}", value);

					Cubes = new ObservableCollection<Cube>(_selectedServer.Cubes);
					Dimensions = new ObservableCollection<Dimension>(from dimension in _selectedServer.Dimensions where dimension.Cube == null select dimension);
				} else {
					Console.WriteLine("Selecting Server: null");

					Cubes = null;
					Dimensions = null;
				}
			}
		}

		private ObservableCollection<Cube> _cubes;
		public ObservableCollection<Cube> Cubes {
			get {
				return _cubes;
			}
			set {
				_cubes = value;

				if (_cubes != null) {
					//Console.WriteLine("Setting Cubes: {0}", value);
					cubesListView.DataSource = _cubes.ToBindingList();
					cubesGroupBox.Enabled = true;
				} else {
					//Console.WriteLine("Setting Cubes: null");
					cubesListView.DataSource = null;
					cubesListView.ClearObjects();
					cubesGroupBox.Enabled = false;
				}
			}
		}

		private Cube _selectedCube;
		public Cube SelectedCube {
			get {
				return _selectedCube;
			}
			set {
				_selectedCube = value;

				if (_selectedCube != null) {
					Console.WriteLine("Selecting {0}", value);

					Dimensions = new ObservableCollection<Dimension>(_selectedCube.Dimensions);
				} else {
					Console.WriteLine("Selecting Cube: null");

					Dimensions = null;
				}
			}
		}

		private ObservableCollection<Dimension> _dimensions;
		public ObservableCollection<Dimension> Dimensions {
			get {
				return _dimensions;
			}
			set {
				_dimensions = value;

				if (_dimensions != null) {
					//Console.WriteLine("Setting Dimensions: {0}", value);
					dimensionsListView.DataSource = _dimensions.ToBindingList();
					dimensionsGroupBox.Enabled = true;
				} else {
					//Console.WriteLine("Setting Dimensions: null");
					dimensionsListView.DataSource = null;
					dimensionsListView.ClearObjects();
					dimensionsGroupBox.Enabled = false;
				}
			}
		}

		private ObservableCollection<Dimension> _dimensionTemplates;
		public ObservableCollection<Dimension> DimensionTemplates {
			get {
				return _dimensionTemplates;
			}
			set {
				//Console.WriteLine("Setting DimensionTemplates: {0}", value);
				_dimensionTemplates = value;

				if (_dimensionTemplates != null) {
					dimensionTemplatesListView.DataSource = _dimensionTemplates.ToBindingList();
				} else {
					//Console.WriteLine("Setting Dimension Templates: null");
					dimensionTemplatesListView.DataSource = null;
					dimensionTemplatesListView.ClearObjects();
					dimensionTemplatesGroupBox.Enabled = false;
				}
			}
		}

		private Dimension _selectedDimension;
		private Dimension SelectedDimension {
			get {
				return _selectedDimension;
			}
			set {
				_selectedDimension = value;

				if (_selectedDimension != null) {
					//Console.WriteLine("Selecting {0}", value);

					IEnumerable<Element> elements;
					elements = from element in _selectedDimension.Elements where element.Parent == null select element;
					//if (_selectedDimension.IsTemplate) {
					//	elements = from element in OlapWarehouse.Elements where element.DimensionTemplateId == _selectedDimension.Id select element;
					//} else {
					//	elements = from element in OlapWarehouse.Elements where element.DimensionId == _selectedDimension.Id select element;
					//}

					Elements = new ObservableCollection<Element>(elements);

					elementsGroupBox.Enabled = true;
				} else {
					//Console.WriteLine("Unselecting dimension");

					Elements = null;
					elementsGroupBox.Enabled = false;
				}
			}
		}

		private ObservableCollection<Element> _elements;
		public ObservableCollection<Element> Elements {
			get {
				return _elements;
			}
			set {
				_elements = value;

				if (_elements != null) {
					Console.WriteLine("Setting Elements: {0}", value);

					elementsListView.Reset();
					//elementsListView1.Reset();

					OLVColumn column;
					column = new OLVColumn(OlapWarehouse.COLUMN_NAME, OlapWarehouse.COLUMN_NAME);
					column.FillsFreeSpace = true;
					column.FreeSpaceProportion = 3;
					elementsListView.AllColumns.Add(column);
					//elementsListView1.AllColumns.Add(column);

					column = new OLVColumn(OlapWarehouse.COLUMN_CAPTION, OlapWarehouse.COLUMN_CAPTION);
					column.FillsFreeSpace = true;
					column.FreeSpaceProportion = 3;
					elementsListView.AllColumns.Add(column);
					//elementsListView1.AllColumns.Add(column);

					if (_elements.Count > 0) {
						foreach (var columnName in SelectedDimension.ExtendedColumns) {
							column = new OLVColumn(); //attribute,, "AttributesDict[" + attribute + "]");
							column.Text = columnName;
							column.AspectGetter = (object x) => { return ((Element)x).GetAttributeValue(columnName); };
							column.AspectPutter = (object x, object val) => { ((Element)x).SetAttributeValue(columnName, (string)val); };
							column.FillsFreeSpace = true;
							column.MinimumWidth = 20;
							elementsListView.AllColumns.Add(column);
							//elementsListView1.AllColumns.Add(column);
						}

						elementsExportButton.Enabled = true;
						elementsShowSqlButton.Enabled = true;
					} else {
						elementsExportButton.Enabled = false;
						elementsShowSqlButton.Enabled = false;
					}

					elementsListView.RebuildColumns();
					elementsListView.Roots = _elements.ToBindingList();

					//elementsListView1.RebuildColumns();
					//elementsListView1.DataSource = _elements.ToBindingList();
				} else {
					Console.WriteLine("Setting Elements: null");
					//elementsListView.DataSource = null;
					elementsListView.Reset();
				}
			}
		}

		private void serversListView_SelectionChanged(object sender, EventArgs e) {
			SelectedServer = (Server)serversListView.SelectedObject;
		}

		private void serversAddButton_Click(object sender, EventArgs e) {
			var dimension = new Server("New item");
			Servers.Add(dimension);
			serversListView.SelectObject(dimension);
			serversListView.StartCellEdit(serversListView.SelectedItem, 0);
		}

		private void serversDeleteButton_Click(object sender, EventArgs e) {
			Servers.Remove((Server)serversListView.SelectedObject);
			serversListView.SelectObject(null);
		}

		private void cubesListView_SelectionChanged(object sender, EventArgs e) {
			SelectedCube = (Cube)cubesListView.SelectedObject;
		}

		private void cubesAddButton_Click(object sender, EventArgs e) {
			var cube = new Cube("New item");
			Cubes.Add(cube);
			SelectedServer.Cubes.Add(cube);
			cubesListView.SelectObject(cube);
			cubesListView.StartCellEdit(cubesListView.SelectedItem, 0);
		}

		private void cubesDeleteButton_Click(object sender, EventArgs e) {
			var cube = (Cube)cubesListView.SelectedObject;
			OlapWarehouse.Cubes.Remove(cube);
			Cubes.Remove(cube);
			cubesListView.SelectObject(null);
		}

		private void dimensionsListView_SelectionChanged(object sender, EventArgs e) {
			var dimension = (Dimension)dimensionsListView.SelectedObject;
			if (dimension != null) {
				SelectedDimension = dimension;
				dimensionTemplatesListView.SelectedObject = null;
			}
		}

		private void dimensionsAddButton_Click(object sender, EventArgs e) {
			var dimension = new Dimension("New item");
			Dimensions.Add(dimension);
			SelectedServer.Dimensions.Add(dimension);
			dimensionsListView.SelectObject(dimension);
			dimensionsListView.StartCellEdit(dimensionsListView.SelectedItem, 0);
		}

		private void dimensionsDeleteButton_Click(object sender, EventArgs e) {
			var dimension = (Dimension)dimensionsListView.SelectedObject;
			OlapWarehouse.Dimensions.Remove(dimension);
			Dimensions.Remove(dimension);
			dimensionsListView.SelectedItems.Clear();
			Elements = null;
		}

		private void dimensionTemplatesListView_SelectionChanged(object sender, EventArgs e) {
			var dimension = (Dimension)dimensionTemplatesListView.SelectedObject;
			if (dimension != null) {
				SelectedDimension = dimension;
				dimensionsListView.SelectedObject = null;
			}
		}

		private void dimensionTemplatesAddButton_Click(object sender, EventArgs e) {
			var dimension = new Dimension("New item");
			DimensionTemplates.Add(dimension);
			OlapWarehouse.Dimensions.Add(dimension);
			dimensionTemplatesListView.SelectObject(dimension);
			dimensionTemplatesListView.StartCellEdit(dimensionTemplatesListView.SelectedItem, 0);
		}

		private void dimensionTemplatesDeleteButton_Click(object sender, EventArgs e) {
			var dimension = (Dimension)dimensionsListView.SelectedObject;
			OlapWarehouse.Dimensions.Remove(dimension);
			DimensionTemplates.Remove(dimension);
			dimensionTemplatesListView.SelectedItems.Clear();
			Elements = null;
		}

		private void elementsAddButton_Click(object sender, EventArgs e) {
			var element = new Element("New item");
			Elements.Add(element);
			SelectedDimension.Elements.Add(element);
			elementsListView.SelectObject(element);
			//elementsListView.StartCellEdit(elementsListView.SelectedItem, 0);
		}

		private void elementsDeleteButton_Click(object sender, EventArgs e) {
			Elements.Remove((Element)elementsListView.SelectedObject);
		}

		private void elementsImportButton_Click(object sender, EventArgs e) {
			var fileDialog = new OpenFileDialog();
			fileDialog.DefaultExt = ".xls"; // Default file extension 
			fileDialog.Filter = "Excel documents|*.xlsx"; // Filter files by extension

			if (fileDialog.ShowDialog() == DialogResult.OK) {
				//using (var file = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read)) {
				var file = new FileInfo(fileDialog.FileName);
				var importForm = new ImportForm(file);

				if (importForm.ShowDialog() == DialogResult.OK) {
					var progressForm = new ProgressForm(
						(a, b) => SelectedDimension.ImportDimension(file, importForm.Sheet, importForm.IdColumn, importForm.ParentColumn, importForm.ParentSeparator, importForm.CaptionColumn, importForm.Columns),
						handler => Dimension.StatusChanged += handler
					);
					progressForm.ShowDialog();
					Elements = new ObservableCollection<Element>(SelectedDimension.Elements);
				}
				//}
			}
		}

		private void elementsExportButton_Click(object sender, EventArgs e) {
			var fileDialog = new SaveFileDialog();
			fileDialog.DefaultExt = ".xls"; // Default file extension 
			fileDialog.Filter = "Excel documents|*.xlsx"; // Filter files by extension

			if (fileDialog.ShowDialog() == DialogResult.OK) {
				//using (var stream = new FileStream(fileDialog.FileName, FileMode.Create, FileAccess.ReadWrite)) {
				var file = new FileInfo(fileDialog.FileName);
				var progressForm = new ProgressForm(
						(a, b) => SelectedDimension.ExportDimension(file),
						handler => Dimension.StatusChanged += handler
					);
				progressForm.ShowDialog();
				//}
			}
		}

		private void connectButton_Click(object sender, EventArgs e) {
			_connectionForm.ShowDialog();
			if (_connectionForm.DialogResult == DialogResult.OK) {
				OlapWarehouse = OlapWarehouse;
			}
		}

		private void saveButton_Click(object sender, EventArgs e) {
			SaveChanges();
		}

		private void PrintElementTree(IEnumerable<Element> elements, int level = 0) {
			foreach (var element in elements) {
				Console.WriteLine("{0}Element: Name: \"{1}\", Caption: \"{2}\"", new string('\t', level), element.Name, element.Caption);
				foreach (var attr in element.Attributes) {
					Console.WriteLine("{0}Attribute: Name: \"{1}\", Value: \"{2}\"", new string('\t', level + 1), attr.Name, attr.Value);
				}

				PrintElementTree(element.Children, level + 1);
			}
		}

		private void SaveChanges() {
			RunInBackgroundWorker((a, b) => {
				MakeControlUseWaitCursor(this, true);
				MakeControlEnabled(this, false);
				try {
					OlapWarehouse.SaveChanges();
				} catch (DbEntityValidationException e) {
					foreach (var validationResult in e.EntityValidationErrors) {
						foreach (var validationError in validationResult.ValidationErrors) {
							Console.WriteLine("Validation failed for property {0} of entity {1}. Error: {2}", validationError.PropertyName, validationResult.Entry.Entity, validationError.ErrorMessage);
						}
					}
				}
				MakeControlUseWaitCursor(this, false);
				MakeControlEnabled(this, true);
				MessageBox.Show("Done saving changes!");
			});
		}

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
				MessageBox.Show(GetExceptionMessage(e.Error, "Error: "));
			}

			if (_backgroundWorkerOperationQueue.Count > 0) {
				_backgroundWorkerOperationQueue.Dequeue()(sender, null);
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

		public void MakeControlUseWaitCursor(Control control, bool enabled) {
			if (InvokeRequired) {
				Invoke(new MethodInvoker(() => MakeControlUseWaitCursor(control, enabled)));
				return;
			}

			control.UseWaitCursor = enabled;
		}

		private string GetExceptionMessage(Exception e, string prefix = null) {
			prefix = (prefix == null ? e.Message : prefix + ": " + e.Message);
			if (e.InnerException != null) {
				prefix = GetExceptionMessage(e.InnerException, prefix);
			}

			return prefix;
		}

		private void printChangedEntitiesButton_Click(object sender, EventArgs e) {
			var log = new StringBuilder();
			var entries = from entry in OlapWarehouse.ChangeTracker.Entries() where entry.State != EntityState.Unchanged select entry;
			//Console.WriteLine("Changed entities tracked by context:");
			foreach (var entry in entries) {
				log.Append(string.Format("entity: {0}, state: {1}", entry.Entity, entry.State));
			}

			MessageBox.Show(log.ToString());
		}

		private void elementsShowSqlButton_Click(object sender, EventArgs e) {
			MessageBox.Show(SelectedDimension.ShowSQL());
			//Console.WriteLine(SelectedDimension.ShowSQL());
		}

		private void showOlapStoreButton_Click(object sender, EventArgs e) {
			var store = new OlapStore("admin");
			var server = store.Servers["LOCAL/WDI"];
			server.Connect("Admin", "");
			TreeForm.Show(server);
			server.Disconnect();
		}
	}
}
