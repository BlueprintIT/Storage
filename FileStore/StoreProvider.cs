/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;
using System.IO;
using System.Windows.Forms;
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

		public bool SupportsBrowse
		{
			get
			{
				return true;
			}
		}

		public Uri Browse(Uri uri)
		{
			FolderBrowserDialog browser = new FolderBrowserDialog();
			browser.Description="Select the files to synchronise with:";
			if ((uri!=null)&&(uri.IsFile))
			{
				browser.SelectedPath=uri.LocalPath;
			}
			if (browser.ShowDialog()==DialogResult.OK)
			{
				return new Uri("file://"+browser.SelectedPath.Replace('\\','/'));
			}
			return null;
		}

		public IStore OpenStore(Uri uri)
		{
			if (uri.IsFile)
			{
				DirectoryInfo dir = new DirectoryInfo(uri.LocalPath);
				if (dir.Exists)
				{
					return new Store(dir,uri);
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
