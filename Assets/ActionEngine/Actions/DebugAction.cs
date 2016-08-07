using System.Collections;
using UnityEngine;

namespace ActionEngine {

	public sealed class DebugAction : ActionBase {
		private object message_ = null;
		private Object context_ = null;

		#region Parameters

		public DebugAction SetMessage (object message, Object context = null) {
			message_ = message;
			context_ = context;
			return this;
		}

		#endregion Parameters

		protected override void OnBegin () {
			Debug.Log(message_, context_);
		}

		protected override void OnKill () {
			message_ = null;
			context_ = null;
		}
	}
}
