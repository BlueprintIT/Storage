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
		protected AbstractFolder(IFolder parent): base(parent)
		{
		}

		/// <summary>
		/// <see cref="IFolder.Folders"></see>
		/// </summary>
		public abstract IEnumerator Folders
		{
			get;
		}

		/// <summary>
		/// <see cref="IFolder.Files"></see>
		/// </summary>
		public abstract IEnumerator Files
		{
			get;
		}

		/// <summary>
		/// <see cref="IFolder.this"></see>
		/// </summary>
		public abstract IEntry this[string name]
		{
			get;
		}

		/// <summary>
		/// <see cref="IFolder.Cache"></see>
		/// </summary>
		public void Cache()
		{
		}

		/// <summary>
		/// <see cref="IFolder.GetFile"></see>
		/// </summary>
		public abstract IFile GetFile(string name);

		/// <summary>
		/// <see cref="IFolder.GetFolder"></see>
		/// </summary>
		public abstract IFolder GetFolder(string name);
	}
}
