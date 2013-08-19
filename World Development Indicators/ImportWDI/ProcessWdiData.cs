using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using Microsoft.Samples.EntityDataReader;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Text;

using OlapWarehouseApi;
using OfficeOpenXml;
using Excel;

namespace ImportWDI {
	class ProcessWdiData : IDisposable {
		public const string DATABASE = "WDI";
		public const string CUBE = "WDI";

		public const string COUNTRIES = "Countries";
		private const string COUNTRIES_SHEET_NAME = "Country";
		private const string COUNTRIES_REGIONS = "All regions";
		private const string COUNTRIES_AGGREGATES = "Aggregates";

		public const string INDICATORS = "Indicators";
		private const string INDICATORS_SHEET_NAME = "Series";
		private const string INDICATORS_ALL_INDICATORS = "All indicators";

		public const string YEARS = "Years";
		private const string YEARS_ALL_YEARS = "All years";

		private const string FACTS = "Facts";
		private const string FACTS_SHEET_NAME = "Data";

		private const string COUNTRIES_COLUMN_Id = "Id";
		private const string COUNTRIES_COLUMN_Parent = "Parent";
		private const string COUNTRIES_COLUMN_Caption = "Caption";
		private const string COUNTRIES_COLUMN_CurrencyUnit = "Currency Unit";
		private const string COUNTRIES_COLUMN_ISO_3166_numeric_code = "ISO 3166 numeric code";
		private const string COUNTRIES_COLUMN_ISO_3166_alpha_2_code = "ISO 3166 alpha-2 code";
		private const string COUNTRIES_COLUMN_WB_2_code = "WB-2 code";
		private const string COUNTRIES_COLUMN_ISO_3166_alpha_3_code = "ISO 3166 alpha-3 code";
		private const string COUNTRIES_COLUMN_WB_3_code = "WB-3 code";

		private const string INDICATORS_COLUMN_Id = "SeriesCode";
		private const string INDICATORS_COLUMN_Caption = "Indicator Name";
		private const string INDICATORS_COLUMN_Topic = "Topic";
		private const string INDICATORS_COLUMN_Definition = "Long definition";
		private const string INDICATORS_COLUMN_Source = "Source";
		private const string INDICATORS_COLUMN_Periodicity = "Periodicity";
		private const string INDICATORS_COLUMN_AggregationMethod = "Aggregation method";

		private const string FACTS_COLUMN_CountryCode = "Country Code";
		private const string FACTS_COLUMN_IndicatorCode = "Indicator Code";


		private SortedSet<string> _uniqueIds = new SortedSet<string>();

		private IDictionary<string, int> _elementNameToIdMap = new Dictionary<string, int>();

		//private OleDbConnection _inputFileConnection;
		//private string _inputConnectionString;

		//IDataReader _inputFileDataReader;
		DataSet _inputFileDataSet;
		ExcelPackage _excelPackage;
		ExcelWorkbook _excelWorkbook;

		public event StatusChangedEventHandler StatusChanged;

		public ProcessWdiData(string inputFile) {
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
			ci.NumberFormat.NumberDecimalSeparator = ".";
			System.Threading.Thread.CurrentThread.CurrentCulture = ci;

			Program.LogMessage("Opening ExcelPackage...");

			_excelPackage = new ExcelPackage(new FileInfo(inputFile));
			//_excelPackage = new ExcelPackage(File.Open(inputFile, FileMode.Open, FileAccess.Read));
			_excelWorkbook = _excelPackage.Workbook;


			//Program.LogMessage("Opening ExcelReaderFactory...");

			//var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			//excelDataReader.IsFirstRowAsColumnNames = true;

			//_inputFileDataSet = excelDataReader.AsDataSet();

			//_inputConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + inputFile + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;READONLY=TRUE;IMEX=1;ImportMixedTypes=Text;TypeGuessRows=0\"";
		}

		public void Dispose() {
			ReportStatus("Closing input file...");
			_excelPackage.Dispose();
			_inputFileDataSet.Dispose();
			//_inputFileConnection.Close();
		}

