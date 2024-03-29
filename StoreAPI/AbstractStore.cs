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
	/// An abstract implementation of a store.
	/// </summary>
	public abstract class AbstractStore: IStore
	{
		protected Uri uri;

		protected AbstractStore(Uri uri)
		{
			this.uri=uri;
		}

		/// <summary>
		/// <see cref="IStore.Root"></see>
		/// </summary>
		public abstract IFolder Root
		{
			get;
		}

		/// <summary>
		/// <see cref="IStore.Uri"></see>
		/// </summary>
		public virtual Uri Uri
		{
			get
			{
				return uri;
			}
		}

		/// <summary>
		/// Seeks through the folders for the given file.
		/// <see cref="IStore.GetFile"></see>
		/// </summary>
		public IFile GetFile(string path)
		{
			if (path.StartsWith("/"))
			{
				path=path.Substring(1);
			}
			string[] paths = path.Split('/');
			IFolder folder = Root;
			for (int loop=0; loop<(paths.Length-1); loop++)
			{
				folder=folder.GetFolder(paths[loop]);
			}
			return folder.GetFile(paths[paths.Length-1]);
		}

		/// <summary>
		/// Seeks through the folders for the given folder.
		/// <see cref="IStore.GetFolder"></see>
		/// </summary>
		public IFolder GetFolder(string path)
		{
			if (path.StartsWith("/"))
			{
				path=path.Substring(1);
			}
			string[] paths = path.Split('/');
			IFolder folder = Root;
			for (int loop=0; loop<paths.Length; loop++)
			{
				folder=folder.GetFolder(paths[loop]);
			}
			return folder;
		}

		/// <summary>
		/// <see cref="IStore.Close"></see>
		/// </summary>
		public abstract void Close();
	}
}
