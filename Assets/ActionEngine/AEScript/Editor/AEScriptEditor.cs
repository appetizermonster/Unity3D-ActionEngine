using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ActionEngine {

	[CustomEditor(typeof(AEScriptRunner))]
	[CanEditMultipleObjects]
	public class AEScriptRunnerEditor : Editor {

		[MenuItem("Assets/Create/C# AEScript", false, 94)]
		private static void CreateAEScriptSource () {
			var scriptTemplatePath = GetAEScriptTemplatePath();
			var createScriptAsset = typeof(ProjectWindowUtil).GetMethod("CreateScriptAsset", BindingFlags.NonPublic | BindingFlags.Static);
			createScriptAsset.Invoke(null, new object[] { scriptTemplatePath, "New AEX.cs" });
		}

		private static string scriptTemplatePath_ = null;

		private static string GetAEScriptTemplatePath () {
			if (scriptTemplatePath_ != null && File.Exists(scriptTemplatePath_))
				return scriptTemplatePath_;

			var templates = AssetDatabase.FindAssets("AEX.Template");
			if (templates == null || templates.Length == 0)
				throw new InvalidOperationException("Can't find a script template");

			var template = AssetDatabase.GUIDToAssetPath(templates[0]);
			scriptTemplatePath_ = Path.GetFullPath(template);

			return scriptTemplatePath_;
		}

		public override void OnInspectorGUI () {
			base.OnInspectorGUI();

			GUILayout.Space(10);
			GUI.enabled = EditorApplication.isPlaying;

			GUILayout.BeginHorizontal();

			if (GUILayout.Button(new GUIContent("Reload"))) {
				targets.ToList().ForEach((x) => Reload(x as AEScriptRunner));
			}
			if (GUILayout.Button(new GUIContent("Reload and Play"))) {
				targets.ToList().ForEach((x) => ReloadAndPlay(x as AEScriptRunner));
			}
			if (GUILayout.Button(new GUIContent("Kill"))) {
				targets.ToList().ForEach((x) => Kill(x as AEScriptRunner));
			}

			GUILayout.EndHorizontal();
			GUI.enabled = true;
		}

		private void Reload (AEScriptRunner obj) {
			obj.UseLiveScript();
		}

		private void ReloadAndPlay (AEScriptRunner obj) {
			obj.UseLiveScript();
			obj.Kill();
			obj.Load();

			// Add delay to compensate script loading time
			AE.Sequence(
				AE.Delay(0.5f),
				AE.Script(() => obj.Play())
			).Play(obj.Unscaled ? UpdateType.Unscaled : UpdateType.Normal);
		}

		private void Kill (AEScriptRunner obj) {
			obj.Kill();
		}
	}
}
