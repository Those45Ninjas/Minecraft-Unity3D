using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace  Pack.JsonConverters
{
	public class DictionaryConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Dictionary<string, List<ModelVariant>>);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// Do some checks to see if we are reading something that resembles a block variant.
			if (reader.TokenType == JsonToken.Null)
			{
				Debug.LogError("Variant is null");
				return null;
			}
			if(reader.TokenType != JsonToken.StartObject)
				throw new JsonReaderException("Unexpected token while reading block variants");

			// Create our dictionary that stores all our variants.
			var dictionary = new Dictionary<string, List<ModelVariant>>();

			// Read each variant.
			while (reader.Read())
			{
				// Exit out if we have finished.
				if(reader.TokenType == JsonToken.EndObject)
					break;

				// Read the variantKey.
				string variantKey = reader.Value as string;
				reader.Read();

				// Read a single variant.
				if(reader.TokenType == JsonToken.StartObject)
				{
					ModelVariant variant = serializer.Deserialize<ModelVariant>(reader);
					dictionary.Add(variantKey, new List<ModelVariant>() { variant });
				}
				// Read multiple variants.
				else if(reader.TokenType == JsonToken.StartArray)
				{
					List<ModelVariant> list = serializer.Deserialize<List<ModelVariant>>(reader);
					dictionary.Add(variantKey, list);
				}
				else
					throw new JsonReaderException("Unexpected token while reading a block variant");
			}
			return dictionary;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{

		}
	}
}