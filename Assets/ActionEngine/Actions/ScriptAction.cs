using System;
using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class ScriptAction : ActionBase {
		private Action script_ = null;

		public void Script (Action script) {
			script_ = script;
		}

		protected override void OnKill () {
			script_ = null;
		}

		protected override void OnBegin () {
			if (script_ != null)
				script_();
		}
	}
}
