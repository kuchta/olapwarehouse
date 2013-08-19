#pragma warning disable 1591 // disables xml documentation warning

namespace Infor.BI.Applications.OlapApi.Native
{
    public enum ServerErrorCodes
    {
        KE_OK = 0,          /* OK or the called routine sets not the error value */
        KE_NOPATH = 1,          /* Input Parameter Path not valid */
        KE_DIMEXIST = 2,          /* Dimension already exists */
        KE_NODIM = 3,          /* Dimension wasn't created in mdsCompileDimBegin */
        KE_NODIMADD = 4,          /* Can't add Header to Dim tree in function mdsCompileDimEnd */
        KE_NONAME = 5,          /* No name was specified for element */
        KE_BADTYPE = 6,          /* Unknown element type */
        KE_ELEMEXIST = 7,          /* Element in dimension already exist */
        KE_NOELEMADD = 8,          /* Can't add Element to Dimension */
        KE_NOUSRNAME = 9,          /* No user name was specified */
        KE_NOFREEUSER = 10,          /* No free slot for user, count of logged in user == max user */
        KE_BADUSERNAME = 11,          /* No user with specified name exist on this server */
        KE_BADPASSWORD = 12,          /* Bad password */
        KE_NOMLTLOGIN = 13,          /* Multiple login not enabled */
        KE_NOCFG = 14,          /* missing configuration file */
        KE_BAD_FUNC = 15,          /* bad function number defined in MDSUNI */
        KE_BAD_LIC = 16,          /* bad license file or the user tries to change the license file  */
        KE_BAD_LICFILE = 17,          /* bad license file in configuration file = (can not be opened), */
        KE_MEM_USER = 18,          /* no memory for alloc the communication slots */
        KE_NODB = 19,          /* the MDS database can not be opened in data path */
        KE_BAD_DB = 20,          /* the MDS database has bad format - call service/administrator! */
        KE_READ_DB = 21,          /* error during read from the MDS database */
        KE_MEM_LUSER = 22,          /* no memory for alloc the user = (or user list = (lpUserList), ), */
        KE_MEM_LGROUP = 23,          /* no memory for alloc the user group = (or list = (lpGroupList), ), */
        KE_MEM_TDIM = 24,          /* no memory for alloc the dimension tree = (lpDimHeap), */
        KE_MEM_TTAB = 25,          /* no memory for alloc the table tree = (lpTabHeap), */
        KE_NO_DIM = 26,          /* dimension not found */
        KE_DIMUPDATE = 27,          /* can not update the dimension = (element count), */
        KE_INTERNAL = 28,          /* internal error = (call developer), */
        KE_USEREXIST = 29,          /* cannot add existent user */
        KE_BAD_ID = 30,          /* identifier not valid */
        KE_NOGRPNAME = 31,          /* No or bad user-group name was specified */
        KE_GROUPEXIST = 32,          /* group name allready exists */
        KE_BADGROUPNAME = 33,          /* No usergroup with specified name exist on this server */
        KE_NO_RIGHTS = 34,          /* The user has no rights for this handling ... */
        KE_WRITE_DB = 35,          /* Error while writing database */
        KE_WRITE_DIM = 36,          /* Error while writing dimension file */
        KE_NO_ELEM = 37,          /* Referenced element not exists. */
        KE_MEM_BIGBL = 38,          /* no memory for alloc the big block for consolidated elements */
        KE_NO_CONTEXT = 39,          /* this function was called out of context! */
        KE_MEM_VHEAP = 40,          /* variable heap can not be created */
        KE_MEM_VELEM = 41,          /* one element in variable heap can not be created */
        KE_TABEXIST = 42,          /* Table already exist */
        KE_DIMEXHAUST = 43,          /* Count of dimension > MAX DIM || <2 ==> Error */
        KE_NOTABVAHEAP = 44,          /* Table Value heap wasn't created */
        KE_NOTABINS = 45,          /* Cann't insert new table into TABLE-tree */
        KE_NO_TABDI = 46,          /* Cann't create dimension = (TabDi), in table in mdsCreateTable */
        KE_NO_TAB = 47,          /* Table doesn't exist or can not be loaded. */
        KE_NO_KILLTABDI = 48,          /* Cann't kill table dimension. */
        KE_NO_KILLTABVA = 49,          /* Can not kill table values memory. */
        KE_NO_KILLTABLE = 50,          /* Can not delete table from table list. */
        KE_NO_PTR = 51,          /* Function NHpElementPointer or VHpEl.. returned NULL pointer */
        KE_NO_CTYPE = 52,          /* Type of element isn't 'C' */
        KE_SRANGEOUT = 53,          /* ID of SubElement is out of range */
        KE_NOFILE = 54,          /* The file can not be open. */
        KE_READ_DIM = 55,          /* Error while reading dimension file */
        KE_BAD_VERSION = 56,          /* Bad file version */
        KE_BAD_FILETYPE = 57,          /* Bad file type */
        KE_READERR = 58,          /* Error during reading from file */
        KE_ERRLOADDIM = 59,          /* Can't load dimension */
        KE_TABCREATERR = 60,          /* Can't create table */
        KE_NO_MEM = 61,          /* Can't alloc block of memory */
        KE_ERRFILECREAT = 62,          /* Can't create file */
        KE_NOINFO = 63,          /* Can't get info */
        KE_WRITEERR = 64,          /* Error while writin into file */
        KE_NO_DIMEL = 65,          /* Dim element wasn't found */
        KE_NO_TABVAFND = 66,          /* No value in table found */
        KE_NO_TABVAINS = 67,          /* Cann't add element to Table value */
        KE_NO_TABVAUP = 68,          /* Cann't update element */
        KE_INCTYPE = 69,          /* Incompatible type in PutCell */
        KE_MISSINGVAL = 70,          /* Value on zero level not exists */
        KE_COMPUTE = 71,          /* Error during calculation on TableValue */
        KE_DIMLEVEL = 72,          /* Error during set the element laevel. */
        KE_DIMCYCLE = 73,          /* Error during create the dimension while cycle.*/
        KE_MISSMATCH = 74,          /* The consolidated element has no begin! */
        KE_BADPARAM = 75,          /* Bad parameter. */
        KE_SNDX_YET_EXISTS = 76,       /* sparsity index yet exists = (in lId the slot number), */
        KE_SNDX_TOOMANY = 77,       /* too many indices */
        KE_UPDATE_MEMORY = 78,       /* memory problem while updating */
        KE_NO_INDEX = 79,          /* Index not exists. */
        KE_NO_RTYPE = 80,          /* The type of the element is not 'R' */
        KE_VALUEDEL = 81,          /* Can't delete value with 'R' or 'C' as dim element */
        KE_BAD_RESULT = 82,          /* The type of saved result set not compatible to request. */
        KE_NO_DW = 83,          /* Dataarea database not opened! */
        KE_NO_SUBS = 84,          /* Subsets database not opened! */
        KE_DW_SEARCH = 85,          /* Dataarea not found for update! */
        KE_SSUB_SEARCH = 86,          /* Subset not found for update! */
        KE_NODIMINS = 87,          /* Can't insert deimension element */
        KE_DIMLOADED = 88,          /* Dimension is allready loaded */
        KE_ATTR_YETEXISTS = 89,        /* Attribute file can not been created */
        KE_TOO_MANYFLD = 90,         /* Count of fields in attribute description greater as the constant ATTR_FLD_IN_FILE */
        KE_BAD_FIELD = 91,         /* Bad fields attributes for create a table or read/put the field content. */
        KE_NO_FIELDS = 92,         /* No fields defined for attribute table. */
        KE_MEM_ATTR = 93,         /* No memory to allocate heap for attribute fields. */
        KE_NO_ATTR = 94,         /* No attribute file found. */
        KE_BAD_RECORD = 95,         /* The record number saved by element name is out of range. */
        KE_NO_RECORD = 96,         /* No record for this attribute database over this element. */
        KE_UNKNOWN_FIELD = 97,         /* Field not a member of attribute file. */
        KE_TOOMANY_NDX = 98,         /* Too many indices defined for one attribute file. */
        KE_NORULEHEAP = 99,         /* Local rule heap wasn't created */
        KE_NOTABINMEM = 100,         /* Table isn't in memory */
        KE_RANGE = 101,         /* Identifier out of range. */
        KE_COMPILE = 102,         /* Error in rules */
        KE_NO_MATCH = 103,         /* Cannot find the wanted object... */
        KE_NO_TRFNDX = 104,         /* Cannot create TrefNdx Heap or write to him */
        KE_REMOVETAB = 105,         /* Can't remove table from memory */
        KE_DELETE = 106,         /* Delete error */
        KE_SLTOOBIG = 107,         /* slice is too big */
        KE_NO_SLICE = 108,         /* no slice was defined by user */
        KE_NO_FREESLICE = 109,         /* no space for slice in oneuser struct */
        KE_BADSLICE = 110,         /* bad slice number */
        KE_BADMDS = 111,         /* bad size of MDSUNI on kernel interface */
        KE_FILEREMOVE = 112,         /* can't remove file from disk */
        KE_ELSTRING = 113,         /* the size of element names is greater as the size of the buffer */
        KE_NOFREESLOT = 114,         /* no free CSD slot */
        KE_BIGJOB = 115,         /* Server is busy in big job */
        KE_NO_QUERY = 116,         /* No query defined in user data area. */
        KE_UPDATE = 117,         /* Update error */
        KE_BADCURSOR = 118,         /* The passed cursor is invalid. */
        KE_BADLRTEXTPART = 119,        /* Bad Local Rule text Part given */
        KE_DOUBLE_LIC = 120,         /* The user with this local license yet logged in! */
        KE_NODA = 121,        /* Cannot allocate DATASTRUCT structure */
        KE_NODS = 122,        /* Cannot allocate DATAAREA structure */
        KE_BADDA = 123,        /* Bad DA structure */
        KE_BADDS = 124,        /* Bad DS structure */
        KE_ERRFILEOPEN = 125,        /* can't open existing file */
        KE_COMM_CREATE = 126,        /* can't create Comment Tree */
        KE_NO_COMMTREE = 127,        /* comment text tree in not create */
        KE_NO_DEL_COMM = 128,        /* can't delete comment text tree */
        KE_ERROR_NOT_SETTED = 129,    /* the called function has not setted the error value */
        KE_BAD_CSD = 130,        /* invalid csd slot */
        KE_SERVER_DOWN = 131,        /* server is down or not initialisied */
        KE_SWAPFILECREATE = 132,      /* cannot create swap file */
        KE_TOOMANY_CHILDREN = 133,    /* Too many children for one consolidated element. */
        KE_IDENTPARCHILD = 134,       /* Parent and child element is identical */
        KE_ABORTED_BY_USER = 135,      /* The user has aborted = (abort button in progression box),. */
        KE_LR_EXECUTION = 136,        /* error during execution of local rule */
        KE_ENDFILE = 137,      /* End of file */
        KE_SYSDIM_DELETING = 138,    /* cannot delete system dimensions */
        KE_ELEMREMOVE = 139,      /* cannot remove element from dimension */
        KE_RULETOOLONG = 140,      /* rule text is too long */
        KE_BAD_SAVE = 141,      /* bad save dimension or table */
        KE_DELETE_ERROR = 142,      /* don't delete AttrRecord in dimension */
        KE_NOHANDLE = 143,      /* function NHpAlloc or VHpAlloc returns NOHANDLE */
        KE_TOOMANY_FACTORS = 144,     /* too many diferent factors in one consolidation = (max 254 + 2 defaults = (0.0/1.0),), */
        KE_TOO_FEW_PARAMS = 145,      /* too few parameters in multiple operations = (MultiplePutCel), */
        KE_TOO_MANY_REQUESTS = 146,   /* too many requests in multi function = (MultiplePutCel), */
        KE_ERROR_IN_FILE = 147,      /* error value in file */
        KE_REMOVEDIM = 148,      /* Can't remove dimension from memory */
        KE_BAD_DIMNR = 149,      /* bad dimension number in slice functions */
        KE_STAMPHP_CREATE = 150,      /* cannot create values stamp heap */
        KE_REPLHP_CREATE = 151,      /* cannot create repliction heap */
        KE_NO_REPLICATION = 152,      /* specified replication doesn't exists */
        KE_ACC_TOO_LONG = 153,      /* accelerator text is too long */
        KE_USER_CONNECTED = 154,      /* more than one user connected to the server, server cannot change database */
        KE_ACCELHP_CREATE = 155,      /* cannot create accelerator heap */
        KE_TACCELHP_CREATE = 156,     /* cannot create CSI tree for table */
        KE_DIM_ORDER = 157,      /* dimension order in CSI is incorrect */
        KE_ROOT_PATH = 158,      /* bad database root path */
        KE_DB_NAME = 159,      /* bad database name = (subdirectory in the root), */
        KE_DEL_ADMIN_GRP = 160,      /* user cannot delete or change this protected group */
        KE_DEL_ADMIN_USR = 161,      /* user cannot delete or change this protected user */
        KE_ACC_DB_CREATE = 162,      /* cannot create accelerator databank */
        KE_ACC_RW = 163,      /* error during reading or writing accelerator database */
        KE_LONG_ELEM_NAME = 164,      /* dimension element name is too long */
        KE_ONLY_DEMO = 165,      /* this operetion cannot be executed in demoversion */
        KE_TIMEOUT = 166,      /* abort by timer */
        KE_IDENT_FIELD = 167,      /* two attribute field have the same name */
        KE_PUBLDW_PRIVSST = 168,      /* private subsets cannot be used in public dataworld */
        KE_TOO_MANY_ELEMS = 169,      /* too many elements in dimension, limit is: MAX_ELEM_COUNT */
        KE_CACHE_IS_FULL = 170,      /* count of values in cache is >= RULE_MAXCACHE */
        KE_BAD_PROD_CODE = 171,      /* Bad OEM product code */
        KE_NOKEYHEAP = 172,      /* Key heap heap wasn't created */
        KE_USERFILE_NOTINIT = 173,    /* User file system not initialized */
        KE_USERFILE_INUSE = 174,     /* User file in use by another user */
        KE_SYNCHRONIZATION = 175,     /* General synchronization error */
        KE_AS_CANT_INIT = 176,     /* Can't initialize Advanced subset */
        KE_BADMSHANDLE = 177,     /* Bad mdx-set global handle */
        KE_ATTR_NOT_UNIQUE = 178,     /* dimension element attribute is not unique */
        KE_TUPLENOFACTOR = 179,     /* tuple without factor */
        KE_UFALREADYEXIST = 180,     /* User File already exists */
        KE_STACK_FULL = 181,     /* stack full = (MDSNT35S.INI entry StackLimit=), */
        KE_NOT_SUPPORTED = 182,     /* Alea kernel function is not supported on */
        KE_VIRT_OBJECT = 183,     /* requested operation is forbidden on virtual Alea objects */
        KE_ASO_BADREQ = 184,     /* ASO Server sent unknown request */
        KE_ASO_STACKFULL = 185,     /* ASO event stack is full */
        KE_ASO_STACKEMPTY = 186,     /* ASO event stack is empty */
        KE_ASO_UNKNOWNOBJ = 187,     /* ASO unknown object fired event */
        KE_ASO_UNKNOWNEVNT = 188,     /* ASO object fired unknown event */
        KE_LOCK_FAILED = 189,     /* lock failed = (putcachevalue), */
        KE_FULL_CACHE = 190,     /* cache is full */
        KE_EMPTY_DIM = 191,     /* dimension hasn't any element */
        KE_GRP_NOTEMPTY = 192,     /* the group contains at least one user */
        KE_GRP_NOTEXIST = 193,     /* the group doesn't exist */
        KE_ANOTHER_SERVER = 194,     /* Another server is using this database */
        KE_DIM_USED_MULTI = 195,     /* dimension is used in cube more than once */
        KE_ELEM_INVAL_CHAR = 196,     /* invalid character= (s), in the dimension element name */
        KE_DIM_INVAL_CHAR = 197,     /* invalid character= (s), in the dimension name */
        KE_VIRT_SCHEMA_CHANGED = 198, /* virtual object has changed and cannot be refreshed */
        KE_ASO_ENGINEDISABLED = 199, /* ASO engine is disabled */
        KE_WRITE_OA_ACCEL = 200, /* try to write basic value to overflow area and it invoked creating of accel */
        KE_LOCKEDBYADMIN = 201, /* Server locked by administrator. */
        KE_NOT_LOCKED = 202, /* mdsUnlockServer without mdsLockServer */
        KE_TOOMANY_PARENTS = 203, /* Too many parents for an element */
        KE_NO_VIRT_DIM = 204, /* virtual dimension not found */
        KE_DIM_EMPTY_CONS = 205, /* consolidated element without children */
        KE_ASO_LOCAL_DISABLED = 206, /* local communication is disabled for ASO clients */
        KE_LICENSE_INSUFFICIENTUNITS = 207, /* License server doesn't have sufficient licensing units. */
        KE_LICENSE_EXPIRED = 208,     /* License is expired. */
        KE_LICENSE_TRIALEXPIRED = 209, /* Trial license expired or trial license usage exhausted. */
        KE_XML_PARSE = 210,    /* Error during parsing XML. */
        KE_LICENSE_NOSUCHFEATURE = 211, /* No such feature recognized by server */
        KE_LICENSE_NODELOCKED = 212, /* This /feature is node locked but the request for a key came from a machine other than the host running the SentinelLM server.*/
        KE_LICENSE_USEREXCLUDED = 213, /* User/machine excluded */
        KE_LICENSE_VENDORMISMATCH = 214, /* The vendor identification of requesting application does not match with that of the application licensed by this system. */
        KE_LICENSE_FEATUREINACTIVE = 215, /* The feature is inactive on the requested server. */
        KE_LICENSE_NOSERVERRUNNING = 216, /* On the specified machine, license server is not RUNNING. */
        KE_LICENSE_NOSERVERRESPONSE = 217, /* On the specified machine, license server is not responding. = (Probable cause - network down, wrong port number, some other application on that port etc.), */
        KE_LICENSE_HOSTUNKNOWN = 218, /* Unkown host = (Application is given a server name but that server name doesnt seem to exist), */
        KE_LICENSE_NOSERVERFILE = 219, /* No FILE giving license server name = (Application cannot figure out the license server. */
        KE_LICENSE_NONETWORK = 220, /* The network is unavailable */
        KE_LONG_DIM = 221, /* The called mds function cannot be used for long dimensions */
        KE_OBSOLETE_FUNCTION = 222, /* The called mds function is obsolete, it is not supperted any more */
        KE_SPLASH_COORDCOUNT = 223, /* Count of coordinates differs from count of dimensions in hypercube */
        KE_SPLASH_WRONG_DIMNAME = 224, /* The hypercube doesn't contain the specified dimension */
        KE_SPLASH_TWICE_DIMNAME = 225, /* Dimension specified twice in coordinates */
        KE_SPLASH_ELEMTYPE = 226, /* Wrong type of an element, it cannot be 'R' or 'S' */
        KE_SPLASH_ELEMRULE = 227, /* There is a rule defined on an element */
        KE_SPLASH_CUBETYPE = 228, /* Wrong type of a hypercube, it has to be 'U' */
        KE_SPLASH_DIVISIONBYZERO = 229, /* division by zero, sum of factors is zero */
        KE_SPLASH_NOTSIMILAR = 230, /* the = (consolidated),cell to be updated is not "similar" to external = (consolidated),cell */
        KE_SPLASH_NOTDISTINCT = 231, /* summands of = (consolidated),cell to be updated and summands of external = (consolidated),cell are not distinct */
        KE_SPLASH_UNDOFILE = 232, /* undo file for splashing cannot be created */
        KE_SPLASH_UNDOIMPOSSIBLE = 233, /* undo is not possible, the hypercube was changed */
        KE_XML_NOT_INSTALLED = 234, /* XML parser is not installed - server cannot work */
        KE_SPLASH_TOO_MANY_VALUES = 235, /* too many basic values for splashing */
        KE_DISCONNECT_IMPOSSIBLE = 236, /* User can't disconnect himself this way. */
        KE_DIFERENT_CUBES = 237, /* the target cube differs from the source cube */
        KE_BAD_CRYPT_LEVEL = 238, /* Client is not on same crypting level as server */
        KE_WIN_AUTH = 239, /* User can't change password, because he's only using windows authentication */
        KE_EWB_VIOLATION = 240, /* A restriction for the "External Weighted Basic" splashing mode is violated */
        KE_COS_BAD_PROVIDER_TYPE = 241, /* bad COS provider type */
        KE_COS_BAD_CONNECTION = 242, /* bad COS connection string */
        KE_COS_BAD_PASSWORD = 243, /* bad COS user password */
        KE_COS_USERNAME = 244, /* bad COS user name */
        KE_COS_NTAUTHENTICATION = 245, /* error in COS NT authentication */
        KE_COS_TICKET_CORRUPTED = 246, /* SSO ticket corrupted */
        KE_COS_TICKET_EXPIRED = 247, /* SSO ticket expired */
        KE_COS_OTHER = 248, /* generic COS error */
        KE_MDAC_DIM = 249, /* MDAC doesn't contain #__GRP__ dimension or it contains a dimension which is not in the attached cube */
        KE_CUBETYPE = 250, /* Wrong type of a hypercube */
        KE_MULTI_PROPERTIES = 251, /* one XML property provided multiple times */
        KE_PROPERTY_READ_ONLY = 252, /* XML property is read only */
        KE_RULE_RECURSION = 253, /* rule recursion too deep */
        KE_RULE_CYCLE = 254, /* rule cyclic dependence detected */
        KE_SPLASH_DIVISIONBYZERO_2 = 255, /* division by zero, CC old value is zero */
        KE_SPLASH_DIVISIONBYZERO_3 = 256, /* division by zero, external CC value is zero */
        KE_INVALIDLEVEL = 257,  /* Level number is negative or too high */
        KE_LEVELEXIST = 258,  /* Level with this name in dimension already exists */
        KE_MAXCALCTIME = 259,  /* the calculation was cancelled because of the MaxCalcTime-setting */
        KE_CUBE_REPEATED = 260,  /* only one occurence of cube is allowed */
        KE_NO_MDAC_TAB = 261,  /* MDAC table doesn't exist or cannot be loaded. */
        KE_COS_READONLY = 262,  /* No change in COS allowed, because of internal read-only connection */
        KE_COS_SSOLINK = 263,  /* A connection to the centralized 'SSO database' could not be established */
        KE_COS_PERMISSION = 264,  /* user does not inherit a requested permission */
        KE_COS_REPOSITORY = 265,  /* Error while accessing COS repository. */
        KE_COS_CONFIGURATION = 266,  /* Error in COS configuration. */
        KE_COS_DATABASEVERSION = 267,  /* COS version exception */
        KE_COS_DATABASE = 268,  /* COS database internal exceptions */
        KE_COS_CHECKIN_CHECKOUT = 269,  /* Error during COS checkin/checkout. */
        KE_COS_INSERT_UPDATE = 270,  /* Error during COS insert/update. */
        KE_COS_DELETE_UNDELETE = 271,  /* Error during COS delete/undelete. */
        KE_COS_PASSWORD_TOO_LONG = 272,  /* COS password is too long. */
        KE_ODBODIMTYPE = 273,  /* wrong ODBO dimension type */
        KE_NO_MEASURE_DIM = 274,  /* no measure dimension is set */
        KE_CLIENT_CODEPAGE = 275,  /* invalid client's codepage */
        KE_RULES_CHANGED_MEANWHILE = 276, /* edited rules has been changed by other user before modifications were finished */
        KE_BAD_SUBSET = 277,  /* Subset definition is invalid. = (Bad cube name in data driven subset etc.), */
        KE_COS_BAD_AUTHENTICATION = 278,  /* Invalid COS authentication system */
        KE_ABORTED_BY_ADMIN = 279,  /* Admin has aborted the task */
        KE_OPTIMISTIC_FAILED = 280,  /* optimistic version of function failed - internal error only - should never appear on client side */
        KE_BC_CUBENAME = 281,  /* wrong cube name, other cube name is expected */
        KE_BC_STARTED = 282,  /* batch calculation was already started by another user */
        KE_BC_BREAK_SLOT = 283,  /* batch calculation has to be stopped due to inconsistency of UserSlots */
        KE_BC_BREAK_CALC = 284,  /* batch calculation has to be stopped due to calculation error */
        KE_SUBSET_NOT_STATIC_PUBLIC = 285,  /* subsets used in rules/accelerators must be defined as and must stay public and static */
        KE_COS_NEW_PASSWORD_INVALID = 286, /* The new password is not valid. It breaks security restrictions. */
        KE_COS_PASSWORD_EXPIRED = 287, /* Password has expired. Change password. */
        KE_COS_ACCOUNT_DISABLED = 288, /* User account is disabled. */
        KE_COS_INTERFACE = 289, /* Generic COS error, may be missing COS interface. */
    }

