using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Infor.BI.Applications.OlapApi.Native
{
    internal static class OlapNativeImports64
    {
        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOCallBegin", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void CallBegin(int cursor, UniFunction function);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOCallEnd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int CallEnd(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetDimensionCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetDimensionCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetCubeCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetCubeCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetNValue", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern double GetNValue(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetLevel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetLevel(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetId", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetId(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetCType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern char GetCType(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetRulesCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetRulesCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetElementsCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetElementsCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetUserData", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetUserData(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetParentsCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetParentsCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetValuesCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetValuesCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetSize", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetSize(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetDecimalPlaces", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetDecimalPlaces(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetLeftOperator", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetLeftOperator(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetLeftValue", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern double GetLeftValue(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetRightOperator", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetRightOperator(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetRightValue", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern double GetRightValue(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetStamp", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetStamp(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetCachedValuesCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetCachedValuesCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetErrorCode", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetErrorCode(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetErrorPosition", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetErrorPosition(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetFieldsCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetFieldsCount(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetFlags", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetFlags(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetNameW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetLongNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetLongNameW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetUserW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetUserW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetVersionW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetVersionW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetSValueW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetSValueW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetCubeW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetCubeW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetRefNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetRefNameW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetDARTW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetDARTW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetDimensionW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetDimensionW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetSubsetRefNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetSubsetRefNameW(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetNextItem", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNextItem(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOGetNextSubItem", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNextSubItem(int cursor);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetId", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetId(int cursor, int id);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetParam(int cursor, uint param);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetNValue", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetNValue(int cursor, double value);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetCType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetCType(int cursor, char charType);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetLeftCondition", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetLeftCondition(int cursor, int leftOperator, double leftValue);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetRightCondition", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetRightCondition(int cursor, int rightOperator, double rightValue);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetSize", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetSize(int cursor, int size);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetDecimalPlaces", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetDecimalPlaces(int cursor, int places);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetTableId", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetTableId(int cursor, int tableId);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetFlags", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetFlags(int cursor, int flags);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetEncoding", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetEncoding(int cursor, IpoEncoding encoding);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetName(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string name);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetLongNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetLongName(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string longName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetDimensionW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetDimension(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string dimName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetRefNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetRefName(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string refName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetCubeW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetCube(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string cubeName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetElementW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetElement(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string elemName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetSValueW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetSValue(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string value);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetSubsetRefNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetSubsetRefName(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string subsetRefName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetFileNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetFileName(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string fileName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "IPOSetErrorLogW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern void SetErrorLog(int cursor, [MarshalAs(UnmanagedType.LPWStr)] string fileName);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsConnect", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsConnect([MarshalAs(UnmanagedType.LPStr)] string userName, int userType, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsConnectServerExtW", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsConnectServerExtW(int clientHandle, [MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string username, [MarshalAs(UnmanagedType.LPWStr)] string password, int timeOut, int terminalID, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsConnectServerTicketW", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsConnectServerTicketW(int clientHandle, [MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string ticket, int terminalID, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsConnectServerWinW(int clientHandle, [MarshalAs(UnmanagedType.LPWStr)] string servername, int terminalID, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsDisconnect", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsDisconnect(int clientHandle, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsDisconnectServer", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsDisconnectServer(int clientHandle, int cursor, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsServerCountEx", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsServerCountEx(int clientHandle, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsServerNameExW", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsServerNameExW(int clientHandle, ref OlapNativeServerInformation serverInfo, int id, int[] error);

        [DllImport("Mis.Alea.ClientUI.dll", EntryPoint = "mdsapiGetErrorMessageW", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsapiGetErrorMessageW(int[] error, int errorCode, [MarshalAs(UnmanagedType.LPTStr)] string message, int numChars);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsXMLRequestUTF8String", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsXMLRequestUTF8String(int clientHandle, int cursor, [MarshalAs(UnmanagedType.LPStr)] string xmlIn, out IntPtr result, int[] error);

        [DllImport("Mis.Alea.ClientSupport64.dll", EntryPoint = "mdsDoubleToString", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int MdsDoubleToString(StringBuilder buffer, double number);
    }
}