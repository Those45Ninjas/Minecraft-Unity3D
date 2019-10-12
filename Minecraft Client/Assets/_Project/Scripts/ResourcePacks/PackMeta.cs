using UnityEngine;
using System.IO;
using System.Text;

namespace Pack {
	[System.Serializable]
	public class PackMeta
	{
		
		[System.Serializable]
		private class Pack {
			public string description;
			public int pack_format;
		}

		[SerializeField]
		private Pack pack;

		// The description of the resource back. This can also be a 'Raw JSON text'.
		public string Description { get { return pack.description; } }

		// The pack format is used to check compatability.
		// For minecraft 1.13 and 1.14 a value of 4 is used.
		public int Format { get { return pack.pack_format; } }

		// The name of the pack. (set by the pack manager and has nothing to do with minecraft).
		[System.NonSerialized]
		public string Name;

		public override string ToString() {
			return string.Format("Description: {1}, Format: {0}", Format, Description);
		}
	}
}