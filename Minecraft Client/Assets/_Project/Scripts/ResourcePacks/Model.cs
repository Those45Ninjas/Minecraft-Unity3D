using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
using System.Text;
using Newtonsoft.Json;

namespace Pack {

	// More information on models can be found at https://minecraft.gamepedia.com/Model

	public enum CullFace {
		down,
		up,
		north,
		south,
		west,
		east
	}
	
	[Serializable]
	public class Model
	{
		// The parent model to use a base.
		public string parent;
		public bool ambientocclusion;

		public ModelTextures textures;
		public ModelPositions display;
		public ModelElement[] elements;
	}

	[Serializable]
	public class ModelElement
	{
		[SerializeField]
		private Vector3 from;
		public Vector3 From
		{
			get => new Vector3
			(
				Mathf.Clamp(from[0], -16, 32),
				Mathf.Clamp(from[1], -16, 32),
				Mathf.Clamp(from[2], -16, 32)
			);
			set
			{
				from.x = Mathf.Clamp(value.x, -16, 32);
				from.y = Mathf.Clamp(value.y, -16, 32);
				from.z = Mathf.Clamp(value.z, -16, 32);
			}
		}
		
		[SerializeField]
		private Vector3 to;
		public Vector3 To
		{
			get => new Vector3
			(
				Mathf.Clamp(to[0], -16, 32),
				Mathf.Clamp(to[1], -16, 32),
				Mathf.Clamp(to[2], -16, 32)
			);
			set
			{
				to.x = Mathf.Clamp(value.x, -16, 32);
				to.y = Mathf.Clamp(value.y, -16, 32);
				to.z = Mathf.Clamp(value.z, -16, 32);
			}
		}
	}

	[Serializable]
	public class ElementRotation {
		[SerializeField]
		private int[] origin;
		public Vector3Int Origin {
			get => new Vector3Int(origin[0], origin[0], origin[0]);
			set {
				
			}
		}
	}

	[Serializable]
	public class ModelFace {
		[SerializeField]
		private int[] uv = new int[4];

		public Vector2Int UVMin {
			get => new Vector2Int(uv[0], uv[1]);
			set {
				uv[0] = value.x;
				uv[1] = value.y;
			}
		}
		public Vector2Int UVMax {
			get => new Vector2Int(uv[2], uv[3]);
			set {
				uv[2] = value.x;
				uv[3] = value.y;
			}
		}

		public string texture;
		public CullFace cullface;
	}
	
	[Serializable]
	public class ModelTextures
	{
			// According to wiki 'All breaking particles from non-model blocks are hard-coded.'
			public string texture;
			public string particle;
			public string down;
			public string up;
			public string north;
			public string east;
			public string south;
			public string west;
	}
	[Serializable]
	public class ModelPositions
	{
		[JsonProperty("thridperson_righthand")]
		public ModelTransform TpsRight = new ModelTransform();

		[JsonProperty("thridperson_lefthand")]
		public ModelTransform TpsLeft  = new ModelTransform();

		[JsonProperty("firstperson_righthand")]
		public ModelTransform FpsRight = new ModelTransform();

		[JsonProperty("firstperson_lefthand")]
		public ModelTransform FpsLeft = new ModelTransform();
		
		[JsonProperty("gui")]
		public ModelTransform GUI = new ModelTransform();

		[JsonProperty("head")]
		public ModelTransform Head = new ModelTransform();
		
		[JsonProperty("ground")]
		public ModelTransform Ground = new ModelTransform();

		[JsonProperty("fixed")]
		public ModelTransform Framed = new ModelTransform();
	}
	[Serializable]
	public class ModelTransform
	{
		[Newtonsoft.Json.JsonProperty("rotation")]
		public Vector3Int Rotation;

		[SerializeField]
		private Vector3 translation;
		public Vector3 Translation {
			// The wiki says the range is between -80 and 80.
			get => new Vector3(
				Mathf.Clamp(translation.x, -80f, 80f),
				Mathf.Clamp(translation.y, -80f, 80f),
				Mathf.Clamp(translation.z, -80f, 80f)
			);
			set
			{
				translation.x = Mathf.Clamp(value.x, -80f, 80f);
				translation.y = Mathf.Clamp(value.y, -80f, 80f);
				translation.z = Mathf.Clamp(value.z, -80f, 80f);
			}
		}

		// The scale of the model.
		[Newtonsoft.Json.JsonProperty("scale")]
		public Vector3 Scale = Vector3.one;
	}
}