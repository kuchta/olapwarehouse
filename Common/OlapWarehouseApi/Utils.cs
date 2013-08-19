using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlapWarehouseApi {
	public static class Utils {
		public static IEnumerable<string> GetSheets(FileInfo file) {
			//ReportStatus("Reading sheets...");

			//DataTable dt = InputFileConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			//if (dt == null || dt.Rows.Count < 1) {
			//	ReportStatus("Couldn't read file");
			//	return null;
			//}

			//var sheets = new List<string>();
			//foreach (DataRow row in dt.Rows) {
			//	sheets.Add(row["TABLE_NAME"].ToString().TrimEnd(new char[] { '$' }));
			//}

			ExcelPackage excelPackage = new ExcelPackage(file);

			var sheets = new List<string>();
			foreach (var sheet in excelPackage.Workbook.Worksheets) {
				sheets.Add(sheet.Name);
			}

			//ReportStatus("Sheets read.");

			return sheets;
		}

		public static List<string> GetColumns(FileInfo file, string sheet) {
			//ReportStatus("Reading sheets...");

			ExcelPackage excelPackage = new ExcelPackage(file);

			var workSheet = excelPackage.Workbook.Worksheets[sheet];
			var workSheetDimension = workSheet.Dimension;

			var columns = new List<string>();
			for (int columnIndex = workSheetDimension.Start.Column; columnIndex <= workSheetDimension.End.Column; columnIndex++) {
				columns.Add(workSheet.Cells[workSheetDimension.Start.Row, columnIndex].Text);
			}

			//ReportStatus("Sheets read.");

			return columns;
		}
	}
}
