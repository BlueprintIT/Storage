/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;
using System.Collections;

namespace BlueprintIT.Storage
{
	/// <summary>
	/// Represents a folder in the store's filesystem.
	/// </summary>
	public interface IFolder: IEntry
	{
		/// <summary>
		/// Returns an enumeration of all the subfolders.
		/// </summary>
		IEnumerator Folders
		{
			get;
		}

		/// <summary>
		/// Returns an enumeration of all the files.
		/// </summary>
		IEnumerator Files
		{
			get;
		}

		/// <summary>
		/// Attempts to find an entry with a given name.
		/// </summary>
		IEntry this[string name]
		{
			get;
		}

		/// <summary>
		/// Returns a file entry with the given name. The file may not exist.
		/// </summary>
		/// <param name="name">The file's name.</param>
		/// <returns>The file.</returns>
		IFile GetFile(string name);

		/// <summary>
		/// Returns a folder with a given name. The folder may not exist.
		/// </summary>
		/// <param name="name">The folder's name.</param>
		/// <returns>The folder.</returns>
		IFolder GetFolder(string name);
	}
}
