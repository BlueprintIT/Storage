/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;
using System.IO;
using System.Collections;

namespace BlueprintIT.Storage.File
{
	/// <summary>
	/// Summary description for Folder.
	/// </summary>
	public class Folder: AbstractFolder
	{
		private DirectoryInfo dir;
		private Store store;
		private IDictionary files = new Hashtable();
		private IDictionary folders = new Hashtable();

		public Folder(DirectoryInfo dir, Store store, Folder parent): base(parent)
		{
			this.dir=dir;
			this.store=store;
		}

		internal void Cache(IEntry entry)
		{
			if (entry is Folder)
			{
				folders[entry.Name.ToLower()]=entry;
			}
			else if (entry is File)
			{
				files[entry.Name.ToLower()]=entry;
			}
		}

		internal void Uncache(IEntry entry)
		{
			if (entry is Folder)
			{
				folders.Remove(entry.Name.ToLower());
			}
			else if (entry is File)
			{
				files.Remove(entry.Name.ToLower());
			}
		}

		public override IFile[] Files
		{
			get
			{
				ArrayList list = new ArrayList();
				foreach (FileInfo file in dir.GetFiles())
				{
					list.Add(GetFile(file.Name));
				}
				return (IFile[])list.ToArray(Type.GetType("BlueprintIT.Storage.File.File"));
			}
		}

		public override IFolder[] Folders
		{
			get
			{
				ArrayList list = new ArrayList();
				foreach (DirectoryInfo folder in dir.GetDirectories())
				{
					list.Add(GetFolder(folder.Name));
				}
				return (IFolder[])list.ToArray(Type.GetType("BlueprintIT.Storage.File.Folder"));
			}
		}

		public override string Name
		{
			get
			{
				dir.Refresh();
				return dir.Name;
			}

			set
			{
				((Folder)parent).Uncache(this);
				dir.MoveTo(dir.Parent.FullName+"\\"+value);
				((Folder)parent).Cache(this);
			}
		}

		public override bool Hidden
		{
			get
			{
				dir.Refresh();
				return (dir.Attributes & FileAttributes.Hidden)==FileAttributes.Hidden;
			}

			set
			{
				if (value!=Hidden)
				{
					dir.Attributes=dir.Attributes^FileAttributes.Hidden;
				}
			}
		}

		public override IEntry this[string name]
		{
			get
			{
				IEntry entry = GetFolder(name);
				if (entry.Exists)
				{
					return entry;
				}
				entry = GetFile(name);
				if (entry.Exists)
				{
					return entry;
				}
				return null;
			}
		}

		public override bool Exists
		{
			get
			{
				dir.Refresh();
				return dir.Exists;
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
				dir.MoveTo(targetpath);
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
				dir.MoveTo(path);
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
				dir.Create();
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
				dir.Delete();
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}

		public override IFile GetFile(string name)
		{
			if (files.Contains(name.ToLower()))
			{
				return (IFile)files[name.ToLower()];
			}
			else
			{
				FileInfo newfile = new FileInfo(dir.FullName+"\\"+name);
				File file = new File(newfile,store,this);
				files[name.ToLower()]=file;
				return file;
			}
		}

		public override IFolder GetFolder(string name)
		{
			if (folders.Contains(name.ToLower()))
			{
				return (IFolder)folders[name.ToLower()];
			}
			else
			{
				DirectoryInfo newdir = new DirectoryInfo(dir.FullName+"\\"+name);
				Folder folder = new Folder(newdir,store,this);
				folders[name.ToLower()]=folder;
				return folder;
			}
		}
	}
}
