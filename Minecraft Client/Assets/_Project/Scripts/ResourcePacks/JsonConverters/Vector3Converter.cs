using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Pack.JsonConverters
{
	public class Vector3Converter : JsonConverter<Vector3>
	{
		public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			// Read a list from the json.
			var list = ConverterHelpers.ReadFloatArray(reader);

			// Check to see if there is enough components to read a vector2int.
			if(list.Count < 3)
				throw new JsonReaderException("Not enough components to create a Vector3");

			return new Vector3(list[0], list[1], list[2]);
		}

		public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
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