using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
using Newtonsoft.Json;

namespace Pack {
	public class ResourcePack
	{
		public const String ResourcePackDirectory = "Resourcepacks";
		private const String blockStateDirectory = "blockstates";
		private const int supportedPackFormat = 4;

		public PackMeta Meta;
		public Texture2D Icon;

		public Dictionary<string, Model> Models;
		public Dictionary<string, BlockState> BlockStates;

		private readonly string packPath;
		private readonly string assetPath;
		private readonly System.Text.Encoding fileEncoding;

		public ResourcePack(string packName)
		{
			packPath = Path.Combine(ResourcePackDirectory, packName);
			// TODO: Support multiple namespaces.
			assetPath = Path.Combine(packPath, "assets", "minecraft");
			fileEncoding = System.Text.Encoding.UTF8;

			loadMetaFile();
			loadIcon();

			// Verify that the pack is using the same pack format.
			if(Meta.Format != supportedPackFormat)
				throw new NotSupportedException("Resource pack format is not supported");

			// Load all the block states.
			BlockStates = new Dictionary<string, BlockState>();
			loadAllStates();

			Debug.LogFormat("Loaded resource pack {0}, Description: {1}, Format: {2}", packName, Meta.Description, Meta.Format);
		}
		private void loadMetaFile()
		{
			string packMetaFile = Path.Combine(packPath, "pack.mcmeta");

			if(!File.Exists(packMetaFile))
				throw new FileNotFoundException("Resource Pack does not contain a pack.mcmeta");

			string jsonString = File.ReadAllText(packMetaFile, fileEncoding);
			Meta = JsonUtility.FromJson<PackMeta>(jsonString);
		}
		private void loadIcon()
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

		private void loadAllStates() {
			var jsonFiles = Directory.GetFiles(Path.Combine(assetPath, blockStateDirectory), "*.json", SearchOption.TopDirectoryOnly);
			foreach(var jsonFile in jsonFiles) {
				Debug.Log(jsonFile);
				string json = File.ReadAllText(jsonFile, fileEncoding);
				var state = JsonConvert.DeserializeObject<BlockState>(json);

				string blockID = Path.GetFileNameWithoutExtension(jsonFile);

				if(state != null) {
					BlockStates.Add(blockID, state);
					// Debug.LogFormat("Added {0} with {1} variants from {2}", blockID, state.variants, jsonFile);
				}
			}
		}
	}
}