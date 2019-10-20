using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Pack.JsonConverters
{
	public class Vector3IntConverter : JsonConverter<Vector3Int>
	{
		public override Vector3Int ReadJson(JsonReader reader, Type objectType, Vector3Int existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			// Read a list from the json.
			var list = ConverterHelpers.ReadIntArray(reader);

			// Check to see if there is enough components to read a vector2int.
			if(list.Count < 3)
				throw new JsonReaderException("Not enough components to create a Vector3Int");

			return new Vector3Int(list[0], list[1], list[2]);
		}

		public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
		{
			// Write the vector.
			writer.WriteStartArray();
			writer.WriteValue(value.x);
			writer.WriteValue(value.y);
			writer.WriteValue(value.z);
			writer.WriteEndArray();
		}
	}
}