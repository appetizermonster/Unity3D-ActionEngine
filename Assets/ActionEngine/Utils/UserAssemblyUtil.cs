using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ActionEngine {

	internal static class UserAssemblyUtil {
		private static readonly List<Assembly> assemblies_ = new List<Assembly>(10);

		static UserAssemblyUtil () {
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (var i = 0; i < assemblies.Length; ++i) {
				var assembly = assemblies[i];
				var name = assembly.FullName;
				if (name.StartsWith("Assembly") == false)
					continue;

				assemblies_.Add(assembly);
			}
		}

		public static Type FindType (string typeName) {
			for (var i = 0; i < assemblies_.Count; ++i) {
				var assembly = assemblies_[i];
				var type = assembly.GetType(typeName);
				if (type != null)
					return type;
			}
			return null;
		}
	}
}
