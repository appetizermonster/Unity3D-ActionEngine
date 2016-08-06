using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ActionEngine {

	[InitializeOnLoad]
	public static class RecompileDisabler {

		static RecompileDisabler () {
			if (EditorApplication.isPlayingOrWillChangePlaymode) {
				EditorApplication.update += OnEditorUpdateInPlaying;
			}
		}

		private static void OnEditorUpdateInPlaying () {
			EditorApplication.UnlockReloadAssemblies();

			if (EditorApplication.isPlaying && EditorApplication.isCompiling) {
				EditorApplication.LockReloadAssemblies();
			}

			if (!EditorApplication.isPlaying)
				EditorApplication.update -= OnEditorUpdateInPlaying;
		}
	}
}
