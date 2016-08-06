using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {
#if UNITY_EDITOR

	public static class AEScriptEditorBridge {
		public static Func<AEScriptRunner, ActionBase> createActionFunc_ = null;

		public static ActionBase CreateActionFromScript (AEScriptRunner script) {
			return createActionFunc_(script);
		}
	}

#endif
}
