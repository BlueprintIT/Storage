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

		public override Uri Uri
		{
			get
			{
				return new Uri("file:///"+file.FullName);
			}
		}

		public override bool Hidden
		{
			get
			{
				file.Refresh();
				return (file.Attributes & FileAttributes.Hidden)==FileAttributes.Hidden;
			}

			set
			{
				if (value!=Hidden)
				{
					file.Attributes=file.Attributes^FileAttributes.Hidden;
				}
			}
		}

		public override DateTime Date
		{
			get
			{
				file.Refresh();
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
				file.Refresh();
				return file.Length;
			}
		}

		public override bool Exists
		{
			get
			{
				file.Refresh();
				return file.Exists;
			}
		}

		public override string Name
		{
			get
			{
				file.Refresh();
				return file.Name;
			}

			set
			{
				((Folder)parent).Uncache(this);
				file.MoveTo(file.Directory.FullName+"\\"+value);
				((Folder)parent).Cache(this);
			}
		}

		public override bool Move(string path)
		{
			if (!path.StartsWith("/"))
			{
				return false;
			}
			try
			{
				((Folder)parent).Uncache(this);
				string targetpath=store.BaseDir.FullName+path.Replace('/','\\');
				file.MoveTo(targetpath);
				string[] paths = path.Split('/');
				string newpath="";
				for (int loop=1; loop<(paths.Length-1); loop++)
				{
					newpath+='/'+paths[loop];
				}
				if (newpath.Length==0)
				{
					newpath="/";
				}
				parent = store.GetFolder(newpath);
				((Folder)parent).Cache(this);
				return true;
			}
			catch (IOException)
			{
				((Folder)parent).Cache(this);
				return false;
			}
		}

		public override bool Move(IFolder location)
		{
			string path = store.BaseDir.FullName+location.Path.Replace('/','\\')+"\\"+Name;
			((Folder)parent).Uncache(this);
			try
			{
				file.MoveTo(path);
				parent=location;
				((Folder)parent).Cache(this);
				return true;
			}
			catch (IOException)
			{
				((Folder)parent).Cache(this);
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
			return file.Open(FileMode.Append,FileAccess.Write);
		}

		public override Stream Overwrite()
		{
			return file.OpenWrite();
		}
	}
}
