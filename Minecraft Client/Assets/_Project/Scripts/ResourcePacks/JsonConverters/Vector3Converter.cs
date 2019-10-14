using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	public class Vector3Converter : VectorConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Vector3).IsAssignableFrom(objectType);
		}

		public override object ConvertComponents(float[] components)
		{
			return new Vector3(components[0], components[1], components[2]);
		}

		public override float[] GetComponents(object value)
		{
			Vector3 vector = (Vector3)value;
			return new float[] {
				vector.x,
				vector.y,
				vector.z
			};
		}
	}
}