		public bool CancellationPending {
			get;
			set;
		}

		//private OleDbConnection InputFileConnection {
		//	get {
		//		if (_inputFileConnection == null) {
		//			ReportStatus("Opening input file...");
		//			_inputFileConnection = new OleDbConnection(_inputConnectionString);
		//			_inputFileConnection.Open();
		//			//ReportStatus("Done opening input file.");
		//		}
		//		return _inputFileConnection;
		//	}
		//	set {
		//		_inputFileConnection = value;
		//	}
		//}


		private Dimension _countries;
		public Dimension Countries {
			get {
				if (_countries == null) {
					_countries = LoadCountries();

				}
				return _countries;
			}
		}

		private Dimension _indicators;
		public Dimension Indicators {
			get {
				if (_indicators == null) {
					_indicators = LoadIndicators();
				}
				return _indicators;
			}
		}

		private Dimension _years;
		public Dimension Years {
			get {
				if (_years == null) {
					_years = LoadYears();
				}
				return _years;
			}
		}

		public void TransferData(string serverName, string databaseName, string userId, string password) {
			ReportStatus("Transfering data...");

			using (var olapWarehouse = new OlapWarehouse(serverName, databaseName, userId, password)) {
				TransferDimensions(olapWarehouse);
				TransferFacts(olapWarehouse);
			}

			ReportStatus("Done transfering data.");
		}

		public void TransferDimensions(string serverName, string databaseName, string userId, string password) {
			using (var olapWarehouse = new OlapWarehouse(serverName, databaseName, userId, password)) {
				TransferDimensions(olapWarehouse);
			}
		}

		public void TransferFacts(string serverName, string databaseName, string userId, string password) {
			using (var olapWarehouse = new OlapWarehouse(serverName, databaseName, userId, password)) {
				TransferFacts(olapWarehouse);
			}
		}

		private void TransferDimensions(OlapWarehouse olapWarehouse) {
			ReportStatus("Transfering dimensions...");

			var database = (from db in olapWarehouse.Servers where db.Name == DATABASE select db).FirstOrDefault();
			if (database == null) {
				database = new Server(DATABASE);
				olapWarehouse.Servers.Add(database);
			}

			var cube = (from c in database.Cubes where c.Name == CUBE select c).FirstOrDefault();
			if (cube == null) {
				cube = new Cube(CUBE);
				database.AddCube(cube);
			} else {
				ReportStatus("Deleting dimensions...");
				foreach (var dimension in cube.Dimensions.ToList<Dimension>()) {
					if (CancellationPending) {
						ReportOperationCanceled();
						return;
					}

					cube.Dimensions.Remove(dimension);
				}
				ReportStatus("Done deleting dimensions.");
			}

			cube.AddDimension(Countries);
			cube.AddDimension(Indicators);
			cube.AddDimension(Years);

			ReportStatus("Saving changes...");
			try {
				olapWarehouse.SaveChanges();
			} catch (InvalidOperationException e) {
				MessageBox.Show(e.Message);
			}
			ReportStatus("Done saving changes.");

			ReportStatus("Done transfering dimensions.");
		}

		private void TransferFacts(OlapWarehouse olapWarehouse) {
			var sqlConnection = olapWarehouse.SqlConnection;

			Cube cube = null;
			var database = (from db in olapWarehouse.Servers where db.Name == DATABASE select db).FirstOrDefault();
			if (database == null) {
				database = new Server(DATABASE);
				olapWarehouse.Servers.Add(database);
			}

			cube = (from c in database.Cubes where c.Name == CUBE select c).FirstOrDefault();
			if (cube == null) {
				cube = new Cube(CUBE);
				database.AddCube(cube);
			} else {
				ReportStatus("Deleting facts...");

				sqlConnection.Open();
				ExecuteSqlCommand(sqlConnection, "DELETE FROM " + FACTS + " WHERE CubeId = '" + cube.Id + "'");
				sqlConnection.Close();

				//int counter = 0;
				//int totalLines = cube.Facts.Count;

				//ReportProgress(0, totalLines);
				//foreach (var fact in cube.Facts) {
				//	olapWarehouse.Facts.Remove(fact);

				//ReportProgress(++counter, totalLines);
				//}
				ReportStatus("Done deleting facts.");
			}

			ReportStatus("Saving changes...");
			olapWarehouse.SaveChanges();
			ReportStatus("Done saving changes.");

			sqlConnection.Open();
			TransferFacts(cube, sqlConnection);
			sqlConnection.Close();
		}

