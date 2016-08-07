using System;
using System.Collections;
using System.Collections.Generic;
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
		private readonly AEScriptRunner runner_ = null;
		private readonly Dictionary<string, object> overrideData_ = null;

		public AEScriptContext (AEScriptRunner runner, Dictionary<string, object> overrideData = null) {
			runner_ = runner;
			overrideData_ = overrideData;
		}

		public float GetFloat (string key) {
			return FindData(key, (x) => x.@float);
		}

		public int GetInt (string key) {
			return FindData(key, (x) => x.@int);
		}

		public bool GetBool (string key) {
			return FindData(key, (x) => x.@bool);
		}

		public string GetString (string key) {
			return FindData(key, (x) => x.@string);
		}

		public Vector2 GetVector2 (string key) {
			return FindData(key, (x) => x.@vector2);
		}

		public Vector3 GetVector3 (string key) {
			return FindData(key, (x) => x.vector3);
		}

		public Transform GetTransform (string key) {
			return FindData(key, (x) => x.@transform);
		}

		public GameObject GetGameObject (string key) {
			return FindData(key, (x) => x.@gameobject);
		}

		public AEScriptRunner GetAEScript (string key) {
			return FindData(key, (x) => x.@aescript);
		}

		private static AEScriptData defaultData_ = new AEScriptData();

		private T FindData<T>(string key, Func<AEScriptData, T> accessor) {
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
			return accessor(defaultData_);
		}
	}
}
