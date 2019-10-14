using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
using Newtonsoft.Json;
using Pack.JsonConverters;

namespace Pack
{
	public class ResourcePack
	{

		public const String PackDir = "Resourcepacks";
		private const String stateDir = "blockstates";
		private const String modelDir = "models";
		private const int supportedPackFormat = 4;

		public PackMeta Meta;
		public Texture2D Icon;

		public Dictionary<string, Model> Models;
		public Dictionary<string, BlockState> BlockStates;

		private readonly string packPath;
		private readonly string assetPath;
		private readonly System.Text.Encoding fileEncoding;

		public JsonSerializerSettings SerializerSettings;

		public bool IsLoaded { get; private set; }

		private readonly ILogger logger;



		public ResourcePack(string packName, ILogger logger)
		{
			this.logger = logger;
			IsLoaded = false;
			packPath = Path.Combine(PackDir, packName);
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

			// Create the serializer settings.
			SerializerSettings = new JsonSerializerSettings
			{
				Converters = new List<JsonConverter>
				{
					new Vector3IntConverter(),
					new Vector3IntConverter(),
					new RectIntConverter(),
					new Vector3Converter(),
					new Vector2Converter()
				}
			};

		}


		public IEnumerator LoadPack()
		{
			// TODO: Add a propper loading system.

			// Prepare variables for loading.
			BlockStates = new Dictionary<string, BlockState>();
			Models = new Dictionary<string, Model>();

			// Get all json files in the blockstates directory.
			var jsonFiles = Directory.GetFiles(Path.Combine(assetPath, stateDir), "*.json", SearchOption.TopDirectoryOnly);

			int yieldCount = 0;

			// Load each blockstate.
			foreach(var jsonFile in jsonFiles)
			{
				try
				{
					string blockName = Path.GetFileNameWithoutExtension(jsonFile);
					LoadBlocksState(blockName);
				}
				catch (Exception e)
				{
					Debug.LogException(e);
					logger.LogException(e);
				}

				// Yield every 10 blocks.
				if(yieldCount++ > 10)
				{
					yieldCount = 0;
					yield return 0;
				}
			}

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

		public void LoadBlocksState(string blockstate)
		{
			// Don't process an existing blockstate.
			// TODO: Support reloading and/or over writing existing blockstates.
			if(BlockStates.ContainsKey(blockstate)) {
				logger.LogFormat(LogType.Warning, "{0} has already been loaded. Ignoring", blockstate);
				return;
			}

			// Load the blockstate and add it to the dictionary.
			string blockStateFile = Path.Combine(assetPath, stateDir, blockstate + ".json");
			string fileJson = File.ReadAllText(blockStateFile, fileEncoding);

			logger.Log(blockStateFile);

			BlockState block = JsonConvert.DeserializeObject<BlockState>(fileJson, SerializerSettings);
			BlockStates.Add(blockstate, block);

			// Load each model from the blockstate.
			if(block.variants == null || block.variants.Count == 0)
			{
				logger.LogWarning("No variants were found.", blockstate);
				return;
			}

			foreach(var variant in block.variants.Values)
			{
				if(variant.Count == 0)
					throw new ArgumentOutOfRangeException("Variant was empty");

				foreach (var modelVariant in variant)
					loadModel(modelVariant.model);
			}
		}

		private void loadModel(string modelName) 
		{
			// Make sure the model has not already been loaded.
			if(Models.ContainsKey(modelName))
			{
				// logger.LogFormat(LogType.Log, "Model {0} already exists, skipping", modelName);
				return;
			}

			// Deserialize a model object from it's model file.
			string modelPath = Path.Combine(assetPath, modelDir, modelName + ".json");
			string fileJson = File.ReadAllText(modelPath, fileEncoding);

			logger.LogFormat(LogType.Log, "Loading Model {0}", modelName);

			var model = JsonConvert.DeserializeObject<Model>(fileJson, SerializerSettings);

			// Add the model to the list of models.
			Models.Add(modelName, model);

			// TODO: Import all the textures used on this model.

			// Load the parent for this model if it exists.
			if(model.parent != null && !Models.ContainsKey(model.parent))
			{
				logger.Log(LogType.Log, "Loading parent Model");
				loadModel(model.parent);
			}
		}
	}
}