    internal enum ClientSupportErrorCodes
    {
        ECI_SS = 1000,        /* Subsystem Number: Kernel=0, CI=1000            */
        ECI_OK = 0,           /* all right                                      */
        ECI_NOSLOT = ECI_SS + 1,  /* no slot free for login in CI                   */
        ECI_KERNEL = ECI_SS + 2,  /* Kernel request faild => error in UNI-Struct    */
        ECI_SLOT = ECI_SS + 3,  /* the passed slot number not valid               */
        ECI_ID = ECI_SS + 4,  /* the passed id not valid                        */
        ECI_FILE_OPEN = ECI_SS + 5,  /* file can not be open                           */
        ECI_MEMORY = ECI_SS + 6,  /* memory allocation problem                      */
        ECI_EMPTY = ECI_SS + 7,  /* file empty nothing to do                       */
        ECI_BAD_PARAM = ECI_SS + 8,   /* bad parameter = (usually pointer is NUL),        */
        ECI_BREAK = ECI_SS + 9,  /* operation aborted = (with user YES),              */
        ECI_NOINIT = ECI_SS + 10, /* client interface not initialisied */
        ECI_COLOVER = ECI_SS + 11, /* the column is greater then the count of columns in the line by import in a table */
        ECI_SSCINIT = ECI_SS + 12, /* the tm1u.dll can not be loaded    */
        ECI_SERVERSLOT = ECI_SS + 13, /* bad server slot in uni structure  */
        ECI_SSCERROR = ECI_SS + 14, /* error during SSC ran              */
        ECI_SRVTM = ECI_SS + 15, /* too many servers => no place      */
        ECI_BADSRVNM = ECI_SS + 16, /* bad server name                   */
        ECI_SOCKERR = ECI_SS + 17, /* error while creating one windows socket                                                */
        ECI_NOCONNECT = ECI_SS + 18, /* no connect over the existing endpoint = (can be Windows socket, Novell SPX endpoint, etc.*/
        ECI_SERVERREQ = ECI_SS + 19, /* the other server is down or the network not connects over created socket               */
        ECI_NORESPONSE = ECI_SS + 20, /* the request sent but no response comming from server                                   */
        ECI_OPENTLI = ECI_SS + 21, /* error while opening the SPX over TLI                                                   */
        ECI_SUBSYSTEM = ECI_SS + 22, /* Subsytem error = (not initialised),                                                       */
        ECI_TOOMANY_CONNECTIONS = ECI_SS + 23, /* Too many user´s wanted to logg in on the same server from this client computer.               */
        ECI_COMM_BUSY = ECI_SS + 24, /* Communication library is busy = (an other process needs currently the same communication way = (protoco),),*/
        ECI_ELEM_NAME = ECI_SS + 25, /* Bad element name in import file */
        ECI_INIT_LOCAL = ECI_SS + 26, /* Can not initialise local server., could be disabled in ini file */
        ECI_BAD_LICENSE = ECI_SS + 27, /* Bad license number. */
        ECI_SERVER_NAME = ECI_SS + 28, /* server name is too long */
        ECI_SYS_VERCHCK = ECI_SS + 29, /* system version checking failed */
        ECI_LINE_LONG = ECI_SS + 30, /* line in imported file is too long */
        ECI_INI_READ_FAIL = ECI_SS + 31, /* Can't read ROOT or DB from MDSCP.INI */
        ECI_BAD_PROD_CODE = ECI_SS + 32, /* bad product code specified in mdsConnectOEM, should be "00" - "zz" */
        ECI_BAD_LICPATH = ECI_SS + 33, /* bad licence path param of mdsConnectOEM or in mdscp.ini */
        ECI_NOT_SUPPORTED = ECI_SS + 34, /* not supported */
        ECI_CLIENTCONNECT_NOT_FILLED = ECI_SS + 35, /* clientconnect is not filled */
        ECI_INVALID_XML = ECI_SS + 36, /* Invalid XML. */
        ECI_AUTH_FAILS = ECI_SS + 37, /* Windows authentication fails. */
        ECI_XML_NOT_INSTALLED = ECI_SS + 38, /* XML parser is not installed. */
        ECI_SSO_BAD_PROVIDER_TYPE = ECI_SS + 39, /* bad SSO provider type */
        ECI_SSO_BAD_CONNECTION = ECI_SS + 40,    /* bad SSO connection string */
        ECI_SSO_BAD_PASSWORD = ECI_SS + 41,      /* bad SSO user password */
        ECI_SSO_USERNAME = ECI_SS + 42,      /* bad SSO user name */
        ECI_SSO_NTAUTHENTICATION = ECI_SS + 43,  /* error in SSO NT authentication */
        ECI_SSO_OTHER = ECI_SS + 44,      /* generic SSO error */
        ECI_SHORT_BUFFER = ECI_SS + 45,      /* Buffer is not long enough to hold the data. */
        ECI_INCOMPATIBLEVERSION = ECI_SS + 46,   /* Incompatible version of server */
        ECI_ADMINSTART = ECI_SS + 47,   /* Admin have to start database manualy */
        ECI_ALREADYRUNNING = ECI_SS + 48,   /* DB is already running. */
        ECI_DBEXISTS = ECI_SS + 49,   /* DB is already exists. */
    }