		private Dimension LoadCountries() {
			ReportStatus("Reading countries...");

			var dimension = new Dimension(COUNTRIES);

			var regionsTopNode = new Element(COUNTRIES_REGIONS, COUNTRIES_REGIONS);
			dimension.Add(COUNTRIES_REGIONS, regionsTopNode);

			var aggregatesTopNode = new Element(COUNTRIES_AGGREGATES, COUNTRIES_AGGREGATES);
			dimension.Add(COUNTRIES_AGGREGATES, aggregatesTopNode);

			ReportReadingInput(true);

			//DbDataReader reader = new OleDbCommand("SELECT * FROM [" + COUNTRIES_SHEET_NAME + "]", InputFileConnection).ExecuteReader();

			var reader = _inputFileDataSet.Tables[COUNTRIES_SHEET_NAME].CreateDataReader();

			while (reader.Read()) {
				string id = GetString(reader, COUNTRIES_COLUMN_Id);
				string parent = GetString(reader, COUNTRIES_COLUMN_Parent);
				string caption = GetString(reader, COUNTRIES_COLUMN_Caption);
				string currencyUnit = GetString(reader, COUNTRIES_COLUMN_CurrencyUnit);
				string ISO_3166_numeric_code = GetString(reader, COUNTRIES_COLUMN_ISO_3166_numeric_code);
				string ISO_3166_alpha_2_code = GetString(reader, COUNTRIES_COLUMN_ISO_3166_alpha_2_code);
				string WB_2_code = GetString(reader, COUNTRIES_COLUMN_WB_2_code);
				string ISO_3166_alpha_3_code = GetString(reader, COUNTRIES_COLUMN_ISO_3166_alpha_3_code);
				string WB_3_code = GetString(reader, COUNTRIES_COLUMN_WB_3_code);

				Element node;
				IDictionary<string, Element> parentNode;
				if (parent == null) {
					parentNode = aggregatesTopNode;
				} else {
					parentNode = regionsTopNode;

					if (parentNode.ContainsKey(parent)) {
						node = parentNode[parent];
					} else {
						node = new Element(parent, parent);
						parentNode.Add(parent, node);
					}

					parentNode = node;
				}

				if (id == null) {
					continue;
				}

				node = new Element(id, caption);

				if (currencyUnit != null) {
					node.SetAttributeValue(COUNTRIES_COLUMN_CurrencyUnit, currencyUnit);
				}

				if (ISO_3166_numeric_code != null) {
					node.SetAttributeValue(COUNTRIES_COLUMN_ISO_3166_numeric_code, ISO_3166_numeric_code);
				}

				if (ISO_3166_alpha_2_code != null) {
					node.SetAttributeValue(COUNTRIES_COLUMN_ISO_3166_alpha_2_code, ISO_3166_alpha_2_code);
				}

				if (WB_2_code != null) {
					node.SetAttributeValue(COUNTRIES_COLUMN_WB_2_code, WB_2_code);
				}

				if (ISO_3166_alpha_3_code != null) {
					node.SetAttributeValue(COUNTRIES_COLUMN_ISO_3166_alpha_3_code, ISO_3166_alpha_3_code);
				}

				if (WB_3_code != null) {
					node.SetAttributeValue(COUNTRIES_COLUMN_WB_3_code, WB_3_code);
				}

				parentNode.Add(id, node);
			}

			ReportReadingInput(false);
			ReportStatus("Done reading countries.");

			return dimension;
		}

