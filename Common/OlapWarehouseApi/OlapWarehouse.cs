using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Common;
using System.Text;
//using System.Threading.Tasks;

namespace OlapWarehouseApi {
	public partial class OlapWarehouse {

		public static string COLUMN_NAME = "Name";
		public static string COLUMN_CAPTION = "Caption";

		public static string COLUMN_WEIGHT = "Weight";
		public static string COLUMN_ORDER = "Order";

		private static OlapWarehouse _instance;
		public static OlapWarehouse Instance {
			get {
				return _instance;
			}
		}

		public OlapWarehouse(string serverName, string databaseName, string userId, string password)
			: base(BuildEntityConnectionString(serverName, databaseName, userId, password)) {

			ObjectContext.CommandTimeout = 60 * 5;

			_instance = this;
		}

		public static string GetExceptionMessage(Exception e, string prefix = null) {
			prefix = (prefix == null ? e.Message : "\n" + prefix + ": " + e.Message);
			if (e.InnerException != null) {
				prefix = GetExceptionMessage(e.InnerException, prefix);
			}

			return prefix;
		}

		public ObjectContext ObjectContext {
			get {
				return (this as IObjectContextAdapter).ObjectContext;
			}
		}

		private SqlConnection _sqlConnection;
		public SqlConnection SqlConnection {
			get {
				if (_sqlConnection == null) {
					var entityConnection = ObjectContext.Connection as EntityConnection;

					if (entityConnection != null) {
						_sqlConnection = entityConnection.StoreConnection as SqlConnection;
					}
				}

				return _sqlConnection;
			}
		}

		public override int SaveChanges() {
			int ret = 0;
			try {
				ret = base.SaveChanges();
			} catch (DbEntityValidationException e) {
				var message = new StringBuilder();
				foreach (var validationResult in e.EntityValidationErrors) {
					foreach (var validationError in validationResult.ValidationErrors) {
						message.Append(string.Format("Validation failed for property {0} of entity {1}. Error: {2}\n", validationError.PropertyName, validationResult.Entry.Entity, validationError.ErrorMessage));
					}
				}
				throw new InvalidOperationException(message.ToString(), e);
			} catch (DbUpdateException e) {
				throw new InvalidOperationException(GetExceptionMessage(e), e);
			}

			return ret;
		}

		private static string BuildEntityConnectionString(string serverName, string databaseName, string userId, string password) {
			EntityConnectionStringBuilder entityConnStringBuilder = new EntityConnectionStringBuilder();

			entityConnStringBuilder.Provider = "System.Data.SqlClient";
			entityConnStringBuilder.Metadata = @"res://*/"; //@"res://*/WDI.csdl|res://*/WDI.ssdl|res://*/WDI.msl";
			entityConnStringBuilder.ProviderConnectionString = BuildSqlConnectionString(serverName, databaseName, userId, password);

			return entityConnStringBuilder.ToString();
		}

		private static string BuildSqlConnectionString(string serverName, string databaseName, string userId, string password) {
			SqlConnectionStringBuilder sqlConnStringBuilder = new SqlConnectionStringBuilder();

			sqlConnStringBuilder.DataSource = serverName;
			sqlConnStringBuilder.InitialCatalog = databaseName;
			if (userId == null) {
				sqlConnStringBuilder.IntegratedSecurity = true;
			} else {
				sqlConnStringBuilder.IntegratedSecurity = false;
				sqlConnStringBuilder.UserID = userId;
				sqlConnStringBuilder.Password = password;
			}

			return sqlConnStringBuilder.ToString();
		}
	}

	public partial class Server {

		public Server(string name)
			: this() {
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Name = name;
		}

		public void Clear() {
			Cubes.Clear();
			Dimensions.Clear();
		}

		public void AddDimension(Dimension dimension) {
			dimension.Server = this;
			Dimensions.Add(dimension);
		}

		public void AddCube(Cube cube) {
			cube.Server = this;
			Cubes.Add(cube);
		}

		public override string ToString() {
			return MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Name;
		}
	}

	public partial class Cube {

		public Cube(string name)
			: this() {
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			Name = name;
		}

		public int Count { get { return Dimensions.Count; } }

		public void Clear() {
			Dimensions.Clear();
		}

		public void AddDimension(Dimension dimension) {
			dimension.Cube = this;
			dimension.Server = Server;
			Dimensions.Add(dimension);
		}

		public override string ToString() {
			return MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Name;
		}
	}

	public partial class Dimension : IDictionary<string, Element> {

		public static event StatusChangedEventHandler StatusChanged;
		public static bool CancellationPending { get; set; }

