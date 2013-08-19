using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OlapWarehouseApi;
using OfficeOpenXml;
using Excel;

namespace OlapWarehouseWinClient {
	public static class ProcessData {
		public event StatusChangedEventHandler StatusChanged;

		private const char HIERARCHY_SEPARATOR = '/';

		private const string COLUMN_ID = "Id";
		private const string COLUMN_CAPTION = "Caption";
		private const string COLUMN_PARENT = "Parent";
		private const string COLUMN_WEIGHT = "Weight";
		private const string COLUMN_ORDER = "Order";

		private string _inputConnectionString;
		private OleDbConnection _inputFileConnection;

		ExcelPackage _excel;

		private SortedSet<string> _uniqueElementIds = new SortedSet<string>();

		private Dictionary<string, IDictionary<string, Element>> _dimensions = new Dictionary<string, IDictionary<string, Element>>();

		public ProcessData(string fileName) {
			//_inputConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + inputFileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=0;TypeGuessRows=0\"";
			//_inputConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=0\"";

		}

		public void Dispose() {
			ReportStatus("Closing input file...");
			//InputFileConnection.Close();

			_excel.Dispose();
			ReportStatus("Input file closed.");
		}

		public bool CancellationPending { get; set; }

		private OleDbConnection InputFileConnection {
			get {
				if (_inputFileConnection == null) {
					ReportStatus("Opening input file...");
					_inputFileConnection = new OleDbConnection(_inputConnectionString);
					_inputFileConnection.Open();
					ReportStatus("Input file opened.");
				}
				return _inputFileConnection;
			}
			set {
				_inputFileConnection = value;
			}
		}

		public static IEnumerable<string> GetSheets(FileStream stream) {
			//ReportStatus("Reading sheets...");

			//ReportReadingInput(true);

			//DataTable dt = InputFileConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			//if (dt == null || dt.Rows.Count < 1) {
			//	ReportStatus("Couldn't read file");
			//	return null;
			//}

			//var sheets = new List<string>();
			//foreach (DataRow row in dt.Rows) {
			//	sheets.Add(row["TABLE_NAME"].ToString().TrimEnd(new char[] { '$' }));
			//}

			ExcelPackage excelPackage = new ExcelPackage(stream);

			var sheets = new List<string>();
			foreach (var sheet in excelPackage.Workbook.Worksheets) {
				sheets.Add(sheet.Name);
			}

			//ReportReadingInput(false);
			//ReportStatus("Sheets read.");

			return sheets;
		}

		public static List<string> GetColumns(FileStream stream, string sheet) {
			//ReportStatus("Reading sheets...");

			//ReportReadingInput(true);

			ExcelPackage excelPackage = new ExcelPackage(stream);

			var workSheet = excelPackage.Workbook.Worksheets[sheet];
			var workSheetDimension = workSheet.Dimension;

			var columns = new List<string>();
			for (int columnIndex = workSheetDimension.Start.Column; columnIndex <= workSheetDimension.End.Column; columnIndex++) {
				columns.Add(workSheet.Cells[workSheetDimension.Start.Row, columnIndex].Text);
			}

			//ReportReadingInput(false);
			//ReportStatus("Sheets read.");

			return columns;
		}

		public ICollection<Element> GetDimension(string dimensionName) {
			if (!_dimensions.ContainsKey(dimensionName)) {
				_dimensions.Add(dimensionName, ParseDimension(dimensionName));
			}

			return _dimensions[dimensionName].Values;
		}