		private Dimension LoadIndicators() {
			ReportStatus("Reading indicators...");

			Dimension dimension = new Dimension(INDICATORS);

			var allIndicators = new Element(INDICATORS_ALL_INDICATORS, INDICATORS_ALL_INDICATORS);
			dimension.Add(INDICATORS_ALL_INDICATORS, allIndicators);

			ReportReadingInput(true);

			//DbDataReader reader = new OleDbCommand("SELECT * FROM [" + INDICATORS_SHEET_NAME + "]", InputFileConnection).ExecuteReader();

			var reader = _inputFileDataSet.Tables[INDICATORS_SHEET_NAME].CreateDataReader();

			while (reader.Read()) {
				string id = GetString(reader, INDICATORS_COLUMN_Id);
				string caption = GetString(reader, INDICATORS_COLUMN_Caption);
				string topic = GetString(reader, INDICATORS_COLUMN_Topic).TrimEnd(new char[] { ':', ' ' });
				string definition = GetString(reader, INDICATORS_COLUMN_Definition);
				if (definition.Length >= 254) {
					definition = definition.Remove(253);
				}
				string source = GetString(reader, INDICATORS_COLUMN_Source);
				if (source.Length >= 254) {
					source = source.Remove(253);
				}
				string periodicity = GetString(reader, INDICATORS_COLUMN_Periodicity);
				string aggregationMethod = GetString(reader, INDICATORS_COLUMN_AggregationMethod);

				Element node;
				IDictionary<string, Element> parentNode = allIndicators;
				if (topic.Length > 0) {
					string[] topicPathArray = topic.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (string item in topicPathArray) {
						string topicName = item.Trim();
						if (parentNode.ContainsKey(topicName)) {
							node = parentNode[topicName];
						} else {
							node = new Element(topicName, topicName);
							parentNode.Add(topicName, node);
						}
						parentNode = node;
					}
				}

				if (id == null) {
					continue;
				}

				node = new Element(id, caption);

				if (definition != null) {
					node.SetAttributeValue(INDICATORS_COLUMN_Definition, definition);
				}

				if (source != null) {
					node.SetAttributeValue(INDICATORS_COLUMN_Source, source);
				}

				if (periodicity != null) {
					node.SetAttributeValue(INDICATORS_COLUMN_Periodicity, periodicity);
				}

				if (aggregationMethod != null) {
					node.SetAttributeValue(INDICATORS_COLUMN_AggregationMethod, aggregationMethod);
				}

				parentNode.Add(id, node);
			}

			ReportReadingInput(false);
			ReportStatus("Done reading indicators.");

			return dimension;
		}

		private Dimension LoadYears() {
			ReportStatus("Reading years...");

			Dimension dimension = new Dimension(YEARS);

			var allYears = new Element("0", YEARS_ALL_YEARS);
			dimension.Add("0", allYears);

			ReportReadingInput(true);

			//DbDataReader reader = new OleDbCommand("SELECT * FROM [" + FACTS_SHEET_NAME + "]", InputFileConnection).ExecuteReader();

			var reader = _inputFileDataSet.Tables[FACTS_SHEET_NAME].CreateDataReader();

			Element node;
			IDictionary<string, Element> parentNode = allYears;
			for (int i = 0; i < reader.FieldCount; i++) {
				int yearOut;
				if (int.TryParse(reader.GetName(i), out yearOut)) {
					var year = yearOut.ToString();
					node = new Element(year, year);

					parentNode.Add(year, node);
				}
			}

			ReportReadingInput(false);
			ReportStatus("Done reading years.");

			return dimension;
		}

		private void TransferFacts(Cube cube, SqlConnection sqlConnection) {
			TransferFacts(cube, sqlConnection, null);
		}

		public void TransferFacts(string outputFileName) {
			using (var outputFile = new StreamWriter(outputFileName)) {
				TransferFacts(outputFile);
			}
		}