		public Dimension(string name)
			: this() {
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Name = name;
		}

		public bool IsTemplate {
			get {
				return false;
			}
		}

		public IEnumerable<string> Columns {
			get {
				var columns = new List<string>();

				columns.AddRange(BasicColumns);
				columns.AddRange(ExtendedColumns);

				return columns;
			}
		}

		public IEnumerable<string> BasicColumns {
			get {
				var columns = new List<string>();

				columns.Add("Name");
				columns.Add("Parent");
				columns.Add("Caption");

				return columns;
			}
		}

		public IEnumerable<string> ExtendedColumns {
			get {
				var columns = new List<string>();

				var elements = (from element in Elements where element.Attributes != null select element).Flatten(x => x.Children).ToList();

				var attributes = new HashSet<string>();
				foreach (var element in elements) {
					foreach (var attribute in element.Attributes) {
						attributes.Add(attribute.Name);
					}
				}

				foreach (var attribute in attributes) {
					columns.Add(attribute);
				}

				return columns;
			}
		}

		public ICollection<string> Keys {
			get {
				ICollection<string> ret = new List<string>();
				foreach (var element in Elements) {
					ret.Add(element.Name);
				}

				return ret;
			}
		}

		public ICollection<Element> Values {
			get {
				return Elements;
			}
		}

		public Element this[string key] {
			get {
				foreach (var element in Elements) {
					if (element.Name == key) {
						return element;
					}
				}

				return null;
			}
			set {
				this[key] = value;
			}
		}

		public bool Contains(KeyValuePair<string, Element> pair) {
			return Elements.Contains(pair.Value);
		}

		public bool ContainsKey(string key) {
			foreach (var element in Elements) {
				if (element.Name == key) {
					return true;
				}
			}
			return false;
		}

		public int Count { get { return Elements.Count; } }

		public void Clear() {
			Elements.Clear();
		}

		public void Add(string key, Element element) {
			element.Dimension = this;
			Elements.Add(element);
		}

		public bool Remove(string key) { return Elements.Remove(this[key]); }

		public void Add(KeyValuePair<string, Element> pair) {
			pair.Value.Dimension = this;
			Elements.Add(pair.Value);
		}

		public bool Remove(KeyValuePair<string, Element> pair) { return Elements.Remove(pair.Value); }

		public void CopyTo(KeyValuePair<string, Element>[] array, int arrayIndex) {
			foreach (var element in Elements) {
				array[arrayIndex] = new KeyValuePair<string, Element>(element.Name, element);
			}
		}

		public bool TryGetValue(string key, out Element value) { value = null; return true; }
		public bool IsReadOnly { get { return Elements.IsReadOnly; } }

		//public IEnumerator<Element> System.Collections.Generic.IEnumerable.GetEnumerator() {
		//	foreach (Element element in Children) {
		//		yield return element;
		//	}
		//}

