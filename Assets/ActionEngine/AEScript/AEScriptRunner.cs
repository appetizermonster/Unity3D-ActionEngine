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

		public TextAsset ScriptSource { get { return scriptSource; } }
		public AEScriptData[] DataStore { get { return dataStore; } }

		private void Awake () {
			EnsureAEScriptMethod();

			if (playOnAwake)
				Internal_Play(true); // No need to use CSharpCodeProvider process in the first
		}

		private void OnDestroy () {
			Kill();
		}

		private ActionInstance curActionInstance_ = null;

		/// <summary>
		/// Create and play new action
		/// </summary>
		/// <returns>new action instance</returns>
		public ActionInstance Play () {
			return Internal_Play();
		}

		private ActionInstance Internal_Play (bool useReflectionOnly = false) {
			curActionInstance_ = null;

			var action = CreateAction(null, useReflectionOnly);
			if (action != null) {
				curActionInstance_ = action.Play(unscaled);
			} else {
				Debug.LogErrorFormat("Can't create an action from '{0}'" + __assignedScriptName);
			}
			return curActionInstance_;
		}

		/// <summary>
		/// Complete the current playing action instance
		/// </summary>
		public void Complete () {
			if (curActionInstance_ == null)
				return;
			curActionInstance_.Complete();
			curActionInstance_ = null;
		}

		/// <summary>
		/// Kill the current playing action instance
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
			return CreateAction(overrideData, false);
		}

		private ActionBase CreateAction (Dictionary<string, object> overrideData = null, bool useReflectionOnly = false) {
			ActionBase action = null;
#if UNITY_EDITOR
			if (!useReflectionOnly) {
				// Create Action using CSharpCodeProvider
				action = AEScriptEditorBridge.CreateActionFromScript(this, overrideData);
			}
#endif
			// Create Action using Reflection, Calling real script code
			if (action == null)
				action = CallAEScript(GetContext(overrideData));

			if (action == null)
				throw new InvalidOperationException("Can't create a action");

			return action;
		}

		private MethodInfo aeScriptMethod_ = null;

		private ActionBase CallAEScript (IAEScriptContext context) {
			EnsureAEScriptMethod();
			if (aeScriptMethod_ != null)
				return aeScriptMethod_.Invoke(null, new object[] { context }) as ActionBase;
			return null;
		}

		private void EnsureAEScriptMethod () {
			if (aeScriptMethod_ != null || scriptSource == null)
				return;
			var scriptName = __assignedScriptName;
			var scriptType = Assembly.GetExecutingAssembly().GetType(scriptName);

			if (scriptType == null) {
				Debug.LogError("AEScript Class name must be same with Filename");
				aeScriptMethod_ = null;
				return;
			}

			aeScriptMethod_ = scriptType.GetMethod("Create");
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
