using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
using Newtonsoft.Json;

namespace Pack
{
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

		public bool IsLoaded { get; private set; }

		private readonly ILogger logger;



		public ResourcePack(string packName, ILogger logger)
		{
			this.logger = logger;
			IsLoaded = false;
			packPath = Path.Combine(ResourcePackDirectory, packName);
			// TODO: Support multiple namespaces.
			assetPath = Path.Combine(packPath, "assets", "minecraft");
			fileEncoding = System.Text.Encoding.UTF8;

			// Load the meta file.
			loadMetaFile();
			Meta.Name = packName;

			// Get the icon.
			loadIcon();

			// Verify that the pack is using the same pack format supported in this project.
			if(Meta.Format != supportedPackFormat)
				throw new NotSupportedException("Resource pack format is not supported");
		}


		public IEnumerator LoadPack()
		{
			// TODO: Add a propper loading system.
			BlockStates = new Dictionary<string, BlockState>();
			loadAllStates();
			yield return new WaitForEndOfFrame();
			IsLoaded = true;
			logger.LogFormat(LogType.Log, "Loaded resource pack {0}, Description: {1}, Format: {2}", Meta.Name, Meta.Description, Meta.Format);
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


		private void loadAllModels()
		{
			var modelQueue =new Queue<string>();

			// Find each model.
			foreach (var blockState in BlockStates)
			{
				foreach (var variantList in blockState.Value.variants.Values)
				{
					foreach (var variant in variantList)
					{
						if(!modelQueue.Contains(variant.model))
							modelQueue.Enqueue(variant.model);
					}
				}
			}

			// Import models.
			string file = modelQueue.Dequeue();
			while (file != null)
			{
					logger.Log(LogType.Log, file);

					// Dequeue for the next iteration of the loop.
					file = modelQueue.Dequeue();
			}
		}

		private void loadAllStates()
		{
			// Get all json files in the blockstates directory.
			var jsonFiles = Directory.GetFiles(Path.Combine(assetPath, blockStateDirectory), "*.json", SearchOption.TopDirectoryOnly);

			// Parse each json file assumed as BlockState and add it to the dictionary of BlockStates.
			foreach(var jsonFile in jsonFiles)
			{
				logger.Log(LogType.Log, jsonFile);

				try
				{
					string json = File.ReadAllText(jsonFile, fileEncoding);
					string blockName = Path.GetFileNameWithoutExtension(jsonFile);
				
					var state = JsonConvert.DeserializeObject<BlockState>(json);

					if(state == null)
						return;
					
					BlockStates.Add(blockName, state);
					logger.LogFormat(LogType.Log, "Inserted {1} with {0} variants.", (state.variants != null)?state.variants.Count:0, blockName);
				}
				catch (Exception e)
				{
					Debug.LogException(e);
					logger.LogException(e);
				}

			}

		}

	}
}