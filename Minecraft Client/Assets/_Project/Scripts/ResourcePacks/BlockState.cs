using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Pack.JsonConverters;

namespace Pack {

	public class BlockState {
		[JsonConverter(typeof(BlockVariantConverter))]
		public Dictionary<string, List<ModelVariant>> variants;
	}
}