    internal enum ClientUIErrorCodes
    {
        MDSAPI_OK = 0,
        MDSAPI_CANCEL = -1, // user pressed cancle button
        MDSAPI_NULL_POINTER = -2, // null pointer passed or empty string
        MDSAPI_NO_CLIENT = -3, // no valid client slot set
        MDSAPI_INTERNALERROR = -4, // api internal error
        MDSAPI_SERVERNOTFOUND = -5, // server not found
        MDSAPI_NOTLOGGEDIN = -6, // not logged in
        MDSAPI_INVALIDCURSOR = -7, // cursor is invalid
        MDSAPI_ARG_MISSMATCH = -8, // conflict of arguments
        MDSAPI_NO_SLICE = -9, // slice not found - ID??
        MDSAPI_OUT_OF_RANGE = -10, // element not found  
        MDSAPI_DIM_NOT_FOUND = -11, // dimension not found
        MDSAPI_NO_DIM = -12, // dim not found - ID??
        MDSAPI_NO_TABLE = -13, // table not found!
        MDSAPI_ELEM_NOT_FOUND = -14, // element not found
        MDSAPI_ELEM_NOT_CONS = -15, // element type is not "C"
        MDSAPI_SST_NOT_FOUND = -16, // subset not found
        MDSAPI_NO_CALLBACK = -17, // no callback defined
        MDSAPI_NO_SHEET = -18, // sheet couldn't be found
        MDSAPI_ALREADYCONNECTED = -19, // alreaty connected to the server
        MDSAPI_SLICE_TO_BIG = -20, // the resulting slice is too big
        MDSAPI_NO_ACCESS = -21, // user has no access
        MDSAPI_DIM_CHANGED = -22, // dimension was changed!
        MDSAPI_NOT_INITIALISED = -23, // was to initialise
        MSDAPI_DB_NOT_CHANGED = -24, // couldn't change the db
        MDSAPI_INVALID_LIC = -25, // license is invalid
        MDSAPI_DUBLICATE_IDENT = -26, // dublicated identifier
        MDSAPI_GUI_DISABLED = -27, // GUI was disabled!
        MDSAPI_NO_ATTR_TABLE = -28, // wrong or missing attribute table
        MDSAPI_NO_ATTR_FIELD = -29, // wrong or missing attribute field
        MDSAPI_ODBC_ERROR = -30, // ODBC error
        MDSAPI_IO_ERROR = -31, // file not found etc.
        MDSAPI_FUNC_OBSOLETE = -32, // function obsolete
        MDSAPI_NOTSUPPORTED_VDIM_OPERATION = -33, // function can't handle virtual dims
        MDSAPI_NOTSUPPORTED_VTAB_OPERATION = -34, // function can't handle virtual tables
        MDSAPI_MAXSERVER_EXCEEDED = -35, // number of manageable servers exceeded
        MDSAPI_INDEXOUTOFRANGE = -36, // index is not within the valid range.
        MDSAPI_BUFFERTOSMALL = -37, // passed buffer is to small.
        MDSAPI_STRINGPARAMTOLONG = -38, // invalid length of string parameter 
        MDSAPI_ODBO_INVALIDHANDLE = -39, // ODBO connection handle is invalid *** should be removed
        MDSAPI_ODBO_SESSION_NOTOPENED = -40, // ODBO session is not opened yet *** should be removed
        MDSAPI_ODBO_UNEXPECTEDSESSIONTYPE = -41, // unable to operate using a session of this type *** should be removed
        MDSAPI_ODBO_SRVNOTFOUND = -42, // ODBO server not found *** should be removed
        MDSAPI_ODBO_CUBENOTFOUND = -43, // cube not found on ODBO server *** should be removed
        MDSAPI_ODBO_DIMNOTFOUND = -44, // dimension not found on ODBO server *** should be removed
        MDSAPI_TABLENOTLINKED = -45, // applied operation requires a MDS hypercube link *** should be removed
        MDSAPI_DIMNOTLINKED = -46, // applied operation requires a MDS dimension link *** should be removed
        MDSAPI_VIEW_MANYROWS = -47,
        MDSAPI_VIEW_TOOLARGE_DEFINE_SMALL = -48,
        MDSAPI_XML_PARSE = -49, // any XML parser error
        MDSAPI_USER_EXISTS = -50, // inserted user already exist
        MDSAPI_NOHELP = -51, // help can't be opened = (for any reason),
        MDSAPI_USER_NOTEXISTS = -52, // required user doesn't exist
        MDSAPI_INIPATH_NOTEXISTS = -53, // incorect ini file path
        MDSAPI_USER_AUTH_USED = -54, // user is using win authentication *** should be removed
        MDSAPI_USER_MANYGROUPS = -55, // user has more than one aleagroup *** should be removed
        MDSAPI_NO_PASSWRD = -56, // user has no Aleapassword *** should be removed
        MDSAPI_NO_DESCRIPTION = -57, // user has no description
        MDSAPI_WINUSR_NOTEXIST = -58, // user has no winuser authentication 
        MDSAPI_WINGRP_NOTEXIST = -59, // user has no wingroup authentication
        MDSAPI_INVALID_CLIENT_LIC = -60, // invalid client license
        LSE_GENCONNECTERR = -61, // Error while connecting to Lasata Serducts.
        LSE_CONNECTIONFAILED_ABORT = -62, // Connection failed. Possibly aborted by the user.
        LSE_CONNECTIONFAILED_USERPASS = -63, // Connection failed. Are the user name and password correct?
        LSE_SERDUCTNA = -64, // Lasata Serduct is not available.
        LSE_NOTCONNECTED = -65, // You are not connected to Lasata Serducts.
        LSE_GENERALCOMERR = -66, // Internal COM error while communicating with Lasata software
        LSE_QUERYWINDOWFAILED = -67, // Query window returned an error.
        LSE_ERRINQUERYDEF = -68, // Error in query definition.
        LSE_QUERYEXECUTIONFAILED = -69, // Query Execution Failed.
        LSE_EOF = -70, // EOF - You are reading past a final record.
        LSE_IO_ERROR = MDSAPI_IO_ERROR, // Error in accessing a file.
        LSE_INCOMPATIBLEVERSION = -71, // Incompatible serduct version.
    }

