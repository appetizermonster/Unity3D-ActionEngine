using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class ScriptAction : ActionBase<ScriptAction> {
		private Action script_ = null;
		private bool tryCatch_ = true;

		#region Parameters

		public ScriptAction SetScript (Action script) {
			script_ = script;
			return this;
		}

		public ScriptAction SetTryCatch (bool tryCatch) {
			tryCatch_ = tryCatch;
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			if (script_ == null)
				return;

			if (!tryCatch_) {
				script_();
				return;
			}

			try {
				script_();
			} catch (Exception e) {
				Debug.LogWarning(e);
			}
		}

		protected override void OnKill () {
			script_ = null;
			tryCatch_ = true;
		}
	}
}