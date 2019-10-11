using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	public abstract class PackVectorConverter : JsonConverter
	{
		public override abstract bool CanConvert(Type objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			List<int> components = new List<int>(4);
			while(reader.ReadAsInt32() != null)
				components.Add((int)reader.Value);

			return ConvertComponents(components.ToArray());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartArray();

			foreach(int component in GetComponents(value))
				writer.WriteValue(component);

			writer.WriteEndArray();
		}

		public abstract int[] GetComponents (object value);
		public abstract object ConvertComponents(int[] components);
	}
}