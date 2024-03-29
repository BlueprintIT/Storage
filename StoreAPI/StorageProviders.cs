/*
 * $HeadURL$
 * $LastChangedBy$
 * $Date$
 * $Revision$
 */

using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections;
using log4net;

namespace BlueprintIT.Storage
{
	/// <summary>
	/// Summary description for StorageProviders.
	/// </summary>
	public class StorageProviders
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static IDictionary providers = new Hashtable();

		/// <summary>
		/// Creates the StorageProviders class and loads filestores.
		/// </summary>
		static StorageProviders()
		{
			Assembly assembly = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Assembly;
			Uri uri = new Uri(assembly.EscapedCodeBase);
			if (uri.IsFile)
			{
				FileInfo file = new FileInfo(uri.LocalPath);
				ScanStores(file.Directory);
			}
		}

		public static IStore OpenStore(Uri uri)
		{
			IStoreProvider provider = (IStoreProvider)providers[uri.Scheme];
			if (provider!=null)
			{
				return provider.OpenStore(uri);
			}
			return null;
		}

		/// <summary>
		/// Gets an enumerator to all the storage providers.
		/// </summary>
		public static ICollection Providers
		{
			get
			{
				return providers.Values;
			}
		}

		/// <summary>
		/// Scans a directory for assemblies that contain storage providers.
		/// </summary>
		/// <param name="dir">The directory to scan.</param>
		public static void ScanStores(DirectoryInfo dir)
		{
			foreach (FileInfo file in dir.GetFiles("*.dll"))
			{
				if (file.Name.StartsWith("Store."))
				{
					LoadPotentialStore(file);
				}
			}
		}

		/// <summary>
		/// Loads a potential store provider from an assembly.
		/// </summary>
		/// <param name="file">The potential assembly.</param>
		public static void LoadPotentialStore(FileInfo file)
		{
			try
			{
				Assembly assembly = Assembly.LoadFrom(file.FullName);
				Stream stream = assembly.GetManifestResourceStream("store.xml");
				if (stream!=null)
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(stream);
					foreach(XmlElement element in doc.DocumentElement.GetElementsByTagName("provider"))
					{
						string provider = element.InnerText;
						Type type = assembly.GetType(provider);
						if (type!=null)
						{
							IStoreProvider store = (IStoreProvider)type.GetConstructor(Type.EmptyTypes).Invoke(null);
							providers[store.Protocol]=store;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
