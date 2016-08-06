using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ActionEngine {

	[InitializeOnLoad]
	public static class AEScriptEditorCore {

		static AEScriptEditorCore () {
			AEScriptEditorBridge.createActionFunc_ = CreateActionFromScript;
		}

		private static ActionBase CreateActionFromScript (AEScriptRunner script) {
			return CreateActionFromScript(script.ScriptSource, script.GetContext());
		}

		private static ActionBase CreateActionFromScript (TextAsset script, IAEScriptContext context) {
			var codeProvider = new CSharpCodeProvider();
			var compilerParams = new CompilerParameters();
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = true;

			foreach (var assm in AppDomain.CurrentDomain.GetAssemblies()) {
				try {
					string location = assm.Location;
					if (String.IsNullOrEmpty(location))
						continue;
					compilerParams.ReferencedAssemblies.Add(location);
				} catch (Exception) {
					// Consume exception
				}
			}

			var results = codeProvider.CompileAssemblyFromSource(compilerParams, script.text);
			var errors = results.Errors;
			if (errors != null && errors.Count > 0) {
				for (var i = 0; i < errors.Count; ++i) {
					var error = errors[i];
					var msg = string.Format("{0} from {1}, Line {2}", error.ErrorText, script.name, error.Line);
					if (error.IsWarning) {
						Debug.LogWarning(msg, script);
					} else {
						Debug.LogError(msg, script);
					}
				}
				return null;
			}

			var assembly = results.CompiledAssembly;
			var className = script.name;
			var createMethod = assembly.GetType(className).GetMethod("Create");

			var actionBase = createMethod.Invoke(null, new object[] { context }) as ActionBase;
			return actionBase;
		}
	}
}
