using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public interface IAEScriptContext {

		float GetFloat (string key);

		int GetInt (string key);

		bool GetBool (string key);

		string GetString (string key);

		Vector2 GetVector2 (string key);

		Vector3 GetVector3 (string key);

		Transform GetTransform (string key);

		GameObject GetGameObject (string key);

		AEScriptRunner GetAEScript (string key);
	}

	public sealed class AEScriptContext : IAEScriptContext {
		private AEScriptRunner runner_ = null;

		public AEScriptContext (AEScriptRunner runner) {
			runner_ = runner;
		}

		public float GetFloat (string key) {
			return FindData(key).@float;
		}

		public int GetInt (string key) {
			return FindData(key).@int;
		}

		public bool GetBool (string key) {
			return FindData(key).@bool;
		}

		public string GetString (string key) {
			return FindData(key).@string;
		}

		public Vector2 GetVector2 (string key) {
			return FindData(key).@vector2;
		}

		public Vector3 GetVector3 (string key) {
			return FindData(key).@vector3;
		}

		public Transform GetTransform (string key) {
			return FindData(key).@transform;
		}

		public GameObject GetGameObject (string key) {
			return FindData(key).@gameobject;
		}

		public AEScriptRunner GetAEScript (string key) {
			return FindData(key).@aescript;
		}

		private static AEScriptData defaultData_ = new AEScriptData();

		private AEScriptData FindData (string key) {
			var store = runner_.DataStore;
			for (var i = 0; i < store.Length; ++i) {
				var data = store[i];
				if (data.key == key)
					return data;
			}
			return defaultData_;
		}
	}
}
