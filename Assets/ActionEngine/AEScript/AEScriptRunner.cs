using System;
using System.Collections;
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

			var action = CreateAction(useReflectionOnly);
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
		public ActionBase Create () {
			return CreateAction();
		}

		private ActionBase CreateAction (bool useReflectionOnly = false) {
			ActionBase action = null;
#if UNITY_EDITOR
			if (!useReflectionOnly) {
				// Create Action using CSharpCodeProvider
				action = AEScriptEditorBridge.CreateActionFromScript(this);
			}
#endif
			// Create Action using Reflection, Calling real script code
			if (action == null)
				action = CallAEScript(GetContext());

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

		private IAEScriptContext context_ = null;

		public IAEScriptContext GetContext () {
			if (context_ == null)
				context_ = new AEScriptContext(this);
			return context_;
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
