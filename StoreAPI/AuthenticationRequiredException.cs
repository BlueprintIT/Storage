using System;

namespace BlueprintIT.Storage
{
	/// <summary>
	/// Thrown when attempting to connect to a secured file store with invalid or no credentials.
	/// </summary>
	public class AuthenticationRequiredException: Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public AuthenticationRequiredException(): base()
		{
		}

		/// <summary>
		/// Creates the exception with a defined error message.
		/// </summary>
		/// <param name="message">The error message.</param>
		public AuthenticationRequiredException(string message): base(message)
		{
		}
	}
}
