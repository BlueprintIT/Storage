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
	/// Summary description for AbstractEntry.
	/// </summary>
	public abstract class AbstractEntry: IEntry
	{
		private IFolder parent;

		/// <summary>
		/// Creates the entry, retaining a reference to the parent folder.
		/// </summary>
		/// <param name="parent">The parent folder of this entry. May be null for the root folder.</param>
		protected AbstractEntry(IFolder parent)
		{
			this.parent=parent;
		}

		/// <summary>
		/// <see cref="IEntry.Folder"></see>
		/// </summary>
		public IFolder Folder
		{
			get
			{
				return parent;
			}
		}

		/// <summary>
		/// <see cref="IEntry.Name"></see>
		/// </summary>
		public abstract string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Generates the path of this entry using the path of the parent folder and this entry's name.
		/// <see cref="IEntry.Path"></see>
		/// </summary>
		public string Path
		{
			get
			{
				if (parent!=null)
				{
					return parent.Path+"/"+Name;
				}
				else
				{
					return "/"+Name;
				}
			}
		}

		/// <summary>
		/// <see cref="IEntry.Exists"></see>
		/// </summary>
		public abstract bool Exists
		{
			get;
		}

		/// <summary>
		/// <see cref="IEntry.Create"></see>
		/// </summary>
		public abstract bool Create();

		/// <summary>
		/// <see cref="IEntry.Delete"></see>
		/// </summary>
		public abstract bool Delete();

		/// <summary>
		/// <see cref="IEntry.Move"></see>
		/// </summary>
		public abstract void Move(string path);

		/// <summary>
		/// Just updates the parent reference. Must be overriden.
		/// <see cref="IEntry.Move"></see>
		/// </summary>
		public void Move(IFolder folder)
		{
			parent=folder;
		}

		/// <summary>
		/// Closes this entry and any open streams to it.
		/// </summary>
		internal abstract void Close();
	}
}