		private void TransferFacts(StreamWriter outputFile) {
			TransferFacts(null, null, outputFile);
		}

		private void TransferFacts(Cube cube, SqlConnection sqlConnection, StreamWriter outputFile) {
			ReportStatus("Transfering facts...");

			SqlBulkCopy sqlBulkCopy = null;
			SqlTransaction sqlTransaction = null;

			if (sqlConnection != null) {
				sqlTransaction = sqlConnection.BeginTransaction();

				SqlCommand sqlCommand = sqlConnection.CreateCommand();
				sqlCommand.Transaction = sqlTransaction;
				sqlCommand.CommandTimeout = 300;

				sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction);
				sqlBulkCopy.DestinationTableName = FACTS;
				sqlBulkCopy.BulkCopyTimeout = 300;
			}

			ReportReadingInput(true);

			if (CancellationPending) {
				ReportOperationCanceled();
				return;
			}

			ReportStatus("Loading " + FACTS_SHEET_NAME + " worksheet...");

			var workSheet = _excelWorkbook.Worksheets[FACTS_SHEET_NAME];

			ReportStatus("Done loading \"" + FACTS_SHEET_NAME + "\" worksheet.");
			
			var workSheetDimension = workSheet.Dimension;
			var totalLines = workSheetDimension.End.Row;

			//var table = _inputFileDataSet.Tables[FACTS_SHEET_NAME];
			//var totalLines = table.Rows.Count;
			//var reader = table.CreateDataReader();


			//OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM [" + FACTS_SHEET_NAME + "]", InputFileConnection);
			//int totalLines = (int)command.ExecuteScalar();

			ReportProgress(0, totalLines);

			//command = new OleDbCommand("SELECT * FROM [" + FACTS_SHEET_NAME + "]", InputFileConnection);
			//DbDataReader reader = command.ExecuteReader();

			int countryColumnIndex = 0;
			int indicatorColumnIndex = 0;
			var yearsColumnIndex = new Dictionary<int, string>();
			for (int columnIndex = workSheetDimension.Start.Column; columnIndex <= workSheetDimension.End.Column; columnIndex++) {
				int year;
				if (int.TryParse(workSheet.Cells[workSheetDimension.Start.Row, columnIndex].Text, out year)) {
					yearsColumnIndex[columnIndex] = year.ToString();
				} else if (workSheet.Cells[workSheetDimension.Start.Row, columnIndex].Text == FACTS_COLUMN_CountryCode) {
					countryColumnIndex = columnIndex;
				} else if (workSheet.Cells[workSheetDimension.Start.Row, columnIndex].Text == FACTS_COLUMN_IndicatorCode) {
					indicatorColumnIndex = columnIndex;
				}
			}

			//var years = new Dictionary<int, string>();
			//for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++) {
			//	int year;
			//	if (int.TryParse(reader.GetName(columnIndex), out year)) {
			//		years.Add(columnIndex, year.ToString());
			//	}
			//}

			List<Fact> facts = null;

			ReportStatus("Reading data...");

			string country;
			string indicator;
			int yearColumnIndex;
			string yearColumnName;
			object value;

