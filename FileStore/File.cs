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
	/// Summary description for File.
	/// </summary>
	public class File: AbstractFile
	{
		private Store store;
		private FileInfo file;

		public File(FileInfo file, Store store, Folder parent): base(parent)
		{
			this.file=file;
			this.store=store;
		}

		public override DateTime Date
		{
			get
			{
				return file.LastWriteTime;
			}

			set
			{
				file.LastWriteTime=value;
			}
		}

		public override long Size
		{
			get
			{
				return file.Length;
			}
		}

		public override bool Exists
		{
			get
			{
				return file.Exists;
			}
		}

		public override string Name
		{
			get
			{
				return file.Name;
			}

			set
			{
				file.MoveTo(file.Directory.FullName+"\\"+value);
			}
		}

		public override bool Move(string path)
		{
			try
			{
				path=store.BaseDir.FullName+path;
				file.MoveTo(path);
				string[] paths = path.Split('/');
				string newpath="";
				for (int loop=0; loop<(paths.Length-1); loop++)
				{
					newpath+='/'+paths[loop];
				}
				parent = store.GetFolder(newpath);
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}

		public override bool Move(IFolder location)
		{
			string path = store.BaseDir.FullName+location.Path.Replace('/','\\')+"\\"+Name;
			try
			{
				file.MoveTo(path);
				parent=location;
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}

		public override bool Create()
		{
			try
			{
				file.Create().Close();
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}

		public override bool Delete()
		{
			try
			{
				file.Delete();
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}

		public override Stream Open()
		{
			return file.OpenRead();
		}

		public override Stream Append()
		{
			return file.Open(FileMode.Append);
		}

		public override Stream Overwrite()
		{
			return file.OpenWrite();
		}
	}
}
