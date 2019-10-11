using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Pack.JsonConverters
{
	public class RectIntConverter : PackVectorConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(RectInt).IsAssignableFrom(objectType);
		}

		public override object ConvertComponents(int[] components)
		{
			return new RectInt()
			{
				xMin = components[0],
				yMin = components[1],
				xMax = components[2],
				yMax = components[3]
			};
		}

		public override int[] GetComponents(object value)
		{
			RectInt rect = (RectInt)value;
			return new int[] {
				rect.xMin,
				rect.yMin,
				rect.xMax,
				rect.yMax
			};
		}
	}
}