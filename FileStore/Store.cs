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
	/// Summary description for Store.
	/// </summary>
	public class Store: AbstractStore
	{
		private Folder root;
		private DirectoryInfo basedir;

		public Store(DirectoryInfo basedir, Uri uri): base(uri)
		{
			this.basedir=basedir;
			this.root=new Folder(basedir,this,null);
		}

		public DirectoryInfo BaseDir
		{
			get
			{
				return basedir;
			}
		}

		public override Uri Uri
		{
			get
			{
				return new Uri("file:///"+basedir.FullName.Replace('\\','/'));
			}
		}

		public override IFolder Root
		{
			get
			{
				return root;
			}
		}

		public override void Close()
		{
		}
	}
}
