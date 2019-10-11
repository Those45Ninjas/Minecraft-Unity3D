using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Converters;

public class Vector3IntJsonConverter<Vector3Int> : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return typeof(Vector3Int).IsAssignableFrom(objectType);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		Vector3Int vector = new Vector3Int();
		return vector;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{

	}
}