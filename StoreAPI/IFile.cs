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
	/// Summary description for IFile.
	/// </summary>
	public interface IFile: IEntry
	{
		/// <summary>
		/// The timestamp of this file. Usually a last modified time.
		/// Some storage providers will allow setting the time. Those that don't will
		/// not throw an error, so check the new time after setting it.
		/// </summary>
		DateTime Date
		{
			get;
			set;
		}

		/// <summary>
		/// The size of this file in bytes.
		/// </summary>
		int Size
		{
			get;
		}

		/// <summary>
		/// Opens a stream to read from the file in binary.
		/// </summary>
		/// <returns>An open binary stream.</returns>
		Stream Open();

		/// <summary>
		/// Opens a stream to append to the file in binary.
		/// </summary>
		/// <returns>An open binary stream.</returns>
		Stream Append();

		/// <summary>
		/// Opens a stream to overwrite the file in binary.
		/// </summary>
		/// <returns>An open binary stream.</returns>
		Stream Overwrite();

		/// <summary>
		/// Opens a stream to read from the file in text.
		/// </summary>
		/// <returns>An open text reader.</returns>
		TextReader OpenText();

		/// <summary>
		/// Opens a stream to append to the file in text.
		/// </summary>
		/// <returns>An open binary stream.</returns>
		TextWriter AppendText();

		/// <summary>
		/// Opens a stream to overwrite the file in text.
		/// </summary>
		/// <returns>An open binary stream.</returns>
		TextWriter OverwriteText();
	}
}
