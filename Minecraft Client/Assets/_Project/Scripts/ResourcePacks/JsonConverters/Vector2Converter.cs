using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Pack.JsonConverters
{
	public class Vector2Converter : JsonConverter<Vector2>
	{
		public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			// Read a list from the json.
			var list = ConverterHelpers.ReadFloatArray(reader);

			// Check to see if there is enough components to read a vector2int.

			if(list.Count < 2)
				throw new JsonReaderException("Not enough components to create a Vector2");

			return new Vector2(list[0], list[1]);
		}

		public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
		{
			// Write the vector.
			writer.WriteStartArray();
			writer.WriteValue(value.x);
			writer.WriteValue(value.y);
			writer.WriteEndArray();
		}
	}
}