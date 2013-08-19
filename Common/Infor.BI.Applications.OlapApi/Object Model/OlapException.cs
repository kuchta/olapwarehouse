using System.Runtime.Serialization;

namespace Infor.BI.Applications.OlapApi
{
    /// <summary>
    /// Implements the base class for all exceptions related to the Olap functionality
    /// included with the OlapApi project.
    /// </summary>
    [System.Serializable]
    public class OlapException : System.Exception
    {
        private int _aleaErrorCode;

        /// <summary>
        /// Initializes a new instance of the OlapExceptionClass with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        protected OlapException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _aleaErrorCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of the OlapException class.
        /// </summary>
        public OlapException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the OlapException class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public OlapException(string message)
            : base(message)
        {
            _aleaErrorCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of the OlapException class with a specified error message
        /// and an Olap specific error code.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="aleaErrorCode">An error code delivered by the Olap function which caused the error.</param>
        public OlapException(string message, int aleaErrorCode)
            : base(message)
        {
            _aleaErrorCode = aleaErrorCode;
        }

        /// <summary>
        /// Initializes a new instance of the OlapException class with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception. 
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference (Nothing in Visual Basic), 
        /// the current exception is raised in a catch block that handles the inner exception.</param>
        public OlapException(string message, System.Exception innerException)
            : base(message, innerException)
        {
            _aleaErrorCode = 0;
        }

        /// <summary>
        /// When overridden in a derived class, sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public string GetMessage()
        {
            return Message;
        }

        public int GetOlapErrorCode()
        {
            return _aleaErrorCode;
        }
    }
}
