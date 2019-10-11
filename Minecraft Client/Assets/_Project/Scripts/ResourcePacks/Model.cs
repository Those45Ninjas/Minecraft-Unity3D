using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
using System.Text;

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
		public ModelElement elements;
	}

	[Serializable]
	public class ModelElement
	{
		[SerializeField]
		private int[] from = new int[3];
		public Vector3Int From {
			get => new Vector3Int (
				Mathf.Clamp(from[0], -16, 32),
				Mathf.Clamp(from[1], -16, 32),
				Mathf.Clamp(from[2], -16, 32)
			);
			set {
				from[0] = value.x;
				from[1] = value.y;
				from[2] = value.z;
			}
		}
		[SerializeField]
		private int[] to = new int[3];
		public Vector3Int To {
			get => new Vector3Int (
				Mathf.Clamp(to[0], -16, 32),
				Mathf.Clamp(to[1], -16, 32),
				Mathf.Clamp(to[2], -16, 32)
			);
			set {
				to[0] = value.x;
				to[1] = value.y;
				to[2] = value.z;
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
		[Newtonsoft.Json.JsonProperty("thridperson_righthand")]
		public ModelTransform TpsRight = new ModelTransform();

		[Newtonsoft.Json.JsonProperty("thridperson_lefthand")]
		public ModelTransform TpsLeft  = new ModelTransform();

		[Newtonsoft.Json.JsonProperty("firstperson_righthand")]
		public ModelTransform FpsRight = new ModelTransform();

		[Newtonsoft.Json.JsonProperty("firstperson_lefthand")]
		public ModelTransform FpsLeft = new ModelTransform();
		
		[Newtonsoft.Json.JsonProperty("gui")]
		public ModelTransform GUI = new ModelTransform();

		[Newtonsoft.Json.JsonProperty("head")]
		public ModelTransform Head = new ModelTransform();
		
		[Newtonsoft.Json.JsonProperty("ground")]
		public ModelTransform Ground = new ModelTransform();

		[Newtonsoft.Json.JsonProperty("fixed")]
		public ModelTransform Framed = new ModelTransform();
	}
	[Serializable]
	public class ModelTransform {

		// The rotation of the model.
		[SerializeField]
		private float[] rotation = new float[3];

		public Vector3 Rotation {
			get => new Vector3(rotation[0], rotation[1],	rotation[2]);
			set {
				rotation[0] = value.x;
				rotation[1] = value.y;
				rotation[2] = value.z;
			}
		}

		// The translation of the model.
		[SerializeField]
		private float[] translation = new float[3];

		public Vector3 Translation {

			// The wiki says the range is between -80 and 80.
			get => new Vector3(
				Mathf.Clamp(translation[0], -80f, 80f),
				Mathf.Clamp(translation[1], -80f, 80f),
				Mathf.Clamp(translation[2], -80f, 80f)
			);
			set {
				translation[0] = value.x;
				translation[1] = value.y;
				translation[2] = value.z;
			}
		}

		// The scale of the model.
		[SerializeField]
		private float[] scale = new float[3];

		public Vector3 Scale {
			get => new Vector3(scale[0], scale[1],	scale[2]);
			set {
				scale[0] = value.x;
				scale[1] = value.y;
				scale[2] = value.z;
			}
		}
	}
}