    internal enum ExcelIntegrationErrorCodes
    {
        NO_ERROR = 0,
        MDSINTERR_OFFSET = 2200,
        MDSINTERR_SLICEFAILED = (MDSINTERR_OFFSET + 1),
        MDSINTERR_SLICECANCELD = (MDSINTERR_OFFSET + 2),
        SLICEERR_WRONG_EXCELID = (MDSINTERR_OFFSET + 3),
        SLICEERR_WRONG_VERSION = (MDSINTERR_OFFSET + 4),
        SLICEERR_LOADED_VIA_XL = (MDSINTERR_OFFSET + 5),
        SLICEERR_MORE_THAN_ONE_INSTANCE = (MDSINTERR_OFFSET + 6),
        SLICEERR_CHANGES_MODIFIED_AFTER_RESTART = (MDSINTERR_OFFSET + 7),
        SLICEERR_DATA_LOOP = (MDSINTERR_OFFSET + 8),
        SLICEERR_SHOW_PENDING_ERRMSG = (MDSINTERR_OFFSET + 9),
        SLICEERR_NOTEMARKERS_WILL_DEACTIVATED = (MDSINTERR_OFFSET + 10),
        SLICEERR_STRUCTURE_CHANGED = (MDSINTERR_OFFSET + 11),

