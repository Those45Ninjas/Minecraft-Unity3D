using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace  Pack.JsonConverters
{
		public static class ConverterHelpers
		{
			public static List<float> ReadFloatArray(JsonReader reader)
			{
				// Create a list and fill it up with floats until we run out of floats to read.
				var list = new List<float>();

				var value = reader.ReadAsDouble();

				while (value != null)
				{
					list.Add(System.Convert.ToSingle(value));

					value = reader.ReadAsDouble();
				}
				return list;
			}

			public static List<int> ReadIntArray(JsonReader reader)
			{
				// Create a list and fill it up with ints until we run out of ints to read.
				var list = new List<int>();

				var value = reader.ReadAsInt32();

				while (value != null)
				{
					list.Add((int)value);

					value = reader.ReadAsInt32();
				}
				return list;
			}
		}
}