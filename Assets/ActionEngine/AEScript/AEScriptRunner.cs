using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ActionEngine {

	public sealed class AEScriptRunner : MonoBehaviour {

		[SerializeField]
		private bool playOnAwake;

		[SerializeField]
		private bool unscaled;

		[SerializeField]
		private TextAsset scriptSource;

		[HideInInspector]
		public string __assignedScriptName;

		[SerializeField]
		private AEScriptData[] dataStore;

		public bool Unscaled { get { return unscaled; } }
		public TextAsset ScriptSource { get { return scriptSource; } }
		public AEScriptData[] DataStore { get { return dataStore; } }

		private void Awake () {
			EnsureAEScriptMethod();

			if (playOnAwake)
				Play();
		}

		private void OnDestroy () {
			Kill();
		}

		private ActionInstance loadedActionInstance_ = null;
		private ActionInstance curActionInstance_ = null;

		public bool IsPlaying () {
			if (curActionInstance_ == null)
				return false;
			return (curActionInstance_.State == ActionInstance.InstanceState.PLAYING);
		}

		public AEScriptData FindData (string key) {
			for (var i = 0; i < dataStore.Length; ++i) {
				var data = dataStore[i];
				if (data.key == key)
					return data;
			}
			return null;
		}

		/// <summary>
		/// Play loaded action instance with pre-settinged unscaled option
		/// </summary>
		/// <returns>Current action instance</returns>
		public ActionInstance Play () {
			return Play(unscaled ? UpdateType.UNSCALED : UpdateType.NORMAL);
		}

		/// <summary>
		/// Play loaded action instance, or load and play it if there is no loaded action instance
		/// </summary>
		/// <returns>Current action instance</returns>
		public ActionInstance Play (UpdateType updateType) {
			if (loadedActionInstance_ == null)
				Load();

			curActionInstance_ = loadedActionInstance_;
			loadedActionInstance_ = null;

			return curActionInstance_.Play(updateType);
		}

		/// <summary>
		/// Preload action instance for performance purpose
		/// </summary>
		public void Load () {
			loadedActionInstance_ = null;

			var action = CreateAction(null);
			if (action != null) {
				loadedActionInstance_ = action.Enqueue();
			} else {
				Debug.LogErrorFormat("Can't create an action from '{0}'" + __assignedScriptName);
			}
		}

		/// <summary>
		/// Complete the current action instance
		/// </summary>
		public void Complete () {
			if (curActionInstance_ == null)
				return;
			curActionInstance_.Complete();
			curActionInstance_ = null;
		}

		/// <summary>
		/// Kill the current action instance
		/// </summary>
		public void Kill () {
			if (curActionInstance_ == null)
				return;
			curActionInstance_.Kill();
			curActionInstance_ = null;
		}

		/// <summary>
		/// Create an Action for self control
		/// </summary>
		/// <returns>Action</returns>
		public ActionBase Create (Dictionary<string, object> overrideData = null) {
			return CreateAction(overrideData);
		}

		private ActionBase CreateAction (Dictionary<string, object> overrideData = null) {
			if (scriptSource == null)
				return null;

			ActionBase action = null;
#if UNITY_EDITOR
			if (liveScripts_.Contains(scriptSource)) {
				// Create Action using CSharpCodeProvider
				action = AEScriptEditorBridge.CreateActionFromScript(this, overrideData);
			}
#endif
			// Create Action using Reflection, Calling real script code
			if (action == null)
				action = CallAEScript(GetContext(overrideData));

			return action;
		}

#if UNITY_EDITOR
		private static readonly HashSet<TextAsset> liveScripts_ = new HashSet<TextAsset>();

		public void UseLiveScript () {
			if (scriptSource != null && liveScripts_.Contains(scriptSource) == false)
				liveScripts_.Add(scriptSource);
		}

#endif

		private delegate ActionBase AEScriptFunc (IAEScriptContext ctx);

		private AEScriptFunc aeScriptFunc_ = null;

		private ActionBase CallAEScript (IAEScriptContext context) {
			EnsureAEScriptMethod();
			if (aeScriptFunc_ != null)
				return aeScriptFunc_(context);
			return null;
		}

		private void EnsureAEScriptMethod () {
			if (aeScriptFunc_ != null || scriptSource == null)
				return;
			var scriptName = __assignedScriptName;
			var scriptType = UserAssemblyUtil.FindType(scriptName);

			if (scriptType == null) {
				Debug.LogError("AEScript Class name must be same with Filename");
				aeScriptFunc_ = null;
				return;
			}

			aeScriptFunc_ = (AEScriptFunc)Delegate.CreateDelegate(typeof(AEScriptFunc), scriptType.GetMethod("Create"));
		}

		private IAEScriptContext defaultContext_ = null;

		public IAEScriptContext GetContext (Dictionary<string, object> overrideData = null) {
			if (overrideData != null)
				return new AEScriptContext(this, overrideData);

			if (defaultContext_ == null)
				defaultContext_ = new AEScriptContext(this);
			return defaultContext_;
		}

#if UNITY_EDITOR

		private void OnValidate () {
			var newScriptName = (scriptSource == null) ? null : scriptSource.name;
			if (newScriptName != __assignedScriptName) {
				__assignedScriptName = newScriptName;
				UnityEditor.EditorUtility.SetDirty(this);
			}
		}

#endif
	}
}