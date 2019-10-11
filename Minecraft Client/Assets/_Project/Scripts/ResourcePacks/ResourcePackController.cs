using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pack;
using Pack.JsonConverters;
using Newtonsoft.Json;

public class ResourcePackController : MonoBehaviour
{
	public string ResourcePackToLoad = "default";

	public ResourcePack ActiveResourcePack;

	void Start()
	{
		JsonConvert.DefaultSettings = () => new JsonSerializerSettings
		{
			Converters = new List<JsonConverter>
			{
				new Vector3IntConverter(),
				new Vector3IntConverter(),
				new RectIntConverter()
			}
		};

		ActiveResourcePack = new ResourcePack(ResourcePackToLoad);

		// Pack.BlockState blockState = ActiveResourcePack.GetBlock("birch_wood");
	}
}
