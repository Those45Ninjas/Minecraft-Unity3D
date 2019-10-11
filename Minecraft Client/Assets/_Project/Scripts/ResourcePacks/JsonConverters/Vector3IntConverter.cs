using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	public class Vector3IntConverter : PackVectorConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Vector3Int).IsAssignableFrom(objectType);
		}

		public override object ConvertComponents(int[] components)
		{
			return new Vector3Int(components[0], components[1], components[2]);
		}

		public override int[] GetComponents(object value)
		{
			Vector3Int vector = (Vector3Int)value;
			return new int[] {
				vector.x,
				vector.y,
				vector.z
			};
		}
	}
}