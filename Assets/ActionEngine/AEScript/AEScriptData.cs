using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public enum AEScriptDataType {
		Float,
		Int,
		Bool,
		String,

		Vector2,
		Vector3,

		Transform,
		GameObject,

		AEScript
	}

	[Serializable]
	public sealed class AEScriptData {
		public string key;
		public AEScriptDataType type = AEScriptDataType.Float;

		public float @float;
		public int @int;
		public bool @bool;
		public string @string;

		public Vector2 @vector2;
		public Vector3 @vector3;

		public Transform @transform;
		public GameObject @gameobject;

		public AEScriptRunner @aescript;
	}
}