		public IDictionary<string, Element> ImportDimension(string sheet, string idColumn, string parentColumn, char parentSeparator, string captionColumn) {
			ReportStatus("Reading elements...");

			IDictionary<string, Element> elements = new Dictionary<string, Element>();

			ReportReadingInput(true);

			//DbDataReader reader = new OleDbCommand("SELECT * FROM [" + sheetName + "$]", InputFileConnection).ExecuteReader();
			var workSheet = _excel.Workbook.Worksheets[sheet];
			var workSheetDimension = workSheet.Dimension;
			//while (reader.Read()) {

			var columnNames = new Dictionary<int, string>();
			for (int columnIndex = workSheetDimension.Start.Column; columnIndex <= workSheetDimension.End.Column; columnIndex++) {
				columnNames[columnIndex] = workSheet.Cells[workSheetDimension.Start.Row, columnIndex].Text;
			}

			ReportProgress(0, workSheetDimension.End.Row);

			for (int rowIndex = workSheetDimension.Start.Row + 1; rowIndex <= workSheetDimension.End.Row; rowIndex++) {
				string id = null;
				string caption = null;
				string parent = null;
				float? weight = null;
				short? order = null;
				Dictionary<string, string> attributes = new Dictionary<string, string>();

				//for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++) {
				for (int columnIndex = workSheetDimension.Start.Column; columnIndex <= workSheetDimension.End.Column; columnIndex++) {

					//string columnName = reader.GetName(columnIndex);
					//dynamic columnValue = reader.GetValue(columnIndex);
					string columnName = columnNames[columnIndex];
					dynamic cellValue = workSheet.Cells[rowIndex, columnIndex].Value;

					if (cellValue == null) {
						//Program.LogMessage("ImportExcel: Value at address " + rowIndex + ":" + columnIndex + " is null, skipping...");
						continue;
					}

					if (columnName == idColumn) {
						id = cellValue;
					} else if (columnName == captionColumn) {
						caption = cellValue;
					} else if (columnName == parentColumn) {
						parent = cellValue;
					} else if (columnName == COLUMN_WEIGHT) {
						weight = cellValue;
					} else if (columnName == COLUMN_ORDER) {
						order = cellValue;
					} else {
						attributes.Add(columnName, cellValue.ToString());
					}
				}

				Element node;
				var parentNode = elements;
				if (parent != null) {
					string[] parentPathArray = parent.Split(new char[] { parentSeparator }, StringSplitOptions.RemoveEmptyEntries);

					foreach (string parentPathItem in parentPathArray) {
						string parentName = ProcessId(parentPathItem.Trim());

						if (parentNode == null || !parentNode.ContainsKey(parentName)) {
							node = new Element(parentName, parentName);
							parentNode.Add(parentName, node);
							parentNode = node;
						} else {
							parentNode = parentNode[parentName];
						}
					}
				}

				node = new Element(ProcessId(id), caption);
				foreach (var pair in attributes) {
					node.Attributes.Add(new OlapWarehouseApi.Attribute(ProcessId(pair.Key), pair.Value));
				}

				parentNode.Add(node.Name, node);

				ReportProgress(rowIndex, workSheetDimension.End.Row);
				if (CancellationPending) {
					ReportOperationCanceled();
					return elements;
				}
			}

			ReportReadingInput(false);
			ReportStatus("Elements read.");

			return elements;
		}





		public void TransferDimension(string dimensionName, IDimension dimension) {
			foreach (var pair in GetDimension(dimensionName)) {
				dimension.Elements.Add(pair.Value);
			}
		}

		private void TransferDimension(OlapWarehouseContext context, Dimension dimension) {
			ReportStatus("Transfering dimension " + dimension.Id + "...");

			var dim = context.Dimensions.Find(dimension.Id);
			if (dim != null) {
				context.Dimensions.Remove(dim);
			}
			context.Dimensions.Add(dimension);
			try {
				context.SaveChanges();
			} catch (DbEntityValidationException e) {
				foreach (var result in e.EntityValidationErrors) {
					foreach (var error in result.ValidationErrors) {
						ReportStatus(result.Entry.Entity + ": " + error.ErrorMessage);
					}
				}
			}

			ReportStatus("Dimension " + dimension.Id + " transfered.");
		}

		private void TransferFacts(SqlConnection sqlConnection) {
			TransferFacts(sqlConnection, null);
		}

		private void TransferFacts(StreamWriter outputFile) {
			TransferFacts(null, outputFile);
		}

		private void TransferFacts(SqlConnection sqlConnection, StreamWriter outputFile) {
			ReportStatus("Transfering facts...");

			SqlBulkCopy sqlBulkCopy = null;
			SqlTransaction sqlTransaction = null;

			if (sqlConnection != null) {
				sqlTransaction = sqlConnection.BeginTransaction();

				SqlCommand sqlCommand = sqlConnection.CreateCommand();
				sqlCommand.Transaction = sqlTransaction;

				ReportStatus("Truncating facts table...");
				ExecuteSqlCommand(sqlCommand, "TRUNCATE TABLE " + FACTS);
				ReportStatus("Done truncating facts table.");

				sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction);
				sqlBulkCopy.DestinationTableName = "Facts";
			}

			ReportReadingInput(true);

			if (CancellationPending) {
				ReportOperationCanceled();
				return;
			}

			ReportStatus("Reading input file...");

			OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM [" + FACTS_SHEET_NAME + "]", InputFileConnection);
			int totalLines = (int)command.ExecuteScalar();

			ReportProgress(0, totalLines);

			command = new OleDbCommand("SELECT * FROM [" + FACTS_SHEET_NAME + "]", InputFileConnection);
			DbDataReader reader = command.ExecuteReader();

			Dictionary<int, int> years = new Dictionary<int, int>();
			for (int columnCounter = 0; columnCounter < reader.FieldCount; columnCounter++) {
				int year;
				if (int.TryParse(reader.GetName(columnCounter), out year)) {
					years.Add(year, columnCounter);
				}
			}

