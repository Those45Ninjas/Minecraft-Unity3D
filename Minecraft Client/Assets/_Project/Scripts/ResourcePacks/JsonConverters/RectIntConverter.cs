using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Pack.JsonConverters
{
	public class RectIntConverter : JsonConverter<RectInt>
	{
		public override RectInt ReadJson(JsonReader reader, Type objectType, RectInt existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			// Read a list from the json.
			var list = ConverterHelpers.ReadIntArray(reader);

			// Check to see if there is enough components to read a vector2int.
			if(list.Count < 4)
				throw new JsonReaderException("Not enough components to create a RectInt");

			return new RectInt ()
			{
				xMin = list[0],
				yMin = list[1],
				xMax = list[2],
				yMax = list[3]
			};
		}

		public override void WriteJson(JsonWriter writer, RectInt value, JsonSerializer serializer)
		{
			// Write the vector.
			writer.WriteStartArray();
			writer.WriteValue(value.xMin);
			writer.WriteValue(value.yMin);
			writer.WriteValue(value.xMax);
			writer.WriteValue(value.yMax);
			writer.WriteEndArray();
		}
	}
}