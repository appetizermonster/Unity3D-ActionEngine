using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace ActionEngine {

	public static class FileUtil {

		public static string ReadRawFile (TextAsset textAsset) {
			if (textAsset == null)
				return null;

#if UNITY_EDITOR && !UNITY_WEBPLAYER && !UNITY_WEBGL
			var assetPath = UnityEditor.AssetDatabase.GetAssetPath(textAsset);
			return File.ReadAllText(assetPath);
#endif
			throw new NotSupportedException("This function isn't supported on Actual build");
		}
	}
}
