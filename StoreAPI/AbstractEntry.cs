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
		protected IFolder parent;

		/// <summary>
		/// Creates the entry, retaining a reference to the parent folder.
		/// </summary>
		/// <param name="parent">The parent folder of this entry. May be null for the root folder.</param>
		protected AbstractEntry(IFolder parent)
		{
			this.parent=parent;
		}

		/// <summary>
		/// <see cref="IEntry.Uri"></see>
		/// </summary>
		public abstract Uri Uri
		{
			get;
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
		/// <see cref="IEntry.Hidden"></see>
		/// </summary>
		public abstract bool Hidden
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
					string path = parent.Path;
					if (path!="/")
					{
						path=path+"/";
					}
					return path+Name;
				}
				else
				{
					return "/";
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
		public abstract bool Move(string path);

		/// <summary>
		/// <see cref="IEntry.Move"></see>
		/// </summary>
		public abstract bool Move(IFolder folder);
	}
}
