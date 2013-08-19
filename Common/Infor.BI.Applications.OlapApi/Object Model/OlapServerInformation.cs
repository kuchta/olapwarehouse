namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Defines information about an Olap server.
    /// </summary>
    public class OlapServerInformation
    {
        /// <summary>
        /// Holds the version of the server.
        /// </summary>
        private int _version;

        /// <summary>
        /// Holds the release number of the server.
        /// </summary>
        private int _release;

        /// <summary>
        /// Holds the sub release number of the server.
        /// </summary>
        private int _subRelease;

        /// <summary>
        /// Holds the build number.
        /// </summary>
        private int _build;

        /// <summary>
        /// Holds the number of currently active users on the server.
        /// </summary>
        private int _currentlyAttachedUsers;

        /// <summary>
        /// Holds the number of known users on the server.
        /// </summary>
        private int _userCount;

        /// <summary>
        /// Holds the number of known groups on the server.
        /// </summary>
        private int _groupCount;

        /// <summary>
        /// Holds the server mode.
        /// </summary>
        private OlapServerMode _serverMode;

        /// <summary>
        /// Initializes a new instance of the OlapServerInformation class.
        /// </summary>
        /// <param name="version">The server version.</param>
        /// <param name="release">The server release number.</param>
        /// <param name="subrelease">The server subrelease number.</param>
        /// <param name="build">The server build number.</param>
        /// <param name="currentlyAttachedUsers">The number of attached users.</param>
        /// <param name="userCount">The number of known users.</param>
        /// <param name="groupCount">The number of known groups.</param>
        /// <param name="mode">The server mode.</param>
        public OlapServerInformation(int version, int release, int subrelease, int build, int currentlyAttachedUsers, int userCount, int groupCount, OlapServerMode mode)
        {
            _version = version;
            _release = release;
            _subRelease = subrelease;
            _build = build;
            _currentlyAttachedUsers = currentlyAttachedUsers;
            _userCount = userCount;
            _groupCount = groupCount;
            _serverMode = mode;
        }

        /// <summary>
        /// Gets the version of the server.
        /// </summary>
        public int Version
        {
            get
            {
                return _version;
            }
        }

        /// <summary>
        /// Gets the release number.
        /// </summary>
        public int Release
        {
            get
            {
                return _release;
            }
        }

        /// <summary>
        /// Gets the subrelease number.
        /// </summary>
        public int SubRelease
        {
            get
            {
                return _subRelease;
            }
        }

        /// <summary>
        /// Gets the build number.
        /// </summary>
        public int Build
        {
            get
            {
                return _build;
            }
        }

        /// <summary>
        /// Gets the number of attached users.
        /// </summary>
        public int CurrentlyAttachedUsers
        {
            get
            {
                return _currentlyAttachedUsers;
            }
        }

        /// <summary>
        /// Gets the number of known users.
        /// </summary>
        public int UserCount
        {
            get
            {
                return _userCount;
            }
        }

        /// <summary>
        /// Gets the number of known groups.
        /// </summary>
        public int GroupCount
        {
            get
            {
                return _groupCount;
            }
        }

        /// <summary>
        /// Gets the server mode.
        /// </summary>
        public OlapServerMode ServerMode
        {
            get
            {
                return _serverMode;
            }
        }

        /// <summary>
        /// Creates the string that represents the data of this class.
        /// </summary>
        /// <returns>A string that represents the data of this class.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            result.Append("Version=");
            result.Append(_version);
            result.Append(", Release=");
            result.Append(_release);
            result.Append(", SubRelease=");
            result.Append(_subRelease);
            result.Append(", Build=");
            result.Append(_build);
            result.Append(", CurrentlyAttachedUsers=");
            result.Append(_currentlyAttachedUsers);
            result.Append(", UserCount=");
            result.Append(_userCount);
            result.Append(", GroupCount=");
            result.Append(_groupCount);
            result.Append(", Mode=");
            result.Append(_serverMode);

            return result.ToString();
        }
    }
}
