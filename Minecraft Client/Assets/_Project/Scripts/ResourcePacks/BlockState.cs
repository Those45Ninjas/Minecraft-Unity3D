using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Pack {

	public class BlockState {

		public Dictionary<string, ModelVariant> variants;

	}

	public class ModelVariant {

		public ModelVariant(string model) {
			this.model = model;
			x = 0;
			y = 0;

			weight = 1;
			uvlock = false;
		}

		// The name of the model.
		public string model;

		// Rotation of the model on the x and y axis.
		public int x = 0;
		public int y = 0;

		// How many fractions of a chance to spawn.
		public int weight = 1;

		// Lock the uv rotation.
		// Example wood stairs, the planks all face the same direction.
		public bool uvlock = false;
	}
}