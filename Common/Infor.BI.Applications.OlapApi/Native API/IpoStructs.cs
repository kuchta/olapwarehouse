using System;
using System.Runtime.InteropServices;

namespace Infor.BI.Applications.OlapApi.Native
{
    internal enum IpoEncoding
    {
        Ansi = 0,  // IPOE_ANSI
        Utf16 = 1, // IPOE_UTF16LE
        Utf8 = 2,  // IPOE_UTF8
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct OlapNativeServerInformation
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 251)]
        public string ServerName;

        public byte Communication;
        public byte IsDefaultDB;
    }
}