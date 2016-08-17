using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ActionEngine {

	[InitializeOnLoad]
	public static class AEScriptEditorCore {

		static AEScriptEditorCore () {
			AEScriptEditorBridge.createActionFunc_ = CreateActionFromScript;
		}

		private static ActionBase CreateActionFromScript (AEScriptRunner script, Dictionary<string, object> overrideData) {
			return CreateActionFromScript(script.ScriptSource, script.GetContext(overrideData));
		}

		private static ActionBase CreateActionFromScript (TextAsset script, IAEScriptContext context) {
			try {
				var assembly = LoadAssembly(script);
				var className = script.name;
				var createMethod = assembly.GetType(className).GetMethod("Create");
				return createMethod.Invoke(null, new object[] { context }) as ActionBase;
			} catch (Exception) {
				// Consume it
			}
			return null;
		}

		private static readonly Dictionary<string, string> cachedScripts_ = new Dictionary<string, string>();
		private static readonly Dictionary<string, Assembly> cachedAssemblies_ = new Dictionary<string, Assembly>();

		private static Assembly LoadAssembly (TextAsset script) {
			var scriptName = script.name;
			var scriptSource = FileUtil.ReadRawFile(script); // Don't use TextAsset.text because it could have outdated text

			if (cachedScripts_.ContainsKey(scriptName)) {
				var cachedSource = cachedScripts_[scriptName];
				if (scriptSource == cachedSource)
					return cachedAssemblies_[scriptName];
			}

			Assembly assembly = null;

			RecompileDisabler.RecompileDisabler.ExecuteActionWithCompiler(() => {
				assembly = CompileAssembly(scriptName, scriptSource, script);
			});

			cachedScripts_[scriptName] = scriptSource;
			cachedAssemblies_[scriptName] = assembly;
			return assembly;
		}

		private static Assembly CompileAssembly (string scriptName, string scriptSource, UnityEngine.Object scriptRef) {
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

			var results = codeProvider.CompileAssemblyFromSource(compilerParams, scriptSource);
			var errors = results.Errors;
			if (errors != null && errors.Count > 0) {
				for (var i = 0; i < errors.Count; ++i) {
					var error = errors[i];
					if (error.IsWarning)
						continue;

					var msg = string.Format("{0} from {1}, Line {2}", error.ErrorText, scriptName, error.Line);
					Debug.LogError(msg, scriptRef);
				}
			}

			return results.CompiledAssembly;
		}
	}
}
