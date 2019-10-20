using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Pack.JsonConverters
{
	public class Vector2IntConverter : JsonConverter<Vector2Int>
	{
		public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			// Read a list from the json.
			var list = ConverterHelpers.ReadIntArray(reader);

			// Check to see if there is enough components to read a vector2int.
			if(list.Count < 2)
				throw new JsonReaderException("Not enough components to create a Vector2Int");

			return new Vector2Int(list[0], list[1]);
		}

		public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
		{
			// Write the vector.
			writer.WriteStartArray();
			writer.WriteValue(value.x);
			writer.WriteValue(value.y);
			writer.WriteEndArray();
		}
	}
}