using System;

namespace BlueprintIT.Storage
{
	/// <summary>
	/// An IStore represents a connection to a filestore.
	/// </summary>
	public interface IStore
	{
		/// <summary>
		/// Gets the root folder of this store.
		/// </summary>
		IFolder Root
		{
			get;
		}

		/// <summary>
		/// Closes this store. After calling this all entries for this store may become unusable.
		/// </summary>
		void Close();
	}
}
