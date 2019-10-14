using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	// WIP, subject to change.
	public abstract class VectorConverter : JsonConverter
	{
		public override abstract bool CanConvert(Type objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// The list of components for the vector.
			List<float> components = new List<float>(4);

			while(reader.ReadAsDouble() != null)
				components.Add(System.Convert.ToSingle(reader.Value));

			// Pass these components off to get converted into the correct type (array to vector3 for example)
			return ConvertComponents(components.ToArray());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartArray();

			foreach(float component in GetComponents(value))
				writer.WriteValue(component);

			writer.WriteEndArray();
		}

		public abstract float[] GetComponents (object value);
		public abstract object ConvertComponents(float[] components);
	}
}