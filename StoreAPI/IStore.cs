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
	/// An IStore represents a connection to a filestore.
	/// </summary>
	public interface IStore
	{
		/// <summary>
		/// Gets the uri used to create this store.
		/// </summary>
		Uri Uri
		{
			get;
		}

		/// <summary>
		/// Gets the root folder of this store.
		/// </summary>
		IFolder Root
		{
			get;
		}

		/// <summary>
		/// Returns a file entry with the given path. The file may not exist.
		/// </summary>
		/// <param name="name">The file's path.</param>
		/// <returns>The file.</returns>
		IFile GetFile(string path);

		/// <summary>
		/// Returns a folder with a given path. The folder may not exist.
		/// </summary>
		/// <param name="name">The folder's path.</param>
		/// <returns>The folder.</returns>
		IFolder GetFolder(string path);

		/// <summary>
		/// Closes this store. After calling this all entries for this store may become unusable.
		/// </summary>
		void Close();
	}
}