			for (int rowIndex = workSheetDimension.Start.Row + 1; rowIndex <= workSheetDimension.End.Row; rowIndex++) {
				//for (int lineCounter = 0; reader.Read(); lineCounter++) {
				if (CancellationPending) {
					ReportOperationCanceled();
					return;
				}

				country = (string)workSheet.Cells[rowIndex, countryColumnIndex].Value;
				indicator = (string)workSheet.Cells[rowIndex, indicatorColumnIndex].Value;

				if (facts == null) {
					facts = new List<Fact>();
				}

				foreach (var yearPair in yearsColumnIndex) {
					yearColumnIndex = yearPair.Key;
					yearColumnName = yearPair.Value;

					value = workSheet.Cells[rowIndex, yearColumnIndex].Value;

					if (value == null) {
						continue;
					}

					//if (reader.IsDBNull(columnIndex)) {
					//	continue;
					//}

					//try {
					//	value = reader.GetDouble(columnIndex);
					//} catch {
					//	LogMessage("Warning: GetDouble() throw exception in " + columnIndex + ". column (" + year + ") on line " + lineCounter + 2 + ". [" + reader[columnIndex].ToString() + "]");
					//	try {
					//		double.TryParse(reader.GetValue(columnIndex).ToString(), out value);
					//	} catch {
					//		LogMessage("Warning: Couldn't recognize value in " + columnIndex + ". column (" + year + ") on line " + lineCounter + 2 + ". [" + reader[columnIndex].ToString() + "] as double value");
					//		continue;
					//	}
					//}

					Fact fact = new Fact {
						CubeId = cube.Id,
						ElementId1 = country,
						ElementId2 = indicator,
						ElementId3 = yearColumnName,
						Value = (double) value
					};
					facts.Add(fact);
				}

				ReportProgress(rowIndex + 1, totalLines);

				//Write Data to Destination
				if ((rowIndex + 1) % 50000 == 0) {
					WriteToDestination(facts, sqlBulkCopy, outputFile);
					facts = null;
					GC.Collect();
					ReportStatus("Reading data...");
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
		}

		private void ExecuteSqlCommand(SqlConnection sqlConnection, string commandText, int timeout = 300) {
			var sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = commandText;
			sqlCommand.CommandTimeout = timeout;
			sqlCommand.ExecuteNonQuery();
		}


		//private string BuildEntityConnectionString(string serverName, string databaseName, string userId, string password) {
		//	EntityConnectionStringBuilder entityConnStringBuilder = new EntityConnectionStringBuilder();

		//	entityConnStringBuilder.Provider = "System.Data.SqlClient";
		//	entityConnStringBuilder.Metadata = @"res://*/"; //@"res://*/WDI.csdl|res://*/WDI.ssdl|res://*/WDI.msl";
		//	entityConnStringBuilder.ProviderConnectionString = BuildSqlConnectionString(serverName, databaseName, userId, password);

		//	return entityConnStringBuilder.ToString();
		//}

		//private string BuildSqlConnectionString(string serverName, string databaseName, string userId, string password) {
		//	SqlConnectionStringBuilder sqlConnStringBuilder = new SqlConnectionStringBuilder();

		//	sqlConnStringBuilder.DataSource = serverName;
		//	sqlConnStringBuilder.InitialCatalog = databaseName;
		//	if (userId == null) {
		//		sqlConnStringBuilder.IntegratedSecurity = true;
		//	} else {
		//		sqlConnStringBuilder.IntegratedSecurity = false;
		//		sqlConnStringBuilder.UserID = userId;
		//		sqlConnStringBuilder.Password = password;
		//	}

		//	return sqlConnStringBuilder.ToString();
		//}

		private string GetString(IDataRecord record, string column) {
			var value = record[column];

			if (value == DBNull.Value) {
				return null;
			}

			return value.ToString().Trim();
		}

		//private string GenId(string id) {
		//	if (id == null) {
		//		id = System.Guid.NewGuid().ToString();
		//	} else {
		//		id = id.Trim();
		//		int nextInc = 1;
		//		id = Regex.Replace(id, "&", "and", RegexOptions.Compiled);
		//		id = Regex.Replace(id, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
		//		while (_uniqueIds.Contains(id)) {
		//			Match match = Regex.Match(id, @"(.*\w+)\s*\((\d+)\)");
		//			if (match.Success) {
		//				id = match.Groups[1].Value;
		//				nextInc = int.Parse(match.Groups[2].Value) + 1;
		//			}
		//			id = String.Format("{0} ({1})", id, nextInc);
		//		}
		//	}

		//	_uniqueIds.Add(id);

		//	return id;
		//}

		private void ReportOperationCanceled() {
			ReportStatus("Operation canceled.");
			ReportProgress(-1);
			ReportReadingInput(false);
		}

		private void ReportStatus(string status) {
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
