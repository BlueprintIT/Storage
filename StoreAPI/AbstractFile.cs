/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;
using System.IO;

namespace BlueprintIT.Storage
{
	/// <summary>
	/// An abstract implementation of a file.
	/// </summary>
	public abstract class AbstractFile: AbstractEntry, IFile
	{
		protected AbstractFile(IFolder parent): base(parent)
		{
		}

		/// <summary>
		/// <see cref="IFile.Date"></see>
		/// </summary>
		public abstract DateTime Date
		{
			get;
			set;
		}

		/// <summary>
		/// <see cref="IFile.Size"></see>
		/// </summary>
		public abstract long Size
		{
			get;
		}

		/// <summary>
		/// <see cref="IFile.Open"></see>
		/// </summary>
		public abstract Stream Open();

		/// <summary>
		/// <see cref="IFile.Append"></see>
		/// </summary>
		public abstract Stream Append();

		/// <summary>
		/// <see cref="IFile.Overwrite"></see>
		/// </summary>
		public abstract Stream Overwrite();

		/// <summary>
		/// Simply wraps the Stream returned from Open() with a StreamReader.
		/// <see cref="IFile.OpenText"></see>
		/// </summary>
		public TextReader OpenText()
		{
			return new StreamReader(Open());
		}

		/// <summary>
		/// Simply wraps the Stream returned from Append() with a StreamWriter.
		/// <see cref="IFile.AppendText"></see>
		/// </summary>
		public TextWriter AppendText()
		{
			return new StreamWriter(Append());
		}

		/// <summary>
		/// Simply wraps the Stream returned from Overwrite() with a StreamWriter.
		/// <see cref="IFile.OverwriteText"></see>
		/// </summary>
		public TextWriter OverwriteText()
		{
			return new StreamWriter(Overwrite());
		}
	}
}
