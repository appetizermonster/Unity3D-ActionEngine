using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionEngine {
#if UNITY_EDITOR

	public static class AEScriptEditorBridge {

		public delegate ActionBase CreateActionFunc (AEScriptRunner script, Dictionary<string, object> overrideData);

		public static CreateActionFunc createActionFunc_ = null;

		public static ActionBase CreateActionFromScript (AEScriptRunner script, Dictionary<string, object> overrideData) {
			return createActionFunc_(script, overrideData);
		}
	}

#endif
}
