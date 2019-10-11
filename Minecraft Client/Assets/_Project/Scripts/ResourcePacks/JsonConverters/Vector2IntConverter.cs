using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	public class Vector2IntConverter : PackVectorConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Vector2Int).IsAssignableFrom(objectType);
		}

		public override object ConvertComponents(int[] components)
		{
			return new Vector2Int(components[0], components[1]);
		}

		public override int[] GetComponents(object value)
		{
			Vector2Int vector = (Vector2Int)value;
			return new int[] {
				vector.x,
				vector.y,
			};
		}
	}
}