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
	/// An abstract implementation of a folder.
	/// </summary>
	public abstract class AbstractFolder: AbstractEntry, IFolder
	{
		private IDictionary realFolders = new Hashtable();
		private IDictionary realFiles = new Hashtable();
		private IDictionary fakeFolders = new Hashtable();
		private IDictionary fakeFiles = new Hashtable();

		protected AbstractFolder(IFolder parent): base(parent)
		{
		}

		/// <summary>
		/// <see cref="IFolder.Folders"></see>
		/// </summary>
		public IEnumerator Folders
		{
			get
			{
				return realFolders.Values.GetEnumerator();
			}
		}

		/// <summary>
		/// <see cref="IFolder.Files"></see>
		/// </summary>
		public IEnumerator Files
		{
			get
			{
				return realFiles.Values.GetEnumerator();
			}
		}

		/// <summary>
		/// <see cref="IFolder.this"></see>
		/// </summary>
		public IEntry this[string name]
		{
			get
			{
				if (realFolders.Contains(name))
				{
					return (IEntry)realFolders[name];
				}
				else if (realFiles.Contains(name))
				{
					return (IEntry)realFiles[name];
				}
				else if (fakeFolders.Contains(name))
				{
					return (IEntry)fakeFolders[name];
				}
				else if (fakeFiles.Contains(name))
				{
					return (IEntry)fakeFiles[name];
				}
				return null;
			}
		}

		/// <summary>
		/// Checks the caches for a file and returns one if found. Otherwise returns null.
		/// <see cref="IFolder.GetFile"></see>
		/// </summary>
		public IFile GetFile(string name)
		{
			if (realFiles.Contains(name))
			{
				return (IFile)realFiles[name];
			}
			else if (fakeFiles.Contains(name))
			{
				return (IFile)fakeFiles[name];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Checks the caches for a folder and returns one if found. Otherwise returns null.
		/// <see cref="IFolder.GetFolder"></see>
		/// </summary>
		public IFolder GetFolder(string name)
		{
			if (realFolders.Contains(name))
			{
				return (IFolder)realFolders[name];
			}
			else if (fakeFolders.Contains(name))
			{
				return (IFolder)fakeFolders[name];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Adds an entry to the cache.
		/// </summary>
		/// <param name="entry">The entry to be cached</param>
		protected void CacheEntry(IEntry entry, bool real)
		{
			if (entry is IFolder)
			{
				if (real)
				{
					realFolders[entry.Name]=entry;
					fakeFolders.Remove(entry);
				}
				else
				{
					fakeFolders[entry.Name]=entry;
					realFolders.Remove(entry);
				}
			}
			else if (entry is IFile)
			{
				if (real)
				{
					realFiles[entry.Name]=entry;
					fakeFiles.Remove(entry);
				}
				else
				{
					fakeFiles[entry.Name]=entry;
					realFiles.Remove(entry);
				}
			}
		}

		protected void UncacheEntry(IEntry entry)
		{
			if (entry is IFolder)
			{
				realFolders.Remove(entry.Name);
				fakeFolders.Remove(entry.Name);
			}
			else if (entry is IFile)
			{
				realFiles.Remove(entry.Name);
				fakeFiles.Remove(entry.Name);
			}
		}

		/// <summary>
		/// Closes this folder and all subfolders and files, closing any open streams.
		/// </summary>
		internal override void Close()
		{
			foreach (IFolder folder in realFolders.Values)
			{
				if (folder is AbstractEntry)
				{
					((AbstractEntry)folder).Close();
				}
			}
			realFolders.Clear();

			foreach (IFolder folder in fakeFolders.Values)
			{
				if (folder is AbstractEntry)
				{
					((AbstractEntry)folder).Close();
				}
			}
			fakeFolders.Clear();

			foreach (IFile file in realFiles.Values)
			{
				if (file is AbstractEntry)
				{
					((AbstractEntry)file).Close();
				}
			}
			realFiles.Clear();

			foreach (IFile file in fakeFiles.Values)
			{
				if (file is AbstractEntry)
				{
					((AbstractEntry)file).Close();
				}
			}
			fakeFiles.Clear();
		}
	}
}
