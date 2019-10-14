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

	public Logger logger;
	private FileLogger fileLogger;

	void Start()
	{
		fileLogger = new FileLogger("packs");
		logger = new Logger(fileLogger);

		ActiveResourcePack = new ResourcePack(ResourcePackToLoad,logger);

		StartCoroutine(ActiveResourcePack.LoadPack());
	}

	void FixedUpdate()
	{
		fileLogger.Flush();
	}
}
