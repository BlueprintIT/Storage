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
	/// Represents a filesystem entry, either a file or folder.
	/// </summary>
	public interface IEntry
	{
		/// <summary>
		/// Gets or sets the name of this entry.
		/// </summary>
		string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the full path of this entry relative to the store root.
		/// </summary>
		string Path
		{
			get;
		}

		/// <summary>
		/// Gets the folder that this entry is held in.
		/// </summary>
		/// <remarks>
		/// This will return null for the root folder.
		/// </remarks>
		IFolder Folder
		{
			get;
		}

		/// <summary>
		/// Checks whether this entry currently exists.
		/// </summary>
		bool Exists
		{
			get;
		}

		/// <summary>
		/// Deletes this entry if it exists.
		/// </summary>
		/// <returns>True if the entry was deleted, false otherwise.</returns>
		bool Delete();

		/// <summary>
		/// Creates this entry if it doesn't already exists.
		/// </summary>
		/// <returns>True if the entry exists, false otherwise.</returns>
		bool Create();

		/// <summary>
		/// Moves the entry to a new path. Can just be used to rename the entry
		/// though setting the name is better for that.
		/// </summary>
		/// <param name="path">The new path for the entry.</param>
		bool Move(string path);

		/// <summary>
		/// Moves this entry to a given folder.
		/// </summary>
		/// <param name="folder">The folder to move to.</param>
		bool Move(IFolder folder);
	}
}
