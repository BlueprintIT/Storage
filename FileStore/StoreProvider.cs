/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;
using System.IO;
using BlueprintIT.Storage;

namespace BlueprintIT.Storage.File
{
	/// <summary>
	/// Summary description for StoreProvider.
	/// </summary>
	public class StoreProvider: IStoreProvider
	{
		public StoreProvider()
		{
		}

		public string DisplayName
		{
			get
			{
				return "Local and network drives";
			}
		}

		public IStore OpenStore(Uri uri)
		{
			if (uri.IsFile)
			{
				DirectoryInfo dir = new DirectoryInfo(uri.LocalPath);
				if (dir.Exists)
				{
				}
			}
			return null;
		}

		public string Protocol
		{
			get
			{
				return "file";
			}
		}
	}
}
