using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	public class Vector2Converter : VectorConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Vector2).IsAssignableFrom(objectType);
		}

		public override object ConvertComponents(float[] components)
		{
			return new Vector2(components[0], components[1]);
		}

		public override float[] GetComponents(object value)
		{
			Vector2 vector = (Vector2)value;
			return new float[] {
				vector.x,
				vector.y,
			};
		}
	}
}