/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;

namespace BlueprintIT.Storage
{
	/// <summary>
	/// The store provider is the initial way to get to a filesystem.
	/// </summary>
	public interface IStoreProvider
	{
		/// <summary>
		/// Gets whether this store provider supports the browse method.
		/// </summary>
		bool SupportsBrowse
		{
			get;
		}

		/// <summary>
		/// Gets the protocol that this store can handle i.e. "ftp", "file" etc.
		/// </summary>
		string Protocol
		{
			get;
		}

		/// <summary>
		/// Gets a friendly name for the store provider.
		/// </summary>
		string DisplayName
		{
			get;
		}

		/// <summary>
		/// Browses for a store. Throws an exception if this provider does not support browsing.
		/// </summary>
		/// <param name="uri">The uri to begin browsing with.</param>
		/// <returns>The found store URI or null if the browse was canceled.</returns>
		Uri Browse(Uri uri);

		/// <summary>
		/// Opens the store using a given url.
		/// </summary>
		/// <param name="uri">The uri of the filesystem.</param>
		/// <returns>The opened store.</returns>
		IStore OpenStore(Uri uri);
	}
}
