using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Infor.BI.Applications.OlapApi.Native
{
    internal static class OlapNativeImports
    {
        public static void CallBegin(int cursor, UniFunction function)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.CallBegin(cursor, function);
            }
            else
            {
                OlapNativeImports32.CallBegin(cursor, function);
            }
        }

        public static int CallEnd(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.CallEnd(cursor);
            }
            else
            {
                return OlapNativeImports32.CallEnd(cursor);
            }
        }

        public static int GetCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetCount(cursor);
            }
        }

        public static int GetDimensionCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetDimensionCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetDimensionCount(cursor);
            }
        }

        public static int GetCubeCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetCubeCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetCubeCount(cursor);
            }
        }

        public static double GetNValue(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetNValue(cursor);
            }
            else
            {
                return OlapNativeImports32.GetNValue(cursor);
            }
        }

        public static int GetLevel(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetLevel(cursor);
            }
            else
            {
                return OlapNativeImports32.GetLevel(cursor);
            }
        }

        public static int GetId(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetId(cursor);
            }
            else
            {
                return OlapNativeImports32.GetId(cursor);
            }
        }

        public static char GetCType(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetCType(cursor);
            }
            else
            {
                return OlapNativeImports32.GetCType(cursor);
            }
        }

        public static int GetRulesCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetRulesCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetRulesCount(cursor);
            }
        }

        public static int GetElementsCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetElementsCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetElementsCount(cursor);
            }
        }

        public static int GetUserData(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetUserData(cursor);
            }
            else
            {
                return OlapNativeImports32.GetUserData(cursor);
            }
        }

        public static int GetParentsCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetParentsCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetParentsCount(cursor);
            }
        }

        public static int GetValuesCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetValuesCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetValuesCount(cursor);
            }
        }

        public static int GetSize(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetSize(cursor);
            }
            else
            {
                return OlapNativeImports32.GetSize(cursor);
            }
        }

        public static int GetDecimalPlaces(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetDecimalPlaces(cursor);
            }
            else
            {
                return OlapNativeImports32.GetDecimalPlaces(cursor);
            }
        }

        public static int GetLeftOperator(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetLeftOperator(cursor);
            }
            else
            {
                return OlapNativeImports32.GetLeftOperator(cursor);
            }
        }

        public static double GetLeftValue(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetLeftValue(cursor);
            }
            else
            {
                return OlapNativeImports32.GetLeftValue(cursor);
            }
        }

        public static int GetRightOperator(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetRightOperator(cursor);
            }
            else
            {
                return OlapNativeImports32.GetRightOperator(cursor);
            }
        }

        public static double GetRightValue(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetRightValue(cursor);
            }
            else
            {
                return OlapNativeImports32.GetRightValue(cursor);
            }
        }

        public static int GetStamp(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetStamp(cursor);
            }
            else
            {
                return OlapNativeImports32.GetStamp(cursor);
            }
        }

        public static int GetCachedValuesCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetCachedValuesCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetCachedValuesCount(cursor);
            }
        }

        public static int GetErrorCode(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetErrorCode(cursor);
            }
            else
            {
                return OlapNativeImports32.GetErrorCode(cursor);
            }
        }

        public static int GetErrorPosition(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetErrorPosition(cursor);
            }
            else
            {
                return OlapNativeImports32.GetErrorPosition(cursor);
            }
        }

        public static int GetFieldsCount(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetFieldsCount(cursor);
            }
            else
            {
                return OlapNativeImports32.GetFieldsCount(cursor);
            }
        }

        public static int GetFlags(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetFlags(cursor);
            }
            else
            {
                return OlapNativeImports32.GetFlags(cursor);
            }
        }

        private static IntPtr GetNameW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetNameW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetNameW(cursor);
            }
        }

        private static IntPtr GetLongNameW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetLongNameW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetLongNameW(cursor);
            }
        }

        private static IntPtr GetUserW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetUserW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetUserW(cursor);
            }
        }

        private static IntPtr GetVersionW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetVersionW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetVersionW(cursor);
            }
        }

        private static IntPtr GetSValueW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetSValueW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetSValueW(cursor);
            }
        }

        private static IntPtr GetCubeW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetCubeW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetCubeW(cursor);
            }
        }

        private static IntPtr GetRefNameW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetRefNameW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetRefNameW(cursor);
            }
        }

        private static IntPtr GetDARTW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetDARTW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetDARTW(cursor);
            }
        }

        private static IntPtr GetDimensionW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetDimensionW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetDimensionW(cursor);
            }
        }

        private static IntPtr GetSubsetRefNameW(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetSubsetRefNameW(cursor);
            }
            else
            {
                return OlapNativeImports32.GetSubsetRefNameW(cursor);
            }
        }

        public static bool GetNextItem(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetNextItem(cursor);
            }
            else
            {
                return OlapNativeImports32.GetNextItem(cursor);
            }
        }

        public static bool GetNextSubItem(int cursor)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.GetNextSubItem(cursor);
            }
            else
            {
                return OlapNativeImports32.GetNextSubItem(cursor);
            }
        }

        public static void SetId(int cursor, int id)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetId(cursor, id);
            }
            else
            {
                OlapNativeImports32.SetId(cursor, id);
            }
        }

        public static void SetParam(int cursor, uint param)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetParam(cursor, param);
            }
            else
            {
                OlapNativeImports32.SetParam(cursor, param);
            }
        }

        public static void SetNValue(int cursor, double value)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetNValue(cursor, value);
            }
            else
            {
                OlapNativeImports32.SetNValue(cursor, value);
            }
        }

        public static void SetCType(int cursor, char charType)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetCType(cursor, charType);
            }
            else
            {
                OlapNativeImports32.SetCType(cursor, charType);
            }
        }

        public static void SetLeftCondition(int cursor, int leftOperator, double leftValue)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetLeftCondition(cursor, leftOperator, leftValue);
            }
            else
            {
                OlapNativeImports32.SetLeftCondition(cursor, leftOperator, leftValue);
            }
        }

        public static void SetRightCondition(int cursor, int rightOperator, double rightValue)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetRightCondition(cursor, rightOperator, rightValue);
            }
            else
            {
                OlapNativeImports32.SetRightCondition(cursor, rightOperator, rightValue);
            }
        }

        public static void SetSize(int cursor, int size)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetSize(cursor, size);
            }
            else
            {
                OlapNativeImports32.SetSize(cursor, size);
            }
        }

        public static void SetDecimalPlaces(int cursor, int places)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetDecimalPlaces(cursor, places);
            }
            else
            {
                OlapNativeImports32.SetDecimalPlaces(cursor, places);
            }
        }

        public static void SetTableId(int cursor, int tableId)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetTableId(cursor, tableId);
            }
            else
            {
                OlapNativeImports32.SetTableId(cursor, tableId);
            }
        }

        public static void SetFlags(int cursor, int flags)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetFlags(cursor, flags);
            }
            else
            {
                OlapNativeImports32.SetFlags(cursor, flags);
            }
        }

        public static void SetEncoding(int cursor, IpoEncoding encoding)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetEncoding(cursor, encoding);
            }
            else
            {
                OlapNativeImports32.SetEncoding(cursor, encoding);
            }
        }

        public static void SetName(int cursor, string name)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetName(cursor, name);
            }
            else
            {
                OlapNativeImports32.SetName(cursor, name);
            }
        }

        public static void SetLongName(int cursor, string longName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetLongName(cursor, longName);
            }
            else
            {
                OlapNativeImports32.SetLongName(cursor, longName);
            }
        }

        public static void SetDimension(int cursor, string dimName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetDimension(cursor, dimName);
            }
            else
            {
                OlapNativeImports32.SetDimension(cursor, dimName);
            }
        }

        public static void SetRefName(int cursor, string refName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetRefName(cursor, refName);
            }
            else
            {
                OlapNativeImports32.SetRefName(cursor, refName);
            }
        }

        public static void SetCube(int cursor, string cubeName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetCube(cursor, cubeName);
            }
            else
            {
                OlapNativeImports32.SetCube(cursor, cubeName);
            }
        }

        public static void SetElement(int cursor, string elemName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetElement(cursor, elemName);
            }
            else
            {
                OlapNativeImports32.SetElement(cursor, elemName);
            }
        }

        public static void SetSValue(int cursor, string value)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetSValue(cursor, value);
            }
            else
            {
                OlapNativeImports32.SetSValue(cursor, value);
            }
        }

        public static void SetSubsetRefName(int cursor, string subsetRefName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetSubsetRefName(cursor, subsetRefName);
            }
            else
            {
                OlapNativeImports32.SetSubsetRefName(cursor, subsetRefName);
            }
        }

        public static void SetFileName(int cursor, string fileName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetFileName(cursor, fileName);
            }
            else
            {
                OlapNativeImports32.SetFileName(cursor, fileName);
            }
        }

        public static void SetErrorLog(int cursor, string fileName)
        {
            if (Environment.Is64BitProcess)
            {
                OlapNativeImports64.SetErrorLog(cursor, fileName);
            }
            else
            {
                OlapNativeImports32.SetErrorLog(cursor, fileName);
            }
        }

        public static int MdsConnect(string userName, int userType, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsConnect(userName, userType, error);
            }
            else
            {
                return OlapNativeImports32.MdsConnect(userName, userType, error);
            }
        }

        public static int MdsConnectServerExtW(int clientHandle, string servername, string username, string password, int timeOut, int terminalID, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsConnectServerExtW(clientHandle, servername, username, password, timeOut, terminalID, error);
            }
            else
            {
                return OlapNativeImports32.MdsConnectServerExtW(clientHandle, servername, username, password, timeOut, terminalID, error);
            }
        }

        public static int MdsConnectServerTicketW(int clientHandle, string servername, string ticket, int terminalID, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsConnectServerTicketW(clientHandle, servername, ticket, terminalID, error);
            }
            else
            {
                return OlapNativeImports32.MdsConnectServerTicketW(clientHandle, servername, ticket, terminalID, error);
            }
        }

        public static int MdsConnectServerWinW(int clientHandle, string servername, int terminalID, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsConnectServerWinW(clientHandle, servername, terminalID, error);
            }
            else
            {
                return OlapNativeImports32.MdsConnectServerWinW(clientHandle, servername, terminalID, error);
            }
        }

        public static int MdsDisconnect(int clientHandle, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsDisconnect(clientHandle, error);
            }
            else
            {
                return OlapNativeImports32.MdsDisconnect(clientHandle, error);
            }
        }

        public static int MdsDisconnectServer(int clientHandle, int cursor, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsDisconnectServer(clientHandle, cursor, error);
            }
            else
            {
                return OlapNativeImports32.MdsDisconnectServer(clientHandle, cursor, error);
            }
        }

        public static int MdsServerCountEx(int clientHandle, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsServerCountEx(clientHandle, error);
            }
            else
            {
                return OlapNativeImports32.MdsServerCountEx(clientHandle, error);
            }
        }

        public static int MdsServerNameExW(int clientHandle, ref OlapNativeServerInformation serverInfo, int id, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsServerNameExW(clientHandle, ref serverInfo, id, error);
            }
            else
            {
                return OlapNativeImports32.MdsServerNameExW(clientHandle, ref serverInfo, id, error);
            }
        }

        public static int MdsapiGetErrorMessageW(int[] error, int errorCode, string message, int numChars)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsapiGetErrorMessageW(error, errorCode, message, numChars);
            }
            else
            {
                return OlapNativeImports32.MdsapiGetErrorMessageW(error, errorCode, message, numChars);
            }
        }

        public static int MdsXMLRequestUTF8String(int clientHandle, int cursor, string xmlIn, out IntPtr result, int[] error)
        {
            if (Environment.Is64BitProcess)
            {
                return OlapNativeImports64.MdsXMLRequestUTF8String(clientHandle, cursor, xmlIn, out result, error);
            }
            else
            {
                return OlapNativeImports32.MdsXMLRequestUTF8String(clientHandle, cursor, xmlIn, out result, error);
            }
        }

        public static int MdsDoubleToString(out string formattedNumber, double number)
        {
            StringBuilder sb = new StringBuilder(1000);
            int returnCode = 0;

            if (Environment.Is64BitProcess)
            {
                returnCode = OlapNativeImports64.MdsDoubleToString(sb, number);
                formattedNumber = sb.ToString();
            }
            else
            {
                returnCode = OlapNativeImports32.MdsDoubleToString(sb, number);
                formattedNumber = sb.ToString();
            }
            return returnCode;
        }

        public static string GetName(int cursor)
        {
            return Marshal.PtrToStringUni(GetNameW(cursor));
        }

        public static string GetLongName(int cursor)
        {
            return Marshal.PtrToStringUni(GetLongNameW(cursor));
        }

        public static string GetUser(int cursor)
        {
            return Marshal.PtrToStringUni(GetUserW(cursor));
        }

        public static string GetVersion(int cursor)
        {
            return Marshal.PtrToStringUni(GetVersionW(cursor));
        }

        public static string GetSValue(int cursor)
        {
            return Marshal.PtrToStringUni(GetSValueW(cursor));
        }

        public static string GetCube(int cursor)
        {
            return Marshal.PtrToStringUni(GetCubeW(cursor));
        }

        public static string GetRefName(int cursor)
        {
            return Marshal.PtrToStringUni(GetRefNameW(cursor));
        }

        public static string GetDART(int cursor)
        {
            return Marshal.PtrToStringUni(GetDARTW(cursor));
        }

        public static string GetDimension(int cursor)
        {
            return Marshal.PtrToStringUni(GetDimensionW(cursor));
        }

        public static string GetSubsetRefName(int cursor)
        {
            return Marshal.PtrToStringUni(GetSubsetRefNameW(cursor));
        }
    }
}