		public IEnumerator<KeyValuePair<string, Element>> GetEnumerator() {
			foreach (Element element in Elements) {
				yield return new KeyValuePair<string, Element>(element.Name, element);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return Elements.GetEnumerator();
		}

		public void ExportDimension(FileInfo file, char parentSeparator = '/', bool exportParents = false) {
			ReportStatus("Writing dimension...");

			ExcelPackage excelPackage = new ExcelPackage(file);
			var workSheet = excelPackage.Workbook.Worksheets.Add(Name);

			var elements = from element in Elements.Flatten(x => x.Children) where element.Children.Count == 0 select element;

			ReportProgress(0, elements.Count());

			int row = 1;
			int column = 1;

			foreach (var columnName in Columns) {
				workSheet.Cells[row, column++].Value = columnName;
			}

			row++;

			ExportDimension(Elements, workSheet, ref row, parentSeparator, exportParents);

			excelPackage.Save();

			ReportStatus("Dimension written.");
		}

		private void ExportDimension(ICollection<Element> elements, ExcelWorksheet workSheet, ref int row, char parentSeparator = '/', bool exportParents = false, string parentName = "") {
			foreach (var element in elements) {
				ReportProgress(row - 1);
				int column = workSheet.Dimension.Start.Column;

				if (element.Children.Count == 0 || exportParents) {
					workSheet.Cells[row, column++].Value = parentName;
					workSheet.Cells[row, column++].Value = element.Name;
					workSheet.Cells[row, column++].Value = element.Caption;

					foreach (var columnName in ExtendedColumns) {
						workSheet.Cells[row, column++].Value = element.GetAttributeValue(columnName);
					}

					row++;
				}

				if (element.Children.Count > 0) {
					ExportDimension(element.Children, workSheet, ref row, parentSeparator, exportParents, parentName + parentSeparator + element.Name);
				}
			}
		}

		public void ImportDimension(FileInfo file, string sheet, string idColumn, string parentColumn, char parentSeparator, string captionColumn, ICollection<string> columns) {
			ReportStatus("Reading dimension...");

			ExcelPackage excelPackage = new ExcelPackage(file);

			var workSheet = excelPackage.Workbook.Worksheets[sheet];
			var workSheetDimension = workSheet.Dimension;

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

				for (int columnIndex = workSheetDimension.Start.Column; columnIndex <= workSheetDimension.End.Column; columnIndex++) {

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
					} else if (columnName == OlapWarehouse.COLUMN_WEIGHT) {
						weight = cellValue;
					} else if (columnName == OlapWarehouse.COLUMN_ORDER) {
						order = cellValue;
					} else if (columns.Contains(columnName)) {
						attributes.Add(columnName, cellValue.ToString());
					}
				}

				Element node;
				IDictionary<string, Element> parentNode = this;
				if (parent != null) {
					string[] parentPathArray = parent.Split(new char[] { parentSeparator }, StringSplitOptions.RemoveEmptyEntries);

					foreach (string parentPathItem in parentPathArray) {
						string parentName = ProcessId(parentPathItem.Trim());

						if (!parentNode.ContainsKey(parentName)) {
							node = new Element(parentName, parentName);
							parentNode.Add(node.Name, node);
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
					return;
				}
			}

			ReportStatus("Dimension read.");
		}


		private static string ProcessId(string id) {
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

		private static void ReportOperationCanceled() {
			ReportStatus("Operation canceled.");
			ReportProgress(-1);
			CancellationPending = false;
		}

		private static void ReportStatus(string status) {
			//Program.LogMessage(status);
			StatusChanged(new StatusChangedEventArgs(StatusType.Message, status));
		}

		private static void ReportProgress(int? progress, int? total = null) {
			StatusChanged(new StatusChangedEventArgs(StatusType.Progress, new int?[] { progress, total }));
		}

		public string ShowSQL() {
			return @"
DECLARE @columns NVARCHAR(2000)

SELECT @columns = Coalesce(@columns + ', [', '[') + Name + ']'
FROM (
	SELECT DISTINCT a.Name
	FROM [Attributes] a
	LEFT JOIN [Elements] e		ON a.[ElementId]	= e.[Id]
	LEFT JOIN [Dimensions] d	ON e.[DimensionId]	= d.[Id]
	LEFT JOIN [Cubes] c			ON d.[CubeId]		= c.[Id]
	LEFT JOIN [Servers] db		ON d.[ServerId]	= db.[Id]
	WHERE db.[Name] = '" + Server.Name + "' AND c.[Name] = '" + Cube.Name + "' AND d.[Name] = '" + Name + @"'
) data

DECLARE @select NVARCHAR(2000)
SET @select = '
FROM (
	SELECT e.Name AS Element, e2.Name AS Parent, e.Caption, a.Name as AttributeName, a.Value as AttributeValue
	FROM [Elements] e
	LEFT JOIN [Elements] e2		ON e.[ElementId]	= e2.[Id]
	LEFT JOIN [Attributes] a	ON a.[ElementId]	= e.[Id]
	LEFT JOIN [Dimensions] d	ON e.[DimensionId]	= d.[Id]
	LEFT JOIN [Cubes] c			ON d.[CubeId]		= c.[Id]
	LEFT JOIN [Servers] db		ON d.[ServerId]		= db.[Id]
	WHERE db.[Name] = ''" + Server.Name + "'' AND c.[Name] = ''" + Cube.Name + "'' AND d.[Name] = ''" + Name + @"''
	) data'

DECLARE @query NVARCHAR(4000)

IF LEN(@columns) > 0
	SET @query = '
	SELECT
		[Element]
		,[Parent]
		,[Caption]
		,' + @columns + '
	' + @select + '
	PIVOT (MAX(AttributeValue) FOR AttributeName IN (' + @columns + ')) as pvt'
ELSE
	SET @query = '
	SELECT
		[Element]
		,[Parent]
		,[Caption]
	' + @select

EXECUTE(@query)
";
		}

		public override string ToString() {
			return MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Name;
		}
	}

	public partial class Element : IDictionary<string, Element> {

		public Element(string name)
			: this() {
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Name = name;
		}

		public Element(string name, string caption)
			: this(name) {
			if (caption != null) {
				Caption = caption;
			}
		}

		//public IDimension Dimension {
		//	get {
		//		if (DimensionActual != null) {
		//			return DimensionActual;
		//		} else {
		//			return DimensionTemplate;
		//		}
		//	}
		//}

		//public override bool TryGetMember(GetMemberBinder binder, out object result) {
		//	foreach (var attribute in Attributes) {
		//		if (attribute.Name == binder.Name) {
		//			result = attribute.Value;
		//			return true;
		//		}
		//	}
		//	result = "Invalid Property!";
		//	return false;
		//}

		//public override bool TrySetMember(SetMemberBinder binder, object value) {
		//	var attribute = new Attribute(binder.Name, value.ToString());
		//	if (!Attributes.Contains(attribute)) {
		//		Attributes.Add(attribute);
		//	}
		//	return true;
		//}

		public ICollection<string> Keys {
			get {
				ICollection<string> ret = new List<string>();
				foreach (var element in Children) {
					ret.Add(element.Name);
				}

				return ret;
			}
		}

		public ICollection<Element> Values {
			get {
				return Children;
			}
		}


		public Element this[string key] {
			get {
				foreach (var element in Children) {
					if (element.Name == key) {
						return element;
					}
				}

				return null;
			}
			set {
				this[key] = value;
			}
		}

		public bool Contains(KeyValuePair<string, Element> pair) {
			return Children.Contains(pair.Value);
		}

		public bool ContainsKey(string key) {
			foreach (var element in Children) {
				if (element.Name == key) {
					return true;
				}
			}
			return false;
		}

		//public IEnumerator<Element> System.Collections.Generic.IEnumerable.GetEnumerator() {
		//	foreach (Element element in Children) {
		//		yield return element;
		//	}
		//}

		public IEnumerator<KeyValuePair<string, Element>> GetEnumerator() {
			foreach (Element element in Children) {
				yield return new KeyValuePair<string, Element>(element.Name, element);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return Children.GetEnumerator();
		}

		public void Clear() {
			Children.Clear();
		}

		public void Add(KeyValuePair<string, Element> pair) {
			pair.Value.Dimension = Dimension;
			Children.Add(pair.Value);
		}

		public void Add(string key, Element element) {
			element.Dimension = Dimension;
			Children.Add(element);
		}

		public bool Remove(string key) { return Children.Remove(this[key]); }
		public bool Remove(KeyValuePair<string, Element> pair) { return Children.Remove(pair.Value); }
		public bool TryGetValue(string key, out Element value) { value = null; return true; }
		public bool IsReadOnly { get { return Children.IsReadOnly; } }
		public int Count { get { return Children.Count; } }

		public void CopyTo(KeyValuePair<string, Element>[] array, int arrayIndex) {
			foreach (var element in Children) {
				array[arrayIndex] = new KeyValuePair<string, Element>(element.Name, element);
			}
		}

		public Attribute GetAttribute(string name) {
			foreach (var attr in Attributes) {
				if (attr.Name == name) {
					return attr;
				}
			}
			return null;
		}

		public string GetAttributeValue(string name) {
			string ret = null;
			var attribute = GetAttribute(name);
			if (attribute != null) {
				ret = attribute.Value;
			}

			return ret;
		}

		public void SetAttributeValue(string name, string value) {
			var attribute = GetAttribute(name);
			if (attribute != null) {
				attribute.Value = value;
			} else {
				Attributes.Add(new Attribute(name, value));
			}
		}

		//public IDictionary<string, string> AttributesDict {
		//	get {
		//		var attrsDict = new Dictionary<string, string>();
		//		foreach (var attr in Attributes) {
		//			attrsDict[attr.Name] = attr.Value;
		//		}

		//		return attrsDict;
		//	}
		//}

		public override string ToString() {
			var ret = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Name;
			if (Caption != null) {
				ret += " (" + Caption + ")";
			}

			return ret;
		}
	}

	public partial class Attribute {

		public Attribute() { }

		public Attribute(string name, string value)
			: this() {
			if (name == null) {
				throw new ArgumentNullException("name");
			} else if (value == null) {
				throw new ArgumentNullException("value");

			}
			Name = name;
			Value = value;
		}

		public Element GetElement() {
			return Element;
		}

		public override string ToString() {
			return MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Name + ": " + Value;
		}
	}

	//public static string ToTraceString<T>(this IQueryable<T> t) {
	//	string sql = "";
	//	ObjectQuery<T> oqt = t as ObjectQuery<T>;
	//	if (oqt != null)
	//		sql = oqt.ToTraceString();
	//	return sql;
	//}


	public delegate void StatusChangedEventHandler(StatusChangedEventArgs e);

	public enum Result {
		Success, Failure, Canceled
	};

	public enum StatusType {
		Message, Progress
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