        SLICEERR_VIEWEXIST_FOR_DIFF_CUBE = (MDSINTERR_OFFSET + 12),
        DIMENSION_EDIT_TO_MANY_ELEMS = (MDSINTERR_OFFSET + 13),
        SLICEERR_TABLESTRUCTURE_CHANGED = (MDSINTERR_OFFSET + 14),

        USER_ABORT_TRY_AGAIN = (MDSINTERR_OFFSET + 15),  // internal error, not intended to be visible by the user
        UNKNOWN_ODBC_TYPE = (MDSINTERR_OFFSET + 16),
        SLICEERR_NO_ELEMS_ON_ROWS_OR_COLUMNS = (MDSINTERR_OFFSET + 17),
        /* mds macro errors */
        MDSXLERR_OFFSET = 2400,
        MDSXLERR_SUBSETALREADYEXIST = (MDSXLERR_OFFSET + 1),
        MDSXLERR_INTERNAL_ERROR = (MDSXLERR_OFFSET + 2),
    }

    internal enum VBApiErrorCodes
    {
        MDSVBAERR_OK = 0,
        MDSVBAERR_OFFSET = 2300,
        MDSVBAERR_UNKNOWN_SERVER = (MDSVBAERR_OFFSET + 1),
        MDSVBAERR_UNKNOWN_TABLE = (MDSVBAERR_OFFSET + 2),
        MDSVBAERR_UNKNOWN_DIMENSION = (MDSVBAERR_OFFSET + 3),
        MDSVBAERR_UNKNOWN_SUBSET = (MDSVBAERR_OFFSET + 4),
        MDSVBAERR_UNKNOWN_DATAWORLD = (MDSVBAERR_OFFSET + 5),
        MDSVBAERR_TOO_MANY_SERVERS = (MDSVBAERR_OFFSET + 6),
        MDSVBAERR_WRONG_SPEC_ARGUMENT = (MDSVBAERR_OFFSET + 7),
        MDSVBAERR_NOT_ENOUGH_MEMORY = (MDSVBAERR_OFFSET + 8),
        MDSVBAERR_COULDNT_CREATE_ARRAY = (MDSVBAERR_OFFSET + 9),
        MDSVBAERR_COULDNT_REDIM_ARRAY = (MDSVBAERR_OFFSET + 10),
        MDSVBAERR_INVAILID_INDEX = (MDSVBAERR_OFFSET + 11),
        MDSVBAERR_COERCE_ERROR = (MDSVBAERR_OFFSET + 12),
        MDSVBAERR_ARRAY_NOT_INIT = (MDSVBAERR_OFFSET + 13),
        MDSVBAERR_WRONG_DIMCOUNT = (MDSVBAERR_OFFSET + 14),
        MDSVBAERR_INTERNAL_ERROR = (MDSVBAERR_OFFSET + 15),
        MDSVBAERR_OBJECT_ALREADY_EXISTS = (MDSVBAERR_OFFSET + 16),
        MDSVBAERR_DATAAREA_NOT_DEFINED = (MDSVBAERR_OFFSET + 17),
        MDSVBAERR_ADMINFEATURE_REQUIRED = (MDSVBAERR_OFFSET + 18),
        MDSVBAERR_MODELLINGFEATURE_REQUIRED = (MDSVBAERR_OFFSET + 19),
        MDSVBAERR_NOMDACS_AVAILABLE = (MDSVBAERR_OFFSET + 20),
        MDSVBAERR_VBA_INITIALIZED = (MDSVBAERR_OFFSET + 21),
        MDSVBAERR_VBA_NOT_INITIALIZED = (MDSVBAERR_OFFSET + 22),

        MDSVBAERR_DIM_NO_DESCRIPTION = (MDSVBAERR_OFFSET + 24),
    }
}