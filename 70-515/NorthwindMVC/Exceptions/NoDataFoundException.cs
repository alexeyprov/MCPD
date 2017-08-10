using System;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace NorthwindMVC.Exceptions
{
	/// <summary>
	/// The exception that is thrown when required data is not found.
	/// </summary>
	[Serializable]
	public class NoDataFoundException : Exception
	{
		#region Overrides

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
				throw new ArgumentNullException("info");

			base.GetObjectData(info, context);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NoDataFoundException class.
		/// </summary>
		public NoDataFoundException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the NoDataFoundException class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public NoDataFoundException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the NoDataFoundException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public NoDataFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the NoDataFoundException class with a specified serialization information and a streaming context.
		/// </summary>
		/// <param name="info">Stores all the data needed to serialize or desirialize an object.</param>
		/// <param name="context">Describes the source and destination of a given serialized stream, and provides an additional caller-defined context.</param>
		protected NoDataFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion
	}
}