			ReportStatus("Reading data...");

			List<Fact> facts = null;

			for (int lineCounter = 0; reader.Read(); lineCounter++) {
				if (CancellationPending) {
					ReportOperationCanceled();
					return;
				}

				string country = GetString(reader, "Country Code");
				string indicator = GetString(reader, "Indicator Code");
				double value;

				if (facts == null) {
					facts = new List<Fact>();
				}

				foreach (var yearPair in years) {
					int year = yearPair.Key;
					int columnIndex = yearPair.Value;

					if (reader.IsDBNull(columnIndex)) {
						continue;
					}

					try {
						value = reader.GetDouble(columnIndex);
					} catch {
						double.TryParse(reader.GetValue(columnIndex).ToString(), out value);
						try {
						} catch {
							LogMessage("Warning: Couldn't recognize value in " + columnIndex + ". column (" + year + ") on line " + lineCounter + 2 + ". [" + reader[columnIndex].ToString() + "] as double value");
							continue;
						}
					}

					Fact fact = new Fact {
						CountryId = country,
						IndicatorId = indicator,
						YearId = year,
						MeasureId = "Value",
						Value = (double)value
					};
					facts.Add(fact);
				}

				ReportProgress(lineCounter + 1, totalLines);

				//Write Data to Destination
				if ((lineCounter + 1) % 10000 == 0) {
					WriteToDestination(facts, sqlBulkCopy, outputFile);
					facts = null;
					GC.Collect();
				}
			}

			WriteToDestination(facts, sqlBulkCopy, outputFile);

			ReportReadingInput(false);

			if (sqlTransaction != null) {
				sqlTransaction.Commit();
			}

			ReportStatus("Done transfering facts.");
		}

		private void WriteToDestination(List<Fact> facts, SqlBulkCopy sqlBulkCopy, StreamWriter outputFile) {
			ReportStatus("Writing data...");
			if (outputFile != null) {
				StringBuilder sb = new StringBuilder();
				var factsReader = EntityDataReaderExtensions.AsDataReader<Fact>(facts);
				while (factsReader.Read()) {
					for (int i = 0; i < factsReader.FieldCount; i++) {
						if (i > 0) {
							sb.Append('\t');
						}
						sb.Append(factsReader[i].ToString());
					}
					sb.Append(Environment.NewLine);
				}
				outputFile.Write(sb);
			}

			if (sqlBulkCopy != null) {
				sqlBulkCopy.WriteToServer(EntityDataReaderExtensions.AsDataReader<Fact>(facts));

			}
			ReportStatus("Reading data...");
		}

		private void ExecuteSqlCommand(SqlCommand sqlCommand, string commandText) {
			sqlCommand.CommandText = commandText;
			sqlCommand.ExecuteNonQuery();
		}

		private string ProcessId(string id) {
			if (id == null) {
				id = System.Guid.NewGuid().ToString();
			} else {
				id = id.Trim();
				id = Regex.Replace(id, "&", "and", RegexOptions.Compiled);
				id = Regex.Replace(id, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
				//int nextInc = 1;
				//while (_uniqueElementIds.Contains(id)) {
				//	Match match = Regex.Match(id, @"(.*\w+)\s*\((\d+)\)");
				//	if (match.Success) {
				//		id = match.Groups[1].Value;
				//		nextInc = int.Parse(match.Groups[2].Value) + 1;
				//	}
				//	id = String.Format("{0} ({1})", id, nextInc);
				//}
			}

			//_uniqueElementIds.Add(id);

			return id;
		}

		private void ReportOperationCanceled() {
			ReportStatus("Operation canceled.");
			ReportProgress(-1);
			ReportReadingInput(false);
		}

		private void ReportStatus(string status) {
			Program.LogMessage(status);
			StatusChanged(this, new StatusChangedEventArgs(StatusType.Message, status));
		}

		private void ReportReadingInput(bool reading) {
			StatusChanged(this, new StatusChangedEventArgs(StatusType.ReadingInput, reading));
		}

		private void ReportProgress(int progress, int total = 100) {
			StatusChanged(this, new StatusChangedEventArgs(StatusType.Progress, new int[] { progress, total }));
		}
	}

	public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

	public enum Result {
		Success, Failure, Canceled
	};

	public enum StatusType {
		Message, ReadingInput, Progress
	};

	public class StatusChangedEventArgs : EventArgs {
		public StatusType StatusType {
			get;
			set;
		}
		public object Argument {
			get;
			set;
		}

		public StatusChangedEventArgs(StatusType type, object argument)
			: base() {
			StatusType = type;
			Argument = argument;
		}
	}
}
