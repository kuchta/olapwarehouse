#pragma warning disable 1591 // disables xml documentation warning

namespace Infor.BI.Applications.OlapApi.Native
{
    internal static class ServerConstants
    {
        public const int MaxDim = 20;              /* Maximum number of dimensions in one table */
        public const int DimPnameLength = 9;              /* physical name length */
        public const int DimPnameFullLength = 13;           /* physical name full length (with extention) */
        public const int DimLnameLength = 64;             /* dimension logical name (length) */
        public const int ElementNameLength = 72;             /* maximal length of element name */
        public const int ElementInfoLength = 15;
        public const int ElementAdditionalInfo = 3;         /* maximum count of additinal information handles */
        public const int AttributeFieldLength = 254;      /* maximum length of a field in attribution table */

        public const int MaxLevelNames = 127;      /* maximum count of dimension level name */
        public const int DimLevelNameLength = 42;      /* Maximul length of Dimension Level Name */

        public const int MaxElementCount = 65530;   /* maximal number of elements within one dimension */
        public const int MaxDim16Size = MaxElementCount; /* max number of elements in dimension (where element ID is MDSUINT16) */
        public const int MaxDim32Size = 250000;         /* max number of elements in dimension (where element ID is MDSUINT32) */
        public const int MaxChildrenCount = 65530;   /* maximal number of children in a consolidation */
        /* ... Defines for table structures */
        public const int TablePnameLength = 9;            /* Table physical name (length) */
        public const int TableLnameLength = 64;           /* Table logical name (length) */
        public const int TableUserLength = 9;            /* Table user name (length) */
        public const int MaxStringLength = 127;          /* maximum length of string value */
        /*--------structure for one local rule------------*/
        public const int MaxRuleText = 380;
        /* subset names */
        public const int SubsetRefnameLength = 9;
        public const int SubsetLongnameLength = 96;
        public const int DwPnameLength = 9;
        public const int DwLnameLength = 81;
        public const int DwSuppressNa = 0x08;
        public const int DwZeroAsNa = 0x200;

        public const int DwCalcCells = 0x10;
        public const int DwCellValues = 0x20;         // export cell values - in version 10.1 cannot be combined with DW_CELL_NOTES - enabled by default in 10.1 when DW_CELL_NOTES is not set
        public const int DwCellNotes = 0x40;         // export cell notes - in version 10.1 cannot be combined with DW_CELL_VALUES - in such case is ignored
        public const int DwQuotation = 0x80;         // all string items (element names, notes and string values) are enclosed in quotation marks ascii(0x22)

        public const int AttributeFieldInFile = 50;   /* max. count of fields in one attribute file */
        public const int AttributeMaxDefName = 11;
        public const int AttributeMaxLogname = 48;
        public const int MaxAttributeRecordLength = 4096;
        public const int AttributeNameLength = 31;

        /* flags in lParam */
        public const int PcUser = 0;
        public const int PcImport = 1;
        public const int PcAdditive = 2;  /* add value to exist value (if any) */
        public const int PcLog = 4;  /* log every request in multiple and return array of error (uni.val.lMultiErrors), */
        /* if not seted function returns error on first occurence of any error */
        public const int PcMulti = 8;  /* send multiple values */
        public const int PcStamp = 16;
        public const int PcMultiput = 32;
        public const int PcMultiputFlush = 64;
        public const int PcTrs = 128;
        public const int PcUniXml = 256; /*send as uni, return as xml*/

        public const int CsLocalRuleTop = 0x01;
        public const int CsLocalRule = 0x02;
        public const int CsString = 0x04;
        public const int CsNumeric = 0x08;
        public const int CsGlobalRule = 0x10;
        public const int CsMissing = 0x20;
        public const int CsConsolidated = 0x40;
        public const int CsError = 0x80;

        public const int TiAccessControl = 0x0010;
        public const int TiModified = 0x0020;
        public const int TiLogActive = 0x0040;
        public const int TiLoaded = 0x0080;
        public const int TiSuppressBadType = 0x0100;
        public const int TiVisible = 0x0200;

        public const int UrRead = 0x00;    /* can read data from server */
        public const int UrAdministrator = 0x01;    /* all rights on server */
        public const int UrPassword = 0x02;    /* can change the passwords */
        public const int UrDimChange = 0x04;    /* can change, load and save the dimensions or the dimension attributes */
        public const int UrExport = 0x08;    /* can make an export or import */
        public const int UrWrite = 0x10;    /* write and delete data (attributes) allowed */
        public const int UrRules = 0x20;    /* can read, write and change rules */
        public const int UrUser = 0x40;    /* can change the user/user groups  */
        public const int UrSparsity = 0x80;    /* can add,change and delete sparsity indices */

        public const int MdsEtN = 0x01;
        public const int MdsEtS = 0x02;
        public const int MdsEtC = 0x04;
        public const int MdsEtR = 0x08;    /* global rule */
        public const int MdsEtSave = 0x10;    /* saved consolidation */
        public const int MdsEtHasParents = 0x20;
        public const int MdsEtHasAttributes = 0x40;    /* element has attribute information */
        public const int MdsEtNoRights = 0x80;    /* user has at least no read right on this element */
        public const int MdsEtNoRightsLong = 0x00000080;    /* user has at least no read right on this element */

        public const int MdspNotUserOnly = 0x00000001; /* all elements in the subset (access rights check for user disabled)*/
        public const int MdspRefresh = 0x00000002; /* refresh the subset result set */
        public const int MdspSaveResult = 0x00000004; /* save the result (while large time can be spent in algorithm) */

        /* top level for subset request */
        public const int MdsTopLevel = 254;
        public const int MdsTopLevelInSubset = 253;

        public const int SstPublic = 0x01;            /* global subset type */
        public const int SstPrivate = 0x02;            /* private subset type */
        /* these two defines can be not used as regular subset type: for search only */
        public const int SstAll = SstPublic | SstPrivate; /* all public and my private */
        public const int SstAllSubsets = 0x04;                    /* all public and private */
        public const int SstStatic = 0x08;            /* static subset */
        public const int SstDataQuery = 0x10;            /* dynamic subset - data query */
        public const int SstAttributeQuery = 0x20;            /* dynamic subset - attribute query */
        public const int SstSaveResult = 0x40;            /* subset result is saved on the disk */

        public const int MdsValueDelete = 0x00000016;          /* flag for set dataworld values */
        public const int ResultEnd = 0x00000001;           /* reached end of dataarea result */
    }
}