using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
using System.Text;

namespace Pack {
	public class ResourcePack
	{
			public const String ResourcePackDirectory = "Resourcepacks";
			public const int SupportedPackFormat = 4;

			public PackMeta Meta;
			public Texture2D Icon;

			public Dictionary<string, Model> Models;

			private string packPath;

			public void LoadFromPack(string packName) {
				packPath = Path.Combine(ResourcePackDirectory, packName);

				// Load the pack.mcmeta file.
				{
					string packMetaFile = Path.Combine(ResourcePackDirectory, packName, "pack.mcmeta");

					if(!File.Exists(packMetaFile))
						throw new FileNotFoundException("Resource Pack does not contain a pack.mcmeta");

					string jsonString = File.ReadAllText(packMetaFile, Encoding.UTF8);
					Meta = JsonUtility.FromJson<PackMeta>(jsonString);

					Debug.Log(Meta);
				}

				// Verify that the pack is using the same pack format.
				if(Meta.Format != SupportedPackFormat)
					throw new NotSupportedException("Resource pack format is not supported");

				// Load the pack.png icon if it exists.
				{
					string packIconFile = Path.Combine(packPath, "pack.png");
					
					if(File.Exists(packIconFile))
					{
						// Create a new texture and load the png into it.
						Icon = new Texture2D(128,128);
						byte[] iconBytes = File.ReadAllBytes(packIconFile);
						ImageConversion.LoadImage(Icon,iconBytes);
					}
				}

				Debug.LogFormat("Loaded resource pack {0}, Description: {1}, Format: {2}", packName, Meta.Description, Meta.Format);
			}
			public void LoadModels() {
				
			}
	}
}