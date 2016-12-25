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

		public void SetFloat (float value) {
			@float = value;
			type = AEScriptDataType.Float;
		}

		public void SetInt (int value) {
			@int = value;
			type = AEScriptDataType.Int;
		}

		public void SetBool (bool value) {
			@bool = value;
			type = AEScriptDataType.Bool;
		}

		public void SetString (string value) {
			@string = value;
			type = AEScriptDataType.String;
		}

		public void SetVector2 (Vector2 value) {
			@vector2 = value;
			type = AEScriptDataType.Vector2;
		}

		public void SetVector3 (Vector3 value) {
			@vector3 = value;
			type = AEScriptDataType.Vector3;
		}

		public void SetTransform (Transform value) {
			@transform = value;
			type = AEScriptDataType.Transform;
		}

		public void SetGameObject (GameObject value) {
			@gameobject = value;
			type = AEScriptDataType.GameObject;
		}

		public void SetAEScript (AEScriptRunner value) {
			@aescript = value;
			type = AEScriptDataType.AEScript;
		}
	}
}