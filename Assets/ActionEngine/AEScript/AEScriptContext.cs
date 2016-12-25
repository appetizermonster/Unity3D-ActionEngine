using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {

	public interface IAEScriptContext {

		float GetFloat (string key, float defValue = 0f);

		int GetInt (string key, int defValue = 0);

		bool GetBool (string key, bool defValue = false);

		string GetString (string key, string defValue = null);

		Vector2 GetVector2 (string key, Vector2 defValue = default(Vector2));

		Vector3 GetVector3 (string key, Vector3 defValue = default(Vector3));

		Transform GetTransform (string key, Transform defValue = null);

		GameObject GetGameObject (string key, GameObject defValue = null);

		AEScriptRunner GetAEScript (string key, AEScriptRunner defValue = null);
	}

	public sealed class AEScriptContext : IAEScriptContext {
		private readonly AEScriptRunner runner_ = null;
		private readonly Dictionary<string, object> overrideData_ = null;

		public AEScriptContext (AEScriptRunner runner, Dictionary<string, object> overrideData = null) {
			runner_ = runner;
			overrideData_ = overrideData;
		}

		public float GetFloat (string key, float defValue = 0f) {
			return FindData(key, (x) => x.@float, defValue);
		}

		public int GetInt (string key, int defValue = 0) {
			return FindData(key, (x) => x.@int, defValue);
		}

		public bool GetBool (string key, bool defValue = false) {
			return FindData(key, (x) => x.@bool, defValue);
		}

		public string GetString (string key, string defValue = null) {
			return FindData(key, (x) => x.@string, defValue);
		}

		public Vector2 GetVector2 (string key, Vector2 defValue = default(Vector2)) {
			return FindData(key, (x) => x.@vector2, defValue);
		}

		public Vector3 GetVector3 (string key, Vector3 defValue = default(Vector3)) {
			return FindData(key, (x) => x.vector3, defValue);
		}

		public Transform GetTransform (string key, Transform defValue = null) {
			return FindData(key, (x) => x.@transform, defValue);
		}

		public GameObject GetGameObject (string key, GameObject defValue = null) {
			return FindData(key, (x) => x.@gameobject, defValue);
		}

		public AEScriptRunner GetAEScript (string key, AEScriptRunner defValue = null) {
			return FindData(key, (x) => x.@aescript, defValue);
		}

		private T FindData<T> (string key, Func<AEScriptData, T> accessor, T defValue) {
			if (overrideData_ != null) {
				object overrideValue;
				if (overrideData_.TryGetValue(key, out overrideValue))
					return (T)overrideValue;
			}

			var store = runner_.DataStore;
			for (var i = 0; i < store.Length; ++i) {
				var data = store[i];
				if (data.key == key)
					return accessor(data);
			}

			return defValue;
		